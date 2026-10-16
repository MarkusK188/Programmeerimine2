using System.Collections.Generic;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Assignments
{
    public class ListAssignmentsQuery : IRequest<IEnumerable<Assignment>>
    {
    }
}
