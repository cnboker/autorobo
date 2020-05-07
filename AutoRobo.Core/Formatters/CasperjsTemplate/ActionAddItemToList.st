casper.then(function() {
	 var text;
	 if(<ID>.is('select')){
		if(<Property> == "Value"){
			text = <ID>.val();
		}else{
			text = <ID>.find("option:selected").text(); 
		}
	 }else{
		if(<Property> == 'Text'){
			text = <ID>.text();
		}else if(<Property> == 'Html'){
			text = <ID>.html();
		}else{
			text = <ID>.attr('<Property>');
		}
	 }
	 <VariableName>.push(text);
});