using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Attachements
{
    public class GetAttachementQueryHandler : IRequestHandler<GetAttachementQuery, Attachement>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAttachementQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Attachement> Handle(GetAttachementQuery request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Attachements.FindAsync(request.Id, cancellationToken);

            return item;
        }
    }
}
