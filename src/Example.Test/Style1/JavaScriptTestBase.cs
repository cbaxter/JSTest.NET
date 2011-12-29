using System;
using JSTest.ScriptLibraries;

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
  public abstract class JavaScriptTestBase
  {
    protected readonly TestScript Script = new TestScript();

    protected JavaScriptTestBase()
    {
      Script.AppendBlock(new JsAssertLibrary());
    }

    protected String RunTest(String context, String action)
    {
      try
      {

        // Only the function name to call is provided, thus must add (); to invoke function.
        return Script.RunTest(action + "();");
      }
      catch (ScriptException ex)
      {
        // The xUnit test runner will output the 'Theory' parameters to define the executing 'context'.
        // i.e., JSTest.Example.Test.Style1.UsingCookieContainer.Test("whenSettingCookies", "cookieDocumentSet", "..\..\Style1\whenSettingCookies.js")
        //
        // However, the ReSharper test runner will not show any meaningful information. As such, it may be useful to
        // add the 'context' and 'action' to the exception message to ensure we always know what 'test' failed. 
        //
        // NOTE: The StackTrace contains no meaninful data, so intentionally thrown away here.
        throw new ScriptException(context + '.' + action + Environment.NewLine + ex.Message);
      }
    }
  }
}
