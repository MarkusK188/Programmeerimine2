using KooliProjekt.Application.Features.Attachements;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

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

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveAttachementCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAttachementCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpPost("AddFile")]
        public async Task<IActionResult> AddFile(
    int assignmentId,
    int? uploadedById,
    IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + extension;

            using var inputStream = file.OpenReadStream();

            var command = new AddFileCommand
            {
                AssignmentId = assignmentId,
                UploadedById = uploadedById,
                FileName = fileName,
                InputStream = inputStream
            };

            var result = await _mediator.Send(command);

            return Result(result);
        }
        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var command = new DeleteFileCommand { Id = id };
            var result = await _mediator.Send(command);

            return Result(result);
        }
        [HttpGet("GetFile/{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var command = new GetFileCommand { Id = id };
            var stream = await _mediator.Send(command);

            return File(stream, "application/octet-stream");
        }

        [HttpGet("ListFiles/{assignmentId}")]
        public async Task<IActionResult> ListFiles(
    int assignmentId,
    int page = 1,
    int pageSize = 10)
        {
            if (assignmentId <= 0)
            {
                return BadRequest("Invalid Assignment ID.");
            }

            var query = new ListFilesQuery
            {
                AssignmentId = assignmentId,
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);

            return Result(result);
        }

    }
}
