using System;
using Xunit.Extensions;

namespace JSTest.Example.Test.Style1
{
  public class UsingCookieContainer : JavaScriptTestBase
  {
    public UsingCookieContainer()
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

    [Theory, ClassData(typeof(WhenGettingCookiesData))]
    public void WhenGettingCookies(String fact)
    {
      // Append JavaScript 'Fact' File.
      Script.AppendFile(@"..\..\Style1\whenGettingCookies.js");

      // Verify 'Fact'.
      RunTest(fact);
    }

    private class WhenGettingCookiesData : JavaScriptFactData
    {
      public WhenGettingCookiesData() : base(@"..\..\Style1\whenGettingCookies.js") { }
    }

    [Theory, ClassData(typeof(WhenSettingCookiesData))]
    public void WhenSettingCookies(String fact)
    {
      // Append JavaScript 'Fact' File.
      Script.AppendFile(@"..\..\Style1\whenSettingCookies.js");

      // Verify 'Fact'.
      RunTest(fact);
    }

    private class WhenSettingCookiesData : JavaScriptFactData
    {
      public WhenSettingCookiesData() : base(@"..\..\Style1\whenSettingCookies.js") { }
    }
  }
}
