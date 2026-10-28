using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQuery : IRequest<OperationResult<UserDto>>
    {
        public int Id { get; set; }
    }
}
