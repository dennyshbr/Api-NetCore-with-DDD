using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<object> Login([FromBody] UserEntity user, [FromServices] ILoginService loginService)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await loginService.FindByLogin(user);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                int internalError = (int)HttpStatusCode.InternalServerError;

                return StatusCode(internalError, e.Message);
            }
        }
    }
}
