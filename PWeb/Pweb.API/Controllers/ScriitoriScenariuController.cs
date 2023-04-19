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
            //luam modelul din baza de date

            var scriitoriscenariu_domeniu = dBContext.scriitoriscenariu.ToList();

            //punem domain model ul in DTO

            var scriitoriscenariuDTO = new List<ScriitoriScenariuDTO>(); 
            foreach (var scriitorscenariu in scriitoriscenariu_domeniu) 

            {
                scriitoriscenariuDTO.Add(new ScriitoriScenariuDTO()
                {
                  
                    scriitorid = scriitorscenariu.scriitorid,
                    scenariuid = scriitorscenariu.scenariuid

                });
            }

           
            return Ok(scriitoriscenariuDTO);
 
        }

        //get by id - afisarea unui singur camp din ScriitoriScenariu
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            
            //luam scriitoriscenariu domain model din baza de date

            var scriitoriscenariu = dBContext.scriitoriscenariu.FirstOrDefault(x => x.scriitorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (scriitoriscenariu == null)
            {
                return NotFound();
            }

            // Mapare/Convertire ScriitoriScenariu Domain Model la ScriitoriScenariu DTO

            var scriitoriscenariu_DTO = new ScriitoriScenariuDTO
            {
                scriitorid = scriitoriscenariu.scriitorid,
                scenariuid = scriitoriscenariu.scenariuid
            };

            //Returnare  DTO inapoi la client
            return Ok(scriitoriscenariu_DTO);
        }

        //POST : crearea unui nou camp ScriitoriScenariu
        //POST

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddScriitoriScenariuRequestDTO addScriitoriScenariuRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var scriitoriscenariuDomainModel = new ScriitoriScenariu
            {
                scriitorid = addScriitoriScenariuRequestDTO.scriitorid,
                scenariuid = addScriitoriScenariuRequestDTO.scenariuid
            };

            //Folosim Domain Model pentru a crea ScriitoriScenariu
            dBContext.scriitoriscenariu.Add(scriitoriscenariuDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi DTO

            var scriitoriscenariuDTO = new ScriitoriScenariuDTO
            {
      
                scriitorid = scriitoriscenariuDomainModel.scriitorid,
                scenariuid = scriitoriscenariuDomainModel.scenariuid
            };

            return CreatedAtAction(nameof(GetById), new { id = scriitoriscenariuDTO.scriitorid }, scriitoriscenariuDomainModel);
        }

        //Update ScriitoriScenariu
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateScriitoriScenariuRequestDTO updateScriitoriScenariuRequestDTO)
        {
            //Verificare daca ScriitoriScenariu exista 
            var scriitoriscenariuDomainModel = dBContext.scriitoriscenariu.FirstOrDefault(x => x.scriitorid == id);

            if (scriitoriscenariuDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model


           // scriitoriscenariuDomainModel.scriitorid = updateScriitoriScenariuRequestDTO.scriitorid;
            scriitoriscenariuDomainModel.scenariuid = updateScriitoriScenariuRequestDTO.scenariuid;
           



            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var scriitoriscenariuDTO = new ScriitoriScenariuDTO
            {
                scriitorid = scriitoriscenariuDomainModel.scriitorid,
                scenariuid = scriitoriscenariuDomainModel.scenariuid

            };
            return Ok(scriitoriscenariuDTO);
        }

        //Delete camp din ScriitoriScenariu
        //DELETE 

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

            //stergere scriitoriscenariu 
            dBContext.scriitoriscenariu.Remove(scriitoriscenariuDomainModel);
            dBContext.SaveChanges();

            //afisarea camppului sters din ScriitoriScenariu
            //mapare Domain Model la DTO

            var scriitoriscenariuDTO = new ScriitoriScenariuDTO
            {
                scriitorid = scriitoriscenariuDomainModel.scriitorid,
                scenariuid = scriitoriscenariuDomainModel.scenariuid

            };

            return Ok(scriitoriscenariuDTO);
        }

    }
}
