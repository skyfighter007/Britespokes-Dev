jQuery(function ($) {
  var view = new Britespokes.Admin.Tours.TimelineIndex({
    el: '#experiences-index'
  });
});

Britespokes.Admin.Tours.TimelineIndex = Backbone.View.extend({

  sortable: null,

  initialize: function() {
    var _view = this;
    this.sortable = $("#timeline-table tbody");
    this.sortable.sortable({
      update: function(event, ui) {
        _view.sortTimeline();
      },
      handle: ".sortable-cell",
      helper: function (e, tr) {
        var $originals = tr.children();
        var $helper = tr.clone();
        $helper.children().each(function (index) {
          // Set helper cell sizes to match the original sizes
          $(this).width($originals.eq(index).width());
        });
        return $helper;
      }
    }).disableSelection();
  },

  events: {
  },

  render: function() {
    return this;
  },

  sortTimeline: function(event, ui) {
    var rows = $('tr', this.sortable);
    var data = [];
    var tourId = $("#timeline-table").data("tour-id");

    rows.each(function(idx, row) {
      var $row = $(row);
      data.push({
        id: $row.data("item-id"),
        position: idx
      });
    });

    $.ajax({
      type: "POST",
      url: "/admin/tours/" + tourId + "/timelines/sort",
      data: JSON.stringify(data),
      dataType: "json",
      accept: "application/json",
      contentType: 'application/json; charset=utf-8',
      success: function(data, textStatus, xhr) {
      }
    }).fail(function() {
      alert("Could not update sort order");
    });
  },
});