using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
  public class JavaScriptTestFileAttribute : DataAttribute
  {
    private readonly Regex _testPattern;
    private readonly String _fileName;
    private readonly String _context;

    public JavaScriptTestFileAttribute(String fileName)
      : this(fileName, @"[$A-Za-z_][$A-Za-z0-9_]*")
    { }

    public JavaScriptTestFileAttribute(String fileName, String testFunctionPattern)
    {
      if (String.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");
      if (String.IsNullOrWhiteSpace(testFunctionPattern)) throw new ArgumentNullException("testFunctionPattern");

      _testPattern = new Regex(@"^\s*function\s+(?<fact>" + testFunctionPattern + @")\s*\(\s*\)\s*\{?\s*$", RegexOptions.Multiline);
      _context = Path.GetFileNameWithoutExtension(fileName);
      _fileName = fileName;
    }

    public override IEnumerable<Object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
    {
      return from Match match
               in _testPattern.Matches(File.ReadAllText(_fileName))
           select new Object[] { _context, match.Groups["fact"].Value, _fileName };
    }
  }
}
