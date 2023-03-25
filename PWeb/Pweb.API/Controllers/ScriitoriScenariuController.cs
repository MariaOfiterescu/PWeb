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
    public class ScriitoriScenariuController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public ScriitoriScenariuController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllScriitoriScenariu()
        {
            //Get data from Database - Domain models

            var scriitoriscenariu_domeniu = dBContext.scriitoriscenariu.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var scriitoriscenariuDTO = new List<ScriitoriScenariuDTO>(); //punem domain model ul in DTO
            foreach (var scriitorscenariu in scriitoriscenariu_domeniu) //regionDomain si regionsDomain

            {
                scriitoriscenariuDTO.Add(new ScriitoriScenariuDTO()
                {
                  
                    scriitorid = scriitorscenariu.scriitorid,
                    scenariuid = scriitorscenariu.scenariuid

                });
            }

            //return DTOs
            return Ok(scriitoriscenariuDTO);
 
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

            var scriitoriscenariu = dBContext.scriitoriscenariu.FirstOrDefault(x => x.scriitorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (scriitoriscenariu == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var scriitoriscenariu_DTO = new ScriitoriScenariuDTO
            {
                scriitorid = scriitoriscenariu.scriitorid,
                scenariuid = scriitoriscenariu.scenariuid
            };

            //Return  DTO back to client
            return Ok(scriitoriscenariu_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddScriitoriScenariuRequestDTO addScriitoriScenariuRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var scriitoriscenariuDomainModel = new ScriitoriScenariu
            {
                scriitorid = addScriitoriScenariuRequestDTO.scriitorid,
                scenariuid = addScriitoriScenariuRequestDTO.scenariuid
            };

            //Use Domain Model to create Actori
            dBContext.scriitoriscenariu.Add(scriitoriscenariuDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var scriitoriscenariuDTO = new ScriitoriScenariuDTO
            {
      
                scriitorid = scriitoriscenariuDomainModel.scriitorid,
                scenariuid = scriitoriscenariuDomainModel.scenariuid
            };

            return CreatedAtAction(nameof(GetById), new { id = scriitoriscenariuDTO.scriitorid }, scriitoriscenariuDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateScriitoriScenariuRequestDTO updateScriitoriScenariuRequestDTO)
        {
            // Check if region exists
            var scriitoriscenariuDomainModel = dBContext.scriitoriscenariu.FirstOrDefault(x => x.scriitorid == id);

            if (scriitoriscenariuDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model


            scriitoriscenariuDomainModel.scriitorid = updateScriitoriScenariuRequestDTO.scriitorid;
            scriitoriscenariuDomainModel.scenariuid = updateScriitoriScenariuRequestDTO.scenariuid;



            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var scriitoriscenariuDTO = new ScriitoriScenariuDTO
            {
                scriitorid = scriitoriscenariuDomainModel.scriitorid,
                scenariuid = scriitoriscenariuDomainModel.scenariuid

            };
            return Ok(scriitoriscenariuDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var scriitoriscenariuDomainModel = dBContext.scriitoriscenariu.FirstOrDefault(x => x.scriitorid == id);

            if (scriitoriscenariuDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.scriitoriscenariu.Remove(scriitoriscenariuDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var scriitoriscenariuDTO = new ScriitoriScenariuDTO
            {
                scriitorid = scriitoriscenariuDomainModel.scriitorid,
                scenariuid = scriitoriscenariuDomainModel.scenariuid

            };

            return Ok(scriitoriscenariuDTO);
        }

    }
}
