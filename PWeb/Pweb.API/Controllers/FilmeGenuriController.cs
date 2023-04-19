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
    public class FilmeGenuriController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public FilmeGenuriController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllFilmeGenuri()
        {
            //luam modelul din baza de date 

            var filmegenuri_domeniu = dBContext.filmegenuri.ToList();

            //punem domain model ul in DTO

            var filmegenuriDTO = new List<FilmeGenuriDTO>(); 
            foreach (var filmegen in filmegenuri_domeniu) 

            {
                filmegenuriDTO.Add(new FilmeGenuriDTO()
                {
                    filmid = filmegen.filmid,
                    genid = filmegen.genid
                });
            }

           
            return Ok(filmegenuriDTO);


        }

        //get by id - afisam un singur camp din FilmeGenuri dupa id 
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            

            //luam FilmeGenuri domain model din baza de date

            var filmegenuri = dBContext.filmegenuri.FirstOrDefault(x => x.filmid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (filmegenuri == null)
            {
                return NotFound();
            }

            // Mapare/Convertire FilmeGenuri Domain Model la FilmeGenuri DTO

            var filmegenuri_DTO = new FilmeGenuriDTO
            {
                filmid = filmegenuri.filmid,
                genid = filmegenuri.genid

            };

            //Returnare  DTO inapoi la client
            return Ok(filmegenuri_DTO);
        }

        //POST : creare nou camp FilmeGenuri
        //POST 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddFilmeGenuriRequestDTO addFilmeGenuriRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var filmegenuriDomainModel = new FilmeGenuri
            {
                filmid = addFilmeGenuriRequestDTO.filmid,
                genid = addFilmeGenuriRequestDTO.genid

            };

            //Folosim Domain Model pentru a crea FilmeGenuri
            dBContext.filmegenuri.Add(filmegenuriDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la DTO

            var filmegenuriDTO = new FilmeGenuriDTO
            {
                filmid = filmegenuriDomainModel.filmid,
                genid = filmegenuriDomainModel.genid

            };

            return CreatedAtAction(nameof(GetById), new { id = filmegenuriDTO.filmid }, filmegenuriDomainModel);
        }

        //Update FilmeGenuri
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFilmeGenuriRequestDTO updateFilmeGenuriRequestDTO)
        {
            // Verificare daca filmegenuri exista
            var filmegenuriDomainModel = dBContext.filmegenuri.FirstOrDefault(x => x.filmid == id);

            if (filmegenuriDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model

          //  filmegenuriDomainModel.filmid = updateFilmeGenuriRequestDTO.filmid;
            filmegenuriDomainModel.genid = updateFilmeGenuriRequestDTO.genid;




            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var filmegenuriDTO = new FilmeGenuriDTO
            {
                filmid = filmegenuriDomainModel.filmid,
                genid = filmegenuriDomainModel.genid

            };
            return Ok(filmegenuriDTO);
        }

        //Delete FilmeGenuri
        //DELETE 

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var filmegenuriDomainModel = dBContext.filmegenuri.FirstOrDefault(x => x.filmid == id);

            if (filmegenuriDomainModel == null)
            {
                return NotFound();
            }

            //stergere camp filmegenuri
            dBContext.filmegenuri.Remove(filmegenuriDomainModel);
            dBContext.SaveChanges();

            //afisarea campului FilmeGenuri care a fost sters
            //mapare Domain Model la DTO

            var filmegenuriDTO = new FilmeGenuriDTO
            {
                filmid = filmegenuriDomainModel.filmid,
                genid = filmegenuriDomainModel.genid

            };

            return Ok(filmegenuriDTO);
        }

    }
}
