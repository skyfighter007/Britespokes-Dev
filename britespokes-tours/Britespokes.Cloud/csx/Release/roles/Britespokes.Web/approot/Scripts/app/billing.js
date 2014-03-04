jQuery(function ($) {


    $('#btnSubscribe').click(function (event) {
        subscribeTour();
    });



    $('#exp-month, #exp-year').change(processExpirationDate);

    $('#billing-details-form').submit(function (event) {
        event.preventDefault();
        var $form = $(this);

        clearFieldErrors();
        clearPaymentError();

        // Disable the submit button to prevent repeated clicks
        $form.find('button').prop('disabled', true);

        if (!hasValidPaymentDetails()) {
            $form.find('button').prop('disabled', false);
            return false;
        }

        submitBillingDetails();

        // Prevent the form from submitting with the default action
        return false;
    });

    $('#giftexp-month, #giftexp-year').change(processExpirationDateGift);

    $('#giftbilling-details-form').submit(function (event) {
        event.preventDefault();
        var $form = $(this);

        clearFieldErrorsGift();
        clearPaymentErrorGift();

        // Disable the submit button to prevent repeated clicks
        $form.find('button').prop('disabled', true);

        if (!hasValidPaymentDetailsGift()) {
            $form.find('button').prop('disabled', false);
            return false;
        }

        submitBillingDetailsGift();

        // Prevent the form from submitting with the default action
        return false;
    });
});

var subscribeTour = function () {

    $.ajax({
        type: "GET",
        url: "/SubscribeTour",
        data: {
            EmailToSubscribe: $('#emailtosubscribe').val(),
            MailingListUrl: $('#hdnMailingListUrl').val(),
            MailingListEmailName: $('#hdnMailingListEmailName').val(),
            MailingListID: $('#hdnMailingListID').val(),
        },
        success: function (result) {


            $('.comingsoon').html(result);


        },
        error: function (e) {
            alert(e.status);
        }
    });
};

var hasValidPaymentDetails = function () {
    if (!isRequiredFieldPresent('exp-month', 'please specify an expiration month'))
        return false;
    if (!isRequiredFieldPresent('exp-year', 'please specify an expiration year'))
        return false;
    if (!isRequiredFieldPresent('x_card_num', 'please specify your credit card number'))
        return false;
    if (!isRequiredFieldPresent('x_card_code', 'please specify your security code'))
        return false;

    return true;
};

var isRequiredFieldPresent = function (fieldId, errorMessage, paymentError) {
    var $form = $('#billing-details-form');
    var val = $('#' + fieldId).val();
    if (val == null || val == '') {
        if ($.inArray(fieldId, paymentErrorFields) != -1)
            showPaymentError(errorMessage);
        else
            addFieldError(fieldId, errorMessage);
        return false;
    }
    return true;
};

var billingData = function () {
    var $form = $('#billing-details-form');
    return {
        OrderId: formField('OrderId', $form).val(),
        UserId: formField('UserId', $form).val(),
        BillingAddressId: formField('BillingAddressId', $form).val(),
        CountryId: formField('CountryId', $form).val(),
        FirstName: formField('FirstName', $form).val(),
        LastName: formField('LastName', $form).val(),
        Email: formField('Email', $form).val(),
        Password: formField('Password', $form).val(),
        ConfirmPassword: formField('ConfirmPassword', $form).val(),
        Address1: formField('Address1', $form).val(),
        Address2: formField('Address2', $form).val(),
        City: formField('City', $form).val(),
        StateOrProvince: formField('StateOrProvince', $form).val(),
        ZipCode: formField('ZipCode', $form).val(),
        AcceptedTermsAndConditions: $('#AcceptedTermsAndConditions', $form).is(':checked'),
        GiftCode: $.trim($("#GiftCode").val()),
    };
};

var formField = function (key, $form) {
    var fieldName = key;
    if (key in fieldMap)
        fieldName = fieldMap[key];

    return $('#' + fieldName, $form);
}

var submitBillingDetails = function () {
    var $form = $('#billing-details-form');
    var url = $form.data('update-order-url');
    var data = billingData();
    
    $.post(url, data, function (response, textStatus, xhr) {
        if (response.Errors.length == 0) {
            
            $form.get(0).submit();
        } else {
            processErrors(response.Errors);
        }
    }).done(function () {
        $form.find('button').prop('disabled', false);
    }).fail(function () {
        showPaymentError("sorry, we were unable to process your request. Please <a href='/contact'>contact us</a>");
        $form.find('button').prop('disabled', false);
    });
};

var processExpirationDate = function (event) {
    var $expDate = $('#x_exp_date', $('#billing-details-form'));
    var month = $('#exp-month').val();
    var year = $('#exp-year').val();

    if (month === '' || year === '')
        return false;

    var expdate = month + year;
    $expDate.val(expdate);
    return true;
};

var processErrors = function (errors) {
    for (var errorItem in errors) {
        var error = errors[errorItem];
        if ($.inArray(error.Field, paymentErrorFields) != -1) {
            showPaymentError(error.Error);
        } else {
            addFieldError(error.Field, error.Error);
        }
    }
};

var paymentErrorFields = ["AcceptedTermsAndConditions", "OrderId", "UserId", "BillingAddressId", "exp-month", "exp-year"];

var showPaymentError = function (message) {
    var $form = $('#billing-details-form');
    var $paymentErrors = $form.find('.payment-errors');
    $paymentErrors.addClass("alert").addClass("alert-error");
    $paymentErrors.html(message);
};

var clearPaymentError = function () {
    var $form = $('#billing-details-form');
    var $paymentErrors = $form.find('.payment-errors');
    $paymentErrors.removeClass("alert").removeClass("alert-error");
    $paymentErrors.html('');
};

var addFieldError = function (fieldName, message) {
    var $form = $('#billing-details-form');

    if (fieldName in fieldMap)
        fieldName = fieldMap[fieldName];

    var $field = $('#' + fieldName, $form);
    var $controlGroup = $field.parents('.control-group').addClass('error');

    var $err = $('<p>').addClass('help-block').addClass('field-error');
    $err.html(message);
    $field.after($err);
};

var clearFieldErrors = function () {
    var $form = $('#billing-details-form');
    $('.control-group.error').removeClass('error');
    $('.field-error', $form).remove();
};

var fieldMap = {
    FirstName: 'x_first_name',
    LastName: 'x_last_name',
    Email: 'x_email',
    Address1: 'x_address',
    City: 'x_city',
    StateOrProvince: 'x_state',
    ZipCode: 'x_zip'
};



var hasValidPaymentDetailsGift = function () {
    if (!isRequiredFieldPresentGift('giftexp-month', 'please specify an expiration month'))
        return false;
    if (!isRequiredFieldPresentGift('giftexp-year', 'please specify an expiration year'))
        return false;
    if (!isRequiredFieldPresentGift('x_card_num', 'please specify your credit card number'))
        return false;
    if (!isRequiredFieldPresentGift('x_card_code', 'please specify your security code'))
        return false;

    return true;
};

var isRequiredFieldPresentGift = function (fieldId, errorMessage, paymentError) {
    var $form = $('#giftbilling-details-form');
    var val = $('#' + fieldId).val();
    if (val == null || val == '') {
        if ($.inArray(fieldId, paymentErrorFieldsGift) != -1)
            showPaymentErrorGift(errorMessage);
        else
            addFieldErrorGift(fieldId, errorMessage);
        return false;
    }
    return true;
};

