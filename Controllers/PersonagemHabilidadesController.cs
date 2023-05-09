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
                    .Include(p => p.PersonagemHabilidade).ThenInclude(ps=> ps.Habilidade)
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
                await _context.PersonagemHabilidades.AddAsync(ph);
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


        /*[HttpGet("GetLista/{id}")]
        public async Task<IActionResult> GetLista(int id)
        {
             try
            {
                List<PersonagemHabilidade> phLista = new List<PersonagemHabilidade>();
                phLista = await _context.PersonagemHabilidades
                    .Include(p => p.Personagem)
                    .Include(p => p.Habilidade)
                    .Where(p => p.Personagem.Id == id).ToListAsync();

                return Ok(phLista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/


        [HttpGet("GetHAbilidades")]
        public async Task<IActionResult> GetHAbilidade()
        {
            try
            {
                List<Habilidade> habilidades = new List<Habilidade>();
                habilidades = await _context.Habilidades.ToListAsync();
                return Ok(habilidades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /*[HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeletePersonagemHabilidade(PersonagemHabilidade deletado)
        {
            try
            {
                PersonagemHabilidade phRemover = await _context.PersonagemHabilidades
                    .FirstOrDefaultAsync(p => p.PersonagemId == deletado.PersonagemId 
                        && p.HabilidadeId == deletado.HabilidadeId);

                if(phRemover == null)
                    throw new System.Exception("Personagem ou Habilidade não encontrados.");

                _context.PersonagemHabilidades.Remove(phRemover);
                int linhasAfetedas = await _context.SaveChangesAsync();
                return Ok(linhasAfetedas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
















    }
}