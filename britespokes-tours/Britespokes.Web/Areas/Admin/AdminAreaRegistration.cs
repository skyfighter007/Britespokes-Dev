using System.Web.Mvc;

namespace Britespokes.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // ReSharper disable RedundantArgumentName
          
            var ns = new[] { "Britespokes.Web.Areas.Admin.Controllers" };

            // home
            context.MapRoute("admin-dashboard", "admin", new { Controller = "Dashboard", action = "Index" }, ns);
           

            // images
            context.MapRoute("admin-image-create", "admin/images", new { Controller = "Images", action = "Create" }, ns);

            // organizations
            context.MapRoute("admin-organization-edit", "admin/organizations/{organizationId}/edit", new { Controller = "Organizations", action = "Edit" }, ns);
            context.MapRoute("admin-organization-create", "admin/organizations/new", new { Controller = "Organizations", action = "Create" }, ns);
            context.MapRoute("admin-organization-destroy", "admin/organizations/{organizationId}/destroy", new { Controller = "Organizations", action = "Destroy" }, ns);
            context.MapRoute("admin-organizations", "admin/organizations", new { Controller = "Organizations", action = "Index" }, ns);

            // tours
            context.MapRoute("admin-tour-edit", "admin/tours/{tourId}/edit", new { Controller = "Tours", action = "Edit" }, ns);
            context.MapRoute("admin-tour-create", "admin/tours/new", new { Controller = "Tours", action = "Create" }, ns);
            context.MapRoute("admin-tour-destroy", "admin/tours/{tourId}/destroy", new { Controller = "Tours", action = "Destroy" }, ns);
            context.MapRoute("admin-tour-meta", "admin/tours/{tourId}/meta", new { Controller = "Tours", action = "Meta" }, ns);
            context.MapRoute("admin-tours", "admin/tours", new { Controller = "Tours", action = "Index" }, ns);

            // orders
            context.MapRoute("admin-order", "admin/orders/{orderId}", new { Controller = "Orders", action = "Show" }, ns);
            context.MapRoute("admin-orders-csv", "admin/orders.csv", new { Controller = "Orders", action = "ReportCsv" }, ns);
            context.MapRoute("admin-orders", "admin/orders", new { Controller = "Orders", action = "Index" }, ns);

            // giftorderss
            context.MapRoute("admin-giftorder", "admin/giftorders/{orderId}", new { Controller = "GiftOrders", action = "Show" }, ns);
            context.MapRoute("admin-giftorders", "admin/giftorders", new { Controller = "GiftOrders", action = "Index" }, ns);

            // experiences
            context.MapRoute("admin-experience-create", "admin/experiences/new", new { Controller = "Experiences", action = "Create" }, ns);
            context.MapRoute("admin-experiences-sort", "admin/experiences/sort", new { Controller = "Experiences", action = "Sort" }, ns);
            context.MapRoute("admin-experience-destroy", "admin/experiences/{experienceId}/destroy", new { Controller = "Experiences", action = "Destroy" }, ns);
            context.MapRoute("admin-experiences", "admin/experiences", new { Controller = "Experiences", action = "Index" }, ns);

            // tour timeline items
            context.MapRoute("admin-tour-timeline-sort", "admin/tours/{tourId}/timelines/sort", new { Controller = "TourTimelineItems", action = "Sort" }, ns);
            context.MapRoute("admin-tour-timeline-item-edit", "admin/tours/{tourId}/timelines/{itemId}/edit", new { Controller = "TourTimelineItems", action = "Edit" }, ns);
            context.MapRoute("admin-tour-timeline-item-create", "admin/tours/{tourId}/timelines/new", new { Controller = "TourTimelineItems", action = "Create" }, ns);
            context.MapRoute("admin-tour-timeline-item-destroy", "admin/tours/{tourId}/timelines/{itemId}/destroy", new { Controller = "TourTimelineItems", action = "Destroy" }, ns);
            context.MapRoute("admin-tour-timeline-items", "admin/tours/{tourId}/timelines", new { Controller = "TourTimelineItems", action = "Index" }, ns);

            // departures
            context.MapRoute("admin-departure-edit", "admin/departures/{productId}/edit", new { Controller = "Departures", action = "Edit" }, ns);
            context.MapRoute("admin-departure-create", "admin/tours/{tourId}/departures/new", new { Controller = "Departures", action = "Create" }, ns);
            context.MapRoute("admin-departure-destroy", "admin/departures/{productId}/destroy", new { Controller = "Departures", action = "Destroy" }, ns);
            context.MapRoute("admin-departures", "admin/tours/{tourId}/departures", new { Controller = "Departures", action = "Index" }, ns);

            // discount codes
            context.MapRoute("admin-discount-code-edit", "admin/discount-codes/{discountCodeId}/edit", new { Controller = "DiscountCodes", action = "Edit" }, ns);
            context.MapRoute("admin-discount-code-create", "admin/discount-codes/new", new { Controller = "DiscountCodes", action = "Create" }, ns);
            context.MapRoute("admin-discount-code-destroy", "admin/discount-codes/{discountCodeId}/destroy", new { Controller = "DiscountCodes", action = "Destroy" }, ns);
            context.MapRoute("admin-discount-codes", "admin/discount-codes", new { Controller = "DiscountCodes", action = "Index" }, ns);

            // users
            context.MapRoute("admin-users", "admin/users", new { Controller = "Users", action = "Index" }, ns);
            context.MapRoute("admin-users-prune-guests", "admin/users/guests/prune", new { Controller = "Users", action = "PruneGuests" }, ns);
            context.MapRoute("admin-user-show", "admin/users/{userId}", new { Controller = "Users", action = "Show" }, ns);
            context.MapRoute("admin-user-update-comment", "admin/users/{userId}/update-comment", new { Controller = "Users", action = "UpdateComment" }, ns);
            context.MapRoute("admin-user-toggle-admin", "admin/users/{userId}/toggle-admin", new { Controller = "Users", action = "ToggleAdmin" }, ns);

            //gift cards

            context.MapRoute("admin-giftcards", "admin/giftcards", new { Controller = "giftcards", action = "Index" }, ns);
            context.MapRoute("admin-giftcard-create", "admin/giftcards/new", new { Controller = "Giftcards", action = "Create" }, ns);
            context.MapRoute("admin-giftcard-edit", "admin/giftcards/{giftcardId}/edit", new { Controller = "GiftCards", action = "Edit" }, ns);
            context.MapRoute("admin-giftcard-destroy", "admin/giftcards/{giftcardId}/destroy", new { Controller = "GiftCards", action = "Destroy" }, ns);
            // ReSharper restore RedundantArgumentName

            //Categories
            context.MapRoute("admin-categories", "admin/categories", new { Controller = "Categories", action = "Index" }, ns);
            context.MapRoute("admin-categories-edit", "admin/categories/{categoryid}/edit", new { Controller = "Categories", action = "Edit" }, ns);

            context.MapRoute("admin-category-edit", "admin/categories/{categoryid}/Update", new { Controller = "Categories", action = "Edit" }, ns);
            context.MapRoute("admin-category-delete", "admin/giftcards/{categoryid}/delete", new { Controller = "Categories", action = "Delete" }, ns);
            context.MapRoute("admin-category-create", "admin/categories/new", new { Controller = "Categories", action = "Create" }, ns);
            
            //Sub Category
            context.MapRoute("admin-Sub-categories", "admin/subcategories", new { Controller = "SubCategory", action = "Index" }, ns);
            
            
            context.MapRoute("admin-sub-category-edit", "admin/subcategories/{subcategoryid}/edit", new { Controller = "SubCategory", action = "Edit" }, ns);
            context.MapRoute("admin-sub-category-update", "admin/subcategories/{subcategoryid}/Update", new { Controller = "SubCategory", action = "Edit" }, ns);
            context.MapRoute("admin-sub-category-delete", "admin/subcategories/{subcategoryid}/delete", new { Controller = "SubCategory", action = "Delete" }, ns);
            context.MapRoute("admin-sub-category-create", "admin/subcategories/new", new { Controller = "SubCategory", action = "Create" }, ns);

            //context.MapRoute("admin-Sub-categories", "admin/subcategories/new", new { Controller = "SubCategory", action = "Create" }, ns);
            //Perspectives Post
            context.MapRoute("admin-perspectives", "admin/perspectives", new { Controller = "Perspectives", action = "Index" }, ns);
            context.MapRoute("admin-perspectives-edit", "admin/perspectives/{perspectiveid}/edit", new { Controller = "Perspectives", action = "Edit" }, ns);
            context.MapRoute("admin-perspective-edit", "admin/perspectives/{perspectiveid}/Update", new { Controller = "Perspectives", action = "Edit" }, ns);
            context.MapRoute("admin-perspective-create", "admin/perspectives/new", new { Controller = "Perspectives", action = "Create" }, ns);
            context.MapRoute("admin-perspective-delete", "admin/perspective/{perspectiveid}/delete", new { Controller = "Perspectives", action = "Delete" }, ns);



            //Perspectives Post
            context.MapRoute("admin-seotool-staic-pages", "admin/seotools", new { Controller = "SEOToolStaticPages", action = "Index" }, ns);
            context.MapRoute("admin-seotool-staic-edit", "admin/seotools/{pageid}/edit", new { Controller = "SEOToolStaticPages", action = "Edit" }, ns);
            //context.MapRoute("admin-perspective-edit", "admin/perspectives/{perspectiveid}/Update", new { Controller = "Perspectives", action = "Edit" }, ns);
            context.MapRoute("admin-seotool-staic-create", "admin/seotools/new", new { Controller = "SEOToolStaticPages", action = "Create" }, ns);
            //context.MapRoute("admin-perspective-delete", "admin/perspective/{perspectiveid}/delete", new { Controller = "Perspectives", action = "Delete" }, ns);

        }
    }
}