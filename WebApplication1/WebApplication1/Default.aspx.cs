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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

      
        /// <summary>
        /// this executes when button is clicked, then goes to load request where it pulls up the page requested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchSite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
               //to do: add further validation of string:
                string siteToCraw = SiteId.Text;

 //calling out starting crawler
                LoadDoc(siteToCraw);
            }

        }
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
