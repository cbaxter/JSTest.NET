using System;
using JSTest.ScriptLibraries;
using Xunit;
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

namespace JSTest.Test.ScriptLibraries
{
  public abstract class UsingJsAssert
  {
    protected readonly TestScript Script = new TestScript();

    protected UsingJsAssert()
    {
      Script.AppendBlock(new JsAssertLibrary());
    }
  }

  public class WhenUsingJsAssertLibrary : UsingJsAssert
  {
    [Fact]
    public void RunsInCScript()
    {
      Assert.DoesNotThrow(() => Script.RunTest(ScriptResources.JsAssertSampleTest));
    }

    [Fact]
    public void IgnoresMultipleRegistrations()
    {
      Script.AppendBlock(new JsAssertLibrary());

      Assert.DoesNotThrow(() => Script.RunTest(ScriptResources.JsAssertSampleTest));
    }
  }

  public class WhenAssertingFail : UsingJsAssert
  {
    [Fact]
    public void ThrowScriptException()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.fail();"));

      Assert.Equal("\"assert.fail.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.fail('Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingTrue : UsingJsAssert
  {
    [Fact]
    public void DoNotThrowIfNotViolated()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.isTrue(true);"));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isTrue(false);"));

      Assert.Equal("\"assert.isTrue failed.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isTrue(false, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingFalse : UsingJsAssert
  {
    [Fact]
    public void DoNotThrowIfNotViolated()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.isFalse(false);"));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isFalse(true);"));

      Assert.Equal("\"assert.isFalse failed.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isFalse(true, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingNull : UsingJsAssert
  {
    [Fact]
    public void DoNotThrowIfNotViolated()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.isNull(null);"));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isNull({});"));

      Assert.Equal("\"assert.isNull failed.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isNull({}, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingNotNull : UsingJsAssert
  {
    [Fact]
    public void DoNotThrowIfNotViolated()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.isNotNull({});"));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isNotNull(null);"));

      Assert.Equal("\"assert.isNotNull failed.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isNotNull(null, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingUndefined : UsingJsAssert
  {
    [Fact]
    public void DoNotThrowIfNotViolated()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.isUndefined(undefined);"));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isUndefined({});"));

      Assert.Equal("\"assert.isUndefined failed.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isUndefined({}, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingNotUndefined : UsingJsAssert
  {
    [Fact]
    public void DoNotThrowIfNotViolated()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.isNotUndefined({});"));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isNotUndefined(undefined);"));

      Assert.Equal("\"assert.isNotUndefined failed.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.isNotUndefined(undefined, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingEquality : UsingJsAssert
  {
    [Theory, InlineData("null"), InlineData("0"), InlineData("1"), InlineData("true"), InlineData("false"), InlineData("'value'")]
    public void DoNotThrowIfValueTypesAreEqual(String value)
    {
      Assert.DoesNotThrow(() => Script.RunTest(String.Format("assert.equal({0},{0});", value)));
    }

    [Fact]
    public void DoNotThrowIfListsAreEqual()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.equal([1,2,3], [1,2,3]);"));
    }

    [Fact]
    public void DoNotThrowIfHashesAreEqual()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.equal({value:1}, {value:1});"));
    }

    [Fact]
    public void ThrowScriptExceptionIfValueTypesAreNotEqual()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.equal(0, 1);"));

      Assert.Equal("\"assert.equal failed. Expected <0>. Actual <1>.\"", ex.Message);
    }

    [Fact]
    public void ThrowScriptExceptionIfListssAreNotEqual()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.equal([1,2,3], [3,2,1]);"));

      Assert.Equal("\"assert.equal failed. Expected <[1,2,3]>. Actual <[3,2,1]>.\"", ex.Message);
    }

    [Fact]
    public void ThrowScriptExceptionIfHashesAreNotEqual()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.equal({value:1}, {value:2});"));

      Assert.Equal("\"assert.equal failed. Expected <{\\\"value\\\":1}>. Actual <{\\\"value\\\":2}>.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.equal(0, 1, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }

  public class WhenAssertingInequality : UsingJsAssert
  {
    [Theory, InlineData("null", "undefined"), InlineData("0", "1"), InlineData("1", "0"), InlineData("true", "false"), InlineData("false", "true"), InlineData("'value'", "'anotherValue'")]
    public void DoNotThrowIfValueTypesAreEqual(String expected, String actual)
    {
      Assert.DoesNotThrow(() => Script.RunTest(String.Format("assert.notEqual({0},{1});", expected, actual)));
    }

    [Fact]
    public void DoNotThrowIfListsAreNotEqual()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.notEqual([1,2,3], [3,2,1]);"));
    }

    [Fact]
    public void DoNotThrowIfHashesAreEqual()
    {
      Assert.DoesNotThrow(() => Script.RunTest("assert.notEqual({value:1}, {value:2});"));
    }

    [Fact]
    public void ThrowScriptExceptionIfValueTypesAreNotEqual()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.notEqual(0, 0);"));

      Assert.Equal("\"assert.notEqual failed. Expected any value except <0>. Actual <0>.\"", ex.Message);
    }

    [Fact]
    public void ThrowScriptExceptionIfListssAreNotEqual()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.notEqual([1,2,3], [1,2,3]);"));

      Assert.Equal("\"assert.notEqual failed. Expected any value except <[1,2,3]>. Actual <[1,2,3]>.\"", ex.Message);
    }

    [Fact]
    public void ThrowScriptExceptionIfHashesAreNotEqual()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.notEqual({value:1}, {value:1});"));

      Assert.Equal("\"assert.notEqual failed. Expected any value except <{\\\"value\\\":1}>. Actual <{\\\"value\\\":1}>.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedAndProvided()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.notEqual(0, 0, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }
  
  public class WhenAssertingThrowsException : UsingJsAssert
  {
    [Theory, InlineData("\"exception\""), InlineData("{\"message\":\"exception\"}")]
    public void DoNotThrowIfNotViolated(String ex)
    {
      Assert.DoesNotThrow(() => Script.RunTest(String.Format("assert.throws({0}, function() {{ throw {0}; }});", ex)));
    }

    [Fact]
    public void ThrowScriptExceptionIfViolated()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.throws('exception', function() { throw 'anotherException'; });"));

      Assert.Equal("\"assert.throws failed. Expected <\\\"exception\\\">. Actual <\\\"anotherException\\\">.\"", ex.Message);
    }

    [Fact]
    public void ThrowCustomMessageIfViolatedd()
    {
      var ex = Assert.Throws<ScriptException>(() => Script.RunTest("assert.throws('exception', function() { }, 'Custom Message');"));

      Assert.Equal("\"Custom Message\"", ex.Message);
    }
  }
}
