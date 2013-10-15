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

    private string p; string href;
    private Queue<string> qOfURls = new Queue<string>();
    private Queue<Uri> internalQ = new Queue<Uri>();
    private Queue<Uri> externalQ = new Queue<Uri>();
    //this is for displaying purposes only
    private Queue<Uri> DisplayQ = new Queue<Uri>();

    public Crawls(string p)
    {
        // TODO: Complete member initialization
        this.p = p;
    }

    //todo need to return value to continue walking internally
    //todo need to keep track of every page to avoid infinite loop
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
                if (info.Href != null) 
                { 
                   qOfURls.Enqueue(info.Href);
                }
            }
        }
        catch (Exception)
        {
            //ignore the scripts
        }
    }

    public void ProcessLinks()
    {
        Uri inMemory = new Uri(p, UriKind.RelativeOrAbsolute);
        
        //int count = 0;
        while(qOfURls.Count > 0)
        {
            href = qOfURls.Dequeue().ToString();
          //  count++;
        
        Uri validatr = new Uri(href, UriKind.RelativeOrAbsolute);


        // make relative urls absolute 
        if (!validatr.IsAbsoluteUri)
        {
            validatr = new Uri(inMemory, validatr);
        }

        if (inMemory.IsBaseOf(validatr))
        {
            internalQ.Enqueue(validatr);
            DisplayQ.Enqueue(validatr);
        }
      }
       
       
    }

    public string DisplayCurrentCrawl()
    {
        string bob ="";
        foreach (var dat in DisplayQ)
        {
           bob += dat.ToString() +"<br />";
        }
        return bob;
        
        
    }
}
