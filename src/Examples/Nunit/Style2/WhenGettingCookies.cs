using JSTest.ScriptLibraries;
using NUnit.Framework;

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

namespace JSTest.Examples.Nunit.Style2
{
    [TestFixture]
    public class WhenGettingCookies
    {
        protected TestScript Script { get; set; }

        [SetUp]
        public void Setup()
        {
            Script = new TestScript { IncludeDefaultBreakpoint = false };

            // Append required JavaScript libraries.
            Script.AppendBlock(new JsAssertLibrary());

            // Append required JavaScript Files.
            Script.AppendFile(@"..\..\dateExtensions.js");
            Script.AppendFile(@"..\..\cookieContainer.js");
            Script.AppendFile(@"..\..\whenGettingCookies.js");

            // Setup JavaScript Context
            Script.AppendBlock(@"
                                 var document = {};
                                 var cookieContainer = new CookieContainer(document);
                               ");
        }

        [Test]
        public void ReturnEmptyStringIfCookiesNotSet()
        {
            Script.RunTest(@"
                             document.cookie = '';

                             assert.equal('', cookieContainer.getCookie('MyCookie'));
                           ");
        }

        [Test]
        public void ReturnCookieValueIfSingleCookieDefined()
        {
            Script.RunTest(@"
                             document.cookie = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();

                             assert.equal('Chocolate Chip', cookieContainer.getCookie('MyCookie'));
                           ");
        }

        [Test]
        public void ReturnLastCookieValueIfMultipleCookiesDefined()
        {
            Script.RunTest(@"
                             var cookie1 = 'MyCookie1=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
                             var cookie2 = 'MyCookie2=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

                             document.cookie = cookie1 + '; ' + cookie2;

                             assert.equal('Peanut Butter', cookieContainer.getCookie('MyCookie2'));
                           ");
        }

        [Test]
        public void ReturnCookieValueIfLikeNamedCookiesDefined()
        {
            Script.RunTest(@"
                             var cookie1 = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
                             var cookie2 = 'AlsoMyCookie=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

                             document.cookie = cookie1 + '; ' + cookie2;

                             assert.equal('Chocolate Chip', cookieContainer.getCookie('MyCookie'));
                           ");
        }
    }
}
