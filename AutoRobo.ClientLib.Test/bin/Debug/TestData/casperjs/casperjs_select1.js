var casper = require('casper').create({clientScripts: ['jquery.js']});
casper.start('../getdata.html');
var sysdir='';
var curdir='D:\mywebsite\autorobo\src\AutoRobo.ClientLib.Test\bin\Debug\testdata';
var _g = {};
var selectValue = 'a3';
_g.selectText='';


var el_1264154923;
casper.thenEvaluate(function(){
   el_1264154923 = $('SELECT#myselect');
});



casper.then(function(){
  selectValue = this.evaluate(function(){
    
    return el_1264154923.val();
  
  });
 
});

casper.then(function(){
   this.echo(selectValue);
});

casper.run();