using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Esse campo é obrigatório")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage="Este campo deve possuir no maximo 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage="Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage="O valor deve ser maior que zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage="Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage="ID da categoria deve ser válido")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}