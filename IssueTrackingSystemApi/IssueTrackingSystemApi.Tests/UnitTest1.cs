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
        public void Test1()
        {
            IEnumerable<UserEntity> x;
            x = SqlHelper.Query<UserEntity>(new UserEntity());

            x = SqlHelper.Query<UserEntity>(new UserEntity() { _Account = "User02" });

            x = SqlHelper.Query<UserEntity>(new UserEntity() { CharactorId = 1, Name = "Rex" });

            Assert.Pass();
        }
    }
}