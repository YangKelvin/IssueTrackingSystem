﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IssueTrackingSystemApi.Controllers
{
    [Route("api/NMS")]
    [ApiController]
    public class NotificationMessageSubsystemController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IUserService _userService;

        public NotificationMessageSubsystemController(IConfiguration configuration, IUserService userService)
        {
            _config = configuration;
            _userService = userService;
        }

        [HttpPost]
        [Route("LineBotUserLogin")]
        public IActionResult LineBotUserLogin([FromBody] User user)
        {
            if(string.IsNullOrEmpty(user.Account) ||
               string.IsNullOrEmpty(user.Password)||
               string.IsNullOrEmpty(user.LineId)) // 檢核所需資料
            {
                return BadRequest();
            }

            var effNum = _userService.UpdateUserByAccount(new User()
            {
                Account = user.Account,
                Password = user.Password,
                LineId = user.LineId
            });
            if(effNum == 1)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}