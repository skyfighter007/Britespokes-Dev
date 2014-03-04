using System.Web.Mvc;
using System.Web.Routing;

// ReSharper disable CheckNamespace
namespace Britespokes.Web
// ReSharper restore CheckNamespace
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var ns = new[] { "Britespokes.Web.Controllers" };
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // ReSharper disable RedundantArgumentName

            // tours
            routes.MapRoute(
              name: "tour-preview",
              url: "preview/{permalink}",
              defaults: new { Controller = "Tours", action = "Preview" },
              namespaces: ns);

            routes.MapRoute(
            name: "tours-experiences-show",
            url: "experiences",
            defaults: new { Controller = "Tours", action = "Experiences" },
            namespaces: ns);


            routes.MapRoute(
            name: "toursexperiences",
            url: "experiences/category/{permalink}",
            defaults: new { Controller = "Tours", action = "ExperiencesTours" },
            namespaces: ns);



            //routes.MapRoute(
            //  name: "tours-index",
            //  url: "experiences",
            //  defaults: new { Controller = "Tours", action = "Index" },
            //  namespaces: ns);


            routes.MapRoute(
              name: "tour-show",
              url: "experiences/{permalink}",
              defaults: new { Controller = "Tours", action = "Show" },
              namespaces: ns);
          
            routes.MapRoute(
             name: "confirm-rooms",
             url: "experiences/{permalink}/confirm-rooms",
             defaults: new { Controller = "Tours", action = "ConfirmRooms" },
             namespaces: ns);

            routes.MapRoute(
                name: "confirm-rooms-student",
                url: "experiences/confirm-rooms/{permalink}",
                defaults: new { Controller = "Tours", action = "NewMethod" },
                namespaces: ns);

            routes.MapRoute(
              name: "start-order",
              url: "experiences/{permalink}/start-order",
              defaults: new { Controller = "Tours", action = "StartOrder" },
              namespaces: ns);

            routes.MapRoute(
             name: "Student-Inquiries",
             url: "experiences/student_inquiries/{permalink}",
             defaults: new { Controller = "Tours", action = "StudentInquiryForm" },
             namespaces: ns);

            routes.MapRoute(
              name: "college-contact",
              url: "experiences/student_inquiries/{permalink}/contact",
              defaults: new { Controller = "Tours", action = "Create" },
              namespaces: ns);

            routes.MapRoute(
              name: "billing-details",
              url: "orders/{orderId}/billing",
              defaults: new { Controller = "Tours", action = "Billing" },
              namespaces: ns);

            routes.MapRoute(
              name: "confirmation",
              url: "orders/{orderId}/confirmation",
              defaults: new { Controller = "Tours", action = "Confirmation" },
              namespaces: ns);

            routes.MapRoute(
              name: "authorize-net-sim",
              url: "orders/sim",
              defaults: new { Controller = "Tours", action = "AuthorizeNetSim" },
              namespaces: ns);

            routes.MapRoute(
              name: "test-checkout",
              url: "orders/test-checkout",
              defaults: new { Controller = "TestCheckout", action = "TestCheckout" },
              namespaces: ns);

            routes.MapRoute(
              name: "test-checkout-sim",
              url: "orders/test-checkout-sim",
              defaults: new { Controller = "TestCheckout", action = "TestCheckoutSim" },
              namespaces: ns);

            routes.MapRoute(
              name: "test-checkout-confirmation",
              url: "orders/{invoiceNumber}/test-checkout-confirmation",
              defaults: new { Controller = "TestCheckout", action = "Confirmation" },
              namespaces: ns);

            routes.MapRoute(
           name: "tour-subcategory",
           url: "experiences/{permalink}/{subpermalink}",
           defaults: new { Controller = "Tours", action = "SubCategoryTours" },
           namespaces: ns);

            // login and signup
            routes.MapRoute(
              name: "login",
              url: "login",
              defaults: new { Controller = "Sessions", action = "Create" },
              namespaces: ns);

            routes.MapRoute(
              name: "logout",
              url: "logout",
              defaults: new { Controller = "Sessions", action = "Delete" },
              namespaces: ns);

            routes.MapRoute(
              name: "register",
              url: "register",
              defaults: new { Controller = "Registrations", action = "Create" },
              namespaces: ns);

            routes.MapRoute(
             name: "facebook",
             url: "facebook",
             defaults: new { Controller = "Registrations", action = "Facebook" },
             namespaces: ns);

            routes.MapRoute(
           name: "facebookcallback",
           url: "facebookcallback",
           defaults: new { Controller = "Registrations", action = "FacebookCallback" },
           namespaces: ns);

            routes.MapRoute(
              name: "confirm",
              url: "confirm",
              defaults: new { Controller = "Confirmations", action = "Show" },
              namespaces: ns);

            routes.MapRoute(
              name: "confirmation-resend",
              url: "confirmation/resend",
              defaults: new { Controller = "Confirmations", action = "Create" },
              namespaces: ns);

            routes.MapRoute(
              name: "error",
              url: "error",
              defaults: new { Controller = "Home", action = "Error" },
              namespaces: ns);

            routes.MapRoute(
              name: "robots.txt",
              url: "robots.txt",
              defaults: new { controller = "Home", action = "Robots" },
              namespaces: ns);

            // pages
            var pages = new[]
        {
          "about", "contact", "corporate", "perspective",
          "privacy-policy", "terms-conditions",
          "terms-service", "thank-you", "site-usage",
          "custom"
        };

            routes.MapRoute(
              name: "pages",
              url: "{permalink}",
              defaults: new { Controller = "Pages", action = "Show" },
              constraints: new { permalink = string.Join("|", pages) },
              namespaces: ns);

            routes.MapRoute(
              name: "home",
              url: "",
              defaults: new { Controller = "Home", action = "Index" },
              namespaces: ns);

            //Gift cards
            routes.MapRoute(
            name: "b-GiftCards",
            url: "b-giftcards",
            defaults: new { Controller = "GiftCards", action = "Index" },
            namespaces: ns);

            routes.MapRoute(
            name: "billing-orderdetails",
            url: "giftorders/{orderId}/billing",
            defaults: new { Controller = "GiftCards", action = "Billing" },
             namespaces: ns);

            routes.MapRoute(
           name: "giftcard-show-color",
           url: "giftcards/{permalink}/{color}",
           defaults: new { Controller = "GiftCards", action = "Showwithcolor" },
           namespaces: ns);

            routes.MapRoute(
            name: "giftcard-show",
            url: "giftcards/{permalink}/",
            defaults: new { Controller = "GiftCards", action = "Show" },
            namespaces: ns);

            routes.MapRoute(
              name: "start-giftcardorder",
              url: "giftcards/{permalink}/start-order",
              defaults: new { Controller = "GiftCards", action = "StartOrder" },
              namespaces: ns);

            routes.MapRoute(
           name: "confirm-giftcards",
           url: "giftorders/{orderId}/confirm-giftcards",
           defaults: new { Controller = "GiftCards", action = "ConfirmGiftCards" },
           namespaces: ns);

            //  routes.MapRoute(
            //name: "confirm-giftorder",
            //url: "orders/{orderId}/confirm-giftorder",
            //defaults: new { Controller = "GiftCards", action = "ConfirmGiftCards" },
            //namespaces: ns);

         

            routes.MapRoute(
           name: "authorize-net-sim-gift",
           url: "giftorders/sim",
           defaults: new { Controller = "GiftCards", action = "AuthorizeNetSimGift" },
           namespaces: ns);

            routes.MapRoute(
             name: "giftconfirmation",
             url: "giftorders/{orderId}/confirmation",
             defaults: new { Controller = "GiftCards", action = "Confirmation" },
             namespaces: ns);

            //for partial view
            routes.MapRoute(
            name: "giftcard-confirm",
            url: "giftcard-confirm",
            defaults: new { Controller = "GiftCards", action = "ConfirmGiftOrders" },
            namespaces: ns);

            // Partial view submit  
            routes.MapRoute(
            name: "giftcard-start-orders",
            url: "giftcard-start-orders",
            defaults: new { Controller = "GiftCards", action = "StartGiftOrders" },
            namespaces: ns);

            routes.MapRoute(
              name: "myaccount",
              url: "myaccount",
              defaults: new { Controller = "Users", action = "Edit" },
              namespaces: ns);
            // ReSharper restore RedundantArgumentName

            routes.MapRoute(
            name: "SubscribeTour",
            url: "SubscribeTour",
            defaults: new { Controller = "Tours", action = "SubscribeTour" },
            namespaces: ns);

            routes.MapRoute(
            name: "Perspective",
            url: "Perspectives",
            defaults: new { Controller = "Perspectives", action = "Perspectives" },
            namespaces: ns);

            routes.MapRoute(
          name: "create-perspective",
          url: "perspectives/new",
          defaults: new { Controller = "Perspectives", action = "Create" },
          namespaces: ns);

            routes.MapRoute(
           name: "tour-perspective",
           url: "perspectives/{tourpermalink}",
           defaults: new { Controller = "Perspectives", action = "TourPerspectives" },
           namespaces: ns);

            routes.MapRoute(
            name: "perspective-post",
            url: "perspectives/{tourpermalink}/{permalink}",
            defaults: new { Controller = "Perspectives", action = "PerspectivePost" },
            namespaces: ns);

            routes.MapRoute(
            name: "comment-post",
            url: "comments",
            defaults: new { Controller = "Comments", action = "Create" },
            namespaces: ns);

          

            // images
            routes.MapRoute("image-create", "images", new { Controller = "Images", action = "Create" }, ns);


            //routes.MapRoute(
            //name: "downloadPDF",
            //url: "",
            //defaults: new { Controller = "Tours", action = "downloadPDF" },
            //namespaces: ns);

            //Perspectives Post
            //routes.MapRoute("admin-perspectives", "admin/perspectives", new { Controller = "Perspectives", action = "Index" }, ns);
            //routes.MapRoute("admin-perspectives-edit", "admin/perspectives/{perspectiveid}/edit", new { Controller = "Perspectives", action = "Edit" }, ns);
            //routes.MapRoute("admin-perspective-edit", "admin/perspectives/{perspectiveid}/Update", new { Controller = "Perspectives", action = "Edit" }, ns);
            //routes.MapRoute("perspective-create", "perspectives/new", new { Controller = "Perspectives", action = "Create" }, ns);
            //routes.MapRoute("admin-perspective-delete", "admin/perspective/{perspectiveid}/delete", new { Controller = "Perspectives", action = "Delete" }, ns);



            //context.MapRoute("admin-perspectives", "admin/perspectives", new { Controller = "Perspectives", action = "Index" }, ns);
            //context.MapRoute("admin-perspectives-edit", "admin/perspectives/{perspectiveid}/edit", new { Controller = "Perspectives", action = "Edit" }, ns);
            //context.MapRoute("admin-perspective-edit", "admin/perspectives/{perspectiveid}/Update", new { Controller = "Perspectives", action = "Edit" }, ns);
            //context.MapRoute("admin-perspective-create", "admin/perspectives/new", new { Controller = "Perspectives", action = "Create" }, ns);
            //context.MapRoute("admin-perspective-delete", "admin/perspective/{perspectiveid}/delete", new { Controller = "Perspectives", action = "Delete" }, ns);

        }
    }
}