using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models;
using NUnit.Framework;
using IssueTrackingSystemApi.Services;
using IssueTrackingSystemApi.Dao;

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
            IIssueService issueService = new IssueService(new IssueDao(), new UserDao());

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

        [Test]
        public void UserTest()
        {
            IUserService userService = new UserService(new UserDao());

            var user1 = userService.CreateUser(new User()
            {
                Account = "acc2",
                Password = "pwd2",
                EMail = "email2",
                Charactor = "2",
                LineId = "line2",
                Name = "name2"
            });

            var user = userService.GetUserByAccount("acc2");
            
            Assert.Pass();
        }

        [Test]
        public void IsUserExistTest()
        {
            IUserService userService = new UserService();

            User userExist = new User()
            {
                Account = "acc2",
                Password = "pwd2"
            };
            Assert.IsTrue(userService.IsUserExist(userExist));

            User userNotExist = new User()
            {
                Account = "notExist",
                Password = "notExist"
            };

            Assert.IsFalse(userService.IsUserExist(userNotExist));
        }
    }
}