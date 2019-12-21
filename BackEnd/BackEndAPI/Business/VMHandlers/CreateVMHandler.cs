using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Job;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace BackEndAPI.Business.VMHandlers
{
    public class CreateVMHandler:IRequestHandler<CreateVM, VM>
    {
        private readonly DataContext context;

        public CreateVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(CreateVM request, CancellationToken cancellationToken)
        {
            using (var vsms = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                using (var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance())
                {
                    virtualSystemSettingData.LateBoundObject["ElementName"] = request.Name;
                    virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                    virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                    ManagementPath ReferenceConfiguration = null;
                    string[] ResourceSettings = null;
                    string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                    var ReturnValue = vsms.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                    using (var vsss = VirtualSystemSnapshotService.GetInstances().Cast<VirtualSystemSnapshotService>().ToList().First())
                    {
                        using (var SnapshotSettingsInstance = VirtualSystemSettingData.CreateInstance())
                        {
                            SnapshotSettingsInstance.LateBoundObject["SnapshotDataRoot"] = @"SnapshotDataRoot";
                            SnapshotSettingsInstance.LateBoundObject["VirtualSystemType"] = 5;

                            ManagementPath AffectedSystem = ResultingSystem;
                            ManagementPath ResultingSnapshot = null;
                            string SnapshotSettings = SnapshotSettingsInstance.LateBoundObject.GetText(TextFormat.CimDtd20);
                            ushort SnapshotType = 2;
                            ReturnValue = vsss.CreateSnapshot(AffectedSystem, ref ResultingSnapshot, SnapshotSettings, SnapshotType, out Job);

                            using (ManagementObject JobObject = new ManagementObject(Job))
                            {
                                while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // TODO: maybe events cand be used here?
                                {
                                    Thread.Sleep(TimeSpan.FromSeconds(1));
                                    JobObject.Get();
                                }

                                using (ComputerSystem computerSystem = new ComputerSystem(AffectedSystem))
                                {
                                    var sovsCollection = SnapshotOfVirtualSystem.GetInstances()
                                        .Cast<SnapshotOfVirtualSystem>()
                                        .Where((sovs) => string.Compare(sovs.Antecedent.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                        .ToList();

                                    var mcsibCollection = MostCurrentSnapshotInBranch.GetInstances()
                                        .Cast<MostCurrentSnapshotInBranch>()
                                        .Where((sovs) => string.Compare(sovs.Antecedent.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                        .ToList();

                                    var vssdCollection =
                                        SettingsDefineState.GetInstances()
                                            .Cast<SettingsDefineState>()
                                            .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                            .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                                            .ToList();

                                    uint[] RequestedInformation = (uint[])Enum.GetValues(typeof(SummaryInformation.RequestedInformation));
                                    ManagementPath[] SettingData = new ManagementPath[] { vssdCollection.First().Path };

                                    ReturnValue = vsms.GetSummaryInformation(RequestedInformation, SettingData, out ManagementBaseObject[] SummaryInformation);

                                    using (SummaryInformation summaryInformation = new SummaryInformation(SummaryInformation.First()))
                                    {
                                        string this_vm_directory = System.IO.Path.Combine("Resources", computerSystem.Name);//directory with resources for this virtual machine
                                        System.IO.Directory.CreateDirectory(this_vm_directory);

                                        //create snapshot file
                                        var snapshot_file = JsonConvert.SerializeObject(mcsibCollection.First().LateBoundObject.Properties);//convert the snapshot properties to json
                                        string this_snapshot_filename = System.IO.Path.Combine(this_vm_directory, "snapshot.json");
                                        System.IO.File.WriteAllText(this_snapshot_filename, snapshot_file);

                                        //create configuration file
                                        var configuration_file = JsonConvert.SerializeObject(summaryInformation.LateBoundObject.Properties);//convert the virtual machine informations to json
                                        string this_configuration_filename = System.IO.Path.Combine(this_vm_directory, "configuration.json");
                                        System.IO.File.WriteAllText(this_configuration_filename, configuration_file);

                                        var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId);//get the user that requested this virtual machine
                                        if (user == null)
                                        {
                                            throw new Exception("Couldn't find the requested user!"); // TODO: shouldn't this be the first check?
                                        }
                                        //add the virtual machine to the DB
                                        var vm = VM.Create(computerSystem.Name, computerSystem.ElementName, this_configuration_filename, this_snapshot_filename, user.UserId);
                                        context.VMs.Add(vm);

                                        await context.SaveChangesAsync(cancellationToken);
                                        return vm;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
