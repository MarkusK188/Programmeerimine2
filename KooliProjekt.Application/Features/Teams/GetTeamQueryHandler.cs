using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Teams
{
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, OperationResult<TeamDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTeamQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<TeamDto>> Handle(
            GetTeamQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _dbContext.Teams
                .Where(t => t.Id == request.Id)
                .Select(t => new TeamDto
                {
                    Id = t.Id, TeamLeadId = t.TeamLead == null ? (int?)null : t.TeamLead.Id, TeamLeadUserName = t.TeamLead == null ? null : t.TeamLead.UserName, UserId = t.User == null ? (int?)null : t.User.Id, UserName = t.User == null ? null : t.User.UserName
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new OperationResult<TeamDto>(item);
        }
    }
}
