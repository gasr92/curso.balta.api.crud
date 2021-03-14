using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Username é obrigatório")]
        [MinLength(4, ErrorMessage="Tamanho mínimo do campo é 4")]
        [MaxLength(60, ErrorMessage="Tamanho máximo do campo é 60")]
        public string Username { get; set; }

        [Required(ErrorMessage="Password é obrigatório")]
        [MinLength(4, ErrorMessage="Tamanho mínimo do campo é 4")]
        [MaxLength(60, ErrorMessage="Tamanho máximo do campo é 60")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}