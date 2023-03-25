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
    public class ScriitoriController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public ScriitoriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllScriitori()
        {
            //Get data from Database - Domain models

            var scriitori_domeniu = dBContext.scriitori.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var scriitoriDTO = new List<ScriitoriDTO>(); //punem domain model ul in DTO
            foreach (var scriitor in scriitori_domeniu) //regionDomain si regionsDomain

            {
                scriitoriDTO.Add(new ScriitoriDTO()
                {
                    scriitorid = scriitor.scriitorid,
                    nume = scriitor.nume,
                    prenume = scriitor.prenume,
                    varsta = scriitor.varsta
                });
            }

            //return DTOs
            return Ok(scriitoriDTO);


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

            var scriitori = dBContext.scriitori.FirstOrDefault(x => x.scriitorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (scriitori == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var scriitori_DTO = new ScriitoriDTO
            {
                scriitorid = scriitori.scriitorid,
                nume = scriitori.nume,
                prenume = scriitori.prenume,
                varsta = scriitori.varsta

            };

            //Return  DTO back to client
            return Ok(scriitori_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddScriitoriRequestDTO addScriitoriRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var scriitoriDomainModel = new Scriitori
            {
                scriitorid = addScriitoriRequestDTO.scriitorid,
                nume = addScriitoriRequestDTO.nume,
                prenume = addScriitoriRequestDTO.prenume,
                varsta = addScriitoriRequestDTO.varsta

            };

            //Use Domain Model to create Actori
            dBContext.scriitori.Add(scriitoriDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var scriitoriDTO = new ScriitoriDTO
            {
                scriitorid = scriitoriDomainModel.scriitorid,
                nume = scriitoriDomainModel.nume,
                prenume = scriitoriDomainModel.prenume,
                varsta = scriitoriDomainModel.varsta

            };

            return CreatedAtAction(nameof(GetById), new { id = scriitoriDTO.scriitorid }, scriitoriDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateScriitoriRequestDTO updateScriitoriRequestDTO)
        {
            // Check if region exists
            var scriitoriDomainModel = dBContext.scriitori.FirstOrDefault(x => x.scriitorid == id);

            if (scriitoriDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model

            //filmeactoriDomainModel.filmid = updateFilmeActoriRequestDTO.filmid;
            scriitoriDomainModel.nume = updateScriitoriRequestDTO.nume;
            scriitoriDomainModel.prenume = updateScriitoriRequestDTO.prenume;
            scriitoriDomainModel.varsta = updateScriitoriRequestDTO.varsta;

            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var scriitoriDTO = new ScriitoriDTO
            {
                scriitorid = scriitoriDomainModel.scriitorid,
                nume = scriitoriDomainModel.nume,
                prenume = scriitoriDomainModel.prenume,
                varsta = scriitoriDomainModel.varsta

            };
            return Ok(scriitoriDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var scriitoriDomainModel = dBContext.scriitori.FirstOrDefault(x => x.scriitorid == id);

            if (scriitoriDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.scriitori.Remove(scriitoriDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var scriitoriDTO = new ScriitoriDTO
            {
                scriitorid = scriitoriDomainModel.scriitorid,
                nume = scriitoriDomainModel.nume,
                prenume = scriitoriDomainModel.prenume,
                varsta = scriitoriDomainModel.varsta

            };

            return Ok(scriitoriDTO);
        }

    }
}
