using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExercise.PageObjects
{
    class GeneralPage
    {
        IWebDriver driver = Constant.Constant.WEBDRIVER;
        TimeSpan time = Constant.Constant.TIMESPAN;
        public By userNameLabel() { return By.Id("userName-value"); }
        public By loginBnt() { return By.Id("login"); }
        public By logOutBnt() { return By.Id("submit"); }

        public IWebElement WaitForElement(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, Constant.Constant.TIMESPAN);
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElement(By locator, TimeSpan time)
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void MoveToElement(By locator)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(WaitForElement(locator)).Release().Perform();
        }

        public void ScrollToElementByJS(By locator)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true); ", Find(locator));
        }

        public IWebElement Find(By locator)
        {
            MoveToElement(locator);
            return WaitForElement(locator);
        }

        public ReadOnlyCollection<IWebElement> FindAllElements(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            try
            {
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public ReadOnlyCollection<IWebElement> FindAllElements(By locator, TimeSpan timeSpan)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeSpan);
            try
            {
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));

            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, time);
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElements(locator);
            }
            catch (NoSuchElementException e1)
            {
                Console.WriteLine(GetType().Name + " could not find the element. " + locator + e1.Message); throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(GetType().Name + " encountered an error. " + e.Message); throw;
            }
        }

        public void Click(By locator)
        {
            ScrollToElementByJS(locator);
            Find(locator).Click();
        }
        public void SendKeys(By locator, string text)
        {
            Find(locator).SendKeys(text);
        }


        public void goToLoginPage()
        {
            Click(loginBnt());
        }

        public void ClickLogOutButton()
        {
            Click(logOutBnt());
        }

        public string getUsernameLabelValue()
        {
            return Find(userNameLabel()).Text;
        }

        public void waitForAlertIsVisible()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (UnhandledAlertException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool IsDisplayed(By locator)
        {
            try
            {
                WaitForElement(locator);
                IWebElement element = Find(locator);
                return element.Displayed && element.Enabled;
            }
            catch (WebDriverException ex)
            {
                return false;
            }
        }

        public bool CheckExistedElement(By locator)
        {
            IReadOnlyCollection<IWebElement> listElement = FindAllElements(locator, TimeSpan.FromSeconds(5));
            if (listElement == null || listElement.Count == 0) return false;
            else return true;
        }


    }
}
