jQuery(function ($) {
  var view = new Britespokes.Admin.Tours.Edit({
    el: '#edit-tour'
  });
});

Britespokes.Admin.Tours.Edit = Backbone.View.extend({

  initialize: function() {
      $(".delete-tour").tooltip();
      this.initializeImageUploads("#file-upload");
  },

  events: {
    "click .delete-tour": "deleteTour"
  },

  render: function() {
    return this;
  },

  deleteTour: function(event) {
    return confirm("Are you sure you want to delete this tour?");
  },

  initializeImageUploads: function (selector) {
      var $input = $(selector, this.$el);
      var $urlTarget = $input.siblings(".image-url");
      var $remove = $input.siblings(".image-remove");
      var $thumb = $input.siblings(".image-thumbnail");

      $(selector).fileupload({
          dataType: "json",
          formData: {
              prefix: $input.data("prefix")
          },
          done: function (e, data) {
              $.each(data.result.files, function (index, file) {
                  $urlTarget.val(file.Url);
                  $thumb.attr("src", file.Url).show();
                  $remove.show();
                  _.defer(function () {
                      $(selector).hide();
                  });
              });
          }
      });

      if ($urlTarget.val().length > 0) {
          $input.hide();
          $remove.show();
          $thumb.show();
      } else {
          $input.show();
          $remove.hide();
          $thumb.hide();
      }

      $remove.click(function () {
          $urlTarget.val(null);
          $thumb.attr("src", null).hide();
          $remove.hide();
          _.defer(function () {
              $(selector).show();
          });
      });
  },

});
