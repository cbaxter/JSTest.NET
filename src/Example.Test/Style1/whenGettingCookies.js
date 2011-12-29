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

function returnEmptyStringIfCookiesNotSet() {
  document.cookie = '';

  assert.equal('', cookieContainer.getCookie('MyCookie'));
}

function returnCookieValueIfSingleCookieDefined() {
  document.cookie = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();

  assert.equal('Chocolate Chip', cookieContainer.getCookie('MyCookie'));
}

function returnLastCookieValueIfMultipleCookiesDefined() {
  var cookie1 = 'MyCookie1=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
  var cookie2 = 'MyCookie2=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

  document.cookie = cookie1 + '; ' + cookie2;

  assert.equal('Peanut Butter', cookieContainer.getCookie('MyCookie2'));
}

function returnCookieValueIfLikeNamedCookiesDefined() {
  var cookie1 = 'MyCookie=' + escape('Chocolate Chip') + '; expires=' + new Date().toUTCString();
  var cookie2 = 'AlsoMyCookie=' + escape('Peanut Butter') + '; expires=' + new Date().toUTCString();

  document.cookie = cookie1 + '; ' + cookie2;

  assert.equal('Chocolate Chip', cookieContainer.getCookie('MyCookie'));
}