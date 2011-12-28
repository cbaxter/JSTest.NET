/*
Copyright (c) 2011 CBaxter
 
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
IN THE SOFTWARE. 
*/
if (!this.assert) {
  this.assert = {};

  (function (assert) {
    assert.fail = function (message) {
      throw message || "assert.fail.";
    };

    assert.isTrue = function (expression, message) {
      if (!expression) {
        throw message || "assert.isTrue failed.";
      }
    };

    assert.isFalse = function (expression, message) {
      if (expression) {
        throw message || "assert.isFalse failed.";
      }
    };

    assert.isNull = function (actual, message) {
      if (actual !== null) {
        throw message || "assert.isNull failed.";
      }
    };

    assert.isNotNull = function (actual, message) {
      if (actual === null) {
        throw message || "assert.isNotNull failed.";
      }
    };

    assert.isUndefined = function (actual, message) {
      if (actual !== undefined) {
        throw message || "assert.isUndefined failed.";
      }
    };

    assert.isNotUndefined = function (actual, message) {
      if (actual === undefined) {
        throw message || "assert.isNotUndefined failed.";
      }
    };

    assert.equal = function (expected, actual, message) {
      if (expected !== actual && JSON.stringify(expected) !== JSON.stringify(actual)) {
        throw message || "assert.equal failed. Expected <" + JSON.stringify(expected) + ">. Actual <" + JSON.stringify(actual) + ">.";
      }
    };

    assert.notEqual = function (notExpected, actual, message) {
      if (notExpected === actual || JSON.stringify(notExpected) === JSON.stringify(actual)) {
        throw message || "assert.notEqual failed. Expected any value except <" + JSON.stringify(notExpected) + ">. Actual <" + JSON.stringify(actual) + ">.";
      }
    };

    assert.throws = function (expected, action, message) {
      var exceptionNotThrown = false;
      try {
        action();

        exceptionNotThrown = true;
      }
      catch (ex) {
        if (JSON.stringify(ex) !== JSON.stringify(expected)) {
          throw message || "assert.throws failed. Expected <" + JSON.stringify(expected) + ">. Actual <" + JSON.stringify(ex) + ">.";
        }
      }

      if (exceptionNotThrown) {
        throw message || "assert.throws failed. No exception was thrown.";
      }
    };
  })(this.assert);
}
