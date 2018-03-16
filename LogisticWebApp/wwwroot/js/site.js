function showError(form, inputElementName, errorText) {  // 'this' is the form element
    var errors = {};
    
    var $validator = form.validate();
    errors[inputElementName] = errorText;

    $validator.showErrors(errors);

}


function setNumberOfActiveClaims() {
    $.getJSON('/api/DisplayInfoForCarrier/getactiveclaims').done(
        function (data) {
            $('#numberof-activeclaims').text(data);
        }
    );
}

function setNumberOfCars() {
    $.getJSON('/api/DisplayInfoForCarrier/getcarcount').done(
        function (data) {
            $('#numberof-activeclaims').text(data);
        }
    );
}

function checkAndSetLastActiveClaim() {

    setInterval(function () {
        $.getJSON('/api/DisplayInfoForCarrier/getlastactiveclaim').done(
            function (data) {

                if ((!!data) && localStorage.getItem("lastactiveclaim") !== data) {
                    localStorage.setItem("lastactiveclaim", data);
                    alert("Новые заявки" + data);
                }
            }
        )
    }, 10000);

}

(function () {
    setNumberOfActiveClaims();
    checkAndSetLastActiveClaim();
   // console.log('ok');
}());
