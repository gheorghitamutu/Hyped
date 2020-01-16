using BackEnd.Data;
using MediatR;
using System.Collections.Generic;

namespace BackEnd.DTOs.UserDTOs
{
    public class GetUsers:IRequest<List<User>>
    {
    }
}
