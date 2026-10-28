using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.DTOs;
using MediatR;

namespace KooliProjekt.Application.Features.Teams
{
    public class ListTeamsQuery : IRequest<OperationResult<PagedResult<TeamDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
