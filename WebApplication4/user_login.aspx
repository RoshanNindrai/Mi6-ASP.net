<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="user_login.aspx.cs"  %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
       Welcome to MI6 webpage
    </h2>
<br />
<asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
        <asp:Login ID="Login1" runat="server">
        </asp:Login>
    </AnonymousTemplate>
</asp:LoginView>
</asp:Content>
