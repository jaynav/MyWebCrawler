using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

struct LinkItem
{
    public string Href;
    public string Text;

    public override string ToString()
    {
        return Href + "\n\t" + Text;
    }
}
