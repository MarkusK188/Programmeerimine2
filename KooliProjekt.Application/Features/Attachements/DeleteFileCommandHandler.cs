using System.IO;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Attachements
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileClient _fileClient;

        public DeleteFileCommandHandler(
            ApplicationDbContext dbContext,
            IFileClient fileClient)
        {
            _dbContext = dbContext;
            _fileClient = fileClient;
        }
        public async Task<OperationResult> Handle(
           DeleteFileCommand request,
           CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var attachment = await _dbContext.Attachements
                .Include(a => a.Assignment)
                .FirstOrDefaultAsync(
                    a => a.Id == request.Id,
                    cancellationToken);

            if (attachment == null)
                return result.AddError("File not found");

            var fileStore = Path.Combine(
                FileStore.Photos,
                "assignments",
                attachment.Assignment.Id.ToString());

            await _fileClient.Delete(
                Path.GetFileName(attachment.FilePath),
                fileStore);

            _dbContext.Attachements.Remove(attachment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
