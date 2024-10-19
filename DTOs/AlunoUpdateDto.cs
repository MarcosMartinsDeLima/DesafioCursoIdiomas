namespace CursoIdiomas.DTOs
{
    public record AlunoUpdateDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateOnly DataNascimento { get; set; }
    }
}