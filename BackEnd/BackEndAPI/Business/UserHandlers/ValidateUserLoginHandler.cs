using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.UserHandlers
{
    public class ValidateUserLoginHandler:IRequestHandler<ValidateUserLogin,SecurityToken>
    {
        
            private readonly DataContext context;

            public ValidateUserLoginHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<SecurityToken> Handle(ValidateUserLogin request, CancellationToken cancellationToken)
            {
                var user = await context.Users.SingleOrDefaultAsync(u => (u.Email == request.Email && u.Password == request.Password));
                if (user == null)
                {
                    throw new Exception("No such user with these email and password!");
                }
            /*
            var vms = await context?.VMs?.ToListAsync();
            var networks = await context.Networks.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();
            var user_vms = vms.Where((vm) => vm.UserId == user.UserId).ToList();
            //get this user's virtual machines
            //user.VMS=vms.Where((vm) => vm.UserId == user.UserId).ToList(); when icollection is private
            foreach (var vm in user_vms)
            {
                var vm_networks = networks.Where((n) => n.VMId == vm.VMId).ToList();
                var vm_scs = scs.Where((s) => s.VMId == vm.VMId).ToList();
                foreach (var sc in vm_scs)
                {
                    var sc_vhds = vhds.Where((v) => v.SCId == sc.SCId).ToList();
                    var sc_cdvds = cdvds.Where((v) => v.SCId == sc.SCId).ToList();
                    sc.Update(sc.Name, sc.InstanceId, sc_vhds, sc_cdvds, sc.VMId);
                    await context.SaveChangesAsync(cancellationToken);
                }

                vm.Update(vm.RealID, vm.Name, vm.Configuration, vm.LastSave, vm_networks, vm.RAM, vm.Cores, vm.Threads, vm.Processors, vm_scs);
                await context.SaveChangesAsync(cancellationToken);
            }

            user.Update(user.FirstName, user.LastName, user.Email, user.Country, user.Password, user.Rights, user.Workplace, user.PositionTitle, user.ContactNumber, user_vms);
            await context.SaveChangesAsync(cancellationToken);
            */
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("HYPED.NET2020ANJ2512NA152");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("User Id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
            }
        
    }
}
