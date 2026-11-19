using MediatR;
using KooliProjekt.Application.Infrastructure.Results;


namespace KooliProjekt.Application.Features.Attachements
{
    public class DeleteFileCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
