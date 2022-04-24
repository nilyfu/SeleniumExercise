using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExercise.PageObjects
{
    class HomePage : GeneralPage
    {
        public void open()
        {
            Constant.Constant.WEBDRIVER.Navigate().GoToUrl(Constant.Constant.APP_URL);
        }

    }
}
