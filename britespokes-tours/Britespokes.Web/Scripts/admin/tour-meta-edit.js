jQuery(function ($) {
  var view = new Britespokes.Admin.Tours.MetaEdit({
    el: '#edit-tour-meta'
  });
});

Britespokes.Admin.Tours.MetaEdit = Backbone.View.extend({

  initialize: function() {
    // wysihtml5 plugin on textarea
    $(".wysihtml5", this.$el).wysihtml5({
      "font-styles": false,
      html: true
    });

    // file uploads
    this.initializeImageUploads("#thumbnail-upload");
    this.initializeImageUploads("#banner-upload");
  },

  events: {
  },

  render: function() {
    return this;
  },

  initializeImageUploads: function(selector) {
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
          _.defer(function() {
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