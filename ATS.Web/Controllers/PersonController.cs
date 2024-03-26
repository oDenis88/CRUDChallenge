using System.Collections.Generic;
using System.Linq;
using ATS.Domain.Interfaces;
using ATS.Domain.Models;
using ATS.Web.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ATS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //Service example, if needed
        private readonly PersonService _personService;
        //Repository
        private readonly IRepository<Person> _personRepository;
        //Cache implementation
        private readonly IMemoryCache _cache;

        public PersonController(PersonService personService,
            IRepository<Person> personRepository, IMemoryCache cache)
        {
            _personService = personService;
            _personRepository = personRepository;
            _cache = cache;
        }

         [HttpGet]
         public IEnumerable<Person> Getpersons()
         {
            //Example of caching using Memory Cache
            var cacheKey = "Person";
            List<Person> persons;

            if (!_cache.TryGetValue<List<Person>>(cacheKey, out persons))
            {
                persons =  _personRepository.GetAll().ToList();

                //Small amount of time, for testing purposes
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));

                _cache.Set(cacheKey, persons, cacheOptions);
            }
            
            return persons;
        }
        [HttpGet("GetpersonsFromSP")]
        public IEnumerable<Person> GetpersonsFromSP()
        {
            //Example of calling a Stored Procedure
            var persons = _personRepository.GetAllFromSP("GetPersons");
            return persons;
        }

        [HttpGet("{id}")]
         public  ActionResult<Person> Getperson(int id)
         {
             var person = _personRepository.GetById(id);
             if (person == null)
             {
                 return NotFound(new { message = $"Person id={id} not found" });
             }
             return person;
         }

        [HttpPost]
        public IActionResult Save([FromBody]Person person)
        {
            if (person == null)
                return NotFound();

            _personRepository.Save(person);

            return Ok();
        }

        [HttpPost("SaveBulk")]
        public IActionResult SaveBulk([FromBody] List<Person> persons)
        {
            //Bulk Save implementation
            _personRepository.BulkSave(persons);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Person person)
        {
            if (person == null)
                return NotFound();

            _personRepository.Update(person);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            _personRepository.Delete(id);

            return Ok();
        }
    }
}