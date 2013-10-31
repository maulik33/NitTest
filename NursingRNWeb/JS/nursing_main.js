/********************************************************
Global functions
********************************************************/
function toPost(url, params){
    var newForm = document.createElement("form");
    newForm.action = url;
    newForm.method = 'POST';

    for(var propertyName in params)
    {
        var newHidden = document.createElement("input");
        newHidden.name = propertyName;
        newHidden.type = 'hidden';
        newHidden.value = params[propertyName];
        newForm.appendChild(newHidden);
    }
    
    document.getElementsByTagName('body')[0].appendChild(newForm);
    newForm.submit();
}