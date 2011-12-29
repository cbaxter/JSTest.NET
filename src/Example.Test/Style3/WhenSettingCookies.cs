using System;
using Xunit;

namespace JSTest.Example.Test.Style3
{
  public class WhenSettingCookies : JavaScriptTestBase
  {
    public WhenSettingCookies()
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
    public void CookieDocumentSet()
    {
      var result = RunTest(@"
                             cookieContainer.setCookie('MyCookie', 'Chocolate Chip');

                             return document.cookie;
                           ");

      Assert.Equal("MyCookie=Chocolate%20Chip;path=/", result);
    }

    [Fact]
    public void CookieExpirySetIfDaysSpecified()
    {
      const String dateFormat = "ddd, dd MMM yyyy HH:mm:ss UTC";

      var now = DateTime.UtcNow;
      var result = RunTest(String.Format(@"
                             var now = new Date('{0}');

                             cookieContainer.setCookie('MyCookie', 'Chocolate Chip', 1, now);

                             return document.cookie;
                           ", now.ToString(dateFormat)));

      Assert.Equal(String.Format("MyCookie=Chocolate%20Chip;expires={0};path=/", now.AddDays(1).ToString(dateFormat)), result);
    }
  }
}