var billingDataGift = function () {
    var $form = $('#giftbilling-details-form');
    return {
        GiftOrderId: formFieldGift('GiftOrderId', $form).val(),
        UserId: formFieldGift('UserId', $form).val(),
        BillingAddressId: formFieldGift('BillingAddressId', $form).val(),
        CountryId: formFieldGift('CountryId', $form).val(),
        FirstName: formFieldGift('FirstName', $form).val(),
        LastName: formFieldGift('LastName', $form).val(),
        Email: formFieldGift('Email', $form).val(),
        Password: formFieldGift('Password', $form).val(),
        ConfirmPassword: formFieldGift('ConfirmPassword', $form).val(),
        Address1: formFieldGift('Address1', $form).val(),
        Address2: formFieldGift('Address2', $form).val(),
        City: formFieldGift('City', $form).val(),
        StateOrProvince: formFieldGift('StateOrProvince', $form).val(),
        ZipCode: formFieldGift('ZipCode', $form).val(),
        AcceptedTermsAndConditions: $('#AcceptedTermsAndConditions', $form).is(':checked'),
    };
};

var formFieldGift = function (key, $form) {
    var fieldName = key;
    if (key in fieldMap)
        fieldName = fieldMap[key];

    return $('#' + fieldName, $form);
}

var submitBillingDetailsGift = function () {
    var $form = $('#giftbilling-details-form');
    var url = $form.data('update-order-url');
    var data = billingDataGift();
    debugger;

    $.post(url, data, function (response, textStatus, xhr) {
        if (response.Errors.length == 0) {
            debugger;

            $form.get(0).submit();
        } else {
            processErrorsGift(response.Errors);
        }
    }).done(function () {
        $form.find('button').prop('disabled', false);
    }).fail(function () {
        showPaymentErrorGift("sorry, we were unable to process your request. Please <a href='/contact'>contact us</a>");
        $form.find('button').prop('disabled', false);
    });
};

var processExpirationDateGift = function (event) {
    var $expDate = $('#x_exp_date', $('#giftbilling-details-form'));
    var month = $('#giftexp-month').val();
    var year = $('#giftexp-year').val();

    if (month === '' || year === '')
        return false;

    var expdate = month + year;
    $expDate.val(expdate);
    return true;
};

var processErrorsGift = function (errors) {
    for (var errorItem in errors) {
        var error = errors[errorItem];
        if ($.inArray(error.Field, paymentErrorFieldsGift) != -1) {
            showPaymentErrorGift(error.Error);
        } else {
            addFieldErrorGift(error.Field, error.Error);
        }
    }
};

var paymentErrorFieldsGift = ["AcceptedTermsAndConditions", "GiftOrderId", "UserId", "BillingAddressId", "giftexp-month", "giftexp-year"];

var showPaymentErrorGift = function (message) {
    var $form = $('#giftbilling-details-form');
    var $paymentErrors = $form.find('.payment-errors');
    $paymentErrors.addClass("alert").addClass("alert-error");
    $paymentErrors.html(message);
};

var clearPaymentErrorGift = function () {
    var $form = $('#giftbilling-details-form');
    var $paymentErrors = $form.find('.payment-errors');
    $paymentErrors.removeClass("alert").removeClass("alert-error");
    $paymentErrors.html('');
};

var addFieldErrorGift = function (fieldName, message) {
    var $form = $('#giftbilling-details-form');

    if (fieldName in fieldMap)
        fieldName = fieldMap[fieldName];

    var $field = $('#' + fieldName, $form);
    var $controlGroup = $field.parents('.control-group').addClass('error');

    var $err = $('<p>').addClass('help-block').addClass('field-error');
    $err.html(message);
    $field.after($err);
};

var clearFieldErrorsGift = function () {
    var $form = $('#giftbilling-details-form');
    $('.control-group.error').removeClass('error');
    $('.field-error', $form).remove();
};

var fieldMap = {
    FirstName: 'x_first_name',
    LastName: 'x_last_name',
    Email: 'x_email',
    Address1: 'x_address',
    City: 'x_city',
    StateOrProvince: 'x_state',
    ZipCode: 'x_zip'
};