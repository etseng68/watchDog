using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchDogWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WatchDogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private readonly MixWebContext _mixWebContext ;

        public DomainController(MixWebContext mixWebContext)
        {
            _mixWebContext = mixWebContext;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<object> Get()
        {
         
            return _mixWebContext.Mdomains
                    .Where(d => d.Del == false)
                    .Where(d => d.Wrun == true)
                    .Select(d => new { d.Id, d.Wurl, d.Org,d.OrgNavigation.Tgbot })
                    .ToList();
        }

        // GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
