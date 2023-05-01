using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enum;
using RpgApi.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RpgApi.Utils;
using System.Collections.Generic;
using System.Linq;


namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagemHabilidadesController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonagemHabilidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem personagem = await _context.Personagens
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ps=> ps.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);

                if(personagem == null)
                    throw new System.Exception("Personagem não encontrado para o Id informado.");

                Habilidade habilidade = await _context.Habilidades
                    .FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);

                if(habilidade == null)
                    throw new System.Exception("Habilidade não encontrada.");


                PersonagemHabilidade ph = new PersonagemHabilidade();
                ph.Personagem = personagem;
                ph.Habilidade = habilidade;
                await _context.PersonegemHabilidades.AddAsync(ph);
                int linhasAfetedas = await _context.SaveChangesAsync();

                return Ok(linhasAfetedas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Personagem p = await _context.Personagens
                    .Include(ar => ar.Arma)
                    .Include(ph => ph.PersonagemHabilidades).ThenInclude(h => h.Habilidade)
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/


        [HttpGet("GetLista/{id}")]
        public async Task<IActionResult> GetLista(int id)
        {
             try
            {
                Personagem p = await _context.Personagens
                    .Include(ph => ph.PersonagemHabilidades).ThenInclude(h => h.Habilidade)
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetHAbilidade")]
        public async Task<IActionResult> GetHAbilidade()
        {
            try
            {
                List<Habilidade> lista = await _context.Habilidades.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeletePersonagemHabilidade(PersonagemHabilidade deletado)
        {
            try
            {
                Personagem personagem = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == deletado.PersonagemId);

                if(personagem == null)
                    throw new System.Exception("Personagem não encontrado para o Id informado.");

                Habilidade habilidade = await _context.Habilidades
                    .FirstOrDefaultAsync(h => h.Id == deletado.HabilidadeId);

                if(habilidade == null)
                    throw new System.Exception("Habilidade não encontrada.");


                _context.Personagens.Remove(personagem);
                _context.Habilidades.Remove(habilidade);

                await _context.SaveChangesAsync();
                int linhasAfetedas = await _context.SaveChangesAsync();

                return Ok(linhasAfetedas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
















    }
}