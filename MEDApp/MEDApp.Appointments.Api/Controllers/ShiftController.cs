using MEDApp.Appointments.Api.Messaging;
using MEDApp.Appointments.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEDApp.Appointments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        public ShiftController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        // POST api/<ShiftController>   ADD
        [HttpPost]
        public Shift Post([FromBody] Shift shift)
        {
            return _messagingService.Add<Shift>(shift);
        }

        // DELETE api/<ShiftController>/5
        [HttpDelete("{id}")]
        public Shift Delete(int id)
        {
            return _messagingService.Delete<Shift>(id);
        }

        // GET api/<ShiftController>/5
        [HttpGet("{id}")]
        public Shift Get(int id)
        {
            return _messagingService.Get<Shift>(id);
        }

        // GET: api/<ShiftController>
        [HttpGet]
        public IEnumerable<Shift> Get()
        {
            return _messagingService.GetAll<Shift>();
        }

        // PUT api/<ShiftController>/5
        [HttpPut]
        public void Put([FromBody] Shift shift)
        {

        }

    }
}
