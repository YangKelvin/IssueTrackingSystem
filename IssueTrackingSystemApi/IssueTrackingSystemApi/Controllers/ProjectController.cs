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
    /// <summary>
    /// Project endpoint
    /// </summary>
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<Project> projects = _projectService.GetAllProjects();

            if (projects == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Didn't find any project");
            }
            else
            {
                return Ok(projects);
            }
        }

        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] int id)
        {
            Project project = _projectService.GetProjectById(id);

            if (project == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Didn't find any project");
            }
            else
            {
                return Ok(project);
            }
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Project project)
        {
            int affectedRows = _projectService.CreateProject(project, new User()); // 虛傳入 User
            if (affectedRows == 0)
            {
                return BadRequest("Invalid input, object invalid");
            }
            else if (affectedRows == 1)
            {
                return Ok("Create project success");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Create project failed");
            }
        }

        /// <summary>
        /// Update a specific project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{id}")]
        public IActionResult Update([FromQuery] int id, [FromBody] Project project)
        {
            int affectedRows = _projectService.UpdateProject(project);
            if (affectedRows == 1)
            {
                return Ok("Update success");
            }
            else if (affectedRows == 0)
            {
                return BadRequest("Invalid input, object invalid");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}