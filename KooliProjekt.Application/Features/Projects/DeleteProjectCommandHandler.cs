using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, OperationResult>
    {
        private readonly ApplicationDbContext  _dbContext;

        public DeleteProjectCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<OperationResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Projects
                .Where(project => project.Id == request.Id)
                .ExecuteDeleteAsync();

            return new OperationResult();
        }

    }
}
