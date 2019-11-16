using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IssueTrackingSystemApi.CommonTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IssueTrackingSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
  
        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        [Route("signin")]
        public ActionResult<string> SignIn(string username)
        {
            //TODO: 要放到Config中
            var issuer = "JwtAuthDemo";
            var signKey = "1234567890123456";
            var expires = 30; //min
            var login = new LoginViewModel() { Username = username};
            if (ValidateUser(login))
            {
                return JwtHelpers.GenerateToken(issuer, signKey, login.Username, expires);
            }
            else
            {
                return BadRequest();
            }
        }

        private bool ValidateUser(LoginViewModel login)
        {
            return login.Username == "chris_fs_wang"; // TODO
        }

        [Authorize]
        [HttpGet]
        [Route("claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize]
        [HttpGet("username")]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
