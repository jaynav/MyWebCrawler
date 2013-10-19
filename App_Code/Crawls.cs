using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.IO;
using System.Web;
using System.Xml.Linq;

public class Crawls
{  
    private string p, rs, href; //original url, // needed for looping, 
    private Queue<string> qOfURls = new Queue<string>();
    //this is for displaying purposes only
    private Queue<Uri> DisplayQ = new Queue<Uri>();
    //for processing only
    private HashSet<Uri> alreadyprocessed = new HashSet<Uri>();
    private Queue<Uri> loopingQ = new Queue<Uri>();

    public Crawls(string p)
    {
        this.p = p;
    }

    public void CrawlNow(string p)
    {
        try
        {

        WebClient easyclient = new WebClient();
        easyclient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36");
        easyclient.Headers.Add("accept-Encoding", "deflate");
        string theHtml = easyclient.DownloadString(p);
       
       /* WebHeaderCollection GetHeaders = easyclient.ResponseHeaders;
        for (int i = 0; i < GetHeaders.Count; i++)
        {
            if (GetHeaders.GetKey(i).Contains("Last-Modified"))
            {
                (GetHeaders.Get(i), p);
            }

        }*/

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
        catch( System.Net.WebException)
            {
            // do something
            }
        catch (Exception)
        {
             //catch other errors
        }
        
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
        
        while(qOfURls.Count > 0)
        {
            href = qOfURls.Dequeue().ToString();
     
        // this only excepts strings not uri objects and for the purpose of checking if internal i did it this way
        Uri validatr = new Uri(href, UriKind.RelativeOrAbsolute);
        
            // make relative urls absolute 
            if (!validatr.IsAbsoluteUri)
            {
                validatr = new Uri(inMemory, validatr);
                
            }
        if(!loopingQ.Contains(validatr))
        {
            if (inMemory.IsBaseOf(validatr))
            {
                DisplayQ.Enqueue(validatr);
                loopingQ.Enqueue(validatr);
            }
        }
      }    
    }

    public void RunBot(string p) 
    {
        Uri inMemory = new Uri(p);
        CrawlNow(p);
        ProcessLinks();
       
        if (!alreadyprocessed.Contains(inMemory))
        {
            alreadyprocessed.Add(inMemory);
        }
        
        while (loopingQ.Count > 0)
        {
            rs = loopingQ.Dequeue().ToString();
            inMemory = new Uri(rs);
           
            if (!alreadyprocessed.Contains(inMemory))
            {
                alreadyprocessed.Add(inMemory);
                Thread.Sleep(800);
                CrawlNow(rs);
                ProcessLinks();
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

    public void SaveXmlSiteMap()
    {
        
       var pathFile = HttpContext.Current.Server.MapPath("~/sitemap.xml");
       //using (FileStream der = new FileStream(pathFile, FileMode.CreateNew)) 
      using (FileStream der = new FileStream(pathFile, FileMode.Create))
      {
          using (StreamWriter xwriter = new StreamWriter(der))
          {
              
              XNamespace xns = "http://www.sitemaps.org/schemas/sitemap/0.9";
              XNamespace w3nsp = "http://wwww.w3.org/2001/XMLSchema-instance";

              var xdoc = new XDocument(
                  new XDeclaration("1.0","UTF-8", null),
                  new XElement(xns + "urlset",
                      new XAttribute(XNamespace.Xmlns + "xsi", "http://wwww.w3.org/2001/XMLSchema-instance"),
                      new XAttribute(w3nsp + "schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                      new XElement(xns +"url",
                          new XElement(xns + "loc"),
                          new XElement(xns + "lastmod"))));
             //xdoc.Save(xwriter);
              xwriter.Write(xdoc);
          }
      }
       
    }
}
