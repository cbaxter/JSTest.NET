using System;
using JSTest.ScriptLibraries;

namespace JSTest.Example.Test.Style1
{
  public abstract class JavaScriptTestBase
  {
    protected readonly TestScript Script = new TestScript();

    protected JavaScriptTestBase()
    {
      Script.AppendBlock(new JsAssertLibrary());
    }

    protected String RunTest(String scriptBlock)
    {
      try
      {
        return Script.RunTest(scriptBlock + "();");
      }
      catch (ScriptException ex)
      {
        // The xUnit test runner will output the 'Theory' parameters to include the executing 'scriptBlock'.
        // i.e., JSTest.Example.Test.Style1.UsingCookieContainer.WhenGettingCookies("returnEmptyStringIfCookiesNotSet")
        //
        // However, the ReSharper test runner will not show any meaningful information. As such, it may be useful to
        // add the 'scriptBlock' to the exception message to ensure we always know what 'fact' failed. 
        //
        // NOTE: The StackTrace contains no meaninful data, so intentionally thrown away here.
        throw new ScriptException(scriptBlock + Environment.NewLine + ex.Message);
      }
    }
  }
}
