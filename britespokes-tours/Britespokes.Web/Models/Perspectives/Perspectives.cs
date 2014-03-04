using System.Collections.Generic;
using Britespokes.Core.Domain;
using System;
using System.Web.Mvc;
using System.Globalization;
using Britespokes.Web.Helpers;
using Britespokes.Web.Models.Comments;
using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Models.Perspectives
{
    public class LatestPerspectivePost
    {
        public List<Tour> Tours { get; set; }
        public List<PerspectivePost> PerspectivePosts { get; set; }
        public List<string> MonthName { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int Count { get; set; }
        public int? SelectedMonth { get; set; }
        public string selectedtourpermalink{ get; set; }
        //public Comment Comments { get; set; }
        //public CommentEdit Comment { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime? PostedOn { get; set; }
        public bool IsApproved { get; set; }

        public string FocusKeyword { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }


        public SelectList MonthList
        {
            get
            {
                return new SelectList(AllMonths, "Index", "Name", SelectedMonth);
            }
        }

        public static IEnumerable<MonthInfo> AllMonths
        {
            get
            {
                CultureInfo info =
                   System.Threading.Thread.CurrentThread.CurrentCulture;

                int index = 1;
                foreach (var monthName in info.DateTimeFormat.MonthNames)
                {
                    if (monthName == "")
                        break;
                    yield return new MonthInfo
                    {
                        Index = index,
                        Name = monthName,
                    };
                    ++index;
                }
            }
        }

        public class MonthInfo
        {
            public int Index { get; set; }
            public string Name { get; set; }
        }

    }
    public class CategoryPerspectivePost
    {
        public Category Categories { get; set; }
        public List<string> MonthName { get; set; }

        public int? SelectedMonth { get; set; }

        public SelectList MonthList
        {
            get
            {
                return new SelectList(AllMonths, "Index", "Name", SelectedMonth);
            }
        }

        public static IEnumerable<MonthInfo> AllMonths
        {
            get
            {
                CultureInfo info =
                   System.Threading.Thread.CurrentThread.CurrentCulture;

                int index = 1;
                foreach (var monthName in info.DateTimeFormat.MonthNames)
                {
                    if (monthName == "")
                        break;
                    yield return new MonthInfo
                    {
                        Index = index,
                        Name = monthName,
                    };
                    ++index;
                }
            }
        }

        public class MonthInfo
        {
            public int Index { get; set; }
            public string Name { get; set; }
        }

    }
   
}