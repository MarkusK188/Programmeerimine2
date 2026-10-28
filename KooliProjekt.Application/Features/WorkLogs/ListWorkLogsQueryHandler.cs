using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class ListWorkLogsQueryHandler : IRequestHandler<ListWorkLogsQuery, OperationResult<PagedResult<WorkLogDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListWorkLogsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<WorkLogDto>>> Handle(
            ListWorkLogsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.WorkLogs
                .Select(w => new WorkLogDto
                {
                    Id = w.Id, AssignmentId = w.Assignment == null ? (int?)null : w.Assignment.Id, AssignmentName = w.Assignment == null ? null : w.Assignment.AssignmentName, Date = w.Date, TimeSpent = w.TimeSpent, ImplementerId = w.Implementer == null ? (int?)null : w.Implementer.Id, ImplementerUserName = w.Implementer == null ? null : w.Implementer.UserName, Description = w.Description
                });

            var page = await query.GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<WorkLogDto>>(page);
        }
    }
}
