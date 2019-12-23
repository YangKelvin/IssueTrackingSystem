using IssueTrackingSystemApi.Dao;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Tests
{
    public class IssueTest
    {
        private readonly IIssueDao _issueDao;
        private readonly IUserDao _userDao;
        private IIssueService IssueService { get => new IssueService(_issueDao, _userDao); }
        [Test]
        public void CreateIssueTest()
        {
        }

    }
}
