using System;
using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Attachements
{
    public class SaveAttachementCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadTime { get; set; }
        public int UploadedById { get; set; }
    }
}
