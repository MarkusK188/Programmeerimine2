using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Attachements
{
    public class ListFilesQueryHandler :
        IRequestHandler<ListFilesQuery, OperationResult<PagedResult<AttachementDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListFilesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<AttachementDto>>> Handle(
            ListFilesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Attachements
                .Where(a => a.Assignment != null &&
                            a.Assignment.Id == request.AssignmentId)
                .Select(a => new AttachementDto
                {
                    Id = a.Id,
                    AssignmentId = a.Assignment == null ? (int?)null : a.Assignment.Id,
                    AssignmentName = a.Assignment == null ? null : a.Assignment.AssignmentName,
                    FileName = a.FileName,
                    UploadTime = a.UploadTime,
                    UploadedById = a.UploadedById ?? (a.UploadedBy == null ? (int?)null : a.UploadedBy.Id),
                    UploadedByUserName = a.UploadedBy == null ? null : a.UploadedBy.UserName
                });

            var page = await query.GetPagedAsync(
                request.Page,
                request.PageSize);

            return new OperationResult<PagedResult<AttachementDto>>(page);
        }
    }
}