using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Assignments
{
    public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteAssignmentCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var exists = await _dbContext.Assignments.AnyAsync(a => a.Id == request.Id, cancellationToken);
            if (!exists) return result.AddError("Assignment not found");

            await _dbContext.WorkLogs.Where(w => w.Assignment.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            await _dbContext.Attachements.Where(a => a.Assignment.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            await _dbContext.Assignments.Where(a => a.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            return result;
        }
    }
}
