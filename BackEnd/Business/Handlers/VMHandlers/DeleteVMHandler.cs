﻿using BackEnd.Data;
using BackEnd.DTOs.VMDTOs;
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

namespace BackEnd.Business.VMHandlers
{
    public class DeleteVMHandler:IRequestHandler<DeleteVM>
    {
        private readonly DataContext context;

        public DeleteVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteVM request, CancellationToken cancellationToken)
        {
            var delete_vm = context.VMs.SingleOrDefault(u => u.VMId == request.VMId);
            if (delete_vm == null)
            {
                throw new Exception("This virtual machine doesn't exist!");
            }
            //delete this vm's resource folder first
            if (delete_vm.RealID != null)
            {
                var this_vm_path = System.IO.Path.Combine("Resources", delete_vm.RealID);

                if (System.IO.Directory.Exists(this_vm_path))
                {
                    System.IO.Directory.Delete(this_vm_path, true);
                }
            }

            var networks = await context.Networks.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var cddvds = await context.CDVDs.ToListAsync();

            var delete_vm_networks = networks.Where((n) => n.VMId == delete_vm.VMId).ToList();
            var delete_vm_scs = scs.Where((sc) => sc.VMId == delete_vm.VMId).ToList();
            //delete_vm.Update(delete_vm.RealID, delete_vm.Name, delete_vm.Configuration, delete_vm.LastSave, networks.Where((n) => n.VMId == delete_vm.VMId).ToList(), delete_vm.RAM, delete_vm.Cores, delete_vm.Threads, delete_vm.Processors, scs.Where((sc) => sc.VMId == delete_vm.VMId).ToList());
            foreach (var network in delete_vm_networks)//delete all the networks created for this user's virtual machine
            {
                context.Networks.Remove(network);
                await context.SaveChangesAsync(cancellationToken);

            }
            foreach (var scsicontroller in delete_vm_scs)//delete all the SCSI controllers of this virtual machine
            {
                var scsicontroller_vhds = vhds.Where((vhd) => vhd.SCId == scsicontroller.SCId).ToList();
                var scsicontroller_cddvds = cddvds.Where((cddvd) => cddvd.SCId == scsicontroller.SCId).ToList();
                //scsicontroller.Update(scsicontroller.Name, scsicontroller.InstanceId, vhds.Where((vhd) => vhd.SCId == scsicontroller.SCId).ToList(), cddvds.Where((cddvd) => cddvd.SCId == scsicontroller.SCId).ToList(), scsicontroller.VMId);
                foreach (var vhd in scsicontroller_vhds)//for each SCSI controller delete its vhds
                {
                    context.VHDs.Remove(vhd);
                    await context.SaveChangesAsync(cancellationToken);

                }
                foreach (var cddvd in scsicontroller_cddvds)//for each SCSI controller delete its cddvds
                {
                    context.CDVDs.Remove(cddvd);
                    await context.SaveChangesAsync(cancellationToken);
                }

                //after all the cddvds and vhds related to this SCSI controller have been erased, it's safe to delete the SCSI controller
                context.SCs.Remove(scsicontroller);
                await context.SaveChangesAsync(cancellationToken);
            }

            /*
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

            VirtualSystemManagementService.GetInstances().First().DestroySystem(virtualSystemSettingData.Path, out ManagementPath Job);
            */


            //delete the virtual machine
            
            
            using (var viridianUtils = new ViridianUtils())
            {
                var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == delete_vm.RealID).ToList().First();
                var virtualSystemSettingData= SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                viridianUtils.VSMS.DestroySystem(virtualSystemSettingData.Path, out ManagementPath Job);
                   
            }
            //TODO: delete vm from deleteuserhandler
            //remove the virtual machine from the DB
            context.VMs.Remove(delete_vm);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}