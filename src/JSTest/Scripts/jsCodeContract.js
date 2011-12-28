var Verify = (function() {
  function isNullOrUndefined(value) {
    return typeof(value) === "undefined" || value === null;
  }

  /* ValueVerifier */
  function ValueVerifier(_value) { this.value = _value; }
  
  /* TypeVerifier */
  function TypeVerifier(_value, _nullable) { ValueVerifier.call(this, _value); }
  TypeVerifier.prototype = ValueVerifier;
  TypeVerifier.prototype.constructor = TypeVerifier;
  TypeVerifier.prototype.isString = function() {
    if(!!_nullable && isNullOrUndefined(_value)) {
      throw {};
    }

    if(typeof(this.value) !== "string") {
      throw "";
    }
  };

  /* NullValueVerifier */
  function NullValueVerifier(_value) { TypeVerifier.call(this, _value, false); }
  TypeVerifier.prototype = TypeVerifier;
  TypeVerifier.prototype.constructor = NullValueVerifier;
  TypeVerifier.prototype.whenDefined = function() {
    return new TypeVerifier.constructor(_value, typeof(_value) === "undefined");
  };
  TypeVerifier.prototype.whenNotNull = function() {
    return new TypeVerifier.constructor(_value, _value === null);
  };

  /* Verify */
  return { verify: function(value) { return new NullValueVerifier.constructor(value); } };
})();



function x() {
  
}




//Verify.value(width).whenDefined().isDate();


function ValueVerifier(value) {
  this.value = value;
}

function ValueTypeVerifier(value) { }
ValueTypeVerifier.prototype = Object.extend(ValueVerifier, {
  isString: function() {

  }
});

ValueTypeVerifier.prototype = new ValueTypeVerifier();
ValueTypeVerifier.prototype.constructor = ValueTypeVerifier;
ValueTypeVerifier.prototype

var x = {};


function ArrayVerifier(_value) {

}

function ComparableVerifier(_value) {

}

function StringVerifier(_value) {

}
StringVerifier.prototype = new ComparableVerifier;
StringVerifier.prototype.constructor = StringVerifier;
StringVerifier.prototype.containing = function () {};
StringVerifier.prototype.containing = function () {};
StringVerifier.prototype.containing = function () {};
StringVerifier.prototype.containing = function () {};
StringVerifier.prototype.containing = function () {};

function TypeVerifier(_value, _optional) {

  this.isString = function() {
    if(typeof(_value) !== "string") {
      throw "";
    }

    return new StringVerifier(_value);
  };

}

function NullVerifier() {}
NullVerifier.prototype = new TypeVerifier;
NullVerifier.prototype.constructor = NullVerifier;
NullVerifier.prototype.whenDefined = function () {};
NullVerifier.prototype.whenNotNUll = function () {};

/* TypeVerifier...

.isString = function() { }; 
.isBoolean = function() { };
.isNumber = function() { };
.isChar = function() { };
.isObject = function() { };
.isArray = function() { }; //reminder if (value instanceof Array)
.isFunction = function() { };
*/

/* NullVerifier...

.whenDefined = function() { }; 
.whenNotNull = function() { }; 

+TypeVerifier
*/

/* ComparableVerifier...

.greaterThan = function() { };
.lessThan = function() { };
.equalTo = function() { };
.notEqualTo = function() { };
.greaterThanOrEqualTo = function() { };
.lessThanOrEqualTo = function() { };
.between = function() { };
*/

/* StringVerifier...
.containing = function() { };
.endingWith = function() { };
.startingWith = function() { };
.withLengthOf = function() { };
.withMinimumLengthOf = function() { };
.withMaximumLengthOf = function() { };
.matching = function() { };

+ComparableVerifier
*/

/* ArrayVerifier...
.withLengthOf = function() { };
.withMinimumLengthOf = function() { };
.withMaximumLengthOf = function() { };
.withItems = function() { };
*/

function StringVerifier() {
}
StringVerifier.prototype.

