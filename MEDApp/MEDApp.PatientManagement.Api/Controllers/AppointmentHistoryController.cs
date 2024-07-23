using MEDApp.PatientManagement.Api.Data.Models;
using MEDApp.PatientManagement.Api.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace MEDApp.PatientManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentHistoryController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        public AppointmentHistoryController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpPost]
        public AppointmentHistory Post([FromBody] AppointmentHistory appointmentHistory)
        {
            return _messagingService.Add<AppointmentHistory>(appointmentHistory);
        }

        [HttpGet]
        public AppointmentHistory Get(int id)
        {
            return _messagingService.Get<AppointmentHistory>(id);
        }

    }
}
