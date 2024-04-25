using API.Cache.Interfaces;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using static Bogus.DataSets.Name.Gender;

namespace API.Cache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        

        private readonly ILogger<CacheController> _logger;
        private readonly ICacheService _cacheService;

        public CacheController(ILogger<CacheController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        [HttpGet(Name = "Get")]
        public ActionResult Get()
        {
            return Ok(GetCustomers());
        }

        private List<Customers> GetCustomers()
        { 
            object? obj = _cacheService.GetCacheByKey("CUSTOMERSLIST");
            if (obj != null)
            { 
                return (List<Customers>)obj;
            }
            var list = new List<Customers>();
            var faker = new Faker();

            for (var x = 1; x <= 1000; x++)
            {
                list.Add(new Customers() { Id = x, Name = faker.Name.FirstName(x%2==0 ? Female: Male) });
            }

            _cacheService.AddCache("CUSTOMERSLIST", list);
            return list;
        }


    }
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
