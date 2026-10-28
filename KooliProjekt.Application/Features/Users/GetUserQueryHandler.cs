using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult<UserDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetUserQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<UserDto>> Handle(
            GetUserQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _dbContext.Users
                .Where(u => u.Id == request.Id)
                .Select(u => new UserDto
                {
                    Id = u.Id, UserName = u.UserName, Email = u.Email, Role = u.Role
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new OperationResult<UserDto>(item);
        }
    }
}
