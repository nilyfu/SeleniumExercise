using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExercise.Constant
{
    class Constant
    {
        public static IWebDriver WEBDRIVER;
        public static string APP_URL= "https://demoqa.com/books";
        public static string USERNAME= "vyduong123";
        public static string PASSWORD= "Cuimia@123";
        public static TimeSpan TIMESPAN = TimeSpan.FromSeconds(5);

    }
}
