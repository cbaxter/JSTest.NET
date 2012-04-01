using System;
using System.IO;
using System.Linq;
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

namespace JSTest.Integration.Xunit.Test
{
#pragma warning disable 612,618
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
            var attribute = new JavaScriptTestFileAttribute(@"..\..\TestFile1.js", "(");

            Assert.Throws<ArgumentException>(() => attribute.GetData(null, null));
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
#pragma warning restore 612,618
}
