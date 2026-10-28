using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class GetWorkLogQueryHandler : IRequestHandler<GetWorkLogQuery, OperationResult<WorkLogDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetWorkLogQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<WorkLogDto>> Handle(
            GetWorkLogQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _dbContext.WorkLogs
                .Where(w => w.Id == request.Id)
                .Select(w => new WorkLogDto
                {
                    Id = w.Id, AssignmentId = w.Assignment == null ? (int?)null : w.Assignment.Id, AssignmentName = w.Assignment == null ? null : w.Assignment.AssignmentName, Date = w.Date, TimeSpent = w.TimeSpent, ImplementerId = w.Implementer == null ? (int?)null : w.Implementer.Id, ImplementerUserName = w.Implementer == null ? null : w.Implementer.UserName, Description = w.Description
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new OperationResult<WorkLogDto>(item);
        }
    }
}
