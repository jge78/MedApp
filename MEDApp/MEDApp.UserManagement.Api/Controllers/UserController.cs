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

        // ADD POST api/<UserController> 
        [HttpPost]
        public string Post([FromBody] User user)
        {
            return _messagingService.AddUser<User>(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _messagingService.DeleteUser(id);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _messagingService.GetUser(id);
        }

        // GET: api/<UserController>
        [HttpGet]
        public List<User> Get()
        {
            return _messagingService.GetAllUsers();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User user)
        {
            user.Id= id;    
            return _messagingService.UpdateUser(user);
        }

    }
}
