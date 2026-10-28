using System.Threading.Tasks;
using KooliProjekt.Application.Features.Attachements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class AttachementsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AttachementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new ListAttachementsQuery();
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetAttachementQuery { Id = id };
            var result = await _mediator.Send(query);

            return Result(result);
        }
    }
}
