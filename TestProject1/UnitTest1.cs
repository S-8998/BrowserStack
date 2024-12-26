using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject1;

public class Tests : BaseClass
{

    [Test]
    [TestCaseSource(typeof(BaseClass), "BrowserToRun")]
    [Parallelizable(ParallelScope.All)]
    public void Article(String browserName)
    {
        int count = 0;

        //Clicking on Opinion section
        WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(5));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='sm _df']//a[@href='https://elpais.com/opinion/']")));
        
        driver.Value.FindElement(By.XPath("//div[@class='sm _df']//a[@href='https://elpais.com/opinion/']")).Click();

        //Changing the window after clicking on opinion
        String childWindow = driver.Value.CurrentWindowHandle;
         driver.Value.SwitchTo().Window(childWindow);

         //fetching the articles 
         IList<IWebElement> articles = driver.Value.FindElements(By.XPath("//article[contains(@class,'c c-o c-d')]"));

            foreach(IWebElement article in articles)
            {
                    //printing first five article's headline
                    Console.WriteLine(article.Text);

                    //saving cover image of each article
                    if(articles.Contains(driver.Value.FindElement(By.XPath("//img"))))
                    {
                        Actions action = new Actions(driver.Value);
                        action.ContextClick(driver.Value.FindElement(By.XPath("//img"))).Build().Perform();
                        action.KeyDown(Keys.Control).SendKeys("s").Perform();
                    }

                   
                    
                count++;
                if(count==6)
                      break;

            }
            
    }

    [Test]
    [TestCaseSource(typeof(BaseClass), "BrowserToRun")]
    [Parallelizable(ParallelScope.All)]
    public void HeaderCount(String broswerName)
    {
        int counter = 0;

        //Clicking on Opinion section
        driver.Value.FindElement(By.XPath("//div[@class='sm _df']//a[@href='https://elpais.com/opinion/']")).Click();

        //Changing the window after clicking on opinion
        String childWindow = driver.Value.CurrentWindowHandle;
        driver.Value.SwitchTo().Window(childWindow);
        
        //counting the number of words in header title
        IList<IWebElement> headers = driver.Value.FindElements(By.XPath("//h2[@class='c_t c_t-i ']"));
        foreach(IWebElement header in headers)
        {
            String headerTitle = header.Text;

            String[] words = headerTitle.Split(" ");

            for(int i = 0; i<words.Length; i++)
            {
                for(int j=i+1; j<words.Length; j++)
                {
                    if(words[i].Equals(words[j]))
                    {
                        counter++;
                        if(counter>2)
                        {
                            Console.WriteLine(header.Text);
                            Console.WriteLine(words[i]+" : "+counter);
                        }
                    }

                 }

             }

         }
    }

    
            
                
    
}