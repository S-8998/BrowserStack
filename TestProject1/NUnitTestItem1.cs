using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject1
{
    public class BaseClass
    {

        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Value = new ChromeDriver();
            //implicit wait
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://elpais.com/";

            //clicking on the alert after opening the page
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Aceptar']")));

            driver.Value.FindElement(By.XPath("//span[text()='Aceptar']")).Click();

        }
        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)
        {
            switch(browserName)
            {
                //factory design pattern
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;

            }
        }

        public static IEnumerable<string> BrowserToRun()
        {   
            String[] browsers = {"Chrome" , "Firefox" , "Edge"};

            foreach(String browser in browsers)
            {
                yield return browser;
            }
        }

        [TearDown]
        public void CloseDriver()
        {
            driver.Value.Quit();
        }
    }
}