using Api.Domain.Interfaces.Services.User;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //[AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDTO loginDto, [FromServices] ILoginService loginService)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (loginDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await loginService.FindByLogin(loginDto);

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
