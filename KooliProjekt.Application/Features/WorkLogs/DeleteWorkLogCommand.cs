using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class DeleteWorkLogCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
