using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;

namespace TIWO_RJakubiec
{
    public enum BrowserType
    {
        Firefox,
        Chrome
    }

    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]
    public class SeleniumTests 
    {
        [ThreadStatic]
        public static IWebDriver Instance;

        [ThreadStatic]
        public static WebDriverWait Wait;

        public BrowserType BrowserType;

        public SeleniumTests(BrowserType browser)
        {
            BrowserType = browser;
        }

        [SetUp]
        public void Setup()
        {
            var _browserConfiguration = new BrowserConfiguration();
            switch (BrowserType)
            {
                case BrowserType.Firefox:
                    Instance = new FirefoxDriver(_browserConfiguration.UseBrowserConfigurationForFirefox());
                    break;
                case BrowserType.Chrome:
                    Instance = new ChromeDriver(_browserConfiguration.UseBrowserConfigurationForChrome());
                    break;
            }
            Wait = new WebDriverWait(Instance, TimeSpan.FromSeconds(60));
        }

        [TearDown]
        public void Cleanup()
        {
            Instance.Manage().Cookies.DeleteAllCookies();
            Instance.Close();
            Instance.Quit();
        }

        [Test]
        public void Is_google_exist()
        {
            Instance.Navigate().GoToUrl("https://www.google.pl");
            Assert.AreEqual("https://www.google.pl/", Instance.Url);
        }

        [Test]
        public void Wikipedia_serach_test()
        {
            var searchedPhrase = "Testowanie oprogramowania";
            Instance.Navigate().GoToUrl("https://www.google.pl");
            Instance.FindElement(By.Id("lst-ib")).SendKeys("Wikipedia");
            Instance.FindElement(By.Name("btnK")).SendKeys(Keys.Enter);
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id='rso']/div/div/div[1]/div/div/h3/a")));
            Instance.FindElement(By.XPath(@"//*[@id='rso']/div/div/div[1]/div/div/h3/a")).Click();
            Assert.AreEqual("https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna", Instance.Url);
            Instance.FindElement(By.Name("search")).SendKeys(searchedPhrase);
            Instance.FindElement(By.Id("searchButton")).Click();
            StringAssert.Contains(searchedPhrase, Instance.FindElement(By.Id("firstHeading")).Text);
        }
    }
}
