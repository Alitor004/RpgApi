using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using RpgApi.Utils;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Personagem> Personagens { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Habilidade> Habilidades{get; set;}
        public DbSet<PersonagemHabilidade> PersonagemHabilidades{get; set;}
        public DbSet<Disputa> Disputas{get; set;}
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().HasData
            (
                new Personagem() { Id = 1, Nome = "Frodo", PontosVida = 100, Forca = 17, Defesa = 23, Inteligencia = 33, Classe = ClasseEnum.Cavaleiro },
                new Personagem() { Id = 2, Nome = "Sam", PontosVida = 100, Forca = 15, Defesa = 25, Inteligencia = 30, Classe = ClasseEnum.Cavaleiro },
                new Personagem() { Id = 3, Nome = "Galadriel", PontosVida = 100, Forca = 18, Defesa = 21, Inteligencia = 35, Classe = ClasseEnum.Clerigo },
                new Personagem() { Id = 4, Nome = "Gandalf", PontosVida = 100, Forca = 18, Defesa = 18, Inteligencia = 37, Classe = ClasseEnum.Mago },
                new Personagem() { Id = 5, Nome = "Hobbit", PontosVida = 100, Forca = 20, Defesa = 17, Inteligencia = 31, Classe = ClasseEnum.Cavaleiro },
                new Personagem() { Id = 6, Nome = "Celeborn", PontosVida = 100, Forca = 21, Defesa = 13, Inteligencia = 34, Classe = ClasseEnum.Clerigo },
                new Personagem() { Id = 7, Nome = "Radagast", PontosVida = 100, Forca = 25, Defesa = 11, Inteligencia = 35, Classe = ClasseEnum.Mago }
            );

           modelBuilder.Entity<Arma>().HasData
            (
                new Arma() { Id = 1, Nome = "Espada Sangrenta", Dano = 10, PersonagemId = 1 },
                new Arma() { Id = 2, Nome = "Espada Sagrada", Dano = 20, PersonagemId = 2 },
                new Arma() { Id = 3, Nome = "Cajado dos Elementos", Dano = 15, PersonagemId = 3 },
                new Arma() { Id = 4, Nome = "Orbe da Destruição", Dano = 30, PersonagemId = 4 },
                new Arma() { Id = 5, Nome = "Cajado das 10 Sombras", Dano = 20, PersonagemId = 6 },
                new Arma() { Id = 6, Nome = "Orbe de Nishgman", Dano = 25, PersonagemId = 7 },
                new Arma() { Id = 7, Nome = "Espada da Matix", Dano = 25, PersonagemId = 5 }
            );

            modelBuilder.Entity<PersonagemHabilidade>()
                .HasKey(ph => new {ph.PersonagemId, ph.HabilidadeId });

            modelBuilder.Entity<Habilidade>().HasData
            (
                new Habilidade(){Id=1, Nome="Adormecer", Dano=39},
                new Habilidade(){Id=2, Nome="Congelar", Dano=41},
                new Habilidade(){Id=3, Nome="Hipnotizar", Dano=37}
            );

            modelBuilder.Entity<PersonagemHabilidade>().HasData
            (                  
                new PersonagemHabilidade() { PersonagemId = 1, HabilidadeId =1 }, 
                new PersonagemHabilidade() { PersonagemId = 1, HabilidadeId =2 }, 
                new PersonagemHabilidade() { PersonagemId = 2, HabilidadeId =2 }, 
                new PersonagemHabilidade() { PersonagemId = 3, HabilidadeId =2 }, 
                new PersonagemHabilidade() { PersonagemId = 3, HabilidadeId =3 }, 
                new PersonagemHabilidade() { PersonagemId = 4, HabilidadeId =3 }, 
                new PersonagemHabilidade() { PersonagemId = 5, HabilidadeId =1 }, 
                new PersonagemHabilidade() { PersonagemId = 6, HabilidadeId =2 }, 
                new PersonagemHabilidade() { PersonagemId = 7, HabilidadeId =3 }                               
            );


            //Início da criação do usuário padrão.
            Usuario user = new Usuario();
            Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[]salt);
            user.Id = 1;
            user.Username = "UsuarioAdmin";
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.Perfil = "Admin";
            user.Email = "seuEmail@gmail.com";
            user.Latitude = -23.5200241;
            user.Longitude = -46.596498;

            modelBuilder.Entity<Usuario>().HasData(user);            
            //Fim da criação do usuário padrão.   

            //Define que se o Perfil não for informado, o valor padrão será jogador
            modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Jogador");
        }

        





    }
}