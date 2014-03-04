
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Britespokes.Web
{
    public partial class DownloadPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["url"] != null && Request.QueryString["url"].Trim() != "")
            {
                string fileURL = Request.QueryString["url"];
                ConvertToStream(fileURL);
            }
        }

        private static void ConvertToStream(string fileUrl)
        {
            using (WebClient wc = new WebClient())
            {
                
                byte[] data = wc.DownloadData(fileUrl);
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=itinerary.pdf");
                HttpContext.Current.Response.ContentType="application/pdf";
                HttpContext.Current.Response.BinaryWrite(data);
            }
        }
    }
}