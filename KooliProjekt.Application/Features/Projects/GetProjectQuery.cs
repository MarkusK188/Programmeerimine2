using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.Features.Projects
{
    public class GetProjectQuery : IRequest<OperationResult<ProjectDto>>
    {
        public int Id { get; set; }
    }
}
