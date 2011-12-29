using Xunit;

namespace JSTest.Example.Test.Style2
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
      Script.RunTest(@"
                       cookieContainer.setCookie('MyCookie', 'Chocolate Chip');

                       assert.equal('MyCookie=' + escape('Chocolate Chip') + ';path=/', document.cookie);
                     ");
    }

    [Fact]
    public void CookieExpirySetIfDaysSpecified()
    {
      Script.RunTest(@"
                       var now = new Date();

                       cookieContainer.setCookie('MyCookie', 'Chocolate Chip', 1, now);

                       assert.equal('MyCookie=' + escape('Chocolate Chip') + ';expires=' + now.addDays(1).toUTCString() + ';path=/', document.cookie);
                     ");
    }
  }
}
