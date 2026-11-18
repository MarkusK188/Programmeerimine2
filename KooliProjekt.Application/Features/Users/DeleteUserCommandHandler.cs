using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteUserCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
            if (user == null) return result.AddError("User not found");

            var isReferenced = await _dbContext.Teams.AnyAsync(t => t.TeamLead.Id == request.Id || (t.User != null && t.User.Id == request.Id), cancellationToken)
                || await _dbContext.Assignments.AnyAsync(a => a.TeamLead.Id == request.Id, cancellationToken)
                || await _dbContext.WorkLogs.AnyAsync(w => w.Implementer.Id == request.Id, cancellationToken)
                || await _dbContext.Attachements.AnyAsync(a => a.UploadedBy.Id == request.Id, cancellationToken);

            if (isReferenced) return result.AddError("User is referenced by other records and cannot be deleted");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
