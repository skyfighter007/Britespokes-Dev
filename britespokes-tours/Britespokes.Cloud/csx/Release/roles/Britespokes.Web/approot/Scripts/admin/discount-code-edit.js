jQuery(function ($) {
  var view = new Britespokes.Admin.DiscountCodes.Edit({
    el: '#edit-discount-code'
  });
});

Britespokes.Admin.DiscountCodes.Edit = Backbone.View.extend({

  targetEditor: null,

  initialize: function() {
    this.targetEditor = new Britespokes.Admin.DiscountCodes.TargetEditor({
      el: '#discount-code-target-editor'
    });

    this.toggleTargets({ currentTarget: $("#IsGlobal", this.$el)});
    $('.delete-discount-code').tooltip();
    $('#StartsAt, #ExpiresAt').datepicker().on('changeDate', function(event) {
      $(this).datepicker('hide');
    });
    this.setupDiscount($("input[name=UsePercentage]").val().toLowerCase() == "true")

    // select2 plugin for select elements
    $(".select2").select2({
      placeholder: "select tours",
      minimumInputLength: 1,
      multiple: true,
      ajax: {
        url: "/api/tours",
        dataType: "json",
        data: function(term) {
          return {
            q: term
          };
        },
        results: function(data, page) {
          return {
            results: $.map(data, function(val, i) {
              return { id: val.id, text: (val.code + " - " + val.name) }
            })
          }
        }
      }
    });
  },

  events: {
    "click .delete-discount-code": "deleteDiscountCode",
    "change input[name=UsePercentage]": "usePercentageChanged",
    "change #IsGlobal": "toggleTargets",
  },

  render: function() {
    return this;
  },

  usePercentageChanged: function(event) {
    var $discountField = $("#Discount", this.$el);
    var usePercentage = $(event.target).val().toLowerCase() == "true";
    this.setupDiscount(usePercentage);
    $discountField.focus();
  },

  setupDiscount: function(usePercentage) {
    var $discountField = $("#Discount", this.$el);
    $('#discount-type').remove();
    var $span = $("<span>").addClass("add-on").attr("id", "discount-type");
    if (usePercentage) {
      $('#discount-field').addClass('input-append');
      $('#discount-field').removeClass('input-prepend');
      $span.html("%");
      $discountField.after($span);
    } else {
      $('#discount-field').addClass('input-prepend');
      $('#discount-field').removeClass('input-append');
      $span.html("$");
      $discountField.before($span);
    }
  },

  deleteDiscountCode: function(event) {
    return confirm("Are you sure you want to delete this discount code?");
  },

  toggleTargets: function(event) {
    var global = $(event.currentTarget).is(":checked");
    if (!global)
      this.targetEditor.$el.show();
    else
      this.targetEditor.$el.hide();
  }

});

Britespokes.Admin.DiscountCodes.TargetItems = Backbone.Collection.extend();

Britespokes.Admin.DiscountCodes.TargetItemView = Backbone.View.extend({

  initialize: function() {

  },

  events: {
    "click .remove-item": "remove"
  },

  render: function() {
    var template = Handlebars.compile( $("#target-item-template").html() );
    this.$el.html(template(this.model.toJSON()));
    return this;
  },

  remove: function(event) {
    this.collection.remove(this.model);
  },

});

Britespokes.Admin.DiscountCodes.TargetItemsView = Backbone.View.extend({

  initialize: function() {
    this.items = [];
    this.collection.on("add", this.add, this);
    this.collection.on("remove", this.remove, this);

    var _view = this;
    var bootstrapItems = this.$el.data("items");
    if (bootstrapItems != null && bootstrapItems.length > 0) {
      _.each(bootstrapItems, function(item) {
        _view.collection.add(item);
      });
    }
  },

  render: function() {
    var _view = this;
    this.$el.empty();

    _(this.items).each(function(view) {
      _view.$el.append(view.render().$el);
    })

    return this;
  },

  add: function(item) {
    var itemView = new Britespokes.Admin.DiscountCodes.TargetItemView({
      tagName: "li",
      model: item,
      collection: this.collection
    });

    this.$el.find("li.none").remove();
    this.items.push(itemView);
    this.$el.append(itemView.render().$el);
  },

  remove : function(item) {
    var viewToRemove = _(this.items).select(function(cv) { return cv.model === item; })[0];
    this.items = _(this.items).without(viewToRemove);
    $(viewToRemove.el).remove();

    if (this.items.length === 0)
      this.$el.append($("<li>").addClass("none").html("(none)"));
  },

});

