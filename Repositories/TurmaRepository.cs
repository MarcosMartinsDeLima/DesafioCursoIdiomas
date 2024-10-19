using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoIdiomas.DTOs;
using CursoIdiomas.Model;
using CursoIdiomas.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Repositories
{
    public class TurmaRepository :ITurmaRepository
    {
        private readonly MysqlContext context;

        public TurmaRepository(MysqlContext context)
        {
            this.context = context;
        }

        public async Task<Turma> CriarTurma(TurmaDto turmaDto)
        {
            Turma turma = new Turma{
                Codigo = turmaDto.Codigo,
                Nivel = turmaDto.Nivel,
            };
            context.Add(turma);
            await context.SaveChangesAsync();
            return turma;
        }

        public async Task<bool> ExcluirTurma(int id)
        {
            Turma turma = await context.Turma.Where(t => t.Id == id).FirstOrDefaultAsync();
            context.Remove(turma);
            await context.SaveChangesAsync();
            if(turma == null ) return false; 
            return true;
        }

        public async Task<IEnumerable<Turma>> ListarTodas()
        {
            IEnumerable<Turma> turmaList = await context.Turma.ToListAsync();
            return turmaList;
        }

        public async Task<Turma> ListarTurmaPorId(int id)
        {
            Turma turma = await context.Turma.Where(t => t.Id == id).FirstOrDefaultAsync();
            return turma; 
        }

        public async Task<int> ListarQtdMatriculas(int idTurma)
        {
            var turma = await context.AlunoTurma.CountAsync(x => x.TurmaId == idTurma);
            return turma;
        }
    }
}