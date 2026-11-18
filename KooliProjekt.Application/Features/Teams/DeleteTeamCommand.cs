using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Teams
{
    public class DeleteTeamCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
