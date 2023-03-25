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
    public class FilmeActoriController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public FilmeActoriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllFilmeActori()
        {
            //Get data from Database - Domain models

            var filmeactori_domeniu = dBContext.filmeactori.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var filmeactoriDTO = new List<FilmeActoriDTO>(); //punem domain model ul in DTO
            foreach (var filmeactor in filmeactori_domeniu) //regionDomain si regionsDomain

            {
                filmeactoriDTO.Add(new FilmeActoriDTO()
                {
                    filmid = filmeactor.filmid,
                    actorid = filmeactor.actorid
                });
            }

            //return DTOs
            return Ok(filmeactoriDTO);


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

            var filmeactori = dBContext.filmeactori.FirstOrDefault(x => x.filmid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (filmeactori == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var filmeactori_DTO = new FilmeActoriDTO
            {
                filmid = filmeactori.filmid,
                actorid = filmeactori.actorid
               
            };

            //Return  DTO back to client
            return Ok(filmeactori_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddFilmeActoriRequestDTO addFilmeActoriRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var filmeactoriDomainModel = new FilmeActori
            {
                filmid = addFilmeActoriRequestDTO.filmid,
                actorid = addFilmeActoriRequestDTO.actorid
               
            };

            //Use Domain Model to create Actori
            dBContext.filmeactori.Add(filmeactoriDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var filmeactoriDTO = new FilmeActoriDTO
            {
                filmid = filmeactoriDomainModel.filmid,
                actorid = filmeactoriDomainModel.actorid

            };

            return CreatedAtAction(nameof(GetById), new { id = filmeactoriDTO.filmid }, filmeactoriDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFilmeActoriRequestDTO updateFilmeActoriRequestDTO)
        {
            // Check if region exists
            var filmeactoriDomainModel = dBContext.filmeactori.FirstOrDefault(x => x.filmid == id);

            if (filmeactoriDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model

            //filmeactoriDomainModel.filmid = updateFilmeActoriRequestDTO.filmid;
            filmeactoriDomainModel.actorid = updateFilmeActoriRequestDTO.actorid;
           



            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var filmeactoriDTO = new FilmeActoriDTO
            {
                filmid = filmeactoriDomainModel.filmid,
                actorid = filmeactoriDomainModel.actorid

            };
            return Ok(filmeactoriDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var filmeactoriDomainModel = dBContext.filmeactori.FirstOrDefault(x => x.filmid == id);

            if (filmeactoriDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.filmeactori.Remove(filmeactoriDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var filmeactoriDTO = new FilmeActoriDTO
            {
                filmid = filmeactoriDomainModel.filmid,
                actorid = filmeactoriDomainModel.actorid

            };

            return Ok(filmeactoriDTO);
        }

    }
}
