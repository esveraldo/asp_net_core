namespace Api.Domain.Entities.User
{
    public class UserEntity : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}