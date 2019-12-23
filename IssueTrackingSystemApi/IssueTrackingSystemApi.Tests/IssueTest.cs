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
            var testIssues1 = IssueService.CreateIssue(new Issue()
            {
                
                Number = "Test-1",
                Summary = "test1",
                Description = "test1",
                CreateTime = System.DateTime.Now,
                CreateUser = new User() { Id = 3 }
            });

            var Test_2Id = IssueService.CreateIssue(new Issue()
            {
                Number = "Test_2",
                CreateTime = System.DateTime.Now,
                CreateUser = new User() { Id = 10 }
            });

            int effCount = 0;

            effCount = IssueService.UpdateIssue(new Issue()
            {
                Id = Test_2Id,
                Number = "Test_2(M)",
                ModifyTime = System.DateTime.Now,
                ModifyUser = new User() { Id = 13 }
            });

            var issuses = IssueService.GetAllIssues();


            Assert.Pass();
        }

    }
}
