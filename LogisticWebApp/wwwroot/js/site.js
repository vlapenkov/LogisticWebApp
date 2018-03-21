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

function showNotification() {
    
    if (!("Notification" in window)) {
        console.log("This browser does not support desktop notification");
    }

    // Проверка разрешения на отправку уведомлений
    else if (Notification.permission === "granted") {
        // Если разрешено то создаем уведомлений
      

        (function spawnNotification(theBody, theIcon, theTitle) {
            var options = {
                body: theBody,
                icon: theIcon,

            }
            var n = new Notification(theTitle, options);
            n.onclick = function (event) {
                event.preventDefault(); // prevent the browser from focusing the Notification's tab
                window.location.href='/';
            }

        }) ("новые заявки в системе","/favicon.png","новые заявки");
        
    }

}

function checkAndSetLastActiveClaim() {

    setInterval(function () {
        $.getJSON('/api/DisplayInfoForCarrier/getlastactiveclaim').done(
            function (data) {
            
                if ((!!data) && localStorage.getItem("lastactiveclaim") !== data) {

                    localStorage.setItem("lastactiveclaim", data);
                    showNotification();
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
