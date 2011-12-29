Date.prototype.addDays = function (days) {
  var date = new Date(this.getTime());

  date.setDate(this.getDate() + parseInt(days));

  return date;
};