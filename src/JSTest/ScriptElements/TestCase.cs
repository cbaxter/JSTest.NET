using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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

namespace JSTest.ScriptElements
{
  public class TestCase : ScriptElement
  {
    private readonly String _fileName;
    private readonly String _functionName;
    private readonly String _testName;

    public String TestName { get { return _testName; } }
    public String TestFile { get { return _fileName; } }
    public String TestFunction { get { return _functionName; } }

    public TestCase(String fileName, String functionName)
    {
      Verify.NotWhiteSpace(fileName, "fileName");
      Verify.NotWhiteSpace(functionName, "functionName");

      _fileName = fileName;
      _functionName = functionName;
      _testName = String.Concat(Path.GetFileNameWithoutExtension(fileName), '.', _functionName);
    }

    protected override string ToScriptFragment()
    {
      return String.Format("return {0}();", _functionName);
    }

    public static TestCase[] LoadFrom(String fileName)
    {
      return LoadFrom(fileName, null);
    }

    public static TestCase[] LoadFrom(String fileName, String testFunctionPattern)
    {
      var result = new List<TestCase>();
      var regexPattern = @"^\s*function\s+(?<FunctionName>" + (testFunctionPattern ?? @"[$A-Za-z_][$A-Za-z0-9_]*") + @")\s*\(\s*\)\s*\{?\s*$";

      foreach (Match match in Regex.Matches(File.ReadAllText(fileName), regexPattern, RegexOptions.Multiline))
        result.Add(new TestCase(fileName, match.Groups["FunctionName"].Value));

      return result.ToArray();
    }
  }
}
