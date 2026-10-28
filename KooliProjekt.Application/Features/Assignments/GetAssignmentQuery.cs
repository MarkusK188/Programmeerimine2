using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.Features.Assignments
{
    public class GetAssignmentQuery : IRequest<OperationResult<AssignmentDto>>
    {
        public int Id { get; set; }
    }
}
