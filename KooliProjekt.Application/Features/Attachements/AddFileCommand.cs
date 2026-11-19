using System.IO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Attachements
{
    public class AddFileCommand : IRequest<OperationResult>
    {
        public int AssignmentId { get; set; }
        public int? UploadedById { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }



    }
}
