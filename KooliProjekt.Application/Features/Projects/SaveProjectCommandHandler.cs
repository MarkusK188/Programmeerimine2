using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Projects
{
    public class SaveProjectCommandHandler : IRequestHandler<SaveProjectCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectCommandHandler(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveProjectCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Team team = null;

            if (request.TeamId.HasValue)
            {
                team = await _dbContext.Teams
                    .FirstOrDefaultAsync(t => t.Id == request.TeamId);

                if (team == null) 
                {
                    return result.AddPropertyError(
                        "TeamId",
                        "Cannot find team with ID " + request.TeamId);
                }
            }

            var project = new Project();

            if (request.Id == 0)
            {
                _dbContext.Add(project);
            }
            else 
            {
                project = await _dbContext.FindAsync<Project>(request.Id);

                if (project == null)
                {
                    return result.AddError("Project not found");
                }
            }
            project.ProjectName = request.ProjectName;
            project.StartDate = request.StartDate;
            project.DueDate = request.DueDate;
            project.Budget = request.Budget;
            project.HourlyRate = request.HourlyRate;
            project.Team = team;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
