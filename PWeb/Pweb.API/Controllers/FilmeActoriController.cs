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
    public class FilmeActoriController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public FilmeActoriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllFilmeActori()
        {
            //luam modelul din baza de date 

            var filmeactori_domeniu = dBContext.filmeactori.ToList();

            //punem domain model ul in DTO

            var filmeactoriDTO = new List<FilmeActoriDTO>();
            foreach (var filmeactor in filmeactori_domeniu) 

            {
                filmeactoriDTO.Add(new FilmeActoriDTO()
                {
                    filmid = filmeactor.filmid,
                    actorid = filmeactor.actorid
                });
            }

           
            return Ok(filmeactoriDTO);


        }

        //get by id - afisare FilmeActori dupa un singur id
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            

            //luam FilmeActori domain model din baza de date

            var filmeactori = dBContext.filmeactori.FirstOrDefault(x => x.filmid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (filmeactori == null)
            {
                return NotFound();
            }

            // Convertire FilmeActori Domain Model la FilmeActori DTO

            var filmeactori_DTO = new FilmeActoriDTO
            {
                filmid = filmeactori.filmid,
                actorid = filmeactori.actorid
               
            };

            //Returnare  DTO inapoi la client
            return Ok(filmeactori_DTO);
        }

        //POST : creare o noua legatura FilmeActori
        //POST 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddFilmeActoriRequestDTO addFilmeActoriRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var filmeactoriDomainModel = new FilmeActori
            {
                filmid = addFilmeActoriRequestDTO.filmid,
                actorid = addFilmeActoriRequestDTO.actorid
               
            };

            //Folosim Domain Model pentru a crea FilmeActori
            dBContext.filmeactori.Add(filmeactoriDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la DTO

            var filmeactoriDTO = new FilmeActoriDTO
            {
                filmid = filmeactoriDomainModel.filmid,
                actorid = filmeactoriDomainModel.actorid

            };

            return CreatedAtAction(nameof(GetById), new { id = filmeactoriDTO.filmid }, filmeactoriDomainModel);
        }

        //Update FilmeActori
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFilmeActoriRequestDTO updateFilmeActoriRequestDTO)
        {
            // Verificare daca FilmeActori exista 
            var filmeactoriDomainModel = dBContext.filmeactori.FirstOrDefault(x => x.filmid == id);

            if (filmeactoriDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model

            //filmeactoriDomainModel.filmid = updateFilmeActoriRequestDTO.filmid;
            filmeactoriDomainModel.actorid = updateFilmeActoriRequestDTO.actorid;
           

            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var filmeactoriDTO = new FilmeActoriDTO
            {
                filmid = filmeactoriDomainModel.filmid,
                actorid = filmeactoriDomainModel.actorid

            };
            return Ok(filmeactoriDTO);
        }

        //Delete FilmeActori
        //DELETE 

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var filmeactoriDomainModel = dBContext.filmeactori.FirstOrDefault(x => x.filmid == id);

            if (filmeactoriDomainModel == null)
            {
                return NotFound();
            }

            //stergere FilmeActori
            dBContext.filmeactori.Remove(filmeactoriDomainModel);
            dBContext.SaveChanges();

            //returnare campul FilmeActori sters 
            //mapare Domain Model la DTO

            var filmeactoriDTO = new FilmeActoriDTO
            {
                filmid = filmeactoriDomainModel.filmid,
                actorid = filmeactoriDomainModel.actorid

            };

            return Ok(filmeactoriDTO);
        }

    }
}
