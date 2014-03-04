jQuery(function ($) {
  var view = new Britespokes.Admin.Departures.Edit({
    el: '#edit-departure'
  });
});

Britespokes.Admin.Departures.Edit = Backbone.View.extend({

  initialize: function() {
    $(".delete-departure").tooltip();
    $('#DepartureDate, #ReturnDate, #AvailableOn').datepicker().on('changeDate', function(event) {
      $(this).datepicker('hide');
    });
  },

  events: {
    "click .delete-departure": "deleteDeparture"
  },

  render: function() {
    return this;
  },

  deleteDeparture: function(event) {
    return confirm("Are you sure you want to delete this departure?");
  },

});
