using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Person.Models;

namespace Person.Controllers.Api
{
    public class PersonsController : ApiController
    {
        private ApplicationDbContext _context;

        public PersonsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        
            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("api/person")]
        public List<Models.Person> GetPeople()
        {
            return _context.People.ToList();

        }

        [HttpGet]
        [Route("api/person/{id}")]
        public Models.Person GetPerson(int id)
        {
            var person = _context.People.Single(x => x.Id == id);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }

            return person;
        }

        [HttpPost]
        [Route("api/person")]
        public Models.Person CreatePerson(Models.Person person)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }

            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }


        [HttpPut]
        [Route("api/person/{id}")]
        public void UpdateCustomer(int id, Models.Person person)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var personInDb = _context.People.SingleOrDefault(p => p.Id == id);
            if (personInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            personInDb.Name = person.Name;
            _context.SaveChanges();
        }


        [HttpDelete]
        [Route("api/person/{id}")]
        public void DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var personInDb = _context.People.SingleOrDefault(p => p.Id == id);
            if (personInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.People.Remove(personInDb);
            _context.SaveChanges();
        }
    }
}
