using MediatR;
using System.IO;


namespace KooliProjekt.Application.Features.Attachements
{
    public class GetFileCommand : IRequest<Stream>
    {
        public int Id { get; set; }
    }
}
