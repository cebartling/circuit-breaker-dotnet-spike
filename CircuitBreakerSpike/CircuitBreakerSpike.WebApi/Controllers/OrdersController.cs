using System.Collections.Generic;
using System.Web.Http;
using CircuitBreakerSpike.Repositories;
using CircuitBreakerSpike.Services;

namespace CircuitBreakerSpike.WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        private IOrderManagementService _orderManagementService;

        public OrdersController(IOrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }

        // GET api/orders
        public IEnumerable<Order> Get()
        {
            return _orderManagementService.GetOrders();
        }

//        // GET api/orders/5
//        public string Get(int id)
//        {
//            return "value";
//        }
//
//        // POST api/orders
//        public void Post([FromBody] string value)
//        {
//        }
//
//        // PUT api/orders/5
//        public void Put(int id, [FromBody] string value)
//        {
//        }
//
//        // DELETE api/orders/5
//        public void Delete(int id)
//        {
//        }
    }
}