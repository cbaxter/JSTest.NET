using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace JSTest.Example.Test.Style1
{
  public abstract class JavaScriptFactData : IEnumerable<Object[]>
  {
    private static readonly Regex TestPattern = new Regex(@"^function\s+(?<fact>[\w\d]+)\s*\(\s*\)\s*\{?\s*$", RegexOptions.Multiline);
    private readonly String _fileName;

    protected JavaScriptFactData(String fileName)
    {
      if (String.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");
      if (!File.Exists(fileName)) throw new FileNotFoundException("fileName");

      _fileName = fileName;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IEnumerator<Object[]> GetEnumerator()
    {
      foreach (Match match in TestPattern.Matches(File.ReadAllText(_fileName)))
        yield return new Object[] { match.Groups["fact"].Value };
    }
  }
}
