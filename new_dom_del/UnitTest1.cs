using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{

    [TestFixture]
    public class NewDomestic
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        private StringBuilder verificationErrors;
        private string baseURL, id;
        private bool acceptNextAlert = true, load;
        public string idcheck()
        {
            return driver.FindElement(By.Id("PageId")).Text;
        }

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            //driver = new ChromeDriver();
            js =  driver as IJavaScriptExecutor;
            baseURL = "https://clicpltest.egroup.hu";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            
        }

        [Test]
        public void TheNewDomesticTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login?ReturnUrl=%2F");
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl(baseURL + "/Login/LoginWithRSADemo");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            Thread.Sleep(3000);
            try
            {
                driver.FindElement(By.Id("submit")).Click();
                Thread.Sleep(1000);
            }
            catch { }
            driver.Navigate().GoToUrl("https://clicpltest.egroup.hu/Domestic/New#forward");
            Thread.Sleep(2000);
            /*
            driver.FindElement(By.Id("Input_BnAccount_formatted")).Clear();
            driver.FindElement(By.Id("Input_BnAccount_formatted")).SendKeys("47160014752463344927179927");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Input_BnName")).Clear();
            driver.FindElement(By.Id("Input_BnName")).SendKeys("ikgGXKlY EUZLi");
            */
            driver.FindElement(By.Id("Input_BnName-Search")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//tr[@id='gvDomesticPartnerSearch_DXDataRow0']/td[3]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Input_Details")).Clear();
            driver.FindElement(By.Id("Input_Details")).SendKeys("automatic test");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
            }
            catch { }
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl("https://clicpltest.egroup.hu/PaymentList/List#forward");
            Thread.Sleep(15000);
            /*driver.FindElement(By.XPath(".//*[@id='divFilterBlock']/div[1]/div[1]/div[1]/div/div")).Click();
            Thread.Sleep(2000);*/
            driver.FindElement(By.Id("Clear")).Click();
            //-------------------------------------------------------------------
            do
            {
                Thread.Sleep(1000);
                load = (Boolean)js.ExecuteScript("return window.ajaxInProgress");
            } while (load != false);
            //-------------------------------------------------------------------

            Thread.Sleep(1000);
            driver.FindElement(By.Id("Filter_AmountFrom")).Clear();
            driver.FindElement(By.Id("Filter_AmountFrom")).SendKeys("1000");
            driver.FindElement(By.Id("Filter_AmountTo")).Clear();
            driver.FindElement(By.Id("Filter_AmountTo")).SendKeys("1000");
            driver.FindElement(By.Id("dk2-combobox")).Click();
            driver.FindElement(By.Id("dk2-ToEdit")).Click();
            new SelectElement(driver.FindElement(By.Id("Filter_TransactionStatus"))).SelectByText("To edit");
            driver.FindElement(By.Id("Filter")).Click();

            //-------------------------------------------------------------------
            do
            {
                Thread.Sleep(1000);
                load = (Boolean)js.ExecuteScript("return window.ajaxInProgress");
            } while (load != false);
            //-------------------------------------------------------------------

            Thread.Sleep(1000);
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=f2f8e779-4026-4f46-89b4-ca6d130079fa | ]]
            try
            {
                Assert.AreEqual("automatic test", driver.FindElement(By.CssSelector("#gvPaymentList_tccell0_8 > span")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.XPath("//tr[@id='gvPaymentList_DXDataRow0']/td[3]")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=f2f8e779-4026-4f46-89b4-ca6d130079fa | ]]
            try
            {
                Assert.AreEqual("automatic test", driver.FindElement(By.XPath(".//*[@id='main']/div[4]/div[8]/div[2]/div/p")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            do
            {
                Thread.Sleep(1000);
                id = idcheck();
                
            } while (id == "View");
            /*switch (switch_on)
            {
                case (id = "View"):
                    break;
                    
                default:
                    break;
            }*/

            /*
            -----------------
            -----------------
            -----------------
            -----------------
            -----------------
            */
            // ERROR: Caught exception [unknown command [clickandWait]]
            driver.FindElement(By.Id("actionButton_Delete")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(".//*[@id='alertMessage']/div[1]")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=f2f8e779-4026-4f46-89b4-ca6d130079fa | ]]
            try
            {
                Assert.AreEqual(baseURL + "/PaymentList/List#forward", driver.Url);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
