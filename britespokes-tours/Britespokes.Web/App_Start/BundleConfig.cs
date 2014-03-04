using System.Web.Optimization;

// ReSharper disable CheckNamespace
namespace Britespokes.Web
// ReSharper restore CheckNamespace
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/app")
                    .Include("~/Scripts/app/jquery.js")
                    //.Include("~/Scripts/app/jquery.jcarousel.min.js")
                    //.Include("~/Scripts/app/jcarousel.responsive.js")
                    .Include("~/Scripts/app/application.js")
                    .Include("~/Scripts/app/confirm-rooms.js")
                    .Include("~/Scripts/app/billing.js")
                    .Include("~/Scripts/app/giftcard.js")
                    .Include("~/Scripts/bootstrap/bootstrap-transition.js")
                    .Include("~/Scripts/bootstrap/bootstrap-carousel.js")
                    .Include("~/Scripts/bootstrap/bootstrap-tooltip.js")
                    .Include("~/Scripts/bootstrap/bootstrap-modal.js"));

      bundles.Add(new ScriptBundle("~/bundles/css_browser_selector")
                    .Include("~/Scripts/app/css_browser_selector.js"));

      bundles.Add(new ScriptBundle("~/bundles/html5shiv")
                    .Include("~/Scripts/html5shiv.js"));

      bundles.Add(new StyleBundle("~/main").Include("~/Content/main.css").Include("~/Content/jcarousel.responsive.css"));

     // bundles.Add(new StyleBundle("~/main").Include("~/Content/jcarousel.responsive.css"));
      // Admin bundles
      bundles.Add(new ScriptBundle("~/bundles/admin")
                    .Include("~/Scripts/admin/wysihtml5-0.3.0.js")
                    .Include("~/Scripts/app/jquery.js")
                    .Include("~/Scripts/admin/bootstrap.js")
                    .Include("~/Scripts/admin/bootstrap-wysihtml5-0.0.2.js")
                    .Include("~/Scripts/admin/bootstrap.datepicker.js")
                    .Include("~/Scripts/admin/jquery-ui-1.10.3.custom.js")
                    .Include("~/Scripts/admin/jquery.knob.js")
                    .Include("~/Scripts/admin/jquery.flot.js")
                    .Include("~/Scripts/admin/jquery.flot.stack.js")
                    .Include("~/Scripts/admin/jquery.flot.resize.js")
                    .Include("~/Scripts/admin/jquery.iframe-transport.js")
                    .Include("~/Scripts/admin/jquery.fileupload.js")
                    .Include("~/Scripts/admin/select2.js")
                    .Include("~/Scripts/admin/handlebars.js")
                    .Include("~/Scripts/admin/underscore.js")
                    .Include("~/Scripts/admin/backbone.js")
                    .Include("~/Scripts/admin/admin.js"));

      bundles.Add(new StyleBundle("~/styles/admin")
                    .Include("~/Content/admin/bootstrap.css")
                    .Include("~/Content/admin/bootstrap-responsive.css")
                    .Include("~/Content/admin/bootstrap-overrides.css")
                    .Include("~/Content/admin/bootstrap-wysihtml5.css")
                    .Include("~/Content/admin/wysiwyg-color.css")
                    .Include("~/Content/admin/uniform.default.css")
                    .Include("~/Content/admin/bootstrap.datepicker.css")
                    .Include("~/Content/admin/jquery-ui-1.10.2.custom.css")
                    .Include("~/Content/admin/font-awesome.css")
                    .Include("~/Content/admin/layout.css")
                    .Include("~/Content/admin/elements.css")
                    .Include("~/Content/admin/icons.css")
                    .Include("~/Content/admin/select2.css")
                    .Include("~/Content/admin/admin.css"));



      bundles.Add(new ScriptBundle("~/bundles/blog")
                 //.Include("~/Scripts/app/jquery.js")
                 //.Include("~/Scripts/app/application.js")
                 //.Include("~/Scripts/app/confirm-rooms.js")
                 //.Include("~/Scripts/app/billing.js")
                 //.Include("~/Scripts/app/giftcard.js")
                 //.Include("~/Scripts/bootstrap/bootstrap-transition.js")
                 //.Include("~/Scripts/bootstrap/bootstrap-carousel.js")
                 //.Include("~/Scripts/bootstrap/bootstrap-tooltip.js")
                 //.Include("~/Scripts/bootstrap/bootstrap-modal.js")
                 .Include("~/Scripts/admin/jquery-ui-1.10.3.custom.js")
                 .Include("~/Scripts/admin/wysihtml5-0.3.0.js")
                 .Include("~/Scripts/admin/bootstrap.js")
                 .Include("~/Scripts/admin/bootstrap-wysihtml5-0.0.2.js")
                 .Include("~/Scripts/admin/bootstrap.datepicker.js")
                 .Include("~/Scripts/admin/jquery.fileupload.js")
                 .Include("~/Scripts/admin/underscore.js")
                 .Include("~/Scripts/admin/backbone.js")
                 .Include("~/Scripts/admin/admin.js")
                 );






    }
  }
}
