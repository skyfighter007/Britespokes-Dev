jQuery(function ($) {
  var view = new Britespokes.Admin.Tours.TimelineItemEdit({
    el: '#edit-tour-timeline-item'
  });
});

Britespokes.Admin.Tours.TimelineItemEdit = Backbone.View.extend({

  initialize: function() {
    $(".delete-tour-timeline-item").tooltip();
    // file uploads
    this.initializeImageUploads("#image-upload");
  },

  events: {
    "click .delete-tour-timeline-item": "deleteTimelineItem"
  },

  render: function() {
    return this;
  },

  deleteTimelineItem: function(event) {
    return confirm("Are you sure you want to delete this timeline item?");
  },

  // TODO: this should probably be refactored into a shared class somewhere
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
          $(selector).siblings(".image-url").val(file.Url);
          $(selector).siblings(".image-thumbnail").attr("src", file.Url).show();
          $(selector).siblings(".image-remove").show();
          _.defer(function() {
            $(selector).hide();
          });
        });
      }
    });

    var urlTarget = $urlTarget.val();
    if (urlTarget != null && urlTarget.length > 0) {
      $(selector).hide();
      $(selector).siblings(".image-remove").show();
      $(selector).siblings(".image-thumbnail").show();
    } else {
      $(selector).show();
      $(selector).siblings(".image-remove").hide();
      $(selector).siblings(".image-thumbnail").hide();
    }

    $(selector).siblings(".image-remove").click(function () {
      $(selector).siblings(".image-url").val(null);
      $(selector).siblings(".image-thumbnail").attr("src", null).hide();
      $(selector).siblings(".image-remove").hide();
      _.defer(function () {
        $(selector).show();
      });
    });
  },

});
