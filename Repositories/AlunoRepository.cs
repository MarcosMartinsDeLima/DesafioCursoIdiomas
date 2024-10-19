using CursoIdiomas.DTOs;
using CursoIdiomas.Model;
using CursoIdiomas.Model.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly MysqlContext context;
        private readonly ITurmaRepository turmaRepository;

        public AlunoRepository(MysqlContext context, ITurmaRepository turmaRepository)
        {
            this.context = context;
            this.turmaRepository = turmaRepository;
        }
        public async Task<Aluno> AtualizarAluno(Aluno aluno)
        {
            context.Aluno.Update(aluno);
            await context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> CriarAluno(AlunoDto alunoDto)
        {
            Aluno aluno = new Aluno{
                Nome = alunoDto.Nome,
                Cpf = alunoDto.Cpf,
                Email = alunoDto.Email,
                DataNascimento = alunoDto.DataNascimento
            };
            
            context.Add(aluno);
            await context.SaveChangesAsync();
            await this.MatricularAluno(aluno.Id,alunoDto.IdTurma);
            return aluno;
        }

        public async Task<bool> DeletarAluno(int id)
        {
            var aluno = await context.Aluno.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(aluno == null) return false;
            context.Aluno.Remove(aluno);

            var turmas = await context.AlunoTurma.Where(x => x.AlunoId == id).ToListAsync();
            foreach(var turma in turmas){
                context.AlunoTurma.Remove(turma);
            }
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<Aluno> ListarAlunoPorId(int id)
        {
            var aluno = await context.Aluno.Where(x => x.Id == id).FirstOrDefaultAsync();
            return aluno;
        }

        public async Task<IEnumerable<Aluno>> ListarTodos()
        {
            IEnumerable<Aluno> alunos  = await context.Aluno.ToListAsync();
            return alunos;
        }
        public async Task<Aluno> ListarAlunoPorCpf(string cpf)
        {
            Aluno aluno = await context.Aluno.Where(a => a.Cpf == cpf).FirstOrDefaultAsync();
            return aluno;
        }

        public async Task<bool> ListarMatriculaDeAluno(int idAluno,int idTurma)
        {
            var result = await context.AlunoTurma.Where(a => a.AlunoId == idAluno && a.TurmaId == idTurma).FirstOrDefaultAsync();
            if (result == null) return false;
            return true;
        }

        public async Task<AlunoTurma> MatricularAluno(int idAluno, int idTurma)
        {
            AlunoTurma alunoTurma = new AlunoTurma{
                AlunoId = idAluno,
                TurmaId = idTurma
            };
            context.AlunoTurma.Add(alunoTurma);
            await context.SaveChangesAsync();
            return alunoTurma;
        }

        public async Task<bool> DesmatricularAluno(int idAluno,int idTurma)
        {
            AlunoTurma aluno = new AlunoTurma{
                AlunoId = idAluno,
                TurmaId = idTurma
            };
            context.AlunoTurma.Remove(aluno);
            try{
                await context.SaveChangesAsync();
                return true;
            }catch(InvalidOperationException)
            {
                return false;
            }
        }

    }
}