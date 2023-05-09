using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using RpgApi.Models.Enum;
using System.Threading.Tasks;
using RpgApi.Utils;
using System.Collections.Generic;
using System.Linq;


namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class DisputasController : ControllerBase
    {
        private readonly DataContext _context;

        public DisputasController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Arma")]
        public async Task<IActionResult> AtaqueComArmaAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.Personagens
                    .Include(p => p.Arma)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem oponente = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                int dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));

                dano = dano - new Random().Next(oponente.Defesa);

                if (dano > 0)
                    oponente.PontosVida = oponente.PontosVida - (int)dano;
                if (oponente.PontosVida <= 0)
                    d.Narracao = $"{oponente.Nome} foi derrotado!";

                _context.Personagens.Update(oponente);
                await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat("Atacante: {0}. ", atacante.Nome);
                dados.AppendFormat(" Oponente: {0}. ", oponente.Nome);
                dados.AppendFormat(" Pontos de vida do atacante: {0}.", atacante.PontosVida);
                dados.AppendFormat(" Pontos de vida do oponente: {0}.", oponente.PontosVida);
                dados.AppendFormat(" Arma Utilizada: {0}.", atacante.Arma.Nome);
                dados.AppendFormat(" Dano: {0}.", dano);

                d.Narracao += dados.ToString();
                d.DataDisputa = DateTime.Now;
                _context.Disputas.Add(d);
                _context.SaveChanges();

                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Habilidade")]
        public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.Personagens
                    .Include(p => p.PersonagemHabilidade)
                    .ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem oponente = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                PersonagemHabilidade ph = await _context.PersonagemHabilidades
                    .Include(p => p.Habilidade)
                    .FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId
                    && phBusca.PersonagemId == d.AtacanteId);

                if (ph == null)
                    d.Narracao = $"{atacante.Nome} não possui esta habilidade!";
                else{
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));

                    dano = dano - new Random().Next(oponente.Defesa);

                    if (dano > 0)
                        oponente.PontosVida = oponente.PontosVida - dano;
                    if (oponente.PontosVida <= 0)
                        d.Narracao = $"{oponente.Nome} foi derrotado!";

                    _context.Personagens.Update(oponente);
                    await _context.SaveChangesAsync();

                    StringBuilder dados = new StringBuilder();
                    dados.AppendFormat("Atacante: {0}. ", atacante.Nome);
                    dados.AppendFormat(" Oponente: {0}. ", oponente.Nome);
                    dados.AppendFormat(" Pontos de vida do atacante: {0}.", atacante.PontosVida);
                    dados.AppendFormat(" Pontos de vida do oponente: {0}.", oponente.PontosVida);
                    dados.AppendFormat(" Habilidade Utilizada: {0}.", atacante.Arma.Nome);
                    dados.AppendFormat(" Dano: {0}.", dano);

                    d.Narracao += dados.ToString();
                    d.DataDisputa = DateTime.Now;
                    _context.Disputas.Add(d);
                    _context.SaveChanges();
                }
                    
                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DisputaEmGrupo")]

        public async Task<IActionResult> DisputaEmGrupoAsync(Disputa d)
        {
            try
            {
                d.Resultados = new List<string>(); //Instancia a lista de resultados

                //Busca na base dos personagens informados no parametro incluindo Arma  e Habilidades
                List<Personagem> personagens = await _context.Personagens
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidade).ThenInclude(ph => ph.Habilidade)
                    .Where(p => d.ListaIdPersonagens.Contains(p.Id)).ToListAsync();

                //Contagem de presonagens vivos na lista obtida do banco de dados
                int qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                //Enquanto houver mais de um personagem vivo haverá disputa
                while (qtdPersonagensVivos > 1)
                {
                    //Seleciona personagens com pontos positivos e depois faz sorteio.
                    List<Personagem> atacantes = personagens.Where(p => p.PontosVida > 0).ToList();
                    Personagem atacante = atacantes[new Random().Next(atacantes.Count)];
                    d.AtacanteId = atacante.Id;

                    //Seleciona personagens com pontos positivos, exeto o atacante escolido e depois faz sorteio.
                    List<Personagem> oponentes = personagens.Where(p => p.Id != atacante.Id && p => p.PontosVida > 0).ToList();
                    Personagem oponente = oponentes[new Random().Next(oponentes.Count)];
                    d.OponenteId = oponente.Id;

                    //declara e redefine a cada passagem do while o valor das variaveis que serão usadas.
                    int dano = 0;
                    string ataqueUsado = string.Empty;
                    string resultado = string.Empty;

                    //Sorteia entre 0 e 1: 0 é um ataque com arma e 1 é um ataque com habilidades
                    bool ataqueUsaArma = (new Random().Next(1) == 0);

                    if( ataqueUsaArma && atacante.Arma != null)
                    {
                        dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));
                        dano = dano - new Random().Next(oponente.Defesa);
                        ataqueUsado = atacante.Arma.Nome;

                        if (dano > 0)
                            oponente.PontosVida = oponente.PontosVida - (int)dano;

                        resultado =
                            string.Format("{0} atacou {1} usado {2} com o dano {3}.", atacante.Nome, oponente.Nome, ataqueUsado, dano);
                        d.Narracao += resultado;
                        d.Resultados.Add(resultado);
                    }
                    else if (atacante.PersonagemHabilidade.Count != 0)
                    {
                        int sorteioHabilidadeId = new Random().Next(atacante.PersonagemHabilidade.Count);
                        Habilidade habilidadeEscolida = atacante.PersonagemHabilidade[sorteioHabilidadeId].Habilidade;
                        ataqueUsado = habilidadeEscolida.Nome;

                        dano = habilidadeEscolida.Dano + (new Random().Next(atacante.Inteligencia));
                        dano = dano - new Random().Next(oponente.Defesa);

                        if (dano > 0)
                            oponente.PontosVida = oponente.PontosVida - (int)dano;

                        resultado =
                            string.Format("{0} atacou {1} usado {2} com o dano {3}.", atacante.Nome, oponente.Nome, ataqueUsado, dano);
                        d.Narracao += resultado;
                        d.Resultados.Add(resultado);
                    }

                    if (!string.IsNullOrEmpty(ataqueUsado))
                    {
                        atacante.Vitorias++;
                        oponente.Derrotas++;
                        atacante.Disputas++;
                        oponente.Disputas++;

                        d.Id = 0;
                        d.DataDisputa = DateTime.Now;
                        _context.Disputas.Add(d);
                        await _context.SaveChangesAsync();
                    }
                    qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                    if(qtdPersonagensVivos == 1)
                    {
                        string resultadoFinal = 
                        
                    }
                }
                //Codigo após o fechamento do While. Atualizará os pontos de vida,
                //disputas, vitorias e derrotas de todos os personagens ao final das batalhas
                _context.Personagens.UpdateRange(personagens);
                await _context.SaveChangesAsync();

                return Ok(d);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }











    }
}