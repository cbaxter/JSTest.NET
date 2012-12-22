using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using JSTest.ScriptElements;
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

namespace JSTest
{
    public class TestScript
    {
        private const String NoAction = "";
        private const String Breakpoint = "debugger;";
        private readonly StringBuilder _script = new StringBuilder();
        private readonly ICScriptCommand _cscriptCommand;

        public Boolean IncludeDefaultBreakpoint { get; set; }

        public TestScript()
            : this(TimeSpan.FromSeconds(10))
        { }

        public TestScript(TimeSpan timeout)
            : this(new CScriptCommand(timeout))
        { }

        internal TestScript(ICScriptCommand cscriptCommand)
        {
            IncludeDefaultBreakpoint = true;

            _cscriptCommand = cscriptCommand;
        }

        public void AppendBlock(String scriptBlock)
        {
            AppendBlock(new ScriptBlock(scriptBlock));
        }

        public void AppendBlock(ScriptBlock scriptBlock)
        {
            Verify.NotNull(scriptBlock, "scriptBlock");

            _script.AppendLine(scriptBlock.ToScriptFragment());
        }

        public void AppendFile(String fileName)
        {
            _script.AppendLine(new ScriptInclude(fileName));
        }

        public void AppendFile(ScriptInclude scriptInclude)
        {
            Verify.NotNull(scriptInclude, "scriptInclude");

            _script.AppendLine(scriptInclude.ToScriptFragment());
        }

        public String RunTest(TestCase testCase)
        {
            Verify.NotNull(testCase, "testCase");

            return RunTest(testCase.ToScriptFragment());
        }

        public String RunTest(String testScript)
        {
            return RunTest(new TestExecutor(IncludeDefaultBreakpoint ? Breakpoint : NoAction, testScript, NoAction));
        }

        private String RunTest(TestExecutor testExecutor)
        {
            String scriptFile = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()), ".wsf");

            try
            {
                using (var writer = new StreamWriter(scriptFile))
                {
                    writer.WriteLine("<job id='UnitTest'>");
                    writer.Write(this);
                    writer.WriteLine(new JsonLibrary());
                    writer.WriteLine(testExecutor);
                    writer.WriteLine("</job>");
                }

                return Debugger.IsAttached ? _cscriptCommand.Debug(scriptFile) : _cscriptCommand.Run(scriptFile);
            }
            finally
            {
                File.Delete(scriptFile);
            }
        }

        public override string ToString()
        {
            return _script.ToString();
        }
    }
}
