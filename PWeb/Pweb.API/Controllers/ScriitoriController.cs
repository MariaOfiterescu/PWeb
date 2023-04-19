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
            //luam modelul din baza de date 

            var scriitori_domeniu = dBContext.scriitori.ToList();

            //punem domain model ul in DTO

            var scriitoriDTO = new List<ScriitoriDTO>(); 
            foreach (var scriitor in scriitori_domeniu) 

            {
                scriitoriDTO.Add(new ScriitoriDTO()
                {
                    scriitorid = scriitor.scriitorid,
                    nume = scriitor.nume,
                    prenume = scriitor.prenume,
                    varsta = scriitor.varsta
                });
            }

          
            return Ok(scriitoriDTO);


        }

        //get by id - afisare un singur scriitor dupa id
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {

            //luam scriitori domain model din baza de date

            var scriitori = dBContext.scriitori.FirstOrDefault(x => x.scriitorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (scriitori == null)
            {
                return NotFound();
            }

            // Mapare/Convertire Scriitori Domain Model la Scriitori DTO

            var scriitori_DTO = new ScriitoriDTO
            {
                scriitorid = scriitori.scriitorid,
                nume = scriitori.nume,
                prenume = scriitori.prenume,
                varsta = scriitori.varsta

            };

            //Returnare  DTO inapoi la client
            return Ok(scriitori_DTO);
        }

        //POST : crearea unui nou Scriitor
        //POST

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddScriitoriRequestDTO addScriitoriRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var scriitoriDomainModel = new Scriitori
            {
                scriitorid = addScriitoriRequestDTO.scriitorid,
                nume = addScriitoriRequestDTO.nume,
                prenume = addScriitoriRequestDTO.prenume,
                varsta = addScriitoriRequestDTO.varsta

            };

            //Folosim Domain Model pentru a cre Scriitori
            dBContext.scriitori.Add(scriitoriDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la DTO

            var scriitoriDTO = new ScriitoriDTO
            {
                scriitorid = scriitoriDomainModel.scriitorid,
                nume = scriitoriDomainModel.nume,
                prenume = scriitoriDomainModel.prenume,
                varsta = scriitoriDomainModel.varsta

            };

            return CreatedAtAction(nameof(GetById), new { id = scriitoriDTO.scriitorid }, scriitoriDomainModel);
        }

        //Update Scriitori
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateScriitoriRequestDTO updateScriitoriRequestDTO)
        {
            //Verificare daca scriitorul exista
            var scriitoriDomainModel = dBContext.scriitori.FirstOrDefault(x => x.scriitorid == id);

            if (scriitoriDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model

            scriitoriDomainModel.nume = updateScriitoriRequestDTO.nume;
            scriitoriDomainModel.prenume = updateScriitoriRequestDTO.prenume;
            scriitoriDomainModel.varsta = updateScriitoriRequestDTO.varsta;

            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var scriitoriDTO = new ScriitoriDTO
            {
                scriitorid = scriitoriDomainModel.scriitorid,
                nume = scriitoriDomainModel.nume,
                prenume = scriitoriDomainModel.prenume,
                varsta = scriitoriDomainModel.varsta

            };
            return Ok(scriitoriDTO);
        }

        //Delete Scriitor
        //DELETE 

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

            //stergere scriitor
            dBContext.scriitori.Remove(scriitoriDomainModel);
            dBContext.SaveChanges();

            //afisarea scriitorului sters
            //mapare Domain Model la DTO

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
