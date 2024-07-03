using MEDApp.Appointments.Api.Messaging;
using MEDApp.Appointments.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEDApp.Appointments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMessagingService _messagingService;

        public AppointmentController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        // POST ADD api/<AppointmentController>
        [HttpPost]
        public Appointment Post([FromBody] Appointment appointment)
        {
            return _messagingService.Add<Appointment>(appointment);
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public Appointment Delete(int id)
        {
            return _messagingService.Delete<Appointment>(id);
        }

        // GET: api/<AppointmentController>
        [HttpGet]
        public List<Appointment> Get()
        {
            return _messagingService.GetAll<Appointment>();
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public Appointment Get(int id)
        {
            return _messagingService.Get<Appointment>(id);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut]
        public Appointment Put([FromBody] Appointment appointment)
        {
            return _messagingService.Update<Appointment>(appointment);
        }

    }
}
