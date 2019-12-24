using IssueTrackingSystemApi.Dao;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Tests
{
    public class ProjectTest
    {
        private readonly IUserDao _userDao;
        //private IProjectService ProjectService { get => new ProjectService(); }
        private IProjectDao ProjectDao { get => new ProjectDao(); }

        #region Dao
        [Test]
        public void CreateUserProjectRelationTest()
        {
            ProjectDao.CreateUserProjectRelation(new Models.Entity.UserProjectRelationEntity()
            {
                UserId = 8,
                ProjectId = 3,
                ProjectCharactorId = 1
            });
        }
        #endregion

        //[Test]
        //public void CreateProjectTest()
        //{
        //    IUserService userService = new UserService(new UserDao());
        //    var testUser = userService.GetUserByAccount("acc2");
        //    Project createProject = new Project()
        //    {
        //        Manager = testUser,
        //        Name = "testProject7"
        //    };

        //    ProjectService.CreateProject(createProject, testUser);
        //}
    }
}
