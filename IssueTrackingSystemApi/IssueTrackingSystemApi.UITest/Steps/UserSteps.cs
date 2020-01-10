using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SpecFlowStudy.UiTests.CommonTools;
using System;
using System.Threading;
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
        public void Then我輸入帳號(string account)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("account")));
            element.SendKeys(account);
        }

        [Then(@"我輸入密碼 (.*)")]
        public void Then我輸入密碼(string password)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            element.SendKeys(password);
        }

        [Then(@"我按下登入按鈕")]
        public void Then我按下登入按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit']")));
            element.Click();
        }

        [Then(@"我按下個人資料的頭像")]
        public void Then我按下個人資料的頭像()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".fa-user > path")));
            element.Click();
        }

        [Then(@"我按下Profile按鈕")]
        public void Then我按下Profile按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Profile")));
            element.Click();
        }

        [Then(@"個人資料應該為")]
        public void Then個人資料應該為(Table table)
        {
            var name = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-name")));
            var account = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-account")));
            var email = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-eMail")));
            var authority = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-charactorId")));
            Assert.AreEqual(table.Rows[0]["名稱"], name.GetAttribute("value"));
            Assert.AreEqual(table.Rows[0]["帳號"], account.GetAttribute("value"));
            Assert.AreEqual(table.Rows[0]["信箱"], email.GetAttribute("value"));
            Assert.AreEqual(table.Rows[0]["權限"], authority.GetAttribute("value"));
        }

        [Then(@"我修改個人資料並按下修改按鈕")]
        public void Then我修改個人資料並按下修改按鈕(Table table)
        {
            var name = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-name")));
            var account = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-account")));
            var email = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-eMail")));
            name.Clear();
            name.SendKeys(table.Rows[0]["名稱"]);
            account.Clear();
            account.SendKeys(table.Rows[0]["帳號"]);
            email.Clear();
            email.SendKeys(table.Rows[0]["信箱"]);
            var button = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@type='button'])[3]")));
            button.Click();
        }

        [Then(@"應出現 (.*) 提示訊息")]
        public void Then應出現修改成功提示訊息(string message)
        {
            Thread.Sleep(1000);
            var alert = _webDriver.Current.SwitchTo().Alert();
            string textOnAlert = alert.Text;
            Assert.AreEqual(message, alert.Text);
            alert.Accept();
        }



        [Then(@"首頁應顯示名稱 (.*)")]
        public void Then首頁應顯示名稱(string name)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Kelvin")));
            Assert.AreEqual(name, element.Text);
        }


        [AfterScenario]
        public void CloseBrowser()
        {
            _webDriver.Quit();
        }
    }
}
