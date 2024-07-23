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
        public async Task<IActionResult> Post([FromBody] User user)
        {
            User newUser = await _messagingService.Add<User>(user);

            if (newUser.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(newUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleteResult = await _messagingService.Delete(id);

            if (deleteResult == true)
            {
                return Ok(deleteResult);
            }
            return NotFound();

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await _messagingService.Get<User>(id);

            if (user.Id == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<User> usersList = await _messagingService.GetAll<User>();

            if (usersList.Count == 0)
            { 
                return NoContent();
            }
            return Ok(usersList);
        }

        //// PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            User updatedUser = await _messagingService.Update<User>(user);

            if (user.Id == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }

    }
}
