jQuery(function ($) {
  var view = new Britespokes.Admin.Users.Show({
    el: '#user-show'
  });
});

Britespokes.Admin.Users.Show = Backbone.View.extend({

  initialize: function() {
  },

  events: {
    "click .toggle-admin": "submit"
  },

  render: function() {
    return this;
  },

  submit: function(event) {
    event.preventDefault();
    var revoke = /revoke/.test($(event.target).html());
    var message;
    if (revoke)
      message = "Are you sure you want to revoke administrator privileges for this user?";
    else
      message = "Are you sure you want to grant administrator privileges to this user?";

    if (confirm(message))
      $("#toggle-admin-form", this.$el).submit();
    return false;
  },

});
