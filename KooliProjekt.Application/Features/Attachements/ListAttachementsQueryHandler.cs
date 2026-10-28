using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Attachements
{
    public class ListAttachementsQueryHandler : IRequestHandler<ListAttachementsQuery, OperationResult<PagedResult<AttachementDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAttachementsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<AttachementDto>>> Handle(
            ListAttachementsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Attachements
                .Select(a => new AttachementDto
                {
                    Id = a.Id, AssignmentId = a.Assignment == null ? (int?)null : a.Assignment.Id, AssignmentName = a.Assignment == null ? null : a.Assignment.AssignmentName, FileName = a.FileName, UploadTime = a.UploadTime, UploadedById = a.UploadedById ?? (a.UploadedBy == null ? (int?)null : a.UploadedBy.Id), UploadedByUserName = a.UploadedBy == null ? null : a.UploadedBy.UserName
                });

            var page = await query.GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<AttachementDto>>(page);
        }
    }
}
