using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace WebDriverTests
{
    public class WebDriverManager
    {
        //Chapter_11_Exr_1

        [Test]

        public void CorrectPagesOpened_When_ClickedOnLinks()

        {

            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            IWebDriver _driver = new ChromeDriver();

            _driver.Navigate().GoToUrl("https://www.selenium.dev/documentation/webdriver/getting_started/");

            IWebElement gridAnchor = _driver.FindElement(By.XPath("//*[@id='m-documentationgrid']"));
            gridAnchor.Click();

            IWebElement componentAnchor = _driver.FindElement(By.XPath("//*[@id='m-documentationgridcomponents']"));
            componentAnchor.Click();

            Assert.AreEqual("Selenium Grid Components | Selenium", _driver.Title);

            IWebElement gitHubAnchor = _driver.FindElement(By.XPath("/html/body/div/footer/div/div/div[2]/ul/li[2]/a"));
            gitHubAnchor.Click();

            var browserTabs = _driver.WindowHandles;
            _driver.SwitchTo().Window(browserTabs[1]);

            Assert.AreEqual("GitHub - SeleniumHQ/selenium: A browser automation framework and ecosystem.", _driver.Title);

            _driver.Close();
            _driver.SwitchTo().Window(browserTabs[0]);

            _driver.Quit();

        }

        //Chapter_11_Exrs_2
        
        
        [Test]

        public void LogginRejected_When_IncorrectEmailAndPasswordAreGiven()

        {
            string email = "test";
            string password = "test";

            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            IWebDriver _driver = new ChromeDriver();

            _driver.Navigate().GoToUrl("https://login.bluehost.com/hosting/webmail");

            
            IWebElement emailAnchor = _driver.FindElement(By.Id("email"));
            IWebElement passwordAnchor = _driver.FindElement(By.Id("password"));
            
            emailAnchor.SendKeys(email);
            passwordAnchor.SendKeys(password + Keys.Enter);
            

            var expectedPageTitle = _driver.Title;
            Assert.AreEqual("Webmail Login - Bluehost", expectedPageTitle);

            var errorMessage = _driver.FindElement(By.XPath("//*[@id='content']/section/form/div/span[1]")).Text;
            Assert.AreEqual("Invalid login attempt. That account doesn't seem to be available.", errorMessage);

            _driver.Quit();

        }
        [Test]
        public void LogginRejected_When_EmptyEmailAndPasswordAreGiven()

        {

            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            IWebDriver _driver = new ChromeDriver();


            var expectedEmailRequiredMessage = "Email is required.";
            var expectedPasswordRequiredMessage = "Password is required.";


            _driver.Navigate().GoToUrl("https://login.bluehost.com/hosting/webmail");

            _driver.FindElement(By.Id("email")).SendKeys(Keys.Enter);

            var expectedPageTitle = _driver.Title;
            Assert.AreEqual("Webmail Login - Bluehost", expectedPageTitle);

            var actualEmailRequiredMessage = _driver.FindElement(By.XPath("//*[@id='content']/section/form/div/span[1]")).Text;
            var actualPasswordRequiredMessage = _driver.FindElement(By.XPath("//*[@id='content']/section/form/div/span[2]")).Text;

            Assert.AreEqual(expectedEmailRequiredMessage, actualEmailRequiredMessage);
            Assert.AreEqual(expectedPasswordRequiredMessage, actualPasswordRequiredMessage);
            
            _driver.Quit();

        }



    }
}
