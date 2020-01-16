using BackEnd.Data;
using BackEnd.DTOs.UserDTOs;
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

namespace BackEnd.Business.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly DataContext context;

        public DeleteUserHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = context.Users.SingleOrDefault(u => u.UserId == request.UserId);
            if (user == null)
            {
                throw new Exception("This user doesn't exist!");
            }
            //get this user's virtual machines
            var vms = await context?.VMs?.ToListAsync();

            var user_vms = vms.Where((vm) => vm.UserId == user.UserId).ToList();
            //user.Update(user.FirstName, user.LastName, user.Email, user.Country, user.Password, user.Rights, user.Workplace, user.PositionTitle, user.ContactNumber, vms.Where((vm) => vm.UserId == user.UserId).ToList());
            //user.VMS = vms.Where((vm) => vm.UserId == user.UserId).ToList();

            foreach (var delete_vm in user_vms)//for each virtual machine this user has
            {
                //var delete_vm = context.VMs.SingleOrDefault(v => v.VMId == vm.VMId); probably redundant?
                if (delete_vm.RealID != null)//delete its resource folder
                {
                    var this_vm_path = System.IO.Path.Combine("Resources", delete_vm.RealID);

                    if (System.IO.Directory.Exists(this_vm_path))
                    {
                        System.IO.Directory.Delete(this_vm_path, true);
                    }
                    //var to_delete_vm = new ComputerSystem(delete_vm.Name);
                    //to_delete_vm.DestroySystem();
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


                using (var viridianUtils = new ViridianUtils())
                {
                    var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == delete_vm.RealID).ToList().First();
                    var virtualSystemSettingData = SettingsDefineState.GetInstances()
                            .Cast<SettingsDefineState>()
                            .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                            .ToList()
                            .First();

                    viridianUtils.VSMS.DestroySystem(virtualSystemSettingData.Path, out ManagementPath Job);

                }
                context.VMs.Remove(delete_vm);//remove the virtual machine from DB
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
        //TODO:remove its vhds/cddvds/scs from db
        */


        //remove the user from DB
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
