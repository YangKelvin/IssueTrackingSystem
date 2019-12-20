using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();

        int CreateProject(Project project);

        Project GetProjectById(int id);

        int UpdateProject(Project project);
    }
}
