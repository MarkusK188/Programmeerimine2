using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Teams
{
    public class SaveTeamCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int TeamLeadId { get; set; }
        public int? UserId { get; set; }
    }
}
