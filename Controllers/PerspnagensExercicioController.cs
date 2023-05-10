using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class PersonagensExercicioController : ControllerBase
    {

        private static List<Personagem> personagens = new List<Personagem>()
        {            
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=500, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=400, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=300, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=200, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=50, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=25, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(personagens);
        }


        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetNome(string nome)
        {
            List<Personagem> listaFinal = personagens.FindAll(p => p.Nome == nome);
            decimal qtd = listaFinal.Count();
            if ( qtd == 1 ){
                return Ok(listaFinal);
            }
            else{
                return BadRequest("Nem um Persongem encontrado com este nome.");
            }
        }

        /*[HttpPost]
        public IActionResult AddPersonagem(Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa < 10)
                return BadRequest("Defesa não pode ter o valor menor a 10 (dez).");

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }*/

        [HttpPost]
        public IActionResult AddPersonagem(Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia < 35){
                return BadRequest("inteligencia do Mago não pode ter o valor menor a 35 (trinta e cinco).");
            }
            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago(int pontosVida)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro);
            listaBusca = listaBusca.OrderByDescending(p => p.PontosVida).ToList();
            return Ok(listaBusca);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            return Ok("A quantidade de personagens: " + personagens.Count + " -- e a soma da forca deles é: " + personagens.Sum(p => p.Forca));
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(int classe)
        {
            
            var listaBusca = personagens.Where(p => (int)p.Classe == classe).ToList();
            if ( listaBusca.Count != 0 ){
                return Ok(listaBusca);
            }
            else{
                return BadRequest("Esta classe não existe.");
            }
        }

        /*[HttpGet("GetByClasse/{enumId}")]
        public IActionResult GetByClasse(int enumId)
        {
            ClasseEnum classe = (ClasseEnum)enumId;
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe == classe);
            return Ok(listaBusca);
        }*/











    }
}