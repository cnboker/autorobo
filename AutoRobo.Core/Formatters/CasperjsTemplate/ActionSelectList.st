
var select = $('<ID>');
<if(ByValue)>
        select.val('<Value>');
<else>

var len = select.length;
::= +
for(var i=0; i<len; i++){ 

    if(select.get(0).options[i].text == '<Value>'){
        select.get(0).options[i].selected = true;
        break;
    }
}

<endif>
