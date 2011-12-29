using System;
using JSTest.ScriptLibraries;
using Newtonsoft.Json;

namespace JSTest.Example.Test.Style3
{
  public abstract class JavaScriptTestBase
  {
    protected readonly TestScript Script = new TestScript();

    protected JavaScriptTestBase()
    {
      Script.AppendBlock(new JsAssertLibrary());
    }

    protected Object RunTest(String scriptBlock)
    {
      return JsonConvert.DeserializeObject(Script.RunTest(scriptBlock));
    }
  }
}
