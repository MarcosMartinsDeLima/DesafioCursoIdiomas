using CursoIdiomas.DTOs;
using CursoIdiomas.Model;

namespace CursoIdiomas.Repositories
{
    public interface IAlunoRepository
    {
        Task<Aluno> CriarAluno(AlunoDto alunoDto);
        Task<IEnumerable<Aluno>> ListarTodos();
        Task<Aluno> ListarAlunoPorCpf(string cpf);
        Task<bool> ListarMatriculaDeAluno(int idAluno,int idTurma);
        Task<Aluno> ListarAlunoPorId(int id);
        Task<bool> DeletarAluno(int id);
        Task<Aluno> AtualizarAluno(Aluno aluno);
        Task<AlunoTurma> MatricularAluno(int idAluno, int idTurma);
        Task<bool> DesmatricularAluno(int idAluno,int idTurma);
        
    }
}