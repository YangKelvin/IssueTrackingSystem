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
        private readonly INotificationMessageSubsystem NMS;
        public ProjectController(IProjectService projectService, INotificationMessageSubsystem nms)
        {
            _projectService = projectService;
            NMS = nms;
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
                int?[] userIds = new int?[] { project.managerId };
                userIds = userIds.Concat(project.generalsId).ToArray()
                                 .Concat(project.developersId).ToArray();
                NMS.SendALLmessage($"Create Issue {project.Name}", $"Create Issue {project.Name}", userIds);
                
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
                int?[] userIds = new int?[] { project.managerId };
                userIds = userIds.Concat(project.generalsId).ToArray()
                                 .Concat(project.developersId).ToArray();
                NMS.SendALLmessage($"Update Issue {project.Name}", $"Update Issue {project.Name}", userIds);

                return Ok(affectedRows);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Project project = _projectService.GetProjectById(id);

            int affectedRows = _projectService.DeleteProject(id);
            if (affectedRows == 0)
            {
                return BadRequest("Invalid input, object invalid");
            }
            else
            {
                int?[] userIds = new int?[] { project.Manager.Id };
                userIds = userIds.Concat(project.Generals.Select(i => i.Id)).ToArray()
                                 .Concat(project.Developers.Select(i => i.Id)).ToArray();
                NMS.SendALLmessage($"Create Issue {project.Name}", $"Create Issue {project.Name}", userIds);

                return Ok(affectedRows);
            }
        }
    }
}