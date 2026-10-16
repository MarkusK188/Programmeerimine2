using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Attachements
{
    internal class ListAttachementsQueryHandler : IRequestHandler<ListAttachementsQuery, IEnumerable<Attachement>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAttachementsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Attachement>> Handle(ListAttachementsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Attachements.ToListAsync(cancellationToken);

            return result;
        }
    }
}
