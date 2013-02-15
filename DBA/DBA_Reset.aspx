<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBA_Reset.aspx.cs" Inherits="WebApplication4.DBA_Reset" ValidateRequest="true" ValidateRequest="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        User Name :&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Reset" runat="server" onclick="Reset_Click" Text="Reset" />
    
    </div>
    </form>
</body>
</html>
