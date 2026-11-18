using System.Threading.Tasks;
using KooliProjekt.Application.Features.WorkLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class WorkLogsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public WorkLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new ListWorkLogsQuery();
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

            var query = new GetWorkLogQuery { Id = id };
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveWorkLogCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteWorkLogCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }
    }
}
