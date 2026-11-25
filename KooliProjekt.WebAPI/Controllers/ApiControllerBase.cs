using KooliProjekt.Application.Infrastructure.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace KooliProjekt.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public abstract class ApiControllerBase : ControllerBase
    {
        private static JsonSerializerSettings _serializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore                
            };

        protected IActionResult Result(OperationResult result)
        {
            var serialized = JsonConvert.SerializeObject(result, _serializerSettings);

            if (result.HasErrors)
            {
                return BadRequest(serialized);
            }

            return Ok(serialized);
        }

        protected IActionResult Result<T>(OperationResult<T> result)
        {
            var serialized = JsonConvert.SerializeObject(result, _serializerSettings);

            if (result.HasErrors)
            {
                return BadRequest(serialized);
            }

            if (result.Value == null)
            {
                return NotFound();
            }

            return Ok(serialized);
        }
    }
}