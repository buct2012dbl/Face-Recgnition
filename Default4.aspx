<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    
    </div>
        <asp:ListView ID="ListView1" runat="server">
            <LayoutTemplate>
　　　　　　　　<table id="Table1" runat="server" border="0" style="">
　　　　　　　　　　<tr runat="server" id="itemPlaceholder" />
　　　　　　　　</table>
　　　　　　</LayoutTemplate>
　　　　　　<ItemTemplate>
　　　　　　　　<tr runat="server" id="Tr">
                    <td>
　　　　　　　　　　　　<%#Eval("id") %>
　　　　　　　　　　</td>
　　　　　　　　　　<td>
　　　　　　　　　　　　<%#Eval("score") %>
　　　　　　　　　　</td>
　　　　　　　　　　<td>
　　　　　　　　　　　　<asp:Image ID="img" runat="server" Width="120" Height="90" ImageUrl='<%#Eval("id","showimage.aspx?ID={0}") %>' />
　　　　　　　　　　</td>
　　　　　　　　　　
　　　　　　　　</tr>
　　　　　　</ItemTemplate>
        </asp:ListView>
    </form>
</body>
</html>
