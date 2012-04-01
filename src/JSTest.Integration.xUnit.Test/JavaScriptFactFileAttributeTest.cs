using System;
using System.Collections.Generic;
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
    public class JavaScriptFactFileAttributeTest
    {
        [Fact]
        public void ThrowFileNotFoundExceptionIfFileDoesNotExist()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\DoesNotExist.js");

            Assert.Throws<FileNotFoundException>(() => GetFacts(attribute));
        }

        [Fact]
        public void AllNamedFunctionsAreTestsByDefault()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile1.js");

            Assert.Equal(7, GetFacts(attribute).Count());
        }

        [Fact]
        public void ThrowArgumentExceptionOnBadRegexExpression()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile1.js", "(");

            Assert.Throws<ArgumentException>(() => GetFacts(attribute));
        }

        [Fact]
        public void UseCustomExpressionIfSpecified()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile2.js", @"test_[\w\d]+");

            Assert.Equal(4, GetFacts(attribute).Count());
        }

        [Fact]
        public void TestNameIsFileNameWithoutExtensionAndFunctionName()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile1.js");

            Assert.Equal("TestFile1.function1", GetFacts(attribute).First().TestName);
        }

        [Fact]
        public void TestFunctionIsFunctionNameOnlyIfDefaultPattern()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile1.js");

            Assert.Equal("function1", GetFacts(attribute).First().TestFunction);
        }

        [Fact]
        public void TestFunctionIsFunctionNameOnlyIfCustomPattern()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile2.js", @"test_[\w\d]+");

            Assert.Equal("test_function1", GetFacts(attribute).First().TestFunction);
        }

        [Fact]
        public void FileNameIsSameForAllTestsInFile()
        {
            var attribute = new JavaScriptFactFileAttribute(@"..\..\TestFile1.js");

            Assert.True(GetFacts(attribute).All(fact => fact.TestFile.Equals(@"..\..\TestFile1.js")));
        }

        private static IEnumerable<JavaScriptFact> GetFacts(JavaScriptFactFileAttribute attribute)
        {
            return attribute.GetData(null, null).Select(args => args.Single()).Cast<JavaScriptFact>();
        }
    }
}
