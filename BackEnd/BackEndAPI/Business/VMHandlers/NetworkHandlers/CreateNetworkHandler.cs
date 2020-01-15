using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.NetworkDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.Networking;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using Viridian.Extern;
using System.Collections.Generic;
using System.Globalization;

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
            var ComputerSystemName = (await context.VMs.SingleAsync(u => u.VMId == request.VMId).ConfigureAwait(false)).RealID;

            Operation.GetComputerSystem(
                new Dictionary<string, object>()
                {
                    { "Name", ComputerSystemName }
                },
                out ComputerSystem ComputerSystem
                );

            Operation.GetVirtualSystemSettingData(
                new Dictionary<string, string>()
                {
                    { "ManagedElement", ComputerSystem.Path.Path }
                },
                nameof(SettingsDefineState),
                out VirtualSystemSettingData VirtualSystemSettingData);

            Operation.GetDefaultSyntheticEthernetPortSettingData(
                new Dictionary<string, object>()
                {
                    { "VirtualSystemIdentifiers", Guid.NewGuid().ToString("B", CultureInfo.InvariantCulture) },
                    { "ElementName", request?.Name },
                    { "StaticMacAddress", false }
                },
                out SyntheticEthernetPortSettingData SyntheticEthernetPortSettingData);

            Operation.GetVirtualSystemManagementService(out VirtualSystemManagementService VirtualSystemManagementService);

            var AffectedConfiguration = VirtualSystemSettingData.Path;
            var ResourceSettings = new string[] { SyntheticEthernetPortSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };

            var ReturnValue = VirtualSystemManagementService.AddResourceSettings(AffectedConfiguration, ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings);

            Operation.CreateVirtualEthernetSwitchSettingData(
                    new Dictionary<string, object>()
                    {
                        { "ElementName", request?.Name },
                        { "Notes", request?.Notes }
                    },
                    out ReturnValue,
                    out Job,
                    out ManagementPath ResultingSystem);

            SyntheticEthernetPortSettingData = new SyntheticEthernetPortSettingData(ResultingResourceSettings[0]);
            var VirtualEthernetSwitch = new VirtualEthernetSwitch(ResultingSystem);

            Operation.GetDefaultEthernetPortAllocationSettingData(
                new Dictionary<string, object>()
                {
                    { "Parent", SyntheticEthernetPortSettingData.Path.Path },
                    { "HostResource", VirtualEthernetSwitch.Path.Path }
                },
                out EthernetPortAllocationSettingData EthernetPortAllocationSettingData);

            AffectedConfiguration = VirtualSystemSettingData.Path;
            ResourceSettings = new string[] { EthernetPortAllocationSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
            ReturnValue = VirtualSystemManagementService.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

            var network = Network.Create(request.Name,request.Notes, EthernetPortAllocationSettingData.InstanceID,request.VMId);
            context.Networks.Add(network);

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return network;
        }
    }
}
