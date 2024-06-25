using MEDApp.UserManagement.Api.Messaging;
using MEDApp.UserManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEDApp.UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        public UserController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {

            User user = new User
            {
                Id = 1,
                FirstName = "TestGet",
                LastName = "LAtNameTestGet",
                DateOfBirth = new DateTime(1978, 5, 18),
                PhoneNumber = "999-55889966",
                Email = "mail@gmail.com"
            };

            return user;
            //return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public string Post([FromBody] User user)
        {

            //Obtener el User y serializarlo
            //Enviar el mensaje al queue (rabbitMQ /kafka)
            //_messagingService.SendMessage<User>(user);
            return _messagingService.SendMessage<User>(user);

        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
