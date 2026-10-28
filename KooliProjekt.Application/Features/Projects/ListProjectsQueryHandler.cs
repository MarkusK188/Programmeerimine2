using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Projects
{
    public class ListProjectsQueryHandler : IRequestHandler<ListProjectsQuery, OperationResult<PagedResult<ProjectDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListProjectsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<ProjectDto>>> Handle(
            ListProjectsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Projects
                .Select(p => new ProjectDto
                {
                    Id = p.Id, ProjectName = p.ProjectName, StartDate = p.StartDate, DueDate = p.DueDate, Budget = p.Budget, HourlyRate = p.HourlyRate, TeamId = p.Team == null ? (int?)null : p.Team.Id
                });

            var page = await query.GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<ProjectDto>>(page);
        }
    }
}
