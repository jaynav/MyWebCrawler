using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class FindsLinksViaRegex
{     //this is the actual web scrapper 

    public  List<LinkItem> Find(string file)
        {
            List<LinkItem> linklist = new List<LinkItem>();

           
            // Find all matches in file.
            MatchCollection anchorFind = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

           
            // Loop over each match.
            foreach (Match theAnchr in anchorFind)
            {
                
                string urivalue = theAnchr.Groups[1].Value;
                LinkItem webitem = new LinkItem();

                
                // Get href attribute.
                Match hyperRef = Regex.Match(urivalue, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
                if (hyperRef.Success)
                {
                    webitem.Href = hyperRef.Groups[1].Value;
                }

                
                // Remove inner tags from text.
                string tag = Regex.Replace(urivalue, @"\s*<.*?>\s*", "",
                RegexOptions.Singleline);
                webitem.Text = tag;

                linklist.Add(webitem);
            }
            return linklist;
        }
}
