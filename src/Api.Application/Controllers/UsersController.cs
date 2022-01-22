using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (ArgumentException argumentEx)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;

                return StatusCode(statusCode, argumentEx.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _userService.Get(id));
            }
            catch (ArgumentException argumentEx)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;

                return StatusCode(statusCode, argumentEx.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var result = await _userService.Post(user);

                if (result != null && result.Id != null)
                {
                    var uri = new Uri(Url.Link("GetWithId", new { id = result.Id }));

                    return Created(uri, result);
                }
                else
                    return BadRequest("Occurred error to create user.");
            }
            catch (ArgumentException argumentEx)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;

                return StatusCode(statusCode, argumentEx.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserEntity user)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var result = await _userService.Put(user);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return BadRequest("Occurred error to update user.");
            }
            catch (ArgumentException argumentEx)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;

                return StatusCode(statusCode, argumentEx.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var deleted = await _userService.Delete(id);

                if (deleted)
                {
                    return NoContent();
                }
                else
                    return BadRequest("Occurred error to delete user.");
            }
            catch (ArgumentException argumentEx)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;

                return StatusCode(statusCode, argumentEx.Message);
            }
        }
    }
}
