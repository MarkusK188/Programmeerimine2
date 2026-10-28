using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTOs;

namespace KooliProjekt.Application.Features.Attachements
{
    public class GetAttachementQuery : IRequest<OperationResult<AttachementDto>>
    {
        public int Id { get; set; }
    }
}