function ContractVerifier(_value, _dataType) {

  function FunctionContractVerifier() {

    this.isNotNull = _this.isNotNull;
  }


  

  function throwContractViolation() {
    if (expression) {
      throw { type: "ContractViolation",  message: message };
    }
  }


  this.isNotNull = function () {
    if (_value === null) {
      throwContractViolation("Value must not be null."
    }
  };


  //Contract.requires(value).isNumber().greaterThan(0);
  //Contract.requires(value).isArray().withLengthGreaterThan(0);
  
  
  //Contract.requires(value).isNotNull().

  this.mustHave = function(value) { };
  this.mustNotHave = function(value) { };

  this.isNotNull = function() { };

  //core
  this.isString = function() { }; //nullable
  this.isBoolean = function() { };
  this.isNumber = function() { };
  this.isChar = function() { };
  this.isObject = function() { }; //nullable
  this.isArray = function() { }; //nullable
  this.isFunction = function() { }; //nullable

  //Comparable (number, string/char)
  this.isGreaterThan = function() { };
  this.isLessThan = function() { };
  this.isEqualTo = function() { };
  this.isNotEqualTo = function() { };
  this.isGreaterThanOrEqualTo = function() { };
  this.isLessThanOrEqualTo = function() { };
  this.between = function() { };

  //string
  this.contains = function() { };
  this.endsWith = function() { };
  this.startsWith = function() { };
  this.hasMinimumLengthOf = function() { };
  this.hasMaximumLengthOf = function() { };
  this.matches = function() { };

  //array
  this.hasLengthOf = function() { };
  this.hasMinimumLengthOf = function() { };
  this.hasMaximumLengthOf = function() { };
  this.isNotEmpty() {};
  this.isEmpty() {};

  //Verify.nullable(width).
  //Verify.notUl(width).

  //Verify.value(width).whenDefined().isDate();



  //Verify.value(name).isNotNull().hasTypeOfString().containing("CBaxter").matching("").withMinimumLengthOf(2).withMaximumLengthOf(10); //startingWith, endingWith()
  //verify.value(name).whenDefined().isNotNull()
  //verify.value(name).whenDefined().isObject();
  //verify.value(name).whenNotNull().isObject();
  //verify.value(name).isObject()
  //verify.value(name).whenDefined().is


  /*

    Verify.value(width).whenNotNull().isNumber().between(5, 7);;
    Verify.value(width).whenNotNull().isNumber().between(5, 7);;
  */


  //Verify.requiredValue(name).isObject();
  //Verify.optionalValue(name).isNullableObject();

  //Verify.value(name).isArray();
  //Verify.value(name).isNullableArray();

  //Verify.value(name).isObject();
  //Verify.value(name).isNullableObject();

  //Verify.value(name).isObject();
  //Verify.value(name).isNullableObject();



  //Verify.value(blah).isNullable().isString()

  //Verify.value(blah).isNotNull()

  //Verify.value(blah).[isRequired|isOptional].[hasValue]

  //Verify.value(blah).isArray().withLengthOfAtleast(2);
  //Verify.value(blah).isArray().withLengthOf(2);
  //Verify.value(blah).isArray().withLengthNoMoreThan(2);
  //Verify.value(blah).isArray().isNumber().greaterThan(0).lessThanOrEqualTo(100);


  //isOptional().acceptingNull().whereTypeString().contains("myinner");
  //isRequired().

  //Contract.requires(value).isString().containing("dfdf").startingWith("dsfsd").havingMiniminumLengthOf(1);
  //Verify.optional(value).isString().thatContains("dfdf");


  //isNotNull()
  //().isNotNull()


  //Verify.value(value).isNotNull().ofTypeString().thatContains("dfdf");
  //Verify.value(value).isOptional().ofTypeString().thatContains("dfdf");

  //Verify.nullable(value).isString().thatContains("dfdf");

  //isFunction, isArray, isObject, isString, isChar, isNumber, isDate, isBoolean, isUndefined, isNotNull, 







}
var Contract = { requires: function (value) { return new ContractVerifier(value); } };