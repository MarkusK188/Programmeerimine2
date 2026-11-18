using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class DeleteWorkLogCommandHandler : IRequestHandler<DeleteWorkLogCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteWorkLogCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteWorkLogCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var affected = await _dbContext.WorkLogs.Where(w => w.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            if (affected == 0) return result.AddError("WorkLog not found");
            return result;
        }
    }
}
