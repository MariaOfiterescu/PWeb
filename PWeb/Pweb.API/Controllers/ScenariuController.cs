using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pweb.API.Data;
using Pweb.API.Models.Domain;
using Pweb.API.Models.DTOs;
using System.Data;
using System.IO;

namespace Pweb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenariuController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public ScenariuController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllScenariu()
        {
            //Get data from Database - Domain models

            var scenariu_domeniu = dBContext.scenariu.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var scenariuDTO = new List<ScenariuDTO>(); //punem domain model ul in DTO
            foreach (var scenariu in scenariu_domeniu) //regionDomain si regionsDomain

            {
                scenariuDTO.Add(new ScenariuDTO()
                {
                    scenariuid = scenariu.scenariuid,
                    nume = scenariu.nume

                });
            }

            //return DTOs
            return Ok(scenariuDTO);


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

            var scenariu = dBContext.scenariu.FirstOrDefault(x => x.scenariuid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (scenariu == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var scenariu_DTO = new ScenariuDTO
            {
                scenariuid = scenariu.scenariuid,
                nume = scenariu.nume
               
            };

            //Return  DTO back to client
            return Ok(scenariu_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddScenariuRequestDTO addScenariuRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var scenariuDomainModel = new Scenariu
            {
                scenariuid = addScenariuRequestDTO.scenariuid,
                nume = addScenariuRequestDTO.nume
               
            };

            //Use Domain Model to create Actori
            dBContext.scenariu.Add(scenariuDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var scenariuDTO = new ScenariuDTO
            {
                scenariuid = scenariuDomainModel.scenariuid,
                nume = scenariuDomainModel.nume,
              
            };

            return CreatedAtAction(nameof(GetById), new { id = scenariuDTO.scenariuid }, scenariuDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateScenariuRequestDTO updateScenariuRequestDTO)
        {
            // Check if region exists
            var scenariuDomainModel = dBContext.scenariu.FirstOrDefault(x => x.scenariuid == id);

            if (scenariuDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model

            scenariuDomainModel.nume = updateScenariuRequestDTO.nume;
            

            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var scenariuDTO = new ScenariuDTO
            {
                scenariuid = scenariuDomainModel.scenariuid,
                nume = scenariuDomainModel.nume,
                


            };
            return Ok(scenariuDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var scenariuDomainModel = dBContext.scenariu.FirstOrDefault(x => x.scenariuid == id);

            if (scenariuDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.scenariu.Remove(scenariuDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var scenariuDTO = new ScenariuDTO
            {
                scenariuid = scenariuDomainModel.scenariuid,
                nume = scenariuDomainModel.nume

            };

            return Ok(scenariuDTO);
        }

    }
}
