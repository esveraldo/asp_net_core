using System.Threading.Tasks;
using Api.Domain.Entities.User;
using Api.Domain.Interfaces.Services;
using Api.Domain.Repositories;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> FindByLogin(UserEntity user)
        {
            var baseUrl = new UserEntity();
            if(user != null && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Senha)){
                baseUrl = await _repository.FindByLogin(user.Email, user.Senha);
                if(baseUrl == null){
                    return new { message = "Falha ao autenticar"};
                }else{
                     var token = TokenService.GenerateToken(baseUrl);
                     var Result = new {
                         user = baseUrl,
                         token = token
                     };

                     return Result;
                }
            }else{
                return null;
            }
        }
    }
}