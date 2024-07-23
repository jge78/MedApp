using MEDApp.PatientManagement.Api.Messaging;
using MEDApp.PatientManagement.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MEDApp.PatientManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationHistoryController : ControllerBase
    {

        private readonly IMessagingService _messagingService;
        public MedicationHistoryController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpPost]
        public MedicationHistory Post([FromBody] MedicationHistory medicationHistory)
        {
            return _messagingService.Add<MedicationHistory>(medicationHistory);
        }

        [HttpGet]
        public MedicationHistory Get(int id)
        {
            return _messagingService.Get<MedicationHistory>(id);
        }

    }
}
