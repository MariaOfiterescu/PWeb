using Microsoft.AspNetCore.Authorization;
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
     // DACA NU AVEM Authorize TOATA LUMEA CARE ARE URL UL POATE ACCESA ACTORII
     //[Authorize]  
     // acum fiecare metoda a clasei din interiorul clasei va fi accesata prin userul autentificat
    public class ActoriController : ControllerBase
    {
        
        private readonly PWebDBContext dBContext;

      public ActoriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

       // GET controller
      [HttpGet]
      [Authorize(Roles= "User,Admin")]
      public IActionResult GetAllActors()
        {
            //luam modelul din baza de date 

            var actori_domeniu = dBContext.actori.ToList();

            //punem domain model ul in DTO

            var actoriDTO = new List<ActoriDTO>(); 
            foreach (var  actor in actori_domeniu) 

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

        //get by id - afisarea unui singur actor
        //GET : https://localhost:7183/swagger/index.html
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id) 
        {
            //var actori = dBContext.actori.Find(id); // poate fi folosit doar pentru propriatetea id

            //luam actori domain model din baza de date

            var actor = dBContext.actori.FirstOrDefault(x => x.actorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (actor == null)
            {
                return NotFound();
            }

            // Mapare/Convertire Actori Domain Model la Actori DTO

            var actor_DTO = new ActoriDTO
            {
                actorid = actor.actorid,
                nume = actor.nume,
                prenume = actor.prenume,
                nationalitate = actor.nationalitate,
                datanastere = actor.datanastere
            };

            //Returnare DTO inapoi la client 
            return Ok(actor_DTO);
        }

        //POST : creare nou actor 
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddActoriRequestDTO addActoriRequestDTO)
        {
            //Map / Convert DTO la Domain Model
            var actoriDomainModel = new Actori
            {
                actorid = addActoriRequestDTO.actorid,
                nume = addActoriRequestDTO.nume,
                prenume = addActoriRequestDTO.prenume,
                nationalitate = addActoriRequestDTO.nationalitate,
                datanastere = addActoriRequestDTO.datanastere
            };

            //Folosim Domain Model sa cream Actori
            dBContext.actori.Add(actoriDomainModel);

            dBContext.SaveChanges();

            //Convertire Domain model inapoi la  DTO

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
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateActoriRequestDTO updateActoriRequestDTO)
        {
            // Verificare daca actorul exista
            var actorDomainModel = dBContext.actori.FirstOrDefault(x => x.actorid == id);

            if (actorDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model

            actorDomainModel.nume = updateActoriRequestDTO.nume;
            actorDomainModel.prenume = updateActoriRequestDTO.prenume;
            actorDomainModel.nationalitate = updateActoriRequestDTO.nationalitate;
            actorDomainModel.datanastere = updateActoriRequestDTO.datanastere;

            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

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

        //Delete Actor
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
           var actorDomainModel = dBContext.actori.FirstOrDefault(x => x.actorid ==id);

            if (actorDomainModel == null) 
            { 
                return NotFound();
            }

            //stergere actori
            dBContext.actori.Remove(actorDomainModel);
            dBContext.SaveChanges();

            //returnare actor sters (afisarea actorului sters)
            //mapare Domain Model la DTO

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
