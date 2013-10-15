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
        Crawls crawl = new Crawls(PageName.Text);
      //using http web request

        // XCrawl loadViaXpath = new XCrawl(PageName.Text);
        // var demo =crawl.LoadDoc(PageName.Text);
       
        //uses webclient instead...
        crawl.CrawlNow(PageName.Text);
        crawl.ProcessLinks();
    }
    
 

    /////////////////////////////////////////////////////////////////////////////////////////////////////////



}