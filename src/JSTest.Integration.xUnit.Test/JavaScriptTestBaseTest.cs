using System;
using Xunit;

namespace JSTest.Integration.Xunit.Test
{
  public class JavaScriptTestBaseTest : JavaScriptTestBase
  {
    public JavaScriptTestBaseTest()
      : base(true)
    { }

    [JavaScriptTestSuite]
    [JavaScriptTestFile(@"..\..\TestFile3.js")]
    public void Test(String context, String action, String fileName)
    {
      // Append JavaScript 'Fact' File.
      Script.AppendFile(fileName);

      // Verify 'Fact'.
      Assert.Equal("true", RunTest(context, action));
    }
  }
}
