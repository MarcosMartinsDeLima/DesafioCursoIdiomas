using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.DTOs
{
    public record TurmaDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;

    }
}