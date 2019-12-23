using IssueTrackingSystemApi.Dao;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.View;
using IssueTrackingSystemApi.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Tests
{
    public class UserTest
    {
        private readonly IUserDao _userDao;
        private IUserService userService = new UserService(new UserDao());
        [Test]
        public void GetUserByIdTest()
        {
            var user = userService.GetUserById(8);

            Assert.AreEqual(8, user.Id);
            Assert.AreEqual("acc2", user.Account);
            Assert.AreEqual(2, user.CharactorId);
        }

        [Test]
        public void GetUserByIAccountest()
        {
            var user = userService.GetUserByAccount("acc2");

            Assert.AreEqual(8, user.Id);
            Assert.AreEqual("acc2", user.Account);
            Assert.AreEqual(2, user.CharactorId);
        }

        [Test]
        public void CreateUserTest()
        {
            User addUserData = new User()
            {
                Account = "add1",
                CharactorId = 1,
                EMail = "add email",
                LineId = "add line",
                Name = "add name",
                Password = "add1"
            };
            userService.CreateUser(addUserData);

            var newUser = userService.GetUserByAccount("add1");
            Assert.AreEqual("add1", newUser.Account);
            Assert.AreEqual(1, newUser.CharactorId);
        }

        [Test]
        public void UpdateUserTest()
        {
            var updateUser = userService.GetUserByAccount("add1");
            updateUser.Account = "update1";
            userService.UpdateUser(updateUser);

            var result = userService.GetUserByAccount("update1");
            Assert.AreEqual("update1", result.Account);
            result.Account = "add1";
            userService.UpdateUser(result);
            
            var result2 = userService.GetUserByAccount("add1");
            Assert.AreEqual("add1", result2.Account);
        }

        [Test]
        public void ValidateUserTest()
        {
            LoginInfo loginSuccess = new LoginInfo()
            {
                account = "add1",
                password = "add1"
            };
            Assert.IsTrue(userService.ValidateUser(loginSuccess));

            LoginInfo loginFail = new LoginInfo()
            {
                account = "fail",
                password = "fail"
            };
            Assert.IsFalse(userService.ValidateUser(loginFail));
        }

        [Test]
        public void GetAllUserTest()
        {
            var allUsers = userService.GetAllUsers();
            Assert.IsTrue(true);
        }
    }
}
