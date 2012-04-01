using System;
using System.Diagnostics;
using System.Text;

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
    internal interface ICScriptCommand
    {
        String Run(String fileName);
        String Debug(String fileName);
    }

    internal class CScriptCommand : ICScriptCommand
    {
        private readonly Int16 _timeoutInSeconds;

        public CScriptCommand()
            : this(TimeSpan.FromSeconds(10))
        { }

        public CScriptCommand(TimeSpan timeout)
        {
            if (timeout.TotalSeconds < 0 || timeout.TotalSeconds > Int16.MaxValue)
                throw new ArgumentOutOfRangeException("timeout", timeout.TotalSeconds, String.Format("Timeout must be between {0} and {1} seconds inclusive.", 0, Int16.MaxValue));

            _timeoutInSeconds = Convert.ToInt16(Math.Ceiling(timeout.TotalSeconds));
        }

        public String Run(String fileName)
        {
            return Run(fileName, _timeoutInSeconds, false);
        }

        public String Debug(String fileName)
        {
            return Run(fileName, _timeoutInSeconds, Debugger.IsAttached);
        }

        private static String Run(String fileName, Int16 timeoutInSeconds, Boolean enableDebugging)
        {
            Verify.NotWhiteSpace(fileName, "fileName");

            using (var proc = new Process())
            {
                var standardOutput = new StringBuilder();
                var standardError = new StringBuilder();

                proc.OutputDataReceived += (sender, e) => { if (!String.IsNullOrEmpty(e.Data)) standardOutput.Append(e.Data); };
                proc.ErrorDataReceived += (sender, e) => { if (!String.IsNullOrEmpty(e.Data)) standardError.Append(e.Data); };
                proc.StartInfo = new ProcessStartInfo
                                   {
                                       FileName = "cscript",
                                       Arguments = String.Format("\"{0}\" //NoLogo //T:{1} {2}", fileName, enableDebugging ? 0 : timeoutInSeconds, enableDebugging ? "//D" : String.Empty),
                                       CreateNoWindow = true,
                                       UseShellExecute = false,
                                       RedirectStandardError = true,
                                       RedirectStandardInput = true,
                                       RedirectStandardOutput = true
                                   };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();

                var error = standardError.ToString();
                if (!String.IsNullOrEmpty(error))
                    throw new ScriptException(error);

                if (proc.ExitCode == 0)
                    return standardOutput.ToString();

                throw new ScriptException(standardOutput.ToString());
            }
        }
    }
}
