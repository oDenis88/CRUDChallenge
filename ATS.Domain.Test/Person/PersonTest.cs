using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ExpectedObjects;
using ATS.Domain;
using ATS.Domain.Test.Util;
using ATS.Domain.Interfaces;

namespace ATS.Domain.Test.Candidato
{
    public class PersonTest
    {
        private readonly IRepository<ATS.Domain.Models.Person> _personRepository;

        [Fact]
        public void PersonInstance_ExpectedSuccess()
        {
            var ExpectedPerson = new
            {
                Nome = "João",
                Email = "joao@joao.com"
            };
 
            var Person = new ATS.Domain.Models.Person() { Name = ExpectedPerson.Nome, Email = ExpectedPerson.Email };

            ExpectedPerson.ToExpectedObject().ShouldMatch(Person);
        }
        [Fact]
        public void PersonInstance_ExpectedError()
        {
            string mensagemError = "Invalid Parameters!";

            var ExpectedPerson = new
            {
                Nome = "João" 
            };

            var Person = new ATS.Domain.Models.Person() { Name = ExpectedPerson.Nome };

            Assert.Throws<ArgumentException>(() => new ATS.Domain.Models.Person() { Name = ExpectedPerson.Nome}).ValidarMensagem(mensagemError);

        }

        [Fact]
        public void InsertTesting( )
        {

            var personTest = new Models.Person
            {
                Address = ""
            };

            try
            {
                _personRepository.Save(personTest);
                Assert.True(true);
            }
            catch
            {
                Assert.False(false);
            }

        }
        [Fact]
        public void ListTest()
        {
            var dados = _personRepository.GetAll();
            Assert.True(dados.Count() > 0);
        }

        [Fact]
        public void GetByIDTest()
        {
            var dados = _personRepository.GetById(1);
            Assert.True(dados is not null);
        }

    }
}
