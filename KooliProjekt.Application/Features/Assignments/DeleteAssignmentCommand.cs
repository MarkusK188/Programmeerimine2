using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Assignments
{
    public class DeleteAssignmentCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