Britespokes.Admin.DiscountCodes.TargetEditor = Backbone.View.extend({

  tours: null,

  initialize: function() {
    var _view = this;
    this.tours = new Britespokes.Admin.DiscountCodes.TargetItems();
    this.products = new Britespokes.Admin.DiscountCodes.TargetItems();
    this.productVariants = new Britespokes.Admin.DiscountCodes.TargetItems();

    this.tourItemView = new Britespokes.Admin.DiscountCodes.TargetItemsView({
      el: '#tours-target',
      collection: _view.tours
    });

    this.productItemView = new Britespokes.Admin.DiscountCodes.TargetItemsView({
      el: '#products-target',
      collection: _view.products
    });

    this.productVariantItemView = new Britespokes.Admin.DiscountCodes.TargetItemsView({
      el: '#product-variants-target',
      collection: _view.productVariants
    });
  },

  events: {
    "change #tour-select": "tourChanged",
    "change #product-select": "productChanged",
    "click .select-add": "addItem"
  },

  render: function() {
    return this;
  },

  addItem: function(event) {
    var $addButton = $(event.currentTarget);
    var selector = $addButton.data("select");
    var targetType = $addButton.data("target-type");
    var $select = $(selector, this.$el);
    var name = $(selector + " option:selected").text();
    var model = {
      id: $select.val(),
      name: name,
      targetType: targetType
    };
    this.addToCollection(model, targetType);
  },

  addToCollection: function(model, targetType) {
    switch(targetType) {
      case "TourId":
        this.tours.add(model);
        break;
      case "ProductId":
        this.products.add(model);
        break;
      case "ProductVariantId":
        this.productVariants.add(model);
        break;
    }
  },

  tourChanged: function(event) {
    var $tourSelect = $(event.target);
    var tourId = $tourSelect.val();
    this.populateProducts(tourId);
  },

  productChanged: function(event) {
    var $productSelect = $(event.target);
    var productId = $productSelect.val();
    this.populateProductVariants(productId);
  },

  populateProducts: function(tourId) {
    var _view = this;
    var $productSelect = $("#product-select", this.$el);

    if (tourId == null) {
      $productSelect.html(null);
      return;
    }

    $.get("/api/departures", { tourId: tourId }, function(data) {
      _view.setOptions($productSelect, data, "productId", function(departure) {
        return departure.code + ' - ' + departure.name;
      });

      var selectedProductId = $productSelect.val();
      _view.populateProductVariants(selectedProductId);
    });
  },

  populateProductVariants: function(productId) {
    var _view = this;
    var $productVariantSelect = $("#product-variant-select", this.$el);
    if (productId == null) {
      $productVariantSelect.parents(".field-box").hide();
      $productVariantSelect.html(null);
      return;
    }

    $.get("/api/productvariants", { productId: productId }, function(data) {
      _view.setOptions($productVariantSelect, data, "id", function(variant) {
        return variant.code + ' - ' + variant.displayName;
      });
    });
  },

  setOptions: function($select, data, idField, nameFormat) {
    var options = '';
    if (data.length === 0) {
      $select.html(null);
      $select.parents(".field-box").hide();
    } else {
      $select.parents(".field-box").show();
      $.each(data, function(i, item) {
        var name = nameFormat.call(this, item);
        options += '<option value="' + item[idField] + '">' + name + '</option>';
      });
      $select.html(options);
    }
  },

});
