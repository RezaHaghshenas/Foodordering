using Foodordering.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    { 
    }
}
