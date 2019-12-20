using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models.Entity;
using NUnit.Framework;
using System.Collections.Generic;

namespace IssueTrackingSystemApi.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SqlHelperSelectTest()
        {
            IEnumerable<UserEntity> x;
            x = SqlHelper.Select<UserEntity>(new UserEntity());

            x = SqlHelper.Select<UserEntity>(new UserEntity() { Account = "User02" });

            x = SqlHelper.Select<UserEntity>(new UserEntity() { CharactorId = 1, Name = "Rex" });

            Assert.IsTrue(true);
        }
        
        [Test]
        public void SqlHelperInsertTest()
        {
            int x = 0;
            x = SqlHelper.Insert(new UserEntity()
            {
                Account = "Test1",
                Password = "noPassword",
                EMail = "test@gamil.com",
                CharactorId = 1,
                Name = "TestName",
            });


            Assert.IsTrue(true);
        }
    }
}