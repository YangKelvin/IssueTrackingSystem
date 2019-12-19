﻿using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public interface IProjectService
    {
        List<Project> Projects();

        int CreateProject(Project project);

        Project GetProjectById(int id);

        int UpdateProject(int id);
    }
}
