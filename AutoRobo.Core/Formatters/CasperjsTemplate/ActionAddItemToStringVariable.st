casper.then(function(){
  <VariableName> = this.evaluate(function(){
    return <ID>.val();
  });
  this.echo(<VariableName>);
});