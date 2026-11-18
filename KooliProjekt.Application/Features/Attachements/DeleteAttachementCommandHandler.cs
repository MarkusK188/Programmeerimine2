using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Attachements
{
    public class DeleteAttachementCommandHandler : IRequestHandler<DeleteAttachementCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteAttachementCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteAttachementCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var affected = await _dbContext.Attachements.Where(a => a.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            if (affected == 0) return result.AddError("Attachement not found");
            return result;
        }
    }
}
