using Microsoft.EntityFrameworkCore;
using RestNew.Models;

namespace RestNew.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> batteries { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Building> buildings { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Address> addresses { get; set; }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Building_Detail> building_details { get; set; }
        public DbSet<Intervention> interventions { get; set; }
        public DbSet<RestNew.Models.FactIntervention> FactIntervention { get; set; }

    }
    public class PostgreApplicationContext : DbContext
    {
        public PostgreApplicationContext(DbContextOptions<PostgreApplicationContext> options)
            : base(options)
        { }
        public DbSet<FactIntervention> fact_interventions { get; set; }

    }
}