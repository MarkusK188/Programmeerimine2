using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Assignments
{
    public class ListAssignmentsQueryHandler : IRequestHandler<ListAssignmentsQuery, OperationResult<PagedResult<AssignmentDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAssignmentsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<AssignmentDto>>> Handle(
            ListAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Assignments
                .Select(a => new AssignmentDto
                {
                    Id = a.Id, AssignmentName = a.AssignmentName, StartDate = a.StartDate, DueDate = a.DueDate, EstimatedHours = a.EstimatedHours, FixedPrice = a.FixedPrice, TeamLeadId = a.TeamLead == null ? (int?)null : a.TeamLead.Id, TeamLeadUserName = a.TeamLead == null ? null : a.TeamLead.UserName, AssignmentDesc = a.AssignmentDesc
                });

            var page = await query.GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<AssignmentDto>>(page);
        }
    }
}
