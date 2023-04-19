using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pweb.API.Data;
using Pweb.API.Models.Domain;
using Pweb.API.Models.DTOs;
using System;
using System.Data;
using System.Security.Principal;

namespace Pweb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly PWebDBContext dBContext;

        public FilmeController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllFilme()
        {
            //luam modelul din baza de date 

            var filme_domeniu = dBContext.filme.ToList();

            //punem domain model ul in DTO

            var filmeDTO = new List<FilmeDTO>(); 
            foreach (var film in filme_domeniu) 

            {
                filmeDTO.Add(new FilmeDTO()
                {
                    filmid = film.filmid,
                    directorid1 = film.directorid1,
                    genid = film.genid,
                    actorid = film.actorid,
                    scenariuid1 = film.scenariuid1,
                    titlu = film.titlu,
                    durata = film.durata,
                    descriere=film.descriere,
                    anaparitie = film.anaparitie,
                    categorievarsta=film.categorievarsta


                }) ;
            }

            
            return Ok(filmeDTO);


        }

        //get by id - afisare un singur Film
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {

            //luare Filme domain model din baza de date

            var film = dBContext.filme.FirstOrDefault(x => x.filmid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (film == null)
            {
                return NotFound();
            }

            // Mapare/Convertire Filme Domain Model la Filme DTO

            var film_DTO = new FilmeDTO()
            {
                filmid = film.filmid,
                directorid1 = film.directorid1,
                genid = film.genid,
                actorid = film.actorid,
                scenariuid1 = film.scenariuid1,
                titlu = film.titlu,
                durata = film.durata,
                descriere = film.descriere,
                anaparitie = film.anaparitie,
                categorievarsta = film.categorievarsta
            };

            //Returnare  DTO inapoi la client
            return Ok(film_DTO);
        }

        //POST : pentru a crea un nou Film
        //POST 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddFilmeRequestDTO addFilmeRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var filmeDomainModel = new Filme
            {
                filmid = addFilmeRequestDTO.filmid,
                directorid1 = addFilmeRequestDTO.directorid1,
                genid = addFilmeRequestDTO.genid,
                actorid = addFilmeRequestDTO.actorid,
                scenariuid1 = addFilmeRequestDTO.scenariuid1,
                titlu = addFilmeRequestDTO.titlu,
                durata = addFilmeRequestDTO.durata,
                descriere = addFilmeRequestDTO.descriere,
                anaparitie = addFilmeRequestDTO.anaparitie,
                categorievarsta = addFilmeRequestDTO.categorievarsta
            };

            //Folosim Domain Model pentru a crea Filme
            dBContext.filme.Add(filmeDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la DTO

            var filmeDTO = new FilmeDTO
            {
                filmid = filmeDomainModel.filmid,
                directorid1 = filmeDomainModel.directorid1,
                genid = filmeDomainModel.genid,
                actorid = filmeDomainModel.actorid,
                scenariuid1 = filmeDomainModel.scenariuid1,
                titlu = filmeDomainModel.titlu,
                durata = filmeDomainModel.durata,
                descriere = filmeDomainModel.descriere,
                anaparitie = filmeDomainModel.anaparitie,
                categorievarsta = filmeDomainModel.categorievarsta

            };

            return CreatedAtAction(nameof(GetById), new { id = filmeDTO.filmid }, filmeDomainModel);
        }

        //Update Filme
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFilmeRequestDTO updateFilmeRequestDTO)
        {
            // Verificare daca filmul exista
            var filmeDomainModel = dBContext.filme.FirstOrDefault(x => x.filmid == id);

            if (filmeDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model


            filmeDomainModel.directorid1 = updateFilmeRequestDTO.directorid1;
            filmeDomainModel.genid = updateFilmeRequestDTO.genid;
            filmeDomainModel.actorid = updateFilmeRequestDTO.actorid;
            filmeDomainModel.scenariuid1 = updateFilmeRequestDTO.scenariuid1;
            filmeDomainModel.titlu = updateFilmeRequestDTO.titlu;
            filmeDomainModel.durata = updateFilmeRequestDTO.durata;
            filmeDomainModel.descriere = updateFilmeRequestDTO.descriere;
            filmeDomainModel.anaparitie = updateFilmeRequestDTO.anaparitie;
            filmeDomainModel.categorievarsta = updateFilmeRequestDTO.categorievarsta;

            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var filmeDTO = new FilmeDTO
            {
                filmid = filmeDomainModel.filmid,
                directorid1 = filmeDomainModel.directorid1,
                genid = filmeDomainModel.genid,
                actorid = filmeDomainModel.actorid,
                scenariuid1 = filmeDomainModel.scenariuid1,
                titlu = filmeDomainModel.titlu,
                durata = filmeDomainModel.durata,
                descriere = filmeDomainModel.descriere,
                anaparitie = filmeDomainModel.anaparitie,
                categorievarsta = filmeDomainModel.categorievarsta
            };
            return Ok(filmeDTO);
        }

        //Stergere Film
        //DELETE 

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var filmeDomainModel = dBContext.filme.FirstOrDefault(x => x.filmid == id);

            if (filmeDomainModel == null)
            {
                return NotFound();
            }

            //stergere film
            dBContext.filme.Remove(filmeDomainModel);
            dBContext.SaveChanges();

            //returnare film sters 
            //mapare Domain Model la DTO

            var filmeDTO = new FilmeDTO
            {
                filmid = filmeDomainModel.filmid,
                directorid1 = filmeDomainModel.directorid1,
                genid = filmeDomainModel.genid,
                actorid = filmeDomainModel.actorid,
                scenariuid1 = filmeDomainModel.scenariuid1,
                titlu = filmeDomainModel.titlu,
                durata = filmeDomainModel.durata,
                descriere = filmeDomainModel.descriere,
                anaparitie = filmeDomainModel.anaparitie,
                categorievarsta = filmeDomainModel.categorievarsta

            };

            return Ok(filmeDTO);
        }

    }
}

