using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class CreateVM:IRequest<VM>
    {
        public string RealID { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string LastSave { get; set; }
    }
}
