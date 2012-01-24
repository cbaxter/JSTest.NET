using System;
using System.IO;
using System.Linq;
using JSTest.ScriptElements;
using Xunit;

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
  public class TestCaseTests
  {
    [Fact]
    public void ThrowFileNotFoundExceptionIfFileDoesNotExist()
    {
      Assert.Throws<FileNotFoundException>(() => TestCase.LoadFrom(@"..\..\Scripts\DoesNotExist.js"));
    }

    [Fact]
    public void AllNamedFunctionsAreTestsByDefault()
    {
      Assert.Equal(7, TestCase.LoadFrom(@"..\..\Scripts\TestFile1.js").Count());
    }

    [Fact]
    public void ThrowArgumentExceptionOnBadRegexExpression()
    {
      Assert.Throws<ArgumentException>(() => TestCase.LoadFrom(@"..\..\Scripts\TestFile1.js", "("));
    }

    [Fact]
    public void UseCustomExpressionIfSpecified()
    {
      Assert.Equal(4, TestCase.LoadFrom(@"..\..\Scripts\TestFile2.js", @"test_[\w\d]+").Count());
    }

    [Fact]
    public void TestNameIsFileNameWithoutExtensionAndFunctionName()
    {
      Assert.Equal("TestFile1.function1", TestCase.LoadFrom(@"..\..\Scripts\TestFile1.js").First().TestName);
    }

    [Fact]
    public void TestFunctionIsFunctionNameOnlyIfDefaultPattern()
    {
      Assert.Equal("function1", TestCase.LoadFrom(@"..\..\Scripts\TestFile1.js").First().TestFunction);
    }

    [Fact]
    public void TestFunctionIsFunctionNameOnlyIfCustomPattern()
    {
      Assert.Equal("test_function1", TestCase.LoadFrom(@"..\..\Scripts\TestFile2.js", @"test_[\w\d]+").First().TestFunction);
    }

    [Fact]
    public void FileNameIsSameForAllTestsInFile()
    {
      Assert.True(TestCase.LoadFrom(@"..\..\Scripts\TestFile1.js").All(testCase => testCase.TestFile.Equals(@"..\..\Scripts\TestFile1.js")));
    }
  }
}
