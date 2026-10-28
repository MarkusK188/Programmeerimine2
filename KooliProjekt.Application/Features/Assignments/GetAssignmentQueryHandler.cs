using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Assignments
{
    public class GetAssignmentQueryHandler : IRequestHandler<GetAssignmentQuery, OperationResult<AssignmentDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAssignmentQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<AssignmentDto>> Handle(
            GetAssignmentQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _dbContext.Assignments
                .Where(a => a.Id == request.Id)
                .Select(a => new AssignmentDto
                {
                    Id = a.Id, AssignmentName = a.AssignmentName, StartDate = a.StartDate, DueDate = a.DueDate, EstimatedHours = a.EstimatedHours, FixedPrice = a.FixedPrice, TeamLeadId = a.TeamLead == null ? (int?)null : a.TeamLead.Id, TeamLeadUserName = a.TeamLead == null ? null : a.TeamLead.UserName, AssignmentDesc = a.AssignmentDesc
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new OperationResult<AssignmentDto>(item);
        }
    }
}
