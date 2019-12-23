using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackingSystemApi.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private IIssueService _IssueService;
        public IssueController(IIssueService IssueService)
        {
            _IssueService = IssueService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<Issue> Issues = _IssueService.GetAllIssues();

            if (Issues == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Didn't find any Issue");
            }
            else
            {
                return Ok(Issues);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Issue Issue = _IssueService.GetIssueById(id);

            if (Issue == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Didn't find any Issue");
            }
            else
            {
                return Ok(Issue);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Issue Issue)
        {
            int affectedRows = _IssueService.CreateIssue(Issue);
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
                return Ok(affectedRows);
            }
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] Issue issue)
        {
            issue.Id = id;
            int affectedRows = _IssueService.UpdateIssue(issue);
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
            int affectedRows = _IssueService.DeleteIssue(id);
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