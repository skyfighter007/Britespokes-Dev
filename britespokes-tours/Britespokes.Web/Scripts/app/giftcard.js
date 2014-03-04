
//$('.quantity').bind('change', function () {

//    $.ajax({
//        type: 'GET',
//        url: '/giftcard-confirm',
//        data: {
//            Quantity: $('.quantity').val(),
//            GiftCardId: $('#GiftCardId').val()
//        },
//        success: function (data) {
//           // alert("succsess");
//            $('.giftcard-form').html(data);
//        },
//        error: function (e) {
//            alert("error");
//        },
//    });
//});


$(function () {
    $('.quantity').blur(function () {
        $.ajax({
            type: 'GET',
            url: '/giftcard-confirm',
            data: {
                Quantity: $('.quantity').val(),
                GiftCardId: $('#GiftCardId').val()
            },
            success: function (data) {
                if ($('.quantity').val() > 0)
                    $('.confirm-gift-order').css("display", "block");

                $('.giftcard-form').html(data);
            },
            error: function (e) {
                //alert("error");
            },
        });
    });

    $('.ajax').submit(function (e) {;
        e.preventDefault();
        $.get("/giftcard-start-orders", $(this).serialize(), function (data) {


            var isContainErrorMessage = false;

            if ($(data.replace("\r\n", "").replace("\r\n", "")).find(".field-validation-error").length > 0) {
                isContainErrorMessage = true;
            }

            if (isContainErrorMessage)
                $('.giftcard-form').html(data);
            else {
                var id = $('#GiftOrderId').val();
                var url = "/giftorders/" + id + "/billing";
                location.href = url;
            }

        });

    });

    $('.quantity').keydown(function (e) {
        if (e.keyCode == 13) {
            $('.quantity').blur();
        }
        else if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }

        }
    });

    $('.quantity').blur();
});




//$('#btncheckout').click(function () {

//    //$.ajax({
//    //    type: 'POST',
//    //    url: '/giftcard-confirm',
//    //    data: {
//    //        GiftOrderId: $('#GiftOrderId').val()
//    //        //YourName: $("input=text").EndsWith("YourName").val(),
//    //        //RecipientEmail: $("input=text").EndsWith("RecipientEmail").val(),
//    //        //DisplayAmount: $("input=text").EndsWith("DisplayAmount").val(),
//    //        //Message: $("input=text").EndsWith("Message").val()
//    //    },
//    //    success: function (data) {
//    //        // alert("succsess");
//    //        $('.giftcard-form').html(data);
//    //    },
//    //    error: function (e) {
//    //        alert("error");
//    //    },
//    //});
//});

//$('.amount').keydown(function (e) {

//    var keyCode = e.which; // Capture the event

//    //190 is the key code of decimal if you dont want decimals remove this condition keyCode != 190
//    if (keyCode != 8 && keyCode != 9 && keyCode != 13 && keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40 && keyCode != 46 && keyCode != 110 && keyCode != 190) {
//        if (keyCode < 48) {
//            e.preventDefault();
//        }
//        else if (keyCode > 57 && keyCode < 96) {
//            e.preventDefault();
//        }
//        else if (keyCode > 105) {
//            e.preventDefault();
//        }
//    }
//});
