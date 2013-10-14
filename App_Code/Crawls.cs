using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using HtmlAgilityPack;

public class Crawls
{  //todo need to display links qOfURLs.Dequeue
    //need to process local links only(some logic needed) and then crawl that page(easy)
    //

    private string p;
    private Queue<string> qOfURls = new Queue<string>();

    public Crawls(string p)
    {
        // TODO: Complete member initialization
        this.p = p;
    }

    public void CrawlNow(string p)
    {
        try
        {

        WebClient easyclient = new WebClient();
        string theHtml = easyclient.DownloadString(p);

        // load and holds html

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(theHtml);
 
//creates an instance of the scraper;creates a generic list  class; 
//then addes to the list and thenwhile running  the scrapper
// then it adds url to queue
        FindsLinksViaRegex linkfinder = new FindsLinksViaRegex();
        List<LinkItem> linkitm = new System.Collections.Generic.List<LinkItem>();

        linkitm.AddRange(linkfinder.Find(theHtml));

            foreach (LinkItem info in linkitm)
            { 
            qOfURls.Enqueue(info.Href);
            }
        }
        catch (Exception)
        {
            //ignore the scripts
        }
    }
}
