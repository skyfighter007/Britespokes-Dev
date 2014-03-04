using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.GiftCards;
using Britespokes.Web.Areas.Admin.Models.GiftCards;

namespace Britespokes.Web.Areas.Admin.Controllers
{
    public class GiftCardsController : AdminBaseController
    {
        private readonly IGiftCardQueryService _giftcardQueryService;
        private readonly IGiftCardCommandService _giftcardCommandService;

        public GiftCardsController(IGiftCardQueryService giftcardQueryService, IGiftCardCommandService giftcardCommandService)
        {
            _giftcardQueryService = giftcardQueryService;
            _giftcardCommandService = giftcardCommandService;
        }

        public ActionResult Index()
        {
            var giftcards = _giftcardQueryService.GiftCards();
            return View(new GiftCardsIndex
            {
                Count = giftcards.Count(),
                GiftCards = giftcards
            });
        }


        public ActionResult Create()
        {
            var giftcardEdit = new GiftCardEdit
            {

            };
            return View(giftcardEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GiftCardEdit giftcardEdit)
        {
            if (_giftcardQueryService.IsCodeUnique(giftcardEdit.GiftCode))
                ModelState.AddModelError("GiftCode", "Gift code must be unique");

            if (ModelState.IsValid)
            {
                var giftcardUpdate = Mapper.Map<GiftCardUpdate>(giftcardEdit);

                _giftcardCommandService.Add(giftcardUpdate);
                return RedirectToRoute("admin-giftcards");
            }
            return View(giftcardEdit);
        }


        public ActionResult Edit(int giftcardId)
        {
            var giftcard = _giftcardQueryService.FindGiftCard(giftcardId);
            var giftcardEdit = Mapper.Map<GiftCardEdit>(giftcard);

            if (giftcard.SEOTools != null && giftcard.SEOTools.Count > 0)
            {
                SEOTool SEOTool = giftcard.SEOTools.First();
                giftcardEdit.SEOTitle = SEOTool.SEOTitle;
                giftcardEdit.FocusKeyword = SEOTool.FocusKeyword;
                giftcardEdit.MetaDescription = SEOTool.MetaDescription;
            }


            return View(giftcardEdit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GiftCardEdit giftcardEdit)
        {
            if (_giftcardQueryService.IsCodeUnique(giftcardEdit.GiftCode, giftcardEdit.Id))
                ModelState.AddModelError("GiftCode", "Gift code must be unique");

            if (!ModelState.IsValid)
            {
                return View(giftcardEdit);
            }

            var giftcardUpdate = Mapper.Map<GiftCardUpdate>(giftcardEdit);
            _giftcardCommandService.Update(giftcardUpdate);
            TempData["Info"] = "GiftCard updated";
            return RedirectToRoute("admin-giftcard-edit", new { giftcardEdit.Id });
        }


        [HttpPost]
        public ActionResult Destroy(int giftcardId)
        {
            try
            {
                _giftcardCommandService.Delete(giftcardId);
            }
            catch (DbUpdateException dbex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
                TempData["Error"] = "This giftcard could not be deleted.";
                return RedirectToRoute("admin-giftcard-edit", new { giftcardId });
            }
            return RedirectToRoute("admin-giftcards");
        }
    }
}
