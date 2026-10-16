using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Assignments
{
    public class GetAssignmentQuery : IRequest<Assignment>
    {
        public int Id { get; set; }
    }
}