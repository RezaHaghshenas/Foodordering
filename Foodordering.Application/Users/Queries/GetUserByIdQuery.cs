using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodordering.Application.Common.DTOs;
using MediatR;
using System;

namespace Foodordering.Application.Users.Queries
{ 
        public class GetUserByIdQuery : IRequest<UserDto>
        {
            public Guid UserId { get; set; }
        }
}
