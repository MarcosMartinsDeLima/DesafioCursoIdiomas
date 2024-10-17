using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoIdiomas.Model
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Column]
        [Required]
        [StringLength(100)]
        public string Codigo { get; set; } = string.Empty;

        [Column]
        [Required]
        [StringLength(80)]
        public string Nivel { get; set; } = string.Empty;

    }
}