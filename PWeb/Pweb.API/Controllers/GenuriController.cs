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
            //luam modelul din baza de date 

            var genuri_domeniu = dBContext.genuri.ToList();

            //punem domain model ul in DTO

            var genuriDTO = new List<GenuriDTO>(); 
            foreach (var gen in genuri_domeniu) 

            {
                genuriDTO.Add(new GenuriDTO()
                {
                   
                    genid = gen.genid,
                    numegen = gen.numegen
                });
            }

           
            return Ok(genuriDTO);


        }

        //get by id - afisare un singur gen
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            
            //luare genuri domain model din baza de date

            var genuri = dBContext.genuri.FirstOrDefault(x => x.genid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (genuri == null)
            {
                return NotFound();
            }

            // Mapare/Convertire Genuri Domain Model la Genuri DTO

            var genuri_DTO = new GenuriDTO
            {
                
                genid = genuri.genid,
                numegen = genuri.numegen

            };

            //Returnare DTO inapoi client
            return Ok(genuri_DTO);
        }

        //POST : pentru a crea un nou gen
        //POST 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddGenuriRequestDTO addGenuriRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var genuriDomainModel = new Genuri
            {
                genid = addGenuriRequestDTO.genid,
                numegen = addGenuriRequestDTO.numegen

            };

            //Folosim Domain Model pentru a crea Gen
            dBContext.genuri.Add(genuriDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la  DTO

            var genuriDTO = new GenuriDTO
            {
                
                genid = genuriDomainModel.genid,
                numegen = genuriDomainModel.numegen

            };

            return CreatedAtAction(nameof(GetById), new { id = genuriDTO.genid }, genuriDomainModel);
        }

        //Update Genuri
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateGenuriRequestDTO updateGenuriRequestDTO)
        {
            // Verificare daca genul exista
            var genuriDomainModel = dBContext.genuri.FirstOrDefault(x => x.genid == id);

            if (genuriDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model


            // genuriDomainModel.genid = updateGenuriRequestDTO.genid;

            genuriDomainModel.numegen = updateGenuriRequestDTO.numegen;



            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var genuriDTO = new GenuriDTO
            {
                genid = genuriDomainModel.genid,
                numegen = genuriDomainModel.numegen

            };
            return Ok(genuriDTO);
        }

        //Delete Gen
        //DELETE 

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

            //stergere gen
            dBContext.genuri.Remove(genuriDomainModel);
            dBContext.SaveChanges();

            //afisarea genului sters
            //mapare Domain Model la DTO

            var genuriDTO = new GenuriDTO
            {
                genid = genuriDomainModel.genid,
                numegen = genuriDomainModel.numegen,

            };

            return Ok(genuriDTO);
        }

    }
}
