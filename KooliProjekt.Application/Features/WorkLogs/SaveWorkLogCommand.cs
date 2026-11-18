using System;
using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.WorkLogs
{
    public class SaveWorkLogCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public DateTime Date { get; set; }
        public decimal TimeSpent { get; set; }
        public int ImplementerId { get; set; }
        public string Description { get; set; }
    }
}
