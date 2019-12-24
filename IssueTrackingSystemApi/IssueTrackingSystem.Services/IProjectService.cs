using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.View;

namespace IssueTrackingSystemApi.Services
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();

        int CreateProject(ProjectFront project);

        Project GetProjectById(int id);

        int UpdateProject(int id, ProjectFront project);

        int DeleteProject(int id);
    }
}
