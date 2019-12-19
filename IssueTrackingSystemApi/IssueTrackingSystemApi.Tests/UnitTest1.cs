using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models.Entity;
using NUnit.Framework;

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
            string x = SqlHelper.Test<UserEntity>();
            Assert.Pass();
        }
    }
}