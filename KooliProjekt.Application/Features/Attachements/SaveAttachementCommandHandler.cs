using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Attachements
{
    public class SaveAttachementCommandHandler : IRequestHandler<SaveAttachementCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveAttachementCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveAttachementCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.Id == request.AssignmentId, cancellationToken);
            if (assignment == null) return result.AddPropertyError("AssignmentId", "Cannot find assignment with ID " + request.AssignmentId);
            var uploadedBy = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UploadedById, cancellationToken);
            if (uploadedBy == null) return result.AddPropertyError("UploadedById", "Cannot find user with ID " + request.UploadedById);

            Attachement attachement;
            if (request.Id == 0)
            {
                attachement = new Attachement();
                _dbContext.Attachements.Add(attachement);
            }
            else
            {
                attachement = await _dbContext.Attachements.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
                if (attachement == null) return result.AddError("Attachement not found");
            }

            attachement.Assignment = assignment;
            attachement.FileName = request.FileName;
            attachement.FilePath = request.FilePath;
            attachement.UploadTime = request.UploadTime;
            attachement.UploadedBy = uploadedBy;
            attachement.UploadedById = uploadedBy.Id;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
