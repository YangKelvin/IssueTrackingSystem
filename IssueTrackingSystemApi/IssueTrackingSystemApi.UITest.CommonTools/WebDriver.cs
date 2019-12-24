using System;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowStudy.UiTests.CommonTools
{
    public class WebDriver
    {
        private IWebDriver _currentWebDriver;
        private WebDriverWait _wait;

        private readonly IConfiguration _config;

        public string _seleniumBaseUrl;

        public string _browserPlatform;

        private string _browserConfig;

        public WebDriver()
        {
            var settingPath = Path.GetFullPath(Path.Combine(@"../../../appsettings.json")); // get absolute path
            _config = new ConfigurationBuilder()
                .AddJsonFile(settingPath)
                .Build();
            _seleniumBaseUrl = _config["WebDriver:seleniumBaseUrl"];
            _browserPlatform = _config["WebDriver:platform"];
            _browserConfig = _config["WebDriver:browser"];
        }

        public IWebDriver Current
        {
            get
            {
                if (_currentWebDriver != null)
                    return _currentWebDriver;

                _currentWebDriver = GetWebDriver();
                _currentWebDriver.Url = _seleniumBaseUrl;

                return _currentWebDriver;
            }
        }

        public WebDriverWait Wait
        {
            get
            {
                if (_wait == null)
                {
                    this._wait = new WebDriverWait(Current, TimeSpan.FromSeconds(10));
                }
                return _wait;
            }
        }

        private IWebDriver GetWebDriver()
        {
            switch (_browserConfig)
            {
                case "IE":
                    return new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory) { Url = _seleniumBaseUrl };
                case "Chrome":
                    return new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory) { Url = _seleniumBaseUrl };
                case "Firefox":
                    return new FirefoxDriver(AppDomain.CurrentDomain.BaseDirectory) { Url = _seleniumBaseUrl };
                default:
                    throw new NotSupportedException($"{_browserConfig} is not a supported browser");
            }
        }

        public void Quit()
        {
            _currentWebDriver?.Quit();
        }
    }
}
