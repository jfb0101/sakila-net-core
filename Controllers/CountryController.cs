using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakila.DB;
using Sakila.Models;

namespace Sakila.Controllers {
    [ApiController]
    [Route("/api/Countries")]
    public class CountryController : BaseCRUDController<Country,int> {
        public CountryController(SakilaDBContext dbContext) : base(dbContext) {}
    }
}