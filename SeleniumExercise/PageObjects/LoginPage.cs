using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExercise.PageObjects
{
    class LoginPage : GeneralPage
    {
     
        public By userName() { return By.Id("userName"); }
        public By passWord() { return By.Id("password"); }
        HomePage homePage = new HomePage();
   

        public void login(string username, string password)
        {
            SendKeys(userName(),username);
            SendKeys(passWord(),password);
            ScrollToElementByJS(loginBnt());
            Click(loginBnt());
        }
        public void LoginToApp(string username, string password)
        {

            login(username,password);   
        }
    }
}
