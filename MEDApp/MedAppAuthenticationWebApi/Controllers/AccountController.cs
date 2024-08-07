﻿using MedAppJwtAuthenticationManager;
using MedAppJwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAppAuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationresponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if (authenticationresponse == null)
                return Unauthorized();

            return authenticationresponse;
        }
    }
}
