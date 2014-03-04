$(function(){
  if ($('a.birthday').length > 0) {
    $('a.birthday').tooltip();
    $('a.birthday').click(function() { return false; });
  };
  if ($('table.room-count').length > 0) {

    // FIXME This is a BAD spot for this.
    // Taken from:
    // http://stackoverflow.com/questions/149055/how-can-i-format-numbers-as-money-in-javascript
    Number.prototype.formatMoney = function(c, d, t){
      var n = this,
      c = isNaN(c = Math.abs(c)) ? 2 : c,
      d = d == undefined ? "." : d,
      t = t == undefined ? "," : t,
      s = n < 0 ? "-" : "",
      i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
      j = (j = i.length) > 3 ? j % 3 : 0;
     return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
   };

    // This is a fairly horrible way to go about this
    $('td.price').each(function (i, e) {
      var formattedPrice = parseInt($(e).text(), 10).formatMoney(0, '.', ',');
      $(e).html('$' + formattedPrice);
    });

    var roomTypes = $('tr.room-type'),
        roomCount = $('tr.room-type input');

    roomCount.change(function () {
       // test($(this));
      updateCountsAndTotals($(this));
    });

    //function test(input) {
    //    alert(input.val());
    //    test1();
    //    calculateRoomSubtotal(input);
      //};

    function updateCountsAndTotals(input) {
        calculateRoomSubtotal(input);
        determineNumberofTravelers(input);
        calculateRoomTotal();
        calculateTravelersTotal();
    };

    function calculateRoomSubtotal(input) {
        var travelersForRoomType = input.closest('tr.room-type').data('per-room'),
            numberOfRooms = input.val(),
            pricePerTraveler = input.data('price'),
            subtotalForRoomTypeContainer = input.closest('tr').find('td.price'),
            subtotalForRoomType = travelersForRoomType *
                                            pricePerTraveler *
                                            numberOfRooms;

        subtotalForRoomTypeContainer.data('subtotal', subtotalForRoomType);
    };


    function determineNumberofTravelers(input) {
        var travelersForRoomType = input.closest('tr.room-type').data('per-room'),
            numberOfRooms = input.val(),
            numberOfTravelersContainer = input.closest('tr').find('td.travelers'),
            numberOfTravelers = travelersForRoomType *
                                          numberOfRooms;
        numberOfTravelersContainer.data('travelers', numberOfTravelers);
        numberOfTravelersContainer.html(numberOfTravelers);
    };

    function calculateRoomTotal() {
        var roomSubtotals = $('tr.room-type td.price'),
            totalContainer = $('tr.subtotal td.price'),
            total = 0;
        roomSubtotals.each(function (i, priceElement) {
            var price = $(priceElement).data('subtotal');
            if (typeof price === 'number') {
                total = total + price;
            }
        });
        totalContainer.html('$' + total.formatMoney(0, '.', ','));
    };

    function calculateTravelersTotal() {
        var travelerTotals = $('tr.room-type td.travelers'),
            travelerTotalContainer = $('tr.subtotal td.travelers'),
            total = 0;
        travelerTotals.each(function (i, travelersElement) {
            var travelers = $(travelersElement).data('travelers');
            if (typeof travelers === 'number') {
                total = total + travelers;
            }
        });
        travelerTotalContainer.html(total);
    };


    roomCount.each(function (i, e) {
      updateCountsAndTotals($(e));
    });

  

  };

});
