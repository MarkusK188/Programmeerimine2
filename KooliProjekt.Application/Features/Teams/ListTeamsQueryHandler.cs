using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Teams
{
    public class ListTeamsQueryHandler : IRequestHandler<ListTeamsQuery, OperationResult<PagedResult<TeamDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListTeamsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<TeamDto>>> Handle(
            ListTeamsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Teams
                .Select(t => new TeamDto
                {
                    Id = t.Id, TeamLeadId = t.TeamLead == null ? (int?)null : t.TeamLead.Id, TeamLeadUserName = t.TeamLead == null ? null : t.TeamLead.UserName, UserId = t.User == null ? (int?)null : t.User.Id, UserName = t.User == null ? null : t.User.UserName
                });

            var page = await query.GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<TeamDto>>(page);
        }
    }
}
