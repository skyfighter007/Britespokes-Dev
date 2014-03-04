jQuery(function ($) {
  var view = new Britespokes.Admin.Experiences.Index({
    el: '#experiences-index'
  });
});

Britespokes.Admin.Experiences.Index = Backbone.View.extend({

  sortable: null,

  initialize: function() {
    var _view = this;
    this.sortable = $("#experiences-table tbody");
    this.sortable.sortable({
      update: function(event, ui) {
        _view.sortExperiences();
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
    "click .remove-cell .icon-remove": "removeExperience"
  },

  render: function() {
    return this;
  },

  removeExperience: function(event) {
    return confirm("Are you sure you want to delete this experience?");
  },

  sortExperiences: function(event, ui) {
    var rows = $('tr', this.sortable);
    var data = [];
    rows.each(function(idx, row) {
      var $row = $(row);
      data.push({
        id: $row.data("experience-id"),
        tourId: $row.data("tour-id"),
        position: idx
      });
    });

    $.ajax({
      type: "POST",
      url: "/admin/experiences/sort",
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