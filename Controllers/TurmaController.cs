using CursoIdiomas.DTOs;
using CursoIdiomas.Model;
using CursoIdiomas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaRepository turmaRepository;

        public TurmaController(ITurmaRepository turmaRepository){
            this.turmaRepository=turmaRepository;
        }

        [HttpPost("criar")]
        public async Task<ActionResult<Turma>> CriarTurma([FromBody]TurmaDto turmaDto)
        {
            if(turmaDto.Codigo == string.Empty || turmaDto.Nivel == string.Empty) return BadRequest("Preencha Codigo da Turma e nivel da Turma");

            var turma = await turmaRepository.CriarTurma(turmaDto);
            return Created(string.Empty,turma);
        }

        [HttpGet("listarTodas")]
        public async Task<ActionResult<IEnumerable<Turma>>> ListarTodas()
        {
            var turmas = await turmaRepository.ListarTodas();
            if(turmas == null) return Ok("Nenhuma turma cadastrada");
            return Ok(turmas);
        }

        [HttpGet("listarPorId/{id}")]
        public async Task<ActionResult<Turma>> ListarPorId(int id)
        {
            Turma turma =  await turmaRepository.ListarTurmaPorId(id);
            if(turma == null) return NotFound("Nenhuma turma encontrada");
            return Ok(turma);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoverTurma(int id)
        {
            Turma turma = await turmaRepository.ListarTurmaPorId(id);
            if(turma == null) return NotFound("Não foi possivel achar uma turma com esse id");

            int qtd = await turmaRepository.ListarQtdMatriculas(id);
            if(qtd >0) return BadRequest("Não é possivel apagar uma turma que possui matriculas ativas");

            bool succed = await turmaRepository.ExcluirTurma(id);
            if(!succed) return BadRequest("Não foi possivel excluir essa turma");
            
            return NoContent();
        }

    }
}