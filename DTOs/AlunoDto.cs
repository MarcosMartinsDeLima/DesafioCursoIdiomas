using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.DTOs
{
    public record AlunoDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateOnly DataNascimento { get; set; }
        public int IdTurma { get; set; }
    }
}