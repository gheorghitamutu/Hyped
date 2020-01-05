using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.SCDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEndAPI.Business.VMHandlers.SCHandlers
{
    public class CreateSCHandler:IRequestHandler<CreateSC,SC>
    {
        private readonly DataContext context;

        public CreateSCHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<SC> Handle(CreateSC request, CancellationToken cancellationToken)
        {//creeaza un SCSI Controller atasat unei masini virtuale care se poate gasi dupa vm.RealID
            var vm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doen't exist");
            }
            
            var viridianUtils = new ViridianUtils();
            var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == vm.RealID).ToList().First();
            var virtualSystemSettingData = SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

            var primordialResourcePool = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller");
            var allocationCapabilities = ViridianUtils.GetAllocationCapabilities(primordialResourcePool);
            var resourceAllocationSettingData = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilities);

           //var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First();
            
                var AffectedConfiguration = virtualSystemSettingData.Path;
                var ResourceSettings = new string[] { resourceAllocationSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                var ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings);

            var rasdCollection = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller");
            
            var sc = SC.Create(rasdCollection.LastOrDefault().ElementName,rasdCollection.LastOrDefault().InstanceID,request.VMId);
            context.SCs.Add(sc);

            await context.SaveChangesAsync(cancellationToken);

            return sc;
        }
    }
}
