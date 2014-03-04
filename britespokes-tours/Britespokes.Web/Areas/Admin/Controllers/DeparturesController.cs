using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Services.Admin.Departures;
using Britespokes.Services.Admin.Tours;
using Britespokes.Services.Admin.Organizations;
using Britespokes.Web.Areas.Admin.Models.Tours;
using Britespokes.Core.Domain;
using System;

namespace Britespokes.Web.Areas.Admin.Controllers
{
    public class DeparturesController : AdminBaseController
    {
        private readonly IDepartureQueryService _departureQueryService;
        private readonly IDepartureCommandService _departureCommandService;
        private readonly ITourQueryService _tourQueryService;
        private readonly IOrganizationService _organizationService;

        public DeparturesController(IDepartureQueryService departureQueryService, IDepartureCommandService departureCommandService, ITourQueryService tourQueryService, IOrganizationService organizationService)
        {
            _departureQueryService = departureQueryService;
            _departureCommandService = departureCommandService;
            _tourQueryService = tourQueryService;
            _organizationService = organizationService;
        }

        public ActionResult Index(int tourId)
        {
            var tour = _tourQueryService.FindTour(tourId);
            var departures = _departureQueryService.Departures(tour);

            if (tour == null || departures == null)
                return HttpNotFound();

            var deps = departures.ToArray();
            return View(new DeparturesIndex
            {
                TourId = tour.Id,
                TourName = tour.Name,
                TourCode = tour.Code,
                TourPermalink = tour.Permalink,
                Departures = deps,
                Count = deps.Count()
            });
        }

        public ActionResult Edit(int productId)
        {
            var departure = _departureQueryService.FindDeparture(productId);
            var departureEdit = Mapper.Map<DepartureEdit>(departure);

            departureEdit.OrganizationList = _organizationService.Organizations();
            departureEdit.OrganizationModel = new System.Collections.Generic.List<string>();
            foreach (Organization org in departure.Organizations)
                departureEdit.OrganizationModel.Add(org.Id.ToString());

            departureEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
            return View(departureEdit);
        }

        [HttpPost]
        public ActionResult Edit(DepartureEdit departureEdit)
        {
            if (_departureQueryService.IsCodeUnique(departureEdit.Code, departureEdit.ProductId))
                ModelState.AddModelError("Code", "Departure code must be unique");
            if (!ModelState.IsValid)
            {
                departureEdit.OrganizationList = _organizationService.Organizations();
                departureEdit.OrganizationModel = new System.Collections.Generic.List<string>();
                departureEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
                return View(departureEdit);
            }

            departureEdit.SelectedOrganizations = new System.Collections.Generic.List<int>();
            foreach (string s in departureEdit.OrganizationModel)
                departureEdit.SelectedOrganizations.Add(Convert.ToInt32(s));

            var departureUpdate = Mapper.Map<DepartureUpdate>(departureEdit);
            _departureCommandService.Update(departureUpdate);
            TempData["Info"] = "Departure updated.";
            return RedirectToRoute("admin-departure-edit", new { productId = departureEdit.ProductId });
        }

        public ActionResult Create(int tourId)
        {
            var tour = _tourQueryService.FindTour(tourId);
            var departureEdit = new DepartureEdit
            {
                TourId = tourId,
                TourName = tour.Name,
                TourPermalink = tour.Permalink,
                AvailabilityStatusList = _tourQueryService.Availabilities(),
                OrganizationList = _organizationService.Organizations(),
                OrganizationModel = new System.Collections.Generic.List<string>()
            };
            return View(departureEdit);
        }

        [HttpPost]
        public ActionResult Create(DepartureEdit departureEdit)
        {
            if (_departureQueryService.IsCodeUnique(departureEdit.Code))
                ModelState.AddModelError("Code", "Departure code must be unique");

            departureEdit.SelectedOrganizations = new System.Collections.Generic.List<int>();
            foreach (string s in departureEdit.OrganizationModel)
                departureEdit.SelectedOrganizations.Add(Convert.ToInt32(s));



            if (ModelState.IsValid)
            {
                var departureUpdate = Mapper.Map<DepartureUpdate>(departureEdit);
                var departure = _departureCommandService.Add(departureUpdate);
                return RedirectToRoute("admin-departure-edit", new { productId = departure.ProductId });
            }

            if (departureEdit.OrganizationModel == null)
                departureEdit.OrganizationModel = new System.Collections.Generic.List<string>();
            departureEdit.OrganizationList = _organizationService.Organizations();

            departureEdit.AvailabilityStatusList = _tourQueryService.Availabilities();
            return View(departureEdit);
        }

        [HttpPost]
        public ActionResult Destroy(int tourId, int productId)
        {
            try
            {
                _departureCommandService.Delete(productId);
            }
            catch (DbUpdateException dbex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
                TempData["Error"] = "This departure could not be deleted.";
                return RedirectToRoute("admin-departure-edit", new { productId });
            }
            return RedirectToRoute("admin-departures", new { tourId });
        }
    }
}