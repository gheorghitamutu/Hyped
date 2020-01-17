using BackEnd.Data;
using BackEnd.DTOs.StorageDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Business.Handlers.Storage
{
    public class FileUpload : IRequestHandler<UploadFile,bool>
    {
        private readonly DataContext context;

        public FileUpload(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(UploadFile request, CancellationToken cancellationToken)
        {
            var vm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doesn't exist");
            }

           
            if(request.FileToUpload.Length>0)
            {
                string this_vm_directory = Path.Combine("Resources", vm.RealID);
                var filePath=Path.Combine(this_vm_directory, request.FileToUpload.FileName);
                try
                {
                    using (var stream = File.Create(filePath))
                    {
                        await request.FileToUpload.CopyToAsync(stream, cancellationToken);
                    }
                }catch(IOException)
                {
                    throw new Exception("Could not create this file!");
                }
            }
            //TODO: check if file is in folder and return true; else false;
            return true;
        }
    }
}
