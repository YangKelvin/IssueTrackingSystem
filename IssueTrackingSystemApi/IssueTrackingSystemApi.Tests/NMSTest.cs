using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Services;
using NUnit.Framework;


namespace IssueTrackingSystemApi.Tests
{
    public class NMSTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public static void SendAutomatedEmail()
        {
            //NotificationMessageSubsystem NMS = new NotificationMessageSubsystem();

            //NMS.SendMail("測試標題", "<h1>走啊！萵苣</h1>", new string[] { "f98989000@gmail.com" }, new string[] { "t105590017@ntut.org.tw" });
        }

        [Test]
        public static void PostLineBotTest()
        {
            //NotificationMessageSubsystem NMS = new NotificationMessageSubsystem();

            //var x = NMS.SendLineMessage("測試群發功能", new string[] { 
            //    //"U59ff9c43d3c37fc5ee83b170084fc191", 
            //    "U5ef7af71812b69e7f4f10e6400bff3ac" 
            //});

            var y= 0;
        }
    }
}