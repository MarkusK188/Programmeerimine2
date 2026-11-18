using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Assignments
{
    public class SaveAssignmentCommandHandler : IRequestHandler<SaveAssignmentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveAssignmentCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveAssignmentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var teamLead = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.TeamLeadId, cancellationToken);
            if (teamLead == null) return result.AddPropertyError("TeamLeadId", "Cannot find user with ID " + request.TeamLeadId);

            Assignment assignment;
            if (request.Id == 0)
            {
                assignment = new Assignment();
                _dbContext.Assignments.Add(assignment);
            }
            else
            {
                assignment = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
                if (assignment == null) return result.AddError("Assignment not found");
            }

            assignment.AssignmentName = request.AssignmentName;
            assignment.StartDate = request.StartDate;
            assignment.DueDate = request.DueDate;
            assignment.EstimatedHours = request.EstimatedHours;
            assignment.FixedPrice = request.FixedPrice;
            assignment.TeamLead = teamLead;
            assignment.AssignmentDesc = request.AssignmentDesc;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
