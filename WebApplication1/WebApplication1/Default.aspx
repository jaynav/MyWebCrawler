<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>
        Search a site </h1>
    <p>
        enter site to search - search king ----beta</p>
    <script type="text/javascript">
        function ValidateForEmpty(source, args) {
            var siteIdData = document.getElementById('<%= SiteId.ClientID %>');
            if (siteIdData.value != '') 
            {
                args.IsValid = true;  

            }
            else
            {args.IsValid = false;}
        }
    </script>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" HeaderText="may not be empty" />
    <asp:CustomValidator ID="CustomValidator1" runat="server"
     Display="Dynamic" ErrorMessage="empty" Text="*" ClientValidationFunction="ValidateForEmpty"
        onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
        
        <p>
            &nbsp;<asp:UpdatePanel ID="up1" runat="server">
       <ContentTemplate>
       <asp:TextBox ID="SiteId" runat="server"></asp:TextBox>
        <asp:Button ID="SearchSite" runat="server" Text="start crawler" 
               onclick="SearchSite_Click" />
        <asp:Label ID="dermessage" runat="server" Text="message sent" Visible ="false" />
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID ="up1">
        <ProgressTemplate>
        please wait...
        </ProgressTemplate>
        </asp:UpdateProgress>
    </p>
</asp:Content>
