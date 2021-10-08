using System;

namespace Api.Domain.DTOs
{
    public class UserUpdateResultDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}