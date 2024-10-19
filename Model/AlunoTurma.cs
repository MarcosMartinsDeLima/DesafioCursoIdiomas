using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Model
{
    [PrimaryKey(nameof(AlunoId), nameof(TurmaId))]
    public class AlunoTurma
    {
        [ForeignKey("Aluno")]
 
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }


        [ForeignKey("Turma")]
 
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}