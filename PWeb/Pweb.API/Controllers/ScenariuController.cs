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
            //luam modelul din baza de date 

            var scenariu_domeniu = dBContext.scenariu.ToList();

            //punem domain model ul in DTO

            var scenariuDTO = new List<ScenariuDTO>();
            foreach (var scenariu in scenariu_domeniu) 

            {
                scenariuDTO.Add(new ScenariuDTO()
                {
                    scenariuid = scenariu.scenariuid,
                    nume = scenariu.nume

                });
            }

            
            return Ok(scenariuDTO);


        }

        //get by id - afisarea unui singur Scenariu
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            

            //luare scenariu domain model din baza de date

            var scenariu = dBContext.scenariu.FirstOrDefault(x => x.scenariuid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (scenariu == null)
            {
                return NotFound();
            }

            // Mapare/Convertire Scenariu Domain Model la Scenariu DTO

            var scenariu_DTO = new ScenariuDTO
            {
                scenariuid = scenariu.scenariuid,
                nume = scenariu.nume
               
            };

            //Returnare  DTO inapoi la  client
            return Ok(scenariu_DTO);
        }

        //POST : crearea unui nou scenariu
        //POST 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddScenariuRequestDTO addScenariuRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var scenariuDomainModel = new Scenariu
            {
                scenariuid = addScenariuRequestDTO.scenariuid,
                nume = addScenariuRequestDTO.nume
               
            };

            //Folosim Domain Model pentru a  crea Scenariu
            dBContext.scenariu.Add(scenariuDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la DTO

            var scenariuDTO = new ScenariuDTO
            {
                scenariuid = scenariuDomainModel.scenariuid,
                nume = scenariuDomainModel.nume,
              
            };

            return CreatedAtAction(nameof(GetById), new { id = scenariuDTO.scenariuid }, scenariuDomainModel);
        }

        //Update Secanriu
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateScenariuRequestDTO updateScenariuRequestDTO)
        {
            // verificare daca scenariu exista
            var scenariuDomainModel = dBContext.scenariu.FirstOrDefault(x => x.scenariuid == id);

            if (scenariuDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model

            scenariuDomainModel.nume = updateScenariuRequestDTO.nume;
            

            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var scenariuDTO = new ScenariuDTO
            {
                scenariuid = scenariuDomainModel.scenariuid,
                nume = scenariuDomainModel.nume,
   
            };
            return Ok(scenariuDTO);
        }

        //Delete Scenariu
        //DELETE 

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

            //stergere scenariu
            dBContext.scenariu.Remove(scenariuDomainModel);
            dBContext.SaveChanges();

            //afisarea scenariului sters
            //mapare Domain Model la DTO

            var scenariuDTO = new ScenariuDTO
            {
                scenariuid = scenariuDomainModel.scenariuid,
                nume = scenariuDomainModel.nume

            };

            return Ok(scenariuDTO);
        }

    }
}
