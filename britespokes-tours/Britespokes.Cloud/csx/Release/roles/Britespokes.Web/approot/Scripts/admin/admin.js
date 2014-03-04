var Britespokes = {};
Britespokes.Admin = {};
Britespokes.Admin.Tours = {};
Britespokes.Admin.Departures = {};
Britespokes.Admin.Experiences = {};
Britespokes.Admin.Organizations = {};
Britespokes.Admin.DiscountCodes = {};
Britespokes.Admin.Users = {};
Britespokes.Admin.Orders = {};
Britespokes.Admin.GiftCards = {};


$(function() {

  // sidebar menu dropdown toggle
  $("#dashboard-menu .dropdown-toggle").click(function (e) {
    e.preventDefault();
    var $item = $(this).parent();
    $item.toggleClass("active");
    if ($item.hasClass("active")) {
      $item.find(".submenu").slideDown("fast");
    } else {
      $item.find(".submenu").slideUp("fast");
    }
  });

});

// custom uiDropdown element
var uiDropdown = new function () {
  var self;
  self = this;
  this.hideDialog = function ($el) {
    return $el.find(".dialog").hide().removeClass("is-visible");
  };
  this.showDialog = function ($el) {
    return $el.find(".dialog").show().addClass("is-visible");
  };
  return this.initialize = function () {
    $("html").click(function () {
      $(".ui-dropdown .head").removeClass("active");
      return self.hideDialog($(".ui-dropdown"));
    });
    $(".ui-dropdown .body").click(function (e) {
      return e.stopPropagation();
    });
    return $(".ui-dropdown").each(function (index, el) {
      return $(el).click(function (e) {
        console.log("clicked");
        e.stopPropagation();
        $(el).find(".head").toggleClass("active");
        if ($(el).find(".head").hasClass("active")) {
          return self.showDialog($(el));
        } else {
          return self.hideDialog($(el));
        }
      });
    });
  };
};