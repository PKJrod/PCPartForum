using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PCPartForum.Models
{
    public class PlaywrightHelper
    {
        static string returnName;
        
        public static async Task TestPlaywright(IPlaywright playwright)
        {
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev/dotnet");
            var name = await page.QuerySelectorAsync("h1");
            returnName = name.InnerTextAsync().ToString();

        }

        public string getName()
        {
            return returnName;
        }
    }
}
