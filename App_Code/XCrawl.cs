using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using HtmlAgilityPack;

public class XCrawl
{
    private string p;

    public XCrawl(string p)
    {
        // TODO: Complete member initialization
        this.p = p;
    }

    /// ////////////start web request---hard mental block ;need to use xpath for this method
    private WebRequest CreateRequest(string url)
    {
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Timeout = 8000;
        request.UserAgent = @"Mozilla/5.0(Windows; U; Windows NT 6.1; en-US;  rv:1.9.1.5) Gecko/20091102 FireFox/3.5.5";
        return request;
    }

    public HtmlAgilityPack.HtmlDocument LoadDoc(string uri)
    {
        var docu = new HtmlAgilityPack.HtmlDocument();
        try
        {
            using (var responseStream = CreateRequest(uri).GetResponse().GetResponseStream())
            {
                docu.Load(responseStream, Encoding.UTF8);


            }
        }
        catch (Exception)
        {
            Thread.Sleep(100);
            using (var responseStream = CreateRequest(uri).GetResponse().GetResponseStream())
            {
                docu.Load(responseStream, Encoding.UTF8);

            }
        }
        return docu;
    }
    /// ////////////end  web request---hard mental block
}
