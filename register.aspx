<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                     <span class="label-default">username</span>
                </td>
                <td>
                     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="label-default">password</span>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" CssClass="form-control"  placeholder=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="Button1_Click"/>
                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-default"/> 
                </td>
            </tr>
        </table>        
    </div>
    </form>
</body>
</html>
