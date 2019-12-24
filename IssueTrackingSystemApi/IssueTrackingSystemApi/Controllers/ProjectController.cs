using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.View;
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
        public IActionResult Get(int id)
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
        public IActionResult Create([FromBody] ProjectFront project)
        {
            int affectedRows = _projectService.CreateProject(project);
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

        /// <summary>
        /// Update a specific project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] ProjectFront project)
        {
            int affectedRows = _projectService.UpdateProject(id, project);
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
            int affectedRows = _projectService.DeleteProject(id);
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