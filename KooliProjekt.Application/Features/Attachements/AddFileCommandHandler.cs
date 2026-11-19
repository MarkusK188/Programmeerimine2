using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Attachements
{
    public class AddFileCommandHandler :
        IRequestHandler<AddFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileClient _fileClient;

        public AddFileCommandHandler(
            ApplicationDbContext context,
            IFileClient fileClient)
        {
            _context = context;
            _fileClient = fileClient;
        }

        public async Task<OperationResult> Handle(
            AddFileCommand request,
            CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(
                    a => a.Id == request.AssignmentId,
                    cancellationToken);

            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Id == request.UploadedById,
                    cancellationToken);

            if (assignment == null || user == null)
            {
                throw new InvalidOperationException(
                    "The assignment or uploading user could not be found.");
            }

            var fileStore = Path.Combine(
                FileStore.Photos,
                "assignments",
                request.AssignmentId.ToString());

            var extension = Path.GetExtension(request.FileName);
            var storedFileName = Guid.NewGuid() + extension;

            await _fileClient.Save(
                storedFileName,
                fileStore,
                request.InputStream);

            var attachment = new Attachement
            {
                Assignment = assignment,
                FileName = request.FileName,
                FilePath = Path.Combine(fileStore, storedFileName),
                UploadTime = DateTime.UtcNow,
                UploadedBy = user,
                UploadedById = request.UploadedById
            };

            _context.Attachements.Add(attachment);

            await _context.SaveChangesAsync(cancellationToken);

            return new OperationResult();
        }
    }
}