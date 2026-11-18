using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Teams
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteTeamCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (team == null) return result.AddError("Team not found");

            var projects = await _dbContext.Projects.Where(p => p.Team.Id == request.Id).ToListAsync(cancellationToken);
            foreach (var project in projects) project.Team = null;

            _dbContext.Teams.Remove(team);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
