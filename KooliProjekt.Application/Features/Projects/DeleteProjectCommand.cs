using System;
using System.Collections.Generic;
using System.Text;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using OperationResult = KooliProjekt.Application.Infrastructure.Results.OperationResult;

namespace KooliProjekt.Application.Features.Projects
{
    public class DeleteProjectCommand : IRequest<OperationResult>, ITransactional                                            
    {
        public int Id { get; set; }
    }
}
