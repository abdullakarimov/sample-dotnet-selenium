using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace SampleSpecflowTest.Steps
{
    [Binding]
    public class SearchStepDefinitions
    {
        private readonly ScenarioContext context;

        public SearchStepDefinitions(ScenarioContext scenarioContext)
        {
            context = scenarioContext;
        }

        private string searchEngineUrl = "";
        IWebDriver driver;

        [Given("that I am on the (.*) page")]
        public void GivenUserOnPage(string input)
        {
            switch (input)
            {
                case "Google":
                    searchEngineUrl = "https://www.google.com/";
                    break;
                case "Bing":
                    searchEngineUrl = "https://www.bing.com/";
                    break;
                case "DuckDuckGo":
                    searchEngineUrl = "https://duckduckgo.com/";
                    break;
                default:
                    searchEngineUrl = "https://www.google.com/";
                    break;
            }

            driver = new FirefoxDriver
            {
                Url = searchEngineUrl
            };
            try
            {
                IWebElement changeLanguageLink = driver.FindElement(By.LinkText("English"));
                if (changeLanguageLink.Displayed)
                {
                    changeLanguageLink.Click();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Assert.That(driver.Title.Contains(input));
        }

        [Then("I click on the search input field")]
        public void ThenClickSearchInputField()
        {
            IWebElement searchInput = driver.FindElement(By.XPath("//input[@name='q']"));
            searchInput.Click();
        }

        [Then("I type in (.*)")]
        public void ThenTypeIn(string input)
        {
            IWebElement searchInput = driver.FindElement(By.XPath("//input[@name='q']"));
            searchInput.SendKeys(input);
        }

        [Then("I press Enter")]
        public void ThenPressEnter()
        {
            IWebElement searchInput = driver.FindElement(By.XPath("//input[@name='q']"));
            searchInput.SendKeys(Keys.Return);
        }

        [Then("I verify result statistics are displayed")]
        public void ThenVerifyResultStats()
        {
            try
            {
                IWebElement changeLanguageLink = driver.FindElement(By.LinkText("Change to English"));
                if (changeLanguageLink.Displayed)
                {
                    changeLanguageLink.Click();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            IWebElement resultStats = driver.FindElement(By.Id("result-stats"));
            Assert.That(resultStats.Displayed);
            Assert.That(resultStats.Text.Contains("About"));
            Assert.That(resultStats.Text.Contains("results"));
        }

    }
}
