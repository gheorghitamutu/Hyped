using BackEnd.Data;
using MediatR;
using System.Collections.Generic;

namespace BackEnd.DTO.Users
{
    public class GetMultiple:IRequest<List<User>>
    {
    }
}
