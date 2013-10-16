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

    private string p; //original url 
    string g;
    private string rs;// needed for looping 
    string href;
    private Queue<string> qOfURls = new Queue<string>();
    private Queue<Uri> internalQ = new Queue<Uri>();
    //this is for displaying purposes only
    private Queue<Uri> DisplayQ = new Queue<Uri>();
    //for processing only
    private HashSet<Uri> alreadyprocessed = new HashSet<Uri>();
    private Queue<Uri> loopingQ = new Queue<Uri>();

    public Crawls(string p)
    {
        // TODO: Complete member initialization
        this.p = p;
        g = p;
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
///creates an instance of the scraper;creates a generic list  class; 
///then adds to the list the links it finds on the url requested
/// then it adds url to queue
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
            
            //record 404 errors
        }
        ProcessLinks();
    }
    /// <summary>
    /// after it gets the hyperlinks, it needs to process them, put them in a absolute uri format for the purpose
    /// of creating a sitemap and check if hyperlink is an internal hyperlink, if not trash it.
    /// it also creates a special queue for the purpose of displaying on web page. not really needed but useful to see what it crawled
    /// this allows me to see
    /// 
    /// -------------for future use, if it is external only get the base url. its easier that way-----------
    /// </summary>
    public void ProcessLinks()
    {
        Uri inMemory = new Uri(p, UriKind.RelativeOrAbsolute);
        Uri derLoop = new Uri(g);
        //int count = 0;
        while(qOfURls.Count > 0)
        {
            href = qOfURls.Dequeue().ToString();
          //  count++;
        // this only excepts strings not uri objects and for the purpose of checking if internal i did it this way
        Uri validatr = new Uri(href, UriKind.RelativeOrAbsolute);
        
            // make relative urls absolute 
            if (!validatr.IsAbsoluteUri)
            {
                validatr = new Uri(inMemory, validatr);
                
            }
        if(!internalQ.Contains(validatr))
        {
            if (inMemory.IsBaseOf(validatr))
            {
                internalQ.Enqueue(validatr);
                DisplayQ.Enqueue(validatr);
                loopingQ.Enqueue(validatr);
            }
        }
      }//end while
        //make to its own method
        if(!alreadyprocessed.Contains(inMemory))
        {
            alreadyprocessed.Add(inMemory);
        }
        while (loopingQ.Count > 0)
        {
             rs = loopingQ.Dequeue().ToString();
            derLoop = new Uri(rs);
            if(!alreadyprocessed.Contains(derLoop))
            {
                alreadyprocessed.Add(derLoop);
                Thread.Sleep(1000);
                CrawlNow(rs);
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
