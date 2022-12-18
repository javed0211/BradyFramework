using BradyFramework.PageObjects;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using SpecFlowPlaywright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    public class LoginPage
    {
        private Interactions _interactions;



        //Finding elements by ID / Xpath / CSS
        private static string btnUserMenu => "//button[contains(@class,'user-menu-button--anonymous')]";
        private static string btnSignIn => "//button[contains(@data-name,'sign-in')]";
        private static string txtUserName => "//input[contains(@id,'user-name-input')]";
        private static string txtPassword => "//input[contains(@id,'password-input')]";
        private static string btnSubmit => "//button[contains(@type,'submit')]";
        private static string btnEmail => "//span[contains(@class,'email')]";


        public LoginPage(BrowserDriver browserDriver)
        {
            _interactions = new Interactions(BasePage._page);
        }

        public async Task Login(string userName, string password)
        {
            string decryptedPassword = SecurePassword.Decrypt(password);
            var element = (await BasePage._page).Locator(btnUserMenu);
            if ((await BasePage._page).Locator(btnUserMenu).IsVisibleAsync().Result)
            {
                await (await BasePage._page).Locator(btnUserMenu).ClickAsync();
                await (await BasePage._page).Locator(btnSignIn).ClickAsync();
                await (await BasePage._page).Locator(btnEmail).ClickAsync();
                await (await BasePage._page).Locator(txtUserName).ClearAsync();
                await (await BasePage._page).Locator(txtUserName).FillAsync(userName);
                await (await BasePage._page).Locator(txtPassword).FillAsync(decryptedPassword);
                await (await BasePage._page).Locator(btnSubmit).ClickAsync();
            }
        }

        public async Task<bool> IsLoginSuccessful()
        {
            await (await BasePage._page).Locator("//button[contains(@class,'user-menu-button--logged')]").WaitForAsync();
            return (await BasePage._page).Locator("//button[contains(@class,'user-menu-button--logged')]").IsVisibleAsync().Result;
        }


    }
}
