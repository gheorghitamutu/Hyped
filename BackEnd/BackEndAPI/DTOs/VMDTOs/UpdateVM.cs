using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class UpdateVM:IRequest<VM>
    {
        public Guid VMId { get; set; }
        public string RealID { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string LastSave { get; set; }
    }
}
