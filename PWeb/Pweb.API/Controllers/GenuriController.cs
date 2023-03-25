using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pweb.API.Data;
using Pweb.API.Models.Domain;
using Pweb.API.Models.DTOs;

namespace Pweb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenuriController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public GenuriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllGenuri()
        {
            //Get data from Database - Domain models

            var genuri_domeniu = dBContext.genuri.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var genuriDTO = new List<GenuriDTO>(); //punem domain model ul in DTO
            foreach (var gen in genuri_domeniu) //regionDomain si regionsDomain

            {
                genuriDTO.Add(new GenuriDTO()
                {
                   
                    genid = gen.genid,
                    numegen = gen.numegen
                });
            }

            //return DTOs
            return Ok(genuriDTO);


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

            var genuri = dBContext.genuri.FirstOrDefault(x => x.genid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (genuri == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var genuri_DTO = new GenuriDTO
            {
                
                genid = genuri.genid,
                numegen = genuri.numegen

            };

            //Return  DTO back to client
            return Ok(genuri_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddGenuriRequestDTO addGenuriRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var genuriDomainModel = new Genuri
            {
                genid = addGenuriRequestDTO.genid,
                numegen = addGenuriRequestDTO.numegen

            };

            //Use Domain Model to create Actori
            dBContext.genuri.Add(genuriDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var genuriDTO = new GenuriDTO
            {
                
                genid = genuriDomainModel.genid,
                numegen = genuriDomainModel.numegen

            };

            return CreatedAtAction(nameof(GetById), new { id = genuriDTO.genid }, genuriDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateGenuriRequestDTO updateGenuriRequestDTO)
        {
            // Check if region exists
            var genuriDomainModel = dBContext.genuri.FirstOrDefault(x => x.genid == id);

            if (genuriDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model


            // genuriDomainModel.genid = updateGenuriRequestDTO.genid;

            genuriDomainModel.numegen = updateGenuriRequestDTO.numegen;



            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var genuriDTO = new GenuriDTO
            {
                genid = genuriDomainModel.genid,
                numegen = genuriDomainModel.numegen

            };
            return Ok(genuriDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var genuriDomainModel = dBContext.genuri.FirstOrDefault(x => x.genid == id);

            if (genuriDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.genuri.Remove(genuriDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var genuriDTO = new GenuriDTO
            {
                genid = genuriDomainModel.genid,
                numegen = genuriDomainModel.numegen,

            };

            return Ok(genuriDTO);
        }

    }
}
