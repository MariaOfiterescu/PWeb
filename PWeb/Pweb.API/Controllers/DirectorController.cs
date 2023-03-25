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
    public class DirectorController : ControllerBase
    {

        private readonly PWebDBContext dBContext;

        public DirectorController(PWebDBContext dbContext)
        {
            this.dBContext = dbContext;
        }

        // GET controller
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllDirectors()
        {
            //Get data from Database - Domain models

            var director_domeniu = dBContext.director.ToList(); //luam modelul din baza de date 

            //Map domain models to DTOs

            var directorDTO = new List<DirectorDTO>(); //punem domain model ul in DTO
            foreach (var director in director_domeniu) //regionDomain si regionsDomain

            {
                directorDTO.Add(new DirectorDTO()
                {
                    directorid = director.directorid,
                    nume = director.nume,
                    prenume = director.prenume,
                    varsta = director.varsta

                });
            }

            //return DTOs
            return Ok(directorDTO);


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

            var director = dBContext.director.FirstOrDefault(x => x.directorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (director == null)
            {
                return NotFound();
            }

            // Map/ Convert Actori Domain Model la Actori DTO

            var director_DTO = new DirectorDTO
            {
                directorid = director.directorid,
                nume = director.nume,
                prenume = director.prenume,
                varsta = director.varsta
            };

            //Return  DTO back to client
            return Ok(director_DTO);
        }

        //POST to create new actor
        //POST : https://localhost:7183/swagger/index.html || https://localhost:7183/api/actori/{id}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddDirectorRequestDTO addDirectorRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var directorDomainModel = new Director
            {
                directorid = addDirectorRequestDTO.directorid,
                nume = addDirectorRequestDTO.nume,
                prenume = addDirectorRequestDTO.prenume,
                varsta = addDirectorRequestDTO.varsta
            };

            //Use Domain Model to create Actori
            dBContext.director.Add(directorDomainModel);

            dBContext.SaveChanges();

            //Map Domain model back to DTO

            var directorDTO = new DirectorDTO
            {
                directorid = directorDomainModel.directorid,
                nume = directorDomainModel.nume,
                prenume = directorDomainModel.prenume,
                varsta = directorDomainModel.varsta

            };

            return CreatedAtAction(nameof(GetById), new { id = directorDTO.directorid }, directorDomainModel);
        }

        //Update Actori
        //PUT : https://localhost:7183/api/actori/{id}

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateDirectorRequestDTO updateDirectorRequestDTO)
        {
            // Check if region exists
            var directorDomainModel = dBContext.director.FirstOrDefault(x => x.directorid == id);

            if (directorDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model

            directorDomainModel.nume = updateDirectorRequestDTO.nume;
            directorDomainModel.prenume = updateDirectorRequestDTO.prenume;
            directorDomainModel.varsta = updateDirectorRequestDTO.varsta;



            dBContext.SaveChanges();

            //Convert Domain Model to DTO

            var directorDTO = new DirectorDTO
            {
                directorid = directorDomainModel.directorid,
                nume = directorDomainModel.nume,
                prenume = directorDomainModel.prenume,
                varsta = directorDomainModel.varsta,


            };
            return Ok(directorDTO);
        }

        //Delete Region
        //DELETE : https://localhost:7183/api/actori/{id}

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            var directorDomainModel = dBContext.director.FirstOrDefault(x => x.directorid == id);

            if (directorDomainModel == null)
            {
                return NotFound();
            }

            //delete actor
            dBContext.director.Remove(directorDomainModel);
            dBContext.SaveChanges();

            //return the deleted actor back
            //map Domain Model to DTO

            var directorDTO = new DirectorDTO
            {
                directorid = directorDomainModel.directorid,
                nume = directorDomainModel.nume,
                prenume = directorDomainModel.prenume,
                varsta = directorDomainModel.varsta

            };

            return Ok(directorDTO);
        }

    }
}

