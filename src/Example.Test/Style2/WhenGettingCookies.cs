using Xunit;

namespace JSTest.Example.Test.Style2
{
  public class WhenGettingCookies : JavaScriptTestBase
  {
    public WhenGettingCookies()
    {
      // Append Required JavaScript Files.
      Script.AppendFile(@"..\..\Scripts\dateExtensions.js");
      Script.AppendFile(@"..\..\Scripts\cookieContainer.js");

      // Setup JavaScript Context
      Script.AppendBlock(@"
                           var document = {};
                           var cookieContainer = new CookieContainer(document);
                         ");
    }

    [Fact]
    public void ReturnEmptyStringIfCookiesNotSet()
    {
      Script.RunTest(@"
                       document.cookie = '';

                       assert.equal('', cookieContainer.getCookie('MyCookie'));
                     ");
    }

    [Fact]
    public void ReturnCookieValueIfSingleCookieDefined()
    {
      Script.RunTest(@"
                       document.cookie = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();

                       assert.equal('Chocolate Chip', cookieContainer.getCookie('MyCookie'));
                     ");
    }

    [Fact]
    public void ReturnLastCookieValueIfMultipleCookiesDefined()
    {
      Script.RunTest(@"
                       var cookie1 = 'MyCookie1=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
                       var cookie2 = 'MyCookie2=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

                       document.cookie = cookie1 + '; ' + cookie2;

                       assert.equal('Peanut Butter', cookieContainer.getCookie('MyCookie2'));
                     ");
    }

    [Fact]
    public void ReturnCookieValueIfLikeNamedCookiesDefined()
    {
      Script.RunTest(@"
                       var cookie1 = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
                       var cookie2 = 'AlsoMyCookie=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

                       document.cookie = cookie1 + '; ' + cookie2;

                       assert.equal('Chocolate Chip', cookieContainer.getCookie('MyCookie'));
                     ");
    }
  }
}
