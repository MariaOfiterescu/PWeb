using Microsoft.AspNetCore.Mvc;
using Pweb.API.Data;
using Pweb.API.Models.Domain;
using Pweb.API.Models.DTOs;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace Pweb.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]

    public class ActoriController : ControllerBase
    {
        
        private readonly PWebDBContext dBContext;

      public ActoriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

       // GET controller
      [HttpGet]
      public IActionResult GetAllActors()
        {
            //Get data from Database - Domain models

            var actori_domeniu = dBContext.actori.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var actoriDTO = new List<ActoriDTO>(); //punem domain model ul in DTO
            foreach (var  actor in actori_domeniu) //regionDomain si regionsDomain

                    {
                actoriDTO.Add(new ActoriDTO()
                {
                    actorid = actor.actorid,
                    nume = actor.nume,
                    prenume = actor.prenume,
                    nationalitate = actor.nationalitate,
                    datanastere = actor.datanastere

                });
                    }

            //return DTOs
            return Ok(actoriDTO);
            /*
            
            var actori = new List<Actori>()
            {
                
                new Actori
                {
                    actorid = 20,
                    nume = "Banu",
                    prenume="Sebastian",
                    nationalitate="Romana",
                    datanastere="2000-11-02"
                },

            };
            */


        }

        //get by id - get singe actor
        //GET : https://localhost:7183/swagger/index.html
        [HttpGet]
        [Route("{id:int}")]

        public IActionResult GetById([FromRoute] int id) 
        {
            //var actori = dBContext.actori.Find(id); // poate fi folosit doar pentru propriatetea id

            //get actori domain model din baza de date

            var actor = dBContext.actori.FirstOrDefault(x => x.actorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (actor == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var actor_DTO = new ActoriDTO
            {
                actorid = actor.actorid,
                nume = actor.nume,
                prenume = actor.prenume,
                nationalitate = actor.nationalitate,
                datanastere = actor.datanastere
            };

            //Return  DTO back to client
            return Ok(actor_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        public IActionResult Create([FromBody] AddActoriRequestDTO addActoriRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var actoriDomainModel = new Actori
            {
                actorid = addActoriRequestDTO.actorid,
                nume = addActoriRequestDTO.nume,
                prenume = addActoriRequestDTO.prenume,
                nationalitate = addActoriRequestDTO.nationalitate,
                datanastere = addActoriRequestDTO.datanastere
            };

            //Use Domain Model to create Actori
            dBContext.actori.Add(actoriDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var actorDTO = new ActoriDTO
            {
                actorid = actoriDomainModel.actorid,
                nume = actoriDomainModel.nume,
                prenume = actoriDomainModel.prenume,
                nationalitate = actoriDomainModel.nationalitate,
                datanastere = actoriDomainModel.datanastere

            };

            return CreatedAtAction(nameof(GetById), new { id = actorDTO.actorid }, actoriDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateActoriRequestDTO updateActoriRequestDTO)
        {
            // Check if region exists
            var actorDomainModel = dBContext.actori.FirstOrDefault(x => x.actorid == id);

            if (actorDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model

            actorDomainModel.nume = updateActoriRequestDTO.nume;
            actorDomainModel.prenume = updateActoriRequestDTO.prenume;
            actorDomainModel.nationalitate = updateActoriRequestDTO.nationalitate;
            actorDomainModel.datanastere = updateActoriRequestDTO.datanastere;

            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var actorDTO = new ActoriDTO
            {
                actorid = actorDomainModel.actorid,
                nume = actorDomainModel.nume,
                prenume = actorDomainModel.prenume,
                nationalitate = actorDomainModel.nationalitate,
                datanastere = actorDomainModel.datanastere

            };
            return Ok(actorDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
           var actorDomainModel = dBContext.actori.FirstOrDefault(x => x.actorid ==id);

            if (actorDomainModel == null) 
            { 
                return NotFound();
            }

            //delete actor
            dBContext.actori.Remove(actorDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var actorDTO = new ActoriDTO
            {
                actorid = actorDomainModel.actorid,
                nume = actorDomainModel.nume,
                prenume = actorDomainModel.prenume,
                nationalitate = actorDomainModel.nationalitate,
                datanastere = actorDomainModel.datanastere

            };

            return Ok(actorDTO);
        }

    }
}
