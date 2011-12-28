using System;
using System.IO;
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
  public class WhenCreatingScriptInclude
  {
    [Theory, InlineData(null), InlineData(""), InlineData(" "), InlineData("\r\n")]
    public void ThrowArgumentExceptionIfWhitespaceOnly(String scriptBlock)
    {
      Assert.Throws<ArgumentException>(() => new ScriptInclude(scriptBlock));
    }

    [Fact]
    public void ThrowArgumentExceptionIfFileDoesNotExist()
    {
      Assert.Throws<FileNotFoundException>(() => new ScriptInclude(@"Q:\FakeFolder\FakeFile.ffe"));
    }
  }
  
  public class WhenConvertingScriptIncludeToString
  {
    [Fact]
    public void WrapScriptBlockWithScriptTag()
    {
      using (var tempFile = new TempFile())
      {
        Assert.Equal(
          String.Format(ScriptResources.ScriptIncludeFormat, tempFile.FileName),
          new ScriptInclude(tempFile.FileName)
        );
      }
    }

    [Fact]
    public void CanImplicitlyConvertToString()
    {
      using (var tempFile = new TempFile())
      {
        var scriptInclude = new ScriptInclude(tempFile.FileName);

        String script = scriptInclude;

        Assert.Equal(scriptInclude.ToString(), script);
      }
    }
  }
}
