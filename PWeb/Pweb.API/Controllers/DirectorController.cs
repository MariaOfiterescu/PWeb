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
            //luam modelul din baza de date

            var director_domeniu = dBContext.director.ToList();

            //punem domain model ul in DTO

            var directorDTO = new List<DirectorDTO>(); 
            foreach (var director in director_domeniu) 

            {
                directorDTO.Add(new DirectorDTO()
                {
                    directorid = director.directorid,
                    nume = director.nume,
                    prenume = director.prenume,
                    varsta = director.varsta

                });
            }

            
            return Ok(directorDTO);


        }

        //get by id - afisam un singur director dupa id
        //GET 
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById([FromRoute] int id)
        {
            

            //luam director domain model din baza de date

            var director = dBContext.director.FirstOrDefault(x => x.directorid == id); //putem cauta dupa id, nume ,sau orice altceva

            if (director == null)
            {
                return NotFound();
            }

            // Mapare/Convertire Director Domain Model la Director DTO

            var director_DTO = new DirectorDTO
            {
                directorid = director.directorid,
                nume = director.nume,
                prenume = director.prenume,
                varsta = director.varsta
            };

            //Returnare  DTO inapoi la client
            return Ok(director_DTO);
        }

        //POST : crearea unui nou Director
        //POST 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddDirectorRequestDTO addDirectorRequestDTO)
        {
            //Mapare sau Convertire DTO la Domain Model
            var directorDomainModel = new Director
            {
                directorid = addDirectorRequestDTO.directorid,
                nume = addDirectorRequestDTO.nume,
                prenume = addDirectorRequestDTO.prenume,
                varsta = addDirectorRequestDTO.varsta
            };

            //Folosire Domain Model pentru a crea Director
            dBContext.director.Add(directorDomainModel);

            dBContext.SaveChanges();

            //Mapare Domain model inapoi la DTO

            var directorDTO = new DirectorDTO
            {
                directorid = directorDomainModel.directorid,
                nume = directorDomainModel.nume,
                prenume = directorDomainModel.prenume,
                varsta = directorDomainModel.varsta

            };

            return CreatedAtAction(nameof(GetById), new { id = directorDTO.directorid }, directorDomainModel);
        }

        //Update Director
        //PUT 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateDirectorRequestDTO updateDirectorRequestDTO)
        {
            // Verificare daca director exista 
            var directorDomainModel = dBContext.director.FirstOrDefault(x => x.directorid == id);

            if (directorDomainModel == null)
            {
                return NotFound();
            }

            //Mapare DTO la Domain model

            directorDomainModel.nume = updateDirectorRequestDTO.nume;
            directorDomainModel.prenume = updateDirectorRequestDTO.prenume;
            directorDomainModel.varsta = updateDirectorRequestDTO.varsta;

            dBContext.SaveChanges();

            //Convertire Domain Model la DTO

            var directorDTO = new DirectorDTO
            {
                directorid = directorDomainModel.directorid,
                nume = directorDomainModel.nume,
                prenume = directorDomainModel.prenume,
                varsta = directorDomainModel.varsta,


            };
            return Ok(directorDTO);
        }

        //Delete Director
        //DELETE 

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

            //stergere director
            dBContext.director.Remove(directorDomainModel);
            dBContext.SaveChanges();

            //afisare director sters 
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

