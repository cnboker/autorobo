var casper = require('casper').create({clientScripts: ['jquery.js']});
casper.start('../getdata.html');
var sysdir = '';
var curdir = 'D:\mywebsite\autorobo\src\AutoRobo.ClientLib.Test\bin\Debug\testdata';
var selectValue = '';
var selectText = '';


var el_1264154923;
casper.thenEvaluate(function() {
    el_1264154923 = $('SELECT#myselect');
});
casper.then(function() {
    selectText = this.evaluate(function() {
        return el_1264154923.val();
    });
    this.echo(selectText);
});


var el_1264154923;
casper.thenEvaluate(function() {
    el_1264154923 = $('SELECT#myselect');
});
casper.then(function() {
    selectValue = this.evaluate(function() {
        return el_1264154923.val();
    });
    this.echo(selectValue);
});
casper.run();