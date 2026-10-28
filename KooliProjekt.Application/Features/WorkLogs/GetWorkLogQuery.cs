using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class GetWorkLogQuery : IRequest<OperationResult<WorkLogDto>>
    {
        public int Id { get; set; }
    }
}
