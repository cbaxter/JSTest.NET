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