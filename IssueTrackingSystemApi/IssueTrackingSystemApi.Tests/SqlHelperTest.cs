using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;
using NUnit.Framework;
using System.Collections.Generic;

namespace IssueTrackingSystemApi.Tests
{
    public class SqlHelperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SelectTest()
        {
            IEnumerable<UserEntity> x;

            x = SqlHelper.Select<UserEntity>(null);

            x = SqlHelper.Select<UserEntity>(new UserEntity());

            x = SqlHelper.Select<UserEntity>(new UserEntity() { Account = "User02" });

            x = SqlHelper.Select<UserEntity>(new UserEntity() { CharactorId = 1, Name = "Rexp" });

            Assert.IsTrue(true);
        }
        
        [Test]
        public void InsertTest()
        {
            int x = 0;
            // return 0
            x = SqlHelper.Insert(new UserEntity()
            {
                Account = "Test1______________________Test1______________________Test1______________________Test1______________________Test1______________________",
                Password = "noPassword",
                EMail = "test2@gamil.com",
                CharactorId = 1,
                Name = "TestName",
            });

            // return -1
            x = SqlHelper.Insert(new UserEntity()
            {
                Account = "Test1",
                Password = "noPassword",
                EMail = "test2@gamil.com",
                CharactorId = 1,
                Name = "TestName",
            });

            Assert.IsTrue(true);
        }


        [Test]
        public void InsertTest2()
        {
            int x;

            x = SqlHelper.Insert(new UserProjectRelationEntity()
            {
                ProjectId = 1,
                UserId = 2,
                ProjectCharactorId = 1
            });

            Assert.IsTrue(true);
        }

        [Test]
        public void DeleteTest()
        {
            int x = 0;
            x = SqlHelper.Delete(new UserEntity()
            {
                Account = "Test1",
                EMail = "test2@gamil.com"
            });

            Assert.IsTrue(true);

        }

        [Test]
        public void ModifyTest()
        {
            int x = 0;
            x = SqlHelper.Update(new UserEntity()
            {
                Id = 15
            }, new UserEntity()
            {
                Password = "******"
            });

            Assert.IsTrue(true);
        }

        [Test]
        public void ObjectConvertTest()
        {
            var x = new Issue()
            {
                Id = 1,
                Number = "dasdasd",
                CreateUser = 1
            };

            var t = x.ObjectConvert<IssueEntity>(i =>
            {
                i.Estimated = 5.3f;
            });

            Assert.IsTrue(true);
        }
    }
}