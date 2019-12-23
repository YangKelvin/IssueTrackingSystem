using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTrackingSystemApi.CommonTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IssueTrackingSystemApi.Models.View;
using IssueTrackingSystemApi.Services;
using System.Net;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Controllers
{
    /// <summary>
    /// User endpoint
    /// </summary>
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _config = configuration;
            _userService = userService;
        }

        /// <summary>
        /// token validation
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("token")]
        public IActionResult Token([FromBody] LoginInfo loginInfo)
        {
            //TODO: 要放到Config中
            var issuer = _config["Payload:Claims:Issuer"];
            var signKey = _config["Payload:Claims:SignKey"];
            var expires = 30; //min
            int? userId = _userService.ValidateUser(loginInfo);
            if (userId != null)
            {
                string token = JwtHelpers.GenerateToken(issuer, signKey, loginInfo.account, expires);
                Token viewmodel = new Token()
                {
                    token = token,
                    userId = userId
                };
                return Ok(viewmodel);
            }
            else
            {
                return BadRequest("wrong account or password");
            }
        }

        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user = _userService.GetUserById(id);

            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Didn't find any user");
            }
            else
            {
                return Ok(user);
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = _userService.GetAllUsers();

            if (users == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Didn't find any user");
            }
            else
            {
                return Ok(users);
            }
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            int affectedRows = _userService.CreateUser(user);

            if (affectedRows == 0)
            {
                return BadRequest("Invalid input, object invalid");
            }
            else if (affectedRows == -1)
            {
                return Conflict("An existing item already exist");
            }
            else
            {
                return Created($"api/[controller]", affectedRows);
            }
        }

        /// <summary>
        /// Update a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            user.Id = id;

            int affectedRows = _userService.UpdateUser(user);
            if (affectedRows == 0)
            {
                return BadRequest("Invalid input, object invalid");
            }
            else
            {
                return Ok(affectedRows);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int affectedRows = _userService.DeleteUser(id);
            if (affectedRows == 0)
            {
                return BadRequest("Invalid input, object invalid");
            }
            else
            {
                return Ok(affectedRows);
            }
        }
    }
}