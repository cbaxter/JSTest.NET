using JSTest.ScriptLibraries;

namespace JSTest.Example.Test.Style2
{
  public abstract class JavaScriptTestBase
  {
    protected readonly TestScript Script = new TestScript();

    protected JavaScriptTestBase()
    {
      Script.AppendBlock(new JsAssertLibrary());
    }
  }
}
