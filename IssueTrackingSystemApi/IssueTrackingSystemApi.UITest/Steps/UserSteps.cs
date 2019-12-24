using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SpecFlowStudy.UiTests.CommonTools;
using System;
using TechTalk.SpecFlow;

namespace IssueTrackingSystemApi.UITest.Steps
{
    [Binding]
    public class UserSteps
    {
        private WebDriver _webDriver;

        [BeforeScenario]
        public void GetWebDriver()
        {
            _webDriver = new WebDriver();
        }

        [Given(@"我前往網頁 (.*)")]
        public void Given我前往網頁(string url)
        {
            IWebDriver webDriver = _webDriver.Current;
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(string.Format("{0}{1}", webDriver.Url, url));
        }
        
        [Then(@"首頁應顯示 (.*)")]
        public void Then首頁應顯示(string title)
        {
            var result = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("account")));
            Assert.AreEqual(title, _webDriver.Current.Title);
        }

        [Then(@"我輸入帳號 (.*)")]
        public void Then我輸入帳號Admin(string account)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("account")));
            element.SendKeys(account);
        }

        [Then(@"我輸入密碼 (.*)")]
        public void Then我輸入密碼Admin(string password)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            element.SendKeys(password);
        }

        [Then(@"首頁應顯示名稱 (.*)")]
        public void Then首頁應顯示名稱(string name)
        {
            Assert.Fail();
        }


        [AfterScenario]
        public void CloseBrowser()
        {
            _webDriver.Quit();
        }
    }
}
