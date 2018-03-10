function showError(form, inputElementName, errorText) {  // 'this' is the form element
    var errors = {};
    
    var $validator = form.validate();
    errors[inputElementName] = errorText;

    $validator.showErrors(errors);

}
