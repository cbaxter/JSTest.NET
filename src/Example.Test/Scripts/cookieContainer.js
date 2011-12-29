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