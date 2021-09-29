using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services;
using Api.Service.Services;
using System.Threading.Tasks;
using System;
using System.Net;
using Api.Domain.Entities.User;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] UserEntity user)
        {
            if(!ModelState.IsValid)
                return BadRequest(new {message="Formatação inválida: " + ModelState});

                if(user == null)
                return BadRequest(new {message="Usuário inválido."});

                try{

                    var Result = await _loginService.FindByLogin(user);

                    if(Result != null)
                        return Ok(Result);
                    else
                        return  NotFound(new {message="Usuário não encontrado."});

                }catch(Exception e){
                    return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
                }
        }

    }
}