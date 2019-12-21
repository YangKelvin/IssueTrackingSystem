using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models.Entity;
using IssueTrackingSystemApi.Services;
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
            //string x = SqlHelper.Test<UserEntity>();
            Assert.Pass();
        }

        [Test]
        public static void SendAutomatedEmail()
        {
            NotificationMessageSubsystem NMS = new NotificationMessageSubsystem();

            NMS.SendMail("<h1>¨«°Ú¡I¸±­U</h1>", new string[] { "f98989000@gmail.com" }, new string[] { "t105590017@ntut.org.tw" });

        }
    }
}