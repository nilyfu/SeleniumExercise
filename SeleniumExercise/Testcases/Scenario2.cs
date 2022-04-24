using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExercise.PageObjects;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumExercise
{
    public class Scenario2
    {
        public string username = Constant.Constant.USERNAME;
        public string password = Constant.Constant.PASSWORD;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            Constant.Constant.WEBDRIVER = new ChromeDriver();
            Constant.Constant.WEBDRIVER.Manage().Window.Maximize();
            Constant.Constant.WEBDRIVER.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Constant.Constant.WEBDRIVER.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [TearDown]
        public void TearDown()
        {
            Constant.Constant.WEBDRIVER.Quit();
        }


        [Test]
        public void SearchBookWithMultipleResults()
        {
            string bookTitle1 = "Learning JavaScript Design Patterns";
            string bookTitle2 = "Designing Evolvable Web APIs with ASP.NET";
            string pageTitle = "Book Store";
            string pageURL = Constant.Constant.APP_URL;
            BookStorePage bookStorePage = new BookStorePage();


            // Given the user logs into application
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(username, password);

            string headerText = homePage.getUsernameLabelValue();
            Assert.AreEqual(headerText, username, "Username is not displayed as expected.");

            // 1.Given there are books named “Learning JavaScript Design Patterns” and “Designing Evolvable Web APIs with ASP.NET”
            Assert.IsTrue(bookStorePage.CheckBookInBookStore(bookTitle1));
            Assert.IsTrue(bookStorePage.CheckBookInBookStore(bookTitle2));

            // 2.And the user is on Book Store page(https://demoqa.com/books)
            bookStorePage.VerifyPageURL(pageURL);

            // 3.When the user input book name “Design” or "design"
            string searchText = "Design";
            bookStorePage.sendKeysToSearchBox(searchText);

            // 4.Then all books match with input criteria will be displayed.
            Assert.AreEqual(true, bookStorePage.VerifySearchMultipleResult(searchText));

        }


    }
}