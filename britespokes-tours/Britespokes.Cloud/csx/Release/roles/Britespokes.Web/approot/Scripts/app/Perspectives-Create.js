jQuery(function ($) {
    var view = new Britespokes.Admin.GiftCards.Edit({
        el: '#edit-giftcard'
    });
});

Britespokes.Admin.GiftCards.Edit = Backbone.View.extend({

    initialize: function () {
        $(".delete-giftcard").tooltip();
        $(".delete-category").tooltip();
        // wysihtml5 plugin on textarea
        $(".wysihtml5", this.$el).wysihtml5({
            "font-styles": false,
            html: true
        });

        // file uploads
        this.initializeImageUploads("#thumbnail-upload");
        this.initializeImageUploads("#banner-upload");
        this.initializeImageUploads("#banner-upload2");
        this.initializeImageUploads("#banner-upload3");
        this.initializeImageUploads("#banner-upload4");
        
    },

    events: {
        "click .delete-giftcard": "deleteGiftCard",
        "click .delete-category": "deleteCategory"
    },

    render: function () {
        return this;
    },

    deleteGiftCard: function (event) {
        return confirm("Are you sure you want to delete this giftcard?");
    },
    deleteCategory: function (event) {
        return confirm("Are you sure you want to delete this Category?");
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
