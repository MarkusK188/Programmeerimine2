using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Attachements
{
    public class GetAttachementQueryHandler : IRequestHandler<GetAttachementQuery, OperationResult<AttachementDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAttachementQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<AttachementDto>> Handle(
            GetAttachementQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _dbContext.Attachements
                .Where(a => a.Id == request.Id)
                .Select(a => new AttachementDto
                {
                    Id = a.Id, AssignmentId = a.Assignment == null ? (int?)null : a.Assignment.Id, AssignmentName = a.Assignment == null ? null : a.Assignment.AssignmentName, FileName = a.FileName, UploadTime = a.UploadTime, UploadedById = a.UploadedById ?? (a.UploadedBy == null ? (int?)null : a.UploadedBy.Id), UploadedByUserName = a.UploadedBy == null ? null : a.UploadedBy.UserName
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new OperationResult<AttachementDto>(item);
        }
    }
}
