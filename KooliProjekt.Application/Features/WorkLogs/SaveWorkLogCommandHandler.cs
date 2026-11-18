using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class SaveWorkLogCommandHandler : IRequestHandler<SaveWorkLogCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveWorkLogCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveWorkLogCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.Id == request.AssignmentId, cancellationToken);
            if (assignment == null) return result.AddPropertyError("AssignmentId", "Cannot find assignment with ID " + request.AssignmentId);
            var implementer = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.ImplementerId, cancellationToken);
            if (implementer == null) return result.AddPropertyError("ImplementerId", "Cannot find user with ID " + request.ImplementerId);

            WorkLog workLog;
            if (request.Id == 0)
            {
                workLog = new WorkLog();
                _dbContext.WorkLogs.Add(workLog);
            }
            else
            {
                workLog = await _dbContext.WorkLogs.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
                if (workLog == null) return result.AddError("WorkLog not found");
            }

            workLog.Assignment = assignment;
            workLog.Date = request.Date;
            workLog.TimeSpent = request.TimeSpent;
            workLog.Implementer = implementer;
            workLog.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
