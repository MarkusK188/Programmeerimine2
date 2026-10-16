using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Assignments
{
    internal class ListAssignmentsQueryHandler : IRequestHandler<ListAssignmentsQuery, IEnumerable<Assignment>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAssignmentsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Assignment>> Handle(ListAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Assignments.ToListAsync(cancellationToken);

            return result;
        }
    }
}
