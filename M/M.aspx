<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="M.aspx.cs" Inherits="WebApplication4.WebForm3" ValidateRequest="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        &nbsp;
        User Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        :
        <asp:TextBox ID="TextBox5" runat="server" Height="16px" 
            Width="269px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="TextBox5" ErrorMessage="value is required." 
                                        ToolTip="value is required.">*</asp:RequiredFieldValidator>
    &nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
            Text="Please enter a valid user information" Visible="False"></asp:Label>
        </p>
    <p>
        &nbsp;Current Mission&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:TextBox ID="TextBox10" runat="server" Height="16px" 
            Width="272px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="TextBox10" ErrorMessage="value is required." 
                                        ToolTip="value is required.">*</asp:RequiredFieldValidator>
    &nbsp;
    </p>
    <p>
        Target Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        : 
        <asp:TextBox ID="TextBox4" runat="server" Height="16px" Width="269px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="TextBox4" ErrorMessage="value is required." 
                                        ToolTip="value is required.">*</asp:RequiredFieldValidator>
    </p>
    <p>
&nbsp;Mission Briefing&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:TextBox ID="TextBox11" runat="server" Height="188px" Width="271px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="TextBox11" ErrorMessage="value is required." 
                                        ToolTip="value is required.">*</asp:RequiredFieldValidator>
    </p>
    <p>
        Target Image&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="FileUpload1" ErrorMessage="value is required." 
                                        ToolTip="value is required.">*</asp:RequiredFieldValidator>
    </p>
    <p>
        &nbsp;</p>
    <p>
&nbsp;
        &nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
            Text="Submit" />
&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" CausesValidation="false" onclick="Button2_Click1" 
            Text="job reset" />
    &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" ForeColor="Red" 
            Text="you cant add a job now" Visible="False"></asp:Label>
    </p>
</asp:Content>
