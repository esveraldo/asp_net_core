using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities.User;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UserCrudComplete : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UserCrudComplete(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName="CRUD de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task TesteCrudCompleto()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                UserRepository _repository = new UserRepository(context);
                UserEntity _entity = new UserEntity()
                {
                    Email = "EsveTeste@teste.com",
                    Nome = "Esveraldo Teste",
                    Senha = "123456",
                    Role = "Assistente"
                };

                //Create
                var registroCriado = await _repository.Post(_entity);
                Assert.NotNull(registroCriado);
                Assert.Equal("EsveTeste@teste.com", registroCriado.Email);
                Assert.Equal("Esveraldo Teste", registroCriado.Nome);
                Assert.Equal("123456", registroCriado.Senha);
                Assert.Equal("Assistente", registroCriado.Role);
                Assert.False(registroCriado.Id == Guid.Empty);

                //Existe
                var registroExiste = await _repository.Exist(registroCriado.Id);
                Assert.True(registroExiste);

                //Selecionar um registro
                var registroSelecionado = await _repository.Select(registroCriado.Id);
                Assert.NotNull(registroSelecionado);
                Assert.Equal(registroCriado.Email, registroSelecionado.Email);
                Assert.Equal(registroCriado.Nome, registroSelecionado.Nome);
                Assert.Equal(registroCriado.Senha, registroSelecionado.Senha);
                Assert.Equal(registroCriado.Role, registroSelecionado.Role);

                //Selecionar todos
                var todosRegistros = await _repository.Select();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 1);

                //Update
                _entity.Nome = "Esveraldo Teste Alterado";
                var registroAtualizado = await _repository.Put(_entity);
                Assert.Equal("EsveTeste@teste.com", registroAtualizado.Email);
                Assert.Equal("Esveraldo Teste Alterado", registroAtualizado.Nome);
                Assert.Equal("123456", registroAtualizado.Senha);
                Assert.Equal("Assistente", registroAtualizado.Role);

                //Remover registro
                var removerRegistro = await _repository.Delete(registroSelecionado.Id);
                Assert.True(removerRegistro);

                //Login
                var usuarioLogin = await _repository.FindByLogin("esveraldo@hotmail.com", "123456");
                Assert.NotNull(usuarioLogin);
                Assert.Equal("esveraldo@hotmail.com", usuarioLogin.Email);
                Assert.Equal("Esveraldo M. de Oliveira", usuarioLogin.Nome);
                Assert.Equal("123456", usuarioLogin.Senha);
                Assert.Equal("Admin", usuarioLogin.Role);
            }
        }
    }
}