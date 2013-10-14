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
    public class Validator : _Default
    {
     /// <summary>
       /// this is a custom validator to check for empty string 
       /// </summary>
       /// <param name="source"></param>
       /// <param name="args"></param>
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(SiteId.Text))
            {
                args.IsValid = true;
            }

        }
       
    }
}