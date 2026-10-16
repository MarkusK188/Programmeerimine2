
using System.Threading.Tasks;
using KooliProjekt.Application.Features.Assignments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentsController : Controller
    {
        private readonly IMediator _mediator;

        public AssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new ListAssignmentsQuery();
            var result = await _mediator.Send(query);

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetAssignmentQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }
    }
}
