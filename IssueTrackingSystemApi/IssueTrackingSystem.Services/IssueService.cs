using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public class IssueService : IIssueService
    {
        public IssueService()
        {
        }

        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public int CreateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a specific issue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Issue GetIssueById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all issues
        /// </summary>
        /// <returns></returns>
        public List<Issue> GetAllIssues()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public int UpdateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }
    }
}
