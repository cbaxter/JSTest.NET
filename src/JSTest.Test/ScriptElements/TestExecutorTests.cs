using System;
using JSTest.ScriptElements;
using Xunit;
using Xunit.Extensions;

/* Copyright (c) 2011 CBaxter
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE. 
 */

namespace JSTest.Test.ScriptElements
{
  public class WhenCreatingTestExecutor
  {
    [Theory, InlineData(null), InlineData(""), InlineData(" "), InlineData("\r\n")]
    public void AllowWhitespaceOnly(String scriptBlock)
    {
      Assert.DoesNotThrow(() => new TestExecutor(scriptBlock));
    }

    [Fact]
    public void IncludeDebuggerStatementByDefault()
    {
      Assert.Contains("debugger;", new TestExecutor("return true;"));
    }

    [Fact]
    public void SuppressDefaultDebuggerStatementIfExplicitlyRequested()
    {
      Assert.DoesNotContain("debugger;", new TestExecutor("return true;", false));
    }
  }

  public class WhenConvertingTestExecutorToString
  {
    [Fact]
    public void WrapScriptBlockWithTestRunnerJavaScript()
    {
      var scriptBlock = new ScriptBlock("function myFunction() { }");
      var testExecutor = new TestExecutor(scriptBlock);

      Assert.Equal(String.Format(ScriptResources.TestExecutorScriptBlockFormat, scriptBlock), testExecutor);
    }

    [Fact]
    public void CanImplicitlyConvertToString()
    {
      var scriptBlock = new TestExecutor("function myFunction() { }");

      String script = scriptBlock;

      Assert.Equal(scriptBlock.ToString(), script);
    }
  }
}
