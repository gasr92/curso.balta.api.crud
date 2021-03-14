using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage="Este campo é obrigatório")]
        [MinLength(4, ErrorMessage="Tamanho mínimo do campo é 4")]
        [MaxLength(60, ErrorMessage="Tamanho máximo do campo é 60")]
        [DataType("nvarchar")]
        public string Title { get; set; }
    }
}