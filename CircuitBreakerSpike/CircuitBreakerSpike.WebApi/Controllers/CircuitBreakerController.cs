using System.Collections.Generic;
using System.Web.Http;

namespace CircuitBreakerSpike.WebApi.Controllers
{
    public class CircuitBreakerController : ApiController
    {
        // GET api/circuitbreaker
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/circuitbreaker/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/circuitbreaker
        public void Post([FromBody] string value)
        {
        }

        // PUT api/circuitbreaker/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/circuitbreaker/5
        public void Delete(int id)
        {
        }
    }
}