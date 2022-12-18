using PageObjects;
using SpecFlowPlaywright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyFramework.Steps
{
    [Binding]
    public sealed class LoginPageSteps
    {
        private readonly LoginPage _loginPage;
        public LoginPageSteps(LoginPage loginPage)
        {
            _loginPage = loginPage;
        }

        [When(@"I sign in to the application")]
        public async Task WhenISignInToTheApplication()
        {
            await _loginPage.Login("javed0211", "JKY78rQMQrZRnUH60U9Kgg==");

            Assert.IsTrue(await _loginPage.IsLoginSuccessful());
        }



    }
}
