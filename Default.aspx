﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="PageName" runat="server"></asp:TextBox>
        <asp:Button ID="CrawlSite" runat="server" Text="Button" 
            onclick="CrawlSite_Click" />
    
        <br />
        
        <h4>links on: <asp:Label ID="LblSiteLinks" runat="server" Text=""></asp:Label></h4>
            
        
        <asp:Literal ID="Show_data" runat="server"></asp:Literal>
    
    </div>
    </form>
</body>
</html>
