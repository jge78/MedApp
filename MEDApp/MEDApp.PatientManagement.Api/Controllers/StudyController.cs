using MEDApp.PatientManagement.Api.Messaging;
using MEDApp.PatientManagement.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MEDApp.PatientManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {

        private readonly IMessagingService _messagingService;
        public StudyController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpPost]
        public Study Post([FromBody] Study study)
        {
            return _messagingService.Add<Study>(study);
        }

        [HttpGet]
        public Study Get(int id)
        {
            return _messagingService.Get<Study>(id);
        }



    }
}
