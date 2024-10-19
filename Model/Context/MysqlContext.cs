using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Model.Context
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(){}
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
        public DbSet<Turma> Turma {get;set;}
        public DbSet<Aluno> Aluno {get;set;}
        public DbSet<AlunoTurma> AlunoTurma {get;set;}
    }
}