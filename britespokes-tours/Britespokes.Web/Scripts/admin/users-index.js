jQuery(function ($) {
  var view = new Britespokes.Admin.Users.Index({
    el: '#users-index'
  });
});

Britespokes.Admin.Users.Index = Backbone.View.extend({

  initialize: function() {
    // instantiate new uiDropdown from above to build the plugins
    new uiDropdown();
    $('#btn-user-filter', this.$el).click(this.submit);
  },

  events: {
    'click #prune-guests': 'pruneUsersMaybe'
  },

  render: function () {
    return this;
  },

  pruneUsersMaybe: function(event) {
    return confirm("Are you sure you want to prune guests?");
  },

  submit: function(event) {
    event.preventDefault();
    var $btn = $(event.currentTarget);
    var $form = $btn.parents("form");
    return $form.submit();
  },

});