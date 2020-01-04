using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Microsoft.Windows.Storage.MSFT;
using Viridian.Root.Virtualization.v2.Msvm.Storage;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEndAPI.Business.VMHandlers
{
    public class CreateVHDHandler:IRequestHandler<CreateVHD,VHD>
    {
        private readonly DataContext context;

        public CreateVHDHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VHD> Handle(CreateVHD request, CancellationToken cancellationToken)
        {
            //creeaza un VHD folosind request.Name, request.Size atasandu-l unui SCSI Controller deja existent care se poate gasi dupa sc.InstanceID
            //linia 73 eroare
            var sc = await context.SCs.SingleOrDefaultAsync(u => u.SCId == request.SCId);//SCSI Controller
            if (sc== null)
            {
                throw new Exception("Requested SCSI Controller doesn't exist");
            }
            var vm = await context.VMs.SingleOrDefaultAsync(v => v.VMId == sc.VMId);
            if(vm==null)
            {
                throw new Exception("Could not find the virtual machine of this SCSI Controller!");
            }
            

            var computerSystem =
                ComputerSystem.GetInstances()
                    .Cast<ComputerSystem>()
                    .Where((cs) => cs.Name == vm.RealID)
                    .ToList()
                    .First();

            var virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();
            var AffectedConfiguration = virtualSystemSettingData.Path;

            var vhdxName = $"{"Resources"}\\{vm.RealID}\\{request.Name}.vhdx";

            var viridianUtils = new ViridianUtils();
            var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").Where(s => s.InstanceID == sc.InstanceId).FirstOrDefault();

            var primordialResourcePoolDiskDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Disk Drive");
            var allocationCapabilitiesDiskDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDiskDrive);
            var resourceAllocationSettingDataDiskDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDiskDrive);
            resourceAllocationSettingDataDiskDrive.LateBoundObject["Parent"] = scsiController.Path;
            resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 0;

            var ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
            var ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings); //Object reference not set to an instance of an object!!!!!!!!!!!!!!

            var synthethicDiskDrive = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 17, "Microsoft:Hyper-V:Synthetic Disk Drive").First();
            var primordialResourcePoolVirtualHardDisk = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Virtual Hard Disk");
            var allocationCapabilitiesVirtualHardDisk = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolVirtualHardDisk);
            var resourceAllocationSettingVirtualHardDisk = ViridianUtils.GetDefaultStorageAllocationSettingData(allocationCapabilitiesVirtualHardDisk);

            using (var vhdsd = VirtualHardDiskSettingData.CreateInstance()) // operations on the host
            {
                vhdsd.LateBoundObject["Type"] = VirtualHardDiskSettingData.TypeValues.Dynamic;
                vhdsd.LateBoundObject["Format"] = VirtualHardDiskSettingData.FormatValues.VHDX;
                vhdsd.LateBoundObject["Path"] = vhdxName;
                vhdsd.LateBoundObject["ParentPath"] = null;
                vhdsd.LateBoundObject["MaxInternalSize"] = 1024 * 1024 * 1024;

                var VirtualDiskSettingData = vhdsd.LateBoundObject.GetText(TextFormat.WmiDtd20);
                viridianUtils.IMS.CreateVirtualHardDisk(VirtualDiskSettingData, out Job);

                ViridianUtils.WaitForStorageJobToEnd(Job);

                var AssignDriveLetter = false;
                var Path = vhdxName;
                var ReadOnly = false;
                ReturnValue = viridianUtils.IMS.AttachVirtualHardDisk(AssignDriveLetter, Path, ReadOnly, out Job);

                ViridianUtils.WaitForStorageJobToEnd(Job);

                using (var disk =
                    Disk.GetInstances()
                        .Where((d) => string.Compare(d.Location, vhdxName, true, CultureInfo.InvariantCulture) == 0)
                        .ToList()
                        .First())
                {
                    ushort PartitionStyle = (ushort)Disk.PartitionStyleValues.GPT;
                    disk.Initialize(PartitionStyle, out ManagementBaseObject ExtendedStatus);

                    var Alignment = 0U;
                    AssignDriveLetter = false;
                    var DriveLetter = '\0';
                    var GptType = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}"; // https://en.wikipedia.org/wiki/GUID_Partition_Table
                    var IsActive = false;
                    var IsHidden = false;
                    var MbrType = (ushort)Partition.MbrTypeValues.Huge;
                    var Offset = 0U;
                    var Size = 0UL;
                    var UseMaximumSize = true;

                    ReturnValue =
                        disk.CreatePartition(
                            Alignment,
                            AssignDriveLetter,
                            DriveLetter,
                            GptType,
                            IsActive,
                            IsHidden,
                            MbrType,
                            Offset,
                            Size,
                            UseMaximumSize,
                            out ManagementBaseObject CreatePartition,
                            out ExtendedStatus);

                    if (ExtendedStatus != null)
                    {
                        using (var storageExtendedStatus = new StorageExtendedStatus(ExtendedStatus))
                        {
                            Trace.WriteLine($"{nameof(storageExtendedStatus.CIMStatusCode)}[{storageExtendedStatus.CIMStatusCode}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.CIMStatusCodeDescription)}[{storageExtendedStatus.CIMStatusCodeDescription}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.ErrorSource)}[{storageExtendedStatus.ErrorSource}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.ErrorSourceFormat)}[{storageExtendedStatus.ErrorSourceFormat}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.Message)}[{storageExtendedStatus.Message}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.MessageArguments)}[{storageExtendedStatus.MessageArguments}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.PerceivedSeverity)}[{storageExtendedStatus.PerceivedSeverity}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.ProbableCause)}[{storageExtendedStatus.ProbableCause}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.ProbableCauseDescription)}[{storageExtendedStatus.ProbableCauseDescription}]");
                            Trace.WriteLine($"{nameof(storageExtendedStatus.RecommendedActions)}[{storageExtendedStatus.RecommendedActions}]");
                        }
                    }

                    using (var partition =
                                        DiskToPartition.GetInstances()
                                            .Cast<DiskToPartition>()
                                            .Where((dtp) =>
                                            string.Compare(new Disk(dtp.Disk).ObjectId, disk.ObjectId, true, CultureInfo.InvariantCulture) == 0 &&
                                            string.Compare(new Partition(dtp.Partition).GptType, "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}", true, CultureInfo.InvariantCulture) == 0)
                                            .Select((dtp) => new Partition(dtp.Partition))
                                            .ToList()
                                            .First())
                    using (var volume =
                        PartitionToVolume.GetInstances()
                            .Cast<PartitionToVolume>()
                            .Where((ptv) => string.Compare(new Partition(ptv.Partition).ObjectId, partition.ObjectId, true, CultureInfo.InvariantCulture) == 0)
                            .Select((ptv) => new Volume(ptv.Volume))
                            .ToList()
                            .First())
                    {
                        uint AllocationUnitSize = Convert.ToUInt32(request.Size);//4096
                        bool Compress = true;
                        bool DisableHeatGathering = false;
                        string FileSystem = "NTFS";
                        string FileSystemLabel = request.Name;
                        bool Force = true;
                        bool Full = true;
                        bool IsDAX = false;
                        bool RunAsJob = false;
                        bool SetIntegrityStreams = false;
                        bool ShortFileNameSupport = true;
                        bool UseLargeFRS = false;

                        volume.Format(
                            AllocationUnitSize,
                            Compress,
                            DisableHeatGathering,
                            FileSystem,
                            FileSystemLabel,
                            Force,
                            Full,
                            IsDAX,
                            RunAsJob,
                            SetIntegrityStreams,
                            ShortFileNameSupport,
                            UseLargeFRS,
                            out ManagementPath CreatedStorageJob,
                            out ExtendedStatus,
                            out ManagementBaseObject FormattedVolume);
                    }
                }

                ushort CriterionType = 2;
                var SelectionCriterion = vhdxName;
                viridianUtils.IMS.FindMountedStorageImageInstance(CriterionType, SelectionCriterion, out ManagementPath Image);

                using (var mountedStorageImage = new MountedStorageImage(Image))
                {
                    mountedStorageImage.DetachVirtualHardDisk();
                }

                // end operations on the host

                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Access"] = 3; // read/write
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Address"] = 0;
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Parent"] = synthethicDiskDrive.Path.Path;
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["HostResource"] = new[] { vhdxName };

                ResourceSettings = new string[] { resourceAllocationSettingVirtualHardDisk.LateBoundObject.GetText(TextFormat.WmiDtd20) };
            }
        
        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

            var virtualHardDiskCollection =
                            ViridianUtils.GetStorageAllocationSettingDataList(virtualSystemSettingData, 31, "Microsoft:Hyper-V:Virtual Hard Disk")
                                    .Where((sasd) => string.Compare(sasd.Caption, "Hard Disk Image", true, CultureInfo.InvariantCulture) == 0)
                                    .ToList();
            var latestVHD = virtualHardDiskCollection.LastOrDefault();
            var vhd = VHD.Create(sc.SCId,latestVHD.ElementName,latestVHD.InstanceID,vhdxName, Convert.ToInt32(latestVHD.VirtualQuantity));
            context.VHDs.Add(vhd);

            await context.SaveChangesAsync(cancellationToken);

            return vhd;
        }
    }
}
