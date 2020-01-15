using BackEndAPI.Data;
using MediatR;
using System.Collections.Generic;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class GetVMs:IRequest<List<VM>>
    {
    }
}
