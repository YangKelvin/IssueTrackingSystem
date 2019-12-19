using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public interface IIssueService
    {
        List<Issue> Issues();

        Issue GetIssue(int id);

        int UpdateIssue(Issue issue);

        int CreateIssue(Issue issue);
    }
}
