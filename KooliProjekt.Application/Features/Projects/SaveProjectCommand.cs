using KooliProjekt.Application.Behaviors;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;
namespace KooliProjekt.Application.Features.Projects
{
    public class SaveProjectCommand :IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Budget { get; set; }
        public decimal HourlyRate { get; set; }
        public int? TeamId { get; set; }
    }
}
