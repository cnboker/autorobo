casper.then(function() {
<if(MouseFunctions=='Up'>
     this.mouseEvent('mouseup', <ID>);
<elseif(MouseFunctions=='Down')>
	this.mouseEvent('mousedown', <ID>);
<elseif(MouseFunctions=='Enter')>
	this.mouseEvent('enter', <ID>);
<endif>
});