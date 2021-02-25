using Microsoft.AspNetCore.Mvc;
using Sakila.DB;
using Sakila.Models;

namespace Sakila.Controllers {

    [ApiController]
    [Route("/api/Films")]
    public class FilmController : BaseCRUDController<Film,int> {
        public FilmController(SakilaDBContext dbContext) : base(dbContext) {}

        
    }
}