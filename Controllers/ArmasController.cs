using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enum;
using RpgApi.Data;
using System;
using Microsoft.EntityFrameworkCore;


namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ArmasController : ControllerBase
    {
        private readonly DataContext _context;

        public ArmasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Arma a = await _context.Armas.FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(a);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Arma> lista = await _context.Armas.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(Arma novaArma)
        {
            try
            {
                if(novaArma.Dano > 30)
                {
                    throw new Exception("Dano não podem ser maior que 30");
                }


                Personagem p = await _context.Personagens.FirstOrDefaultAsync(p => p.Id == novaArma.PersonagemId);
                if (p == null)
                {
                throw new System.Exception("Não existe personagem com o Id informado");
                }
            
                await _context.Armas.AddAsync(novaArma);
                await _context.SaveChangesAsync();

                List<Arma> lista = await _context.Armas.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Arma novaArma)
        {
            try
            {
                if(novaArma.Dano > 30)
                {
                    throw new Exception("Dano não podem ser maior que 30");
                }
                _context.Armas.Update(novaArma);
                int linhasAfetedas = await _context.SaveChangesAsync();

                return Ok(linhasAfetedas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Arma aRemove = await _context.Armas.FirstOrDefaultAsync(p => p.Id == id);

                _context.Armas.Remove(aRemove);
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