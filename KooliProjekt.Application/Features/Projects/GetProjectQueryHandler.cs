using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projects
{
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, OperationResult<ProjectDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ProjectDto>> Handle(
            GetProjectQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _dbContext.Projects
                .Where(p => p.Id == request.Id)
                .Select(p => new ProjectDto
                {
                    Id = p.Id, ProjectName = p.ProjectName, StartDate = p.StartDate, DueDate = p.DueDate, Budget = p.Budget, HourlyRate = p.HourlyRate, TeamId = p.Team == null ? (int?)null : p.Team.Id
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new OperationResult<ProjectDto>(item);
        }
    }
}
