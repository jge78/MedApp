using MEDApp.PatientManagement.Api.Messaging;
using MEDApp.PatientManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MEDApp.PatientManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        public AllergyController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpPost]
        public Allergy Post([FromBody] Allergy allergy)
        {
            return _messagingService.Add<Allergy>(allergy);
        }

        [HttpGet]
        public Allergy Get(int id)
        {
            return _messagingService.Get<Allergy>(id);
        }
    }
}
