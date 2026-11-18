using System;
using KooliProjekt.Application.Behaviors;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Assignments
{
    public class SaveAssignmentCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string AssignmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal EstimatedHours { get; set; }
        public bool FixedPrice { get; set; }
        public int TeamLeadId { get; set; }
        public string AssignmentDesc { get; set; }
    }
}
