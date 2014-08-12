using System.Collections.Generic;
using System.Web.Http;
using CircuitBreakerSpike.Services;

namespace CircuitBreakerSpike.WebApi.Controllers
{
    public class CircuitBreakersController : ApiController
    {
        private readonly ICircuitBreakerService _circuitBreakerService;

        public CircuitBreakersController(ICircuitBreakerService circuitBreakerService)
        {
            _circuitBreakerService = circuitBreakerService;
        }

        // GET api/CircuitBreakers
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/CircuitBreakers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/CircuitBreakers
        public void Post([FromBody] PostParameters parameters)
        {
            if (parameters.OrderManagementCircuitBreakerEnabled)
            {
                _circuitBreakerService.Enable(CircuitBreakerService.ORDER_MANAGEMENT_CIRCUIT_BREAKER);
            }
            else
            {
                _circuitBreakerService.Disable(CircuitBreakerService.ORDER_MANAGEMENT_CIRCUIT_BREAKER);
            }
        }

        // PUT api/CircuitBreakers/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/CircuitBreakers/5
        public void Delete(int id)
        {
        }

        public class PostParameters
        {
            public bool OrderManagementCircuitBreakerEnabled { get; set; }
        }
    }
}