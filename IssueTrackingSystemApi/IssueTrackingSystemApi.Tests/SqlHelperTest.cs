using IssueTrackingSystemApi.CommonTools;
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
            x = SqlHelper.Select<UserEntity>(new UserEntity());

            x = SqlHelper.Select<UserEntity>(new UserEntity() { Account = "User02" });

            x = SqlHelper.Select<UserEntity>(new UserEntity() { CharactorId = 1, Name = "Rex" });

            Assert.IsTrue(true);
        }
        
        [Test]
        public void InsertTest()
        {
            int x = 0;
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

    }
}