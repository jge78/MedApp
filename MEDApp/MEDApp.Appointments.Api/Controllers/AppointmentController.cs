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
        public string Post([FromBody] Appointment appointment)
        {
            return _messagingService.AddAppointment<Appointment>(appointment);
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _messagingService.DeleteAppointment(id);
        }

        // GET: api/<AppointmentController>
        [HttpGet]
        public List<Appointment> Get()
        {
            //return new string[] { "value1", "value2" };
            return _messagingService.GetAllAppointment();

        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public Appointment Get(int id)
        {
            return _messagingService.GetAppointment(id);
        }


        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public Appointment Put(int id, [FromBody] Appointment appointment)
        {
            return _messagingService.UpdateAppointment<Appointment>(appointment);
        }

    }
}
