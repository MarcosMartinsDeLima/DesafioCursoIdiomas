using CursoIdiomas.DTOs;
using CursoIdiomas.Model;

namespace CursoIdiomas.Repositories
{
    public interface ITurmaRepository
    {
        Task<Turma> CriarTurma(TurmaDto turma);
        Task<IEnumerable<Turma>> ListarTodas();
        Task<Turma> ListarTurmaPorId(int id);
        Task<bool> ExcluirTurma(int id);
        Task<int> ListarQtdMatriculas(int idTurma);
    }
}