using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class IsUrlCorrect
{
    private string p;

    public  IsUrlCorrect(string p)
    {
        // TODO: Complete member initialization
        this.p = p;

        
    }

    public string FixUrl()
    {
        
            //(?:http://)|(?:https://)
            if (Regex.IsMatch(p, @"\b(?:http://)|\b(?:https://)|\b(?:ftp://)"))
            {
                return p;
            }
            else
            {
                return p = "http://" + p;
            }
        }



    public bool CheckValue()
    {
        if (p == "") 
        {
            return false;
        }
        return true;
    }
}
