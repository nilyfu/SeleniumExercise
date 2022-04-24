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
    public class Scenario3
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
        public void DeleteBookSuccessfully()
        {
            string bookTitle = "Learning JavaScript Design Patterns";
            string pageTitle = "Profile";
            string messageAlert = "Book deleted.";



            //1. Given there is book named “Learning JavaScript Design Patterns”
            //2. And the user logs into application
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(username, password);
            
            string headerText = homePage.getUsernameLabelValue();
            Assert.AreEqual(headerText, username, "Username is not displayed as expected.");

            BookStorePage bookStorePage = new BookStorePage();
            bookStorePage.AddBookToCollection(bookTitle);

            //3. And the user is on Profile page
            bookStorePage.GoToProfilePage();
            bookStorePage.VerifyPageTitle(pageTitle);

            //4. When the user search book “Learning JavaScript Design Patterns”
            bookStorePage.sendKeysToSearchBox(bookTitle);

            //5. And the user clicks on Delete icon
            bookStorePage.ClickDeleteButton();

            //6. And the user clicks on OK button
            bookStorePage.ClickOKButton();

            //7. And the user clicks on OK button of alert “Book deleted.”
            bookStorePage.VerifyAlertMessage(messageAlert);
            bookStorePage.ClickOKButtonOfAlert();

            //8. And the book is not shown
            Assert.False(bookStorePage.CheckBookExistedInProfile());

        }


    }
}