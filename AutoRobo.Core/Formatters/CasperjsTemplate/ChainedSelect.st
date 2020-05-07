//set select values
var optionFirstSelect  = <First>;
var optionSecondSelect = <Second>;    
var optionThirdSelect  = <Third>; 
var select1 = $('<Select1>');
var select2 = $('<Select2>');
var select3 = $('<Select3>');

casper.then(function(){

    //select option on first select
    this.evaluate(function(valueOptionSelect){
        select1.val(valueOptionSelect);
        select1.trigger('change');
    },optionFirstSelect);

    //wait until the second select is populated
    this.waitFor(function check() {
        return this.evaluate(function() {
            return document.querySelectorAll('select#s2 option').length > 1;
        });
    }, function then() {

            //select option on second select
            this.evaluate(function(valueOptionSelect){
                $('select#s2').val(valueOptionSelect);
                $('select#s2').trigger('change');
            },optionSecondSelect);

            //wait until the third select is populated        
            this.waitFor(function check() {
                return this.evaluate(function() {
                    return document.querySelectorAll('select#s3 option').length > 1;
                });
            }, function then() {

                    //select option on third select
                    this.evaluate(function(valueOptionSelect){
                        $('select#s3').val(valueOptionSelect);
                        $('select#s3').trigger('change');
                    },optionThirdSelect);

                    //wait until table with info is populated after an option is seleted on third select. 
                    this.waitFor(function check() {
                                return this.evaluate(function() {
                                    return  document.querySelectorAll('table#info tbody tr').length > 1;
                                });
                            }, function then() { 

                            //Do verifications here ...
                    });               
            });         
    }); 
});

