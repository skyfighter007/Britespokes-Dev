using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.Tours
{
    public class TourCommandService : ITourCommandService
    {
        private readonly IRepository<Tour> _tourRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Experience> _experienceRepo;

        public TourCommandService(IRepository<Tour> tourRepo, IRepository<Product> productRepo, IRepository<Experience> experienceRepo, IRepository<Category> categoryRepo)
        {
            _tourRepo = tourRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _experienceRepo = experienceRepo;
        }

        public void Add(TourUpdate tourUpdate)
        {
           // var category = _categoryRepo.All.Where(a => a.Id == tourUpdate.Category_Id).FirstOrDefault();
            var categories = _categoryRepo.All.Where(a => tourUpdate.SelectedCategories.Contains(a.Id));
            var tour = new Tour
            {
                Code = tourUpdate.Code,
                Name = tourUpdate.Name,
                Permalink = tourUpdate.Permalink,
                AvailabilityStatusId = tourUpdate.AvailabilityStatusId,
                TourTypeId = tourUpdate.TourTypeId,
                IsPublished = tourUpdate.IsPublished,
                LengthDescription = tourUpdate.LengthDescription,
                DepartureCity = tourUpdate.DepartureCity,
                ReturningCity = tourUpdate.ReturningCity,
                MailingListEmailName = tourUpdate.MailingListEmailName,
                MailingListUrl = tourUpdate.MailingListUrl,
                MailingListID = tourUpdate.MailingListID,
                SampleDocumentURL = tourUpdate.SampleDocumentURL
                
            };

            SEOTool SEOTool = new Core.Domain.SEOTool();
            SEOTool.FocusKeyword = tourUpdate.FocusKeyword;
            SEOTool.MetaDescription = tourUpdate.MetaDescription;
            SEOTool.SEOTitle = tourUpdate.SEOTitle;
            tour.SEOTools = new List<Core.Domain.SEOTool>();
            tour.SEOTools.Add(SEOTool);


            tour.Categories = new System.Collections.Generic.List<Category>();
            tour.Categories.AddRange(categories.ToList());
            _tourRepo.Add(tour);
        }

        public void Update(TourUpdate tourUpdate)
        {
            var categories = _categoryRepo.All.Where(a => tourUpdate.SelectedCategories.Contains(a.Id));
            var tour = _tourRepo.Find(tourUpdate.Id);

            tour.Code = tourUpdate.Code;
            tour.Name = tourUpdate.Name;
            tour.Permalink = tourUpdate.Permalink;
            tour.AvailabilityStatusId = tourUpdate.AvailabilityStatusId;
            tour.TourTypeId = tourUpdate.TourTypeId;
            tour.IsPublished = tourUpdate.IsPublished;
            tour.LengthDescription = tourUpdate.LengthDescription;
            tour.DepartureCity = tourUpdate.DepartureCity;
            tour.ReturningCity = tourUpdate.ReturningCity;
            tour.MailingListEmailName = tourUpdate.MailingListEmailName;
            tour.MailingListUrl = tourUpdate.MailingListUrl;
            tour.MailingListID = tourUpdate.MailingListID;
            tour.SampleDocumentURL = tourUpdate.SampleDocumentURL;
            


            SEOTool SEOTool = null;

            if (tour.SEOTools != null && tour.SEOTools.Count() > 0)
                SEOTool = tour.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                tour.SEOTools = new List<Core.Domain.SEOTool>();
                tour.SEOTools.Add(SEOTool);
            }
            SEOTool.FocusKeyword = tourUpdate.FocusKeyword;
            SEOTool.MetaDescription = tourUpdate.MetaDescription;
            SEOTool.SEOTitle = tourUpdate.SEOTitle;
            



            List<Category> exixtingCategories = tour.Categories.ToList();

            foreach (Category c in exixtingCategories)
                tour.Categories.Remove(c);
            //tour.Categories = null;
            tour.Categories = new System.Collections.Generic.List<Category>();
            tour.Categories.AddRange(categories.ToList());
            _tourRepo.Update(tour);
        }

        public void UpdateMeta(TourMetaUpdate tourMetaUpdate)
        {
            var tour = _tourRepo.Find(tourMetaUpdate.TourId);
            if (tour.TourMeta == null)
                tour.TourMeta = new TourMeta();

            tour.TourMeta.Description = tourMetaUpdate.Description;
            tour.TourMeta.BannerImageUrl = tourMetaUpdate.BannerImageUrl;
            tour.TourMeta.BannerImageAltText = tourMetaUpdate.BannerImageAltText;
            tour.TourMeta.PriceIncludesHeader = tourMetaUpdate.PriceIncludesHeader;
            tour.TourMeta.PriceIncludes1 = tourMetaUpdate.PriceIncludes1;
            tour.TourMeta.PriceIncludes2 = tourMetaUpdate.PriceIncludes2;
            tour.TourMeta.PriceIncludesHeader2 = tourMetaUpdate.PriceIncludesHeader2;
            tour.TourMeta.PriceIncludes3 = tourMetaUpdate.PriceIncludes3;
            tour.TourMeta.PriceIncludes4 = tourMetaUpdate.PriceIncludes4;
            tour.TourMeta.BriterPriceIncludesHeader1 = tourMetaUpdate.BriterPriceIncludesHeader1;
            tour.TourMeta.BriterPriceIncludes1 = tourMetaUpdate.BriterPriceIncludes1;
            tour.TourMeta.BriterPriceIncludes2 = tourMetaUpdate.BriterPriceIncludes2;
            tour.TourMeta.BriterPriceIncludesHeader2 = tourMetaUpdate.BriterPriceIncludesHeader2;
            tour.TourMeta.BriterPriceIncludes3 = tourMetaUpdate.BriterPriceIncludes3;
            tour.TourMeta.BriterPriceIncludes4 = tourMetaUpdate.BriterPriceIncludes4;
            tour.TourMeta.AdditionalInformationHeader = tourMetaUpdate.AdditionalInformationHeader;
            tour.TourMeta.AdditionalInformation1 = tourMetaUpdate.AdditionalInformation1;
            tour.TourMeta.AdditionalInformation2 = tourMetaUpdate.AdditionalInformation2;
            tour.TourMeta.ThumbnailImageUrl = tourMetaUpdate.ThumbnailImageUrl;
            tour.TourMeta.ThumbnailImageAltText = tourMetaUpdate.ThumbnailImageAltText;
            tour.TourMeta.ThumbnailCaption = tourMetaUpdate.ThumbnailCaption;
            tour.TourMeta.JourneyDescription = tourMetaUpdate.JourneyDescription;
            tour.IsPublished = tourMetaUpdate.TourIsPublished;

            _tourRepo.Update(tour);
        }

        public void Delete(int tourId)
        {
            // mark all the associated products as Deleted
            var tour = _tourRepo.Find(tourId);
            var products = tour.Departures.Select(d => d.Product);
            foreach (var product in products)
            {
                product.DeletedAt = DateTime.UtcNow;
                _productRepo.Update(product);
            }
            // delete the tour
            _tourRepo.Delete(tourId);
        }

        public void AddExperience(Tour tour)
        {
            if (_experienceRepo.FindBy(e => e.TourId == tour.Id).Any())
                return;

            var lastPosition = 0;
            if (_experienceRepo.All.Any())
                lastPosition = _experienceRepo.All.Max(e => e.Position);
            _experienceRepo.Add(new Experience
            {
                TourId = tour.Id,
                Position = lastPosition + 1
            });
        }

        public void RemoveExperience(Tour tour)
        {
            var exp = _experienceRepo.FindBy(e => e.TourId == tour.Id).SingleOrDefault();
            if (exp != null)
            {
                _experienceRepo.Delete(exp);
            }
        }

        public void AddExperience(int tourId)
        {
            var tour = _tourRepo.Find(tourId);
            AddExperience(tour);
        }

        public void RemoveExperience(int tourId)
        {
            var tour = _tourRepo.Find(tourId);
            RemoveExperience(tour);
        }
    }
}