using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Assignments
{
    public class GetAssignmentQueryHandler : IRequestHandler<GetAssignmentQuery, Assignment>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAssignmentQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Assignment> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Assignments.FindAsync(request.Id, cancellationToken);

            return item;
        }
    }
}
