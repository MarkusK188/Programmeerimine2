using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Teams
{
    public class SaveTeamCommandHandler : IRequestHandler<SaveTeamCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveTeamCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveTeamCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var teamLead = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.TeamLeadId, cancellationToken);
            if (teamLead == null) return result.AddPropertyError("TeamLeadId", "Cannot find user with ID " + request.TeamLeadId);

            User user = null;
            if (request.UserId.HasValue)
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId.Value, cancellationToken);
                if (user == null) return result.AddPropertyError("UserId", "Cannot find user with ID " + request.UserId.Value);
            }

            Team team;
            if (request.Id == 0)
            {
                team = new Team();
                _dbContext.Teams.Add(team);
            }
            else
            {
                team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
                if (team == null) return result.AddError("Team not found");
            }

            team.TeamLead = teamLead;
            team.User = user;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
