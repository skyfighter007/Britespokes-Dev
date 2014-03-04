jQuery(function ($) {
    Britespokes.Admin.Organizations.Edit.init();
    var view = new Britespokes.Admin.Organizations.Edit({
        el: '#edit-organization'
    });
});

Britespokes.Admin.Organizations.Edit = {

  init: function() {
    $('.delete-organization').tooltip();
    $('.delete-organization').click(this.deleteOrganization);
    $('#IsPasscodeRequired').change(this.passcodeRequiredChanged);
    $('#RestrictedEmailDomains').change(this.restrictedEmailDomainsChanged);

    $('#EmailDomainList').select2({
      tags: [], tokenSeparators: [ ",", " " ],
      placeholder: 'email domain whitelist (ex. domain.com)'
    });
    this.initializeImageUploads("#thumbnail-upload");
    this.initializeImageUploads("#banner-upload");
    this.initializeImageUploads("#banner-upload2");
    this.initializeImageUploads("#banner-upload3");
    this.initializeImageUploads("#banner-upload4");
  },

  passcodeRequiredChanged: function(event) {
    if ($(this).is(':checked')) {
      $(this).parents('.field-box').addClass('expanded');
      $('#passcode-field').removeClass('hidden');
      $('#Passcode').focus();
    } else {
      $(this).parents('.field-box').removeClass('expanded');
      $('#passcode-field').addClass('hidden');
    }
  },

  restrictedEmailDomainsChanged: function(event) {
    if ($(this).is(':checked')) {
      $(this).parents('.field-box').addClass('expanded');
      $('#email-domains-field').removeClass('hidden');
      $('.select2-input', $('#email-domains-field')).focus().blur();
    } else {
      $(this).parents('.field-box').removeClass('expanded');
      $('#email-domains-field').addClass('hidden');
    }
  },

  deleteOrganization: function(event) {
    return confirm("Are you sure you want to delete this organization?");
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

};
