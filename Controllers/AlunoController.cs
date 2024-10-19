using CursoIdiomas.DTOs;
using CursoIdiomas.Model;
using CursoIdiomas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CursoIdiomas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController :ControllerBase
    {
        private readonly IAlunoRepository alunoRepository;
        private readonly ITurmaRepository turmaRepository;

        public AlunoController(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            this.alunoRepository = alunoRepository;
            this.turmaRepository = turmaRepository;
        }

        [HttpPost("criar")]
        public async Task<ActionResult<Aluno>> CriarAluno([FromBody] AlunoDto alunoDto)
        {
            if(alunoDto.Cpf.Length != 11) return BadRequest("Cpf deve conter 11 caracteres");
            var turma = await turmaRepository.ListarTurmaPorId(alunoDto.IdTurma);
            if (turma == null) return NotFound($"Não foi possivel achar uma turma com esse id {alunoDto.IdTurma}");
            
            var alunoCadastrado = await alunoRepository.ListarAlunoPorCpf(alunoDto.Cpf);
            if (alunoCadastrado != null) return BadRequest("Aluno já cadastrado com esse cpf");

            var qtd = await turmaRepository.ListarQtdMatriculas(alunoDto.IdTurma);
            if(qtd >= 5) return BadRequest("Numero maximo de 5 alunos já foi atingido");

            var Aluno = await alunoRepository.CriarAluno(alunoDto);
            return Created(string.Empty, Aluno);
        }

        [HttpGet("all")]
        public async Task<ActionResult> ListarTodosAlunos()
        {
            var alunos = await alunoRepository.ListarTodos();
            if(alunos == null) return NotFound("Nenhum aluno cadastrado");
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ListarPorId(int id)
        {
            var aluno = await alunoRepository.ListarAlunoPorId(id);
            if(aluno == null) return NotFound("Não foi possivel encontra um aluno com esse id");
            return Ok(aluno);
        }
        [HttpPost("matricular/{idAluno}")]
        public async Task<ActionResult> MatricularAluno(int idAluno,[FromBody]int idTurma)
        {
            var matriculaAtiva = await alunoRepository.ListarMatriculaDeAluno(idAluno, idTurma);
            if(matriculaAtiva) return BadRequest($"Aluno com id: {idAluno} já está matriculado na turma com de id{idTurma} ");

            await alunoRepository.MatricularAluno(idAluno, idTurma);
            return Ok("Aluno matriculado com sucesso");
        }

        [HttpDelete("desmatricular/{idAluno}")]
        public async Task<ActionResult> DesmatricularAluno(int idAluno,[FromBody]int idTurma)
        {
            var result = await alunoRepository.DesmatricularAluno(idAluno, idTurma);
            if(!result) return BadRequest("Não foi possivel desmatricular aluno");

            return NoContent();
        }

        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult> DeletarAluno(int id)
        {
            var aluno = await alunoRepository.ListarAlunoPorId(id);
            if(aluno == null) return BadRequest("Nao foi possivel achar um aluno com esse id");

            await alunoRepository.DeletarAluno(id);
            return NoContent();
        }

        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> AtualizarAluno([FromBody] AlunoUpdateDto alunoDto, int id )
        {
            if(alunoDto == null) return BadRequest("Preencha os campos de Nome,Email,Cpf,DataNascimento");

            var cpfIgual = await alunoRepository.ListarAlunoPorCpf(alunoDto.Cpf);
            Aluno aluno = await alunoRepository.ListarAlunoPorId(id);

            if(cpfIgual.Id != id) return BadRequest("O cpf informado está cadastrado para um usuario com outro id");

            aluno.Nome = alunoDto.Nome;
            aluno.Email = alunoDto.Email;
            aluno.Cpf = alunoDto.Cpf;
            aluno.DataNascimento = alunoDto.DataNascimento;

            var response = await alunoRepository.AtualizarAluno(aluno);
            return Ok(response);
        }
    }
}