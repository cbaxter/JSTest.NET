using System;
using System.Collections.Generic;
using System.Reflection;
using JSTest.ScriptElements;
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

namespace JSTest.Integration.Xunit
{
  [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
  public class JavaScriptFactFileAttribute : DataAttribute
  {
    private readonly String _fileName;
    private readonly String _testFunctionPattern;

    public JavaScriptFactFileAttribute(String fileName)
      : this(fileName, null)
    { }

    public JavaScriptFactFileAttribute(String fileName, String testFunctionPattern)
    {
      _fileName = fileName;
      _testFunctionPattern = testFunctionPattern;
    }

    public override IEnumerable<Object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
    {
      var result = new List<Object[]>();

      foreach (var testCase in TestCase.LoadFrom(_fileName, _testFunctionPattern))
        result.Add(new Object[] { new JavaScriptFact(testCase) });

      return result;
    }
  }
}
