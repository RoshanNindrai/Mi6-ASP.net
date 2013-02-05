<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="M.aspx.cs" Inherits="WebApplication4.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        &nbsp;
        User Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        :
        <asp:TextBox ID="TextBox5" runat="server" Height="16px" 
            Width="269px"></asp:TextBox>
    &nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Invalid User Name " 
            Visible="False"></asp:Label>
        </p>
    <p>
        &nbsp;Current Mission&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:TextBox ID="TextBox10" runat="server" Height="16px" 
            Width="272px"></asp:TextBox>
    </p>
    <p>
        Target Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        : 
        <asp:TextBox ID="TextBox4" runat="server" Height="16px" Width="269px"></asp:TextBox>
    </p>
    <p>
&nbsp;Mission Briefing&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:TextBox ID="TextBox11" runat="server" Height="188px" Width="271px"></asp:TextBox>
    </p>
    <p>
        Target Image&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </p>
    <p>
        Encryption Key&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:TextBox ID="TextBox6" runat="server" Height="16px" Width="272px"></asp:TextBox>
    </p>
    <p>
&nbsp;
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" 
            Width="77px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Show Jobs" Width="78px" />
    </p>
</asp:Content>
