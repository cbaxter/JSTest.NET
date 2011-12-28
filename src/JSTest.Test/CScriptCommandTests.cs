using System;
using System.IO;
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

namespace JSTest.Test
{
  public class WhenUsingCScriptCommand
  {
    [Theory, InlineData(0), InlineData(100), InlineData(Int16.MaxValue)]
    public void TimeoutAcceptedIfSecondsBetweenZeroAnd32768(Int32 timeout)
    {
      Assert.DoesNotThrow(() => new CScriptCommand(TimeSpan.FromSeconds(timeout)));
    }

    [Theory, InlineData(-1), InlineData(Int16.MaxValue + 1)]
    public void TimeoutRejectedIfSecondsNotBetweenZeroAnd32767(Int32 timeout)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new CScriptCommand(TimeSpan.FromSeconds(timeout)));
    }

    [Fact]
    public void RunReturnsStdOutText()
    {
      using (var tempFile = new TempFile("wsf"))
      {
        File.WriteAllText(tempFile.FileName, @"<job id='Test'><script language='JavaScript'>WScript.Echo('Sample Text');</script></job>");

        Assert.Equal("Sample Text", new CScriptCommand().Run(tempFile.FileName));
      }
    }

    [Fact]
    public void RunThrowsInvalidProgramExceptionOnBadInputFile()
    {
      using (var tempFile = new TempFile("wsf"))
      {
        File.WriteAllText(tempFile.FileName, @"<job id='Test'><script language='JavaScript'>function ) { return 'Missing Opening ('; }</script></job>");

        var ex = Assert.Throws<ScriptException>(() => new CScriptCommand().Run(tempFile.FileName));

        Assert.Contains("Microsoft JScript compilation error: Expected '('", ex.Message);
      }
    }

    [Fact]
    public void RunTimesOutOnLongRunningScript()
    {
      using (var tempFile = new TempFile("wsf"))
      {
        File.WriteAllText(tempFile.FileName, @"<job id='Test'><script language='JavaScript'>while(true) { }</script></job>");

        var ex = Assert.Throws<ScriptException>(() => new CScriptCommand(TimeSpan.FromMilliseconds(100)).Run(tempFile.FileName));

        Assert.Contains("Script execution time was exceeded on script", ex.Message);
      }
    }

    [Fact]
    public void DebugReturnsStdOutText()
    {
      using (var tempFile = new TempFile("wsf"))
      {
        File.WriteAllText(tempFile.FileName, @"<job id='Test'><script language='JavaScript'>WScript.Echo('Sample Text');</script></job>");

        Assert.Equal("Sample Text", new CScriptCommand().Debug(tempFile.FileName));
      }
    }

    [Fact]
    public void DebugThrowsInvalidProgramExceptionOnBadInputFile()
    {
      using (var tempFile = new TempFile("wsf"))
      {
        File.WriteAllText(tempFile.FileName, @"<job id='Test'><script language='JavaScript'>function ) { return 'Missing Opening ('; }</script></job>");

        var ex = Assert.Throws<ScriptException>(() => new CScriptCommand().Debug(tempFile.FileName));

        Assert.Contains("Microsoft JScript compilation error: Expected '('", ex.Message);
      }
    }

    [Fact]
    public void DebugTimesOutOnLongRunningScript()
    {
      using (var tempFile = new TempFile("wsf"))
      {
        File.WriteAllText(tempFile.FileName, @"<job id='Test'><script language='JavaScript'>while(true) { }</script></job>");

        var ex = Assert.Throws<ScriptException>(() => new CScriptCommand(TimeSpan.FromMilliseconds(100)).Debug(tempFile.FileName));

        Assert.Contains("Script execution time was exceeded on script", ex.Message);
      }
    }
  }
}
