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
            //Get data from Database - Domain models

            var filme_domeniu = dBContext.filme.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var filmeDTO = new List<FilmeDTO>(); //punem domain model ul in DTO
            foreach (var film in filme_domeniu) //regionDomain si regionsDomain

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

            //return DTOs
            return Ok(filmeDTO);


        }

        //get by id - get singe actor
        //GET : https://localhost:7183/swagger/index.html
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            //var actori = dBContext.actori.Find(id); // poate fi folosit doar pentru propriatetea id

            //get actori domain model din baza de date

            var film = dBContext.filme.FirstOrDefault(x => x.filmid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (film == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

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

            //Return  DTO back to client
            return Ok(film_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddFilmeRequestDTO addFilmeRequestDTO)
        {
            //Map or Convert DTO to Domain Model
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

            //Use Domain Model to create Actori
            dBContext.filme.Add(filmeDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

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

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFilmeRequestDTO updateFilmeRequestDTO)
        {
            // Check if region exists
            var filmeDomainModel = dBContext.filme.FirstOrDefault(x => x.filmid == id);

            if (filmeDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model


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

            //Convert Domain Model to DTO

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

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

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

            //delete actor
            dBContext.filme.Remove(filmeDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

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

