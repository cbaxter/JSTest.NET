using System;
using System.Collections.Generic;
using System.IO;
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

namespace JSTest.Example.Test.Style1
{
  public class JavaScriptTestFile : DataAttribute
  {
    // Customize regular expression to your naming preference; example below assumes all parameterless functions are tests.
    private static readonly Regex TestPattern = new Regex(@"^function\s+(?<fact>[\w\d]+)\s*\(\s*\)\s*\{?\s*$", RegexOptions.Multiline);
    private readonly String _fileName;
    private readonly String _context;

    public JavaScriptTestFile(String fileName)
    {
      if (String.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");
      if (!File.Exists(fileName)) throw new FileNotFoundException("fileName", fileName);

      _context = Path.GetFileNameWithoutExtension(fileName);
      _fileName = fileName;
    }

    public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
    {
      foreach (Match match in TestPattern.Matches(File.ReadAllText(_fileName)))
        yield return new Object[] { _context, match.Groups["fact"].Value, _fileName };
    }
  }
}
