using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, OperationResult<PagedResult<UserDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListUsersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<UserDto>>> Handle(
            ListUsersQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Users
                .Select(u => new UserDto
                {
                    Id = u.Id, UserName = u.UserName, Email = u.Email, Role = u.Role
                });

            var page = await query.GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<UserDto>>(page);
        }
    }
}
