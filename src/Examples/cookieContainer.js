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

function CookieContainer(document) {
    "use strict";

    this.setCookie = function (name, value, days, now) {
        var expires = (now || new Date()).addDays(days || 0);

        document.cookie = name + "=" + escape(value) + (days ? ";expires=" + expires.toUTCString() : "") + ";path=/";
    };

    this.getCookie = function (name) {
        var match = new RegExp(";?\\s*" + name + "\\=([^;]*);?").exec(document.cookie);

        return match ? unescape(match[1]) : "";
    };
}