using BackEndAPI.Data;
using MediatR;
using System.Collections.Generic;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class GetUsers:IRequest<List<User>>
    {
    }
}
