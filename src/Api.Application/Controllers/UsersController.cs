using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(new {message="Erro de sintaxe: " + ModelState});

            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (Exception e)
            {
                
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
             if(!ModelState.IsValid)
                return BadRequest(new {message="Erro de sintaxe: " + ModelState});

            try
            {
                return Ok(await _userService.Get(id));
            }
            catch (Exception e)
            {
                
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles="Assistente")]
        public async Task<ActionResult> Post([FromBody] UserCreateDTO user)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {message="Erro de sintaxe: " + ModelState});

            try
            {
                var Result = await _userService.Post(user);

                if(Result != null){
                    //return Created(new Uri(Url.Link("GetWriteId", new {id = Result.Id})), Result);
                    return Ok(Result);
                }else{
                    return BadRequest(new {message="Não foi possivel criar o usuário."});
                }
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserUpdateDTO user)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {message="Erro de sintaxe: " + ModelState});

            try
            {
               var Result = await _userService.Put(user);

               if(Result != null){
                   return Ok(Result);
               }else{
                   return BadRequest(new {message="Não foi possível alterar o usuário."});
               }
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
            }
        }

        [HttpDelete("{id}")]
         public async Task<ActionResult> Delete(Guid id)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {message="Erro de sintaxe: " + ModelState});

            try
            {
               return Ok(await _userService.Delete(id));
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
            }
        }
    }
}