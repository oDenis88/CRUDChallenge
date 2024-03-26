using ATS.Domain.Interfaces;
using ATS.Domain.Models;
using ATS.Infra.Context;
using ATS.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.Application.DI {
    public class Initializer {
        public static void Configure (IServiceCollection services, string conection) 
        {
            services.AddDbContext<AppDbContext> (options => options.UseSqlServer (conection));

            services.AddScoped (typeof (IRepository<Person>), typeof (PersonRepository));
            services.AddScoped (typeof (IRepository<>), typeof (Repository<>));
            services.AddScoped (typeof (PersonService));
            services.AddScoped (typeof (IUnitOfWork), typeof (UnitOfWork));
        }
    }
}