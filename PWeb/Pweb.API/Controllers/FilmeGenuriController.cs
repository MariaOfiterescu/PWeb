using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pweb.API.Data;
using Pweb.API.Models.Domain;
using Pweb.API.Models.DTOs;
using System.Data;

namespace Pweb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeGenuriController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public FilmeGenuriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllFilmeGenuri()
        {
            //Get data from Database - Domain models

            var filmegenuri_domeniu = dBContext.filmegenuri.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var filmegenuriDTO = new List<FilmeGenuriDTO>(); //punem domain model ul in DTO
            foreach (var filmegen in filmegenuri_domeniu) //regionDomain si regionsDomain

            {
                filmegenuriDTO.Add(new FilmeGenuriDTO()
                {
                    filmid = filmegen.filmid,
                    genid = filmegen.genid
                });
            }

            //return DTOs
            return Ok(filmegenuriDTO);


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

            var filmegenuri = dBContext.filmegenuri.FirstOrDefault(x => x.filmid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (filmegenuri == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var filmegenuri_DTO = new FilmeGenuriDTO
            {
                filmid = filmegenuri.filmid,
                genid = filmegenuri.genid

            };

            //Return  DTO back to client
            return Ok(filmegenuri_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddFilmeGenuriRequestDTO addFilmeGenuriRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var filmegenuriDomainModel = new FilmeGenuri
            {
                filmid = addFilmeGenuriRequestDTO.filmid,
                genid = addFilmeGenuriRequestDTO.genid

            };

            //Use Domain Model to create Actori
            dBContext.filmegenuri.Add(filmegenuriDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var filmegenuriDTO = new FilmeGenuriDTO
            {
                filmid = filmegenuriDomainModel.filmid,
                genid = filmegenuriDomainModel.genid

            };

            return CreatedAtAction(nameof(GetById), new { id = filmegenuriDTO.filmid }, filmegenuriDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFilmeGenuriRequestDTO updateFilmeGenuriRequestDTO)
        {
            // Check if region exists
            var filmegenuriDomainModel = dBContext.filmegenuri.FirstOrDefault(x => x.filmid == id);

            if (filmegenuriDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model

          //  filmegenuriDomainModel.filmid = updateFilmeGenuriRequestDTO.filmid;
            filmegenuriDomainModel.genid = updateFilmeGenuriRequestDTO.genid;




            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var filmegenuriDTO = new FilmeGenuriDTO
            {
                filmid = filmegenuriDomainModel.filmid,
                genid = filmegenuriDomainModel.genid

            };
            return Ok(filmegenuriDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var filmegenuriDomainModel = dBContext.filmegenuri.FirstOrDefault(x => x.filmid == id);

            if (filmegenuriDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.filmegenuri.Remove(filmegenuriDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var filmegenuriDTO = new FilmeGenuriDTO
            {
                filmid = filmegenuriDomainModel.filmid,
                genid = filmegenuriDomainModel.genid

            };

            return Ok(filmegenuriDTO);
        }

    }
}
