using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models;
using NUnit.Framework;
using IssueTrackingSystemApi.Services;

namespace IssueTrackingSystemApi.Tests
{
    public class ServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IssueTest()
        {
            IIssueService issueService = new IssueService();

            var Test_1Id = issueService.CreateIssue(new Issue()
            {
                Number = "Test_1",
                CreateTime = System.DateTime.Now,
                CreateUser = new User() { Id = 3 }
            });

            var Test_2Id = issueService.CreateIssue(new Issue()
            {
                Number = "Test_2",
                CreateTime = System.DateTime.Now,
                CreateUser = new User() { Id = 10 }
            });

            int effCount = 0;

            effCount = issueService.UpdateIssue(new Issue()
            {
                Id = Test_2Id,
                Number = "Test_2(M)",
                ModifyTime = System.DateTime.Now,
                ModifyUser = new User() { Id = 13 }
            });

            var issuses = issueService.GetAllIssues();


            Assert.Pass();
        }
    }
}