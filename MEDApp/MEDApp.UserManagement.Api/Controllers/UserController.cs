using MEDApp.UserManagement.Api.Messaging;
using MEDApp.UserManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

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
        public User Post([FromBody] User user)
        {
            return _messagingService.Add<User>(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public User Delete(int id)
        {
            return _messagingService.Delete<User>(id);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _messagingService.Get<User>(id);
        }

        // GET: api/<UserController>
        [HttpGet]
        public List<User> Get()
        {
            return _messagingService.GetAll<User>();
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public User Put([FromBody] User user)
        {
            //user.Id= id;
            return _messagingService.Update<User>(user);
        }

    }
}
