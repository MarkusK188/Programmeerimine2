using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveUserCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var duplicate = await _dbContext.Users.AnyAsync(u => u.UserName == request.UserName && u.Id != request.Id, cancellationToken);
            if (duplicate) return result.AddPropertyError("UserName", "This username is already in use");

            User user;
            if (request.Id == 0)
            {
                user = new User();
                _dbContext.Users.Add(user);
            }
            else
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                if (user == null) return result.AddError("User not found");
            }

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Role = request.Role;
            user.IsAdmin = request.IsAdmin;
            if (!string.IsNullOrWhiteSpace(request.PasswordHash)) user.PasswordHash = request.PasswordHash;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
