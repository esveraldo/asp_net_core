using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="O campo nome deve ter no máximo {1} catacteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [EmailAddress(ErrorMessage="Email com formato inválido.")]
        [StringLength(100, ErrorMessage="O campo email deve ter no máximo {1} catacteres.")]
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}