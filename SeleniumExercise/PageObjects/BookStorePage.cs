using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExercise.PageObjects
{
    class BookStorePage : GeneralPage
    {
        IWebDriver driver = Constant.Constant.WEBDRIVER;
        TimeSpan time = Constant.Constant.TIMESPAN;
        public By pageTitle() { return By.CssSelector(".main-header"); }
        public By menu(string item) { return By.XPath(string.Format(" //span[contains(text(),'{0}')]", item)); }
        public By deleteBnt() { return By.XPath("//span[@title='Delete']"); }
        public By listBookTitle() { return By.XPath("//div[@class='action-buttons']//a"); }
        public By addCollection() { return By.XPath("//button[contains(text(),'Add To Your Collection')]"); }
        public By searchBox() { return By.Id("searchBox"); }
        public By okBnt() { return By.Id("closeSmallModal-ok"); }
        public By bookTitle(string book) { return By.XPath(string.Format("//a[contains(text(),'{0}')]", book)); }


        string bookStoreMenu = "Book Store";
        string profileMenu = "Profile";

        public string getPageTitle()
        {
            return Find(pageTitle()).Text;
        }

        public string getPageURL()
        {
            return driver.Url;
        }

        public void VerifyPageTitle(string pageTitle)
        {
            Assert.AreEqual(pageTitle, getPageTitle());
        }       
        public void VerifyPageURL(string url)
        {
            Assert.AreEqual(url, getPageURL());
        }

        public void SelectABook(string book)
        {
            Click(bookTitle(book));
        }

        public void AddBookToCollection()
        {
            Click(addCollection());
        }         
        
        public void ClickDeleteButton()
        {
            Click(deleteBnt());
        }

        public void ClickOKButton()
        {
            Click(okBnt());
        }


        public string GetAlertMessage()
        {
            waitForAlertIsVisible();
            IAlert alert = driver.SwitchTo().Alert();
            return alert.Text;
        }        
        
        public void ClickOKButtonOfAlert()
        {
            waitForAlertIsVisible();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }      
        
        public void VerifyAlertMessage(string messageAlert)
        {
            Assert.AreEqual(GetAlertMessage(), messageAlert);
        }

        public void VerifyBookAddedSuccessfully(string message)
        {
            VerifyAlertMessage(message);
            ClickOKButtonOfAlert();
           
        }

        public void GoToProfilePage()
        {
            Click(menu(profileMenu));
        }

        public void VerifyBookInProfile(string book)
        {
            GoToProfilePage();
            sendKeysToSearchBox(book);
            string actualbook = Find(listBookTitle()).Text;
            Assert.AreEqual(book, actualbook);
        }

        public bool CheckBookExistedInProfile()
        {
            return CheckExistedElement(listBookTitle());
        }

        public bool CheckBookInBookStore(string book)
        {
            return IsDisplayed(bookTitle(book));
        }

        public void sendKeysToSearchBox(string book)
        {
            SendKeys(searchBox(), book);
        }         
        

        public bool VerifySearchMultipleResult(string text)
        {
            bool result = true;
            IReadOnlyCollection<IWebElement> listElement = FindAllElements(listBookTitle());
            foreach (IWebElement ele in listElement)
            {
                result &= ele.Text.ToLower().Contains(text.ToLower());
            }
            if (listElement.Count == 0) result = false;
            return result;
        }

        
        public void DeleteBookIfNeeded(string book)
        {
            Click(menu(profileMenu));
            sendKeysToSearchBox(book);
            if(CheckExistedElement(listBookTitle()))
            {
                ClickDeleteButton();
                ClickOKButton();
                ClickOKButtonOfAlert();
                Click(menu(bookStoreMenu));
            }
            else Click(menu(bookStoreMenu));

        }
        public void AddBookToCollection(string book)
        {
            sendKeysToSearchBox(book);
            SelectABook(book);
            AddBookToCollection();
            ClickOKButtonOfAlert();
        }

    }
}
