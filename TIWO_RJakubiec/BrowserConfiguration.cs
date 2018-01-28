using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TIWO_RJakubiec
{
    public class BrowserConfiguration
    {
        public FirefoxOptions UseBrowserConfigurationForFirefox()
        {
            FirefoxProfile myProfile = new FirefoxProfileManager().GetProfile("default");

            if (myProfile == null)
                throw new Exception("default firefox profile does not exist, please create it first.");

            myProfile.AcceptUntrustedCertificates = true;
            myProfile.AssumeUntrustedCertificateIssuer = true;
            myProfile.SetPreference("browser.cache.memory.enable", false);
            myProfile.SetPreference("browser.cache.offline.enable", false);
            myProfile.SetPreference("browser.cache.disk.enable", false);
            myProfile.SetPreference("network.http.use - cache", false);

            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArguments("--headless");
            firefoxOptions.AddArguments("--window-size=1920,1080");
            firefoxOptions.LogLevel = FirefoxDriverLogLevel.Trace;
            firefoxOptions.Profile = myProfile;

            return firefoxOptions;
        }

        public ChromeOptions UseBrowserConfigurationForChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();


            chromeOptions.AddArguments("--window-size=1920,1080");
            chromeOptions.AddArguments("--ignore-certificate-errors");
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--headless");

            return chromeOptions;
        }
    }
}