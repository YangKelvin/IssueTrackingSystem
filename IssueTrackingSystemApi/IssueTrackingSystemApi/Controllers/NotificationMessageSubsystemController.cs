using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTrackingSystemApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IssueTrackingSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationMessageSubsystemController : ControllerBase
    {
        private readonly IConfiguration _config;

        public NotificationMessageSubsystemController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost]
        [Route("CheckUserLoginInfo")]
        public IActionResult CheckUserLoginInfo([FromBody] User user)
        {
            if(string.IsNullOrEmpty(user.Account) ||
               string.IsNullOrEmpty(user.Password)||
               string.IsNullOrEmpty(user.LineId)) // 檢核所需資料
            {
                return BadRequest();
            }



            return BadRequest();
        }

    }
}