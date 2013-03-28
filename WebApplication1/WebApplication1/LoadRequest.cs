using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.Threading;


namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        //load the document and call the page
        public void LoadDoc(string siteToCraw)
        {
            //this works
            WebClient theClient = new WebClient();
            string derhtml = theClient.DownloadString(siteToCraw);
            HtmlDocument droc = new HtmlDocument();
            droc.LoadHtml(derhtml);

            //to do add code that transfers the document to in memory code scrape links and save to database(save to database needs to be a new thread though)
        }
            
       
    }
}