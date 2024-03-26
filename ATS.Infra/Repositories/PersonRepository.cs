using System.Collections.Generic;
using System.Linq;
using ATS.Domain.Models;
using ATS.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ATS.Infra.Repositories
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(AppDbContext context) : base(context)
        {}

        public override Person GetById(int id)
        {
            var query = _context.Set<Person>().Where(e => e.Id == id);

            if(query.Any())
                return query.First();

            return null;
        }

        public override IEnumerable<Person> GetAll()
        {
            var query = _context.Set<Person>();

            return query.Any() ? query.ToList() : new List<Person>();
        }


    }
}