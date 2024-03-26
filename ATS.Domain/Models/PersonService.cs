using ATS.Domain.Interfaces;

namespace ATS.Domain.Models
{
    public class PersonService
    {
        private readonly IRepository<Person> _atsRepository;

        public PersonService(IRepository<Person> atsRepository)
        {
            _atsRepository = atsRepository;
        }

        public void Create(int id, string name, string email, string address, string phone, string urllinkedin, string profession)
        {

        }

    }
}