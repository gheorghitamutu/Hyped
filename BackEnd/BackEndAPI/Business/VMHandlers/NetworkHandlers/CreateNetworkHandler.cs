using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.NetworkDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.Networking;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEndAPI.Business.VMHandlers.NetworkHandlers
{
    public class CreateNetworkHandler : IRequestHandler<CreateNetwork, Network>
    {
        private readonly DataContext context;

        public CreateNetworkHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Network> Handle(CreateNetwork request, CancellationToken cancellationToken)
        {
            //creeaza un network adapter(?),folosind request.Name si request.Notes, pentru o masina virtuala care poate fi gasita dupa vm.RealID
            var vm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doesn't exist");
            }

            var viridianUtils = new ViridianUtils();
            viridianUtils.SUT_VirtualEthernetSwitchSettingDataMO(
                    request.Name,
                    request.Notes,
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

            var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == vm.RealID).ToList().First();
            var virtualSystemSettingData = SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

            var virtualEthernetSwitch = new VirtualEthernetSwitch(ResultingSystem);
            var primordialResourcePool = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Ethernet Port");
            var allocationCapabilities = ViridianUtils.GetAllocationCapabilities(primordialResourcePool);
            var syntheticEthernetPortSettingData = ViridianUtils.GetDefaultSyntheticEthernetPortSettingData(allocationCapabilities);

            syntheticEthernetPortSettingData.LateBoundObject["VirtualSystemIdentifiers"] = new string[] { Guid.NewGuid().ToString("B") };
            syntheticEthernetPortSettingData.LateBoundObject["ElementName"] = request.Name;
            syntheticEthernetPortSettingData.LateBoundObject["StaticMacAddress"] = false;

            var AffectedConfiguration = virtualSystemSettingData.Path;
            var ResourceSettings = new string[] { syntheticEthernetPortSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
            ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);


            var network = Network.Create(request.Name,request.Notes,request.VMId);
            context.Networks.Add(network);

            await context.SaveChangesAsync(cancellationToken);
            return network;
        }
    }
}
