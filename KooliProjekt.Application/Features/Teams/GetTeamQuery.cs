using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.Features.Teams
{
    public class GetTeamQuery : IRequest<OperationResult<TeamDto>>
    {
        public int Id { get; set; }
    }
}
