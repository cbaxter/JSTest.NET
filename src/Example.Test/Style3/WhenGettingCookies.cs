using System;
using Xunit;

namespace JSTest.Example.Test.Style3
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
      var result = RunTest(@"
                             document.cookie = '';

                             return cookieContainer.getCookie('MyCookie');
                           ");
      
      Assert.Equal(String.Empty, result);
    }

    [Fact]
    public void ReturnCookieValueIfSingleCookieDefined()
    {
      var result = RunTest(@"
                             document.cookie = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();

                             return cookieContainer.getCookie('MyCookie');
                           ");

      Assert.Equal("Chocolate Chip", result);
    }

    [Fact]
    public void ReturnLastCookieValueIfMultipleCookiesDefined()
    {
      var result = RunTest(@"
                             var cookie1 = 'MyCookie1=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
                             var cookie2 = 'MyCookie2=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

                             document.cookie = cookie1 + '; ' + cookie2;

                             return cookieContainer.getCookie('MyCookie2');
                           ");

      Assert.Equal("Peanut Butter", result);
    }

    [Fact]
    public void ReturnCookieValueIfLikeNamedCookiesDefined()
    {
      var result = RunTest(@"
                             var cookie1 = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
                             var cookie2 = 'AlsoMyCookie=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

                             document.cookie = cookie1 + '; ' + cookie2;

                             return cookieContainer.getCookie('MyCookie');
                           ");

      Assert.Equal("Chocolate Chip", result);
    }
  }
}
