using Microsoft.EntityFrameworkCore;
using Sakila.Models;

namespace Sakila.DB {
    public class SakilaDBContext : DbContext {
        public SakilaDBContext(DbContextOptions<SakilaDBContext> options) : base(options) {

        }

        public DbSet<Country> Countries {get;set;}

        public DbSet<Film> Films {get;set;}
    }
}