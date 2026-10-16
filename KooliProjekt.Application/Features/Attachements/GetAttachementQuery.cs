using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Attachements
{
    public class GetAttachementQuery : IRequest<Attachement>
    {
        public int Id { get; set; }
    }
}
