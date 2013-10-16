using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Net;
using System.Threading;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CrawlSite_Click(object sender, EventArgs e)
    {
       IsUrlCorrect theUrl = new IsUrlCorrect(PageName.Text);
          
        if (theUrl.CheckValue() == true)
       {
           string siteUrl = theUrl.FixUrl();
           LblSiteLinks.Text = siteUrl;

           RunCrawl(siteUrl);
         
       }

       else
       {
           Show_data.Text =
               "there was no website added, please input data like so: https://example.com <br /> " +
               "if you input data like so: example.com , the default input will be http://example.com" ;
       }
   }

    private void RunCrawl(string siteUrl)
    {
        Crawls crawl = new Crawls(siteUrl);
        //using http web request......not finished.
       
        // XCrawl loadViaXpath = new XCrawl(PageName.Text);
        // var demo =crawl.LoadDoc(PageName.Text);

        //uses webclient instead...
        crawl.CrawlNow(siteUrl);
        //crawl.ProcessLinks();
         Show_data.Text = crawl.DisplayCurrentCrawl(); 
    }

    

    
 

    /////////////////////////////////////////////////////////////////////////////////////////////////////////



}