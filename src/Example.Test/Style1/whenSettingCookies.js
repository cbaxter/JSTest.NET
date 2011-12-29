function cookieDocumentSet() {
  cookieContainer.setCookie('MyCookie', 'Chocolate Chip');

  assert.equal('MyCookie=' + escape('Chocolate Chip') + ';path=/', document.cookie);
}

function cookieExpirySetIfDaysSpecified() {
  var now = new Date();

  cookieContainer.setCookie('MyCookie', 'Chocolate Chip', 1, now);

  assert.equal('MyCookie=' + escape('Chocolate Chip') + ';expires=' + now.addDays(1).toUTCString() + ';path=/', document.cookie);
}