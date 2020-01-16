using BackEnd.Data;
using MediatR;
using System.Collections.Generic;

namespace BackEnd.DTOs.VMDTOs
{
    public class GetVMs:IRequest<List<VM>>
    {
    }
}
