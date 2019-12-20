using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public class ProjectService : IProjectService
    {
        public ProjectService()
        {

        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int CreateProject(Project project)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project GetProjectById(int id)
        {
            //TODO: EndPoint testing
            return new Project()
            {
                Id = 1,
                Name = "project",
                Owner = new User()
                {
                    Id = 4723,
                    Name = "chris"
                }
            };
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        public List<Project> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a specific project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
