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
        private IProjectService ProjectService { get => new ProjectService(); }
        private IProjectDao ProjectDao { get => new ProjectDao(); }
        private IUserService UserService { get => new UserService(); }

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

        [Test]
        public void CreateProjectTest()
        {
            var testUser = UserService.GetUserByAccount("acc2");
            Project createProject = new Project()
            {
                Manager = testUser,
                Name = "testProject1234"
            };

            //ProjectService.CreateProject(createProject, testUser);
        }
    }
}
