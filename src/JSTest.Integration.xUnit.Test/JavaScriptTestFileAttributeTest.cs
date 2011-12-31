using System;
using System.IO;
using System.Linq;
using Xunit;

namespace JSTest.Integration.Xunit.Test
{
  public class JavaScriptTestFileAttributeTest
  {
    [Fact]
    public void ThrowFileNotFoundExceptionIfFileDoesNotExist()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\DoesNotExist.js");

      Assert.Throws<FileNotFoundException>(() => attribute.GetData(null, null));
    }

    [Fact]
    public void AllNamedFunctionsAreTestsByDefault()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile1.js");

      Assert.Equal(7, attribute.GetData(null, null).Count());
    }

    [Fact]
    public void ThrowArgumentExceptionOnBadRegexExpression()
    {
      Assert.Throws<ArgumentException>(() => new JavaScriptTestFileAttribute(@"..\..\TestFile1.js", "("));
    }

    [Fact]
    public void UseCustomExpressionIfSpecified()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile2.js", @"test_[\w\d]+");

      Assert.Equal(4, attribute.GetData(null, null).Count());
    }

    [Fact]
    public void ContextIsFileNameWithoutExtension()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile1.js");

      Assert.True(attribute.GetData(null, null).All(arguments => arguments[0].Equals("TestFile1")));
    }

    [Fact]
    public void ActionIsFunctionNameOnlyIfDefaultPattern()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile1.js");
      
      Assert.Equal("function1", attribute.GetData(null, null).First()[1]);
    }
    
    [Fact]
    public void ActionIsFunctionNameOnlyIfCustomPattern()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile2.js", @"test_[\w\d]+");

      Assert.Equal("test_function1", attribute.GetData(null, null).First()[1]);
    }

    [Fact]
    public void FileNameIsSameForAllTestsInFile()
    {
      var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile1.js");

      Assert.True(attribute.GetData(null, null).All(arguments => arguments[2].Equals(@"..\..\TestFile1.js")));
    }
  }
}
