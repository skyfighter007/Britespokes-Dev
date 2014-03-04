jQuery(function ($) {
  var view = new Britespokes.Admin.Orders.Index({
    el: '#orders-index'
  });
});

Britespokes.Admin.Orders.Index = Backbone.View.extend({

  initialize: function() {
    $('#startDate, #endDate').datepicker().on('changeDate', function(event) {
      $(this).datepicker('hide');
    });

    // instantiate new uiDropdown from above to build the plugins
    new uiDropdown();
    $('#btn-order-filter', this.$el).click(this.submit);
  },

  events: {
  },

  render: function () {
    return this;
  },

  submit: function(event) {
    event.preventDefault();
    var $btn = $(event.currentTarget);
    var $form = $btn.parents("form");
    return $form.submit();
  },

});