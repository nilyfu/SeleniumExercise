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
    public class Scenario1
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
        public void AddBookToYourCollection()
        {
            string bookTitle = "Git Pocket Guide";
            string pageTitle = "Book Store";
            string messageAlert = "Book added to your collection.";
            BookStorePage bookStorePage = new BookStorePage();

            // Given the user logs into application
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(username, password);

            string headerText = homePage.getUsernameLabelValue();
            Assert.AreEqual(headerText, username , "Username is not displayed as expected.");



            //And the user is on Book Store page
            bookStorePage.VerifyPageTitle(pageTitle);

            //Delete book if needed
            bookStorePage.DeleteBookIfNeeded(bookTitle);

            //When the user selects a book "Git Pocket Guide"
            bookStorePage.SelectABook(bookTitle);

            //And the user clicks on Add To Your Collection
            bookStorePage.AddBookToCollection();

            //Then an alert “Book added to your collection.” is shown
            bookStorePage.VerifyBookAddedSuccessfully(messageAlert);

            //And book is shown in your profile
            bookStorePage.VerifyBookInProfile(bookTitle);

        }


    }
}