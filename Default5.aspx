<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Default5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <asp:ListView ID="ListView1" runat="server">
            <LayoutTemplate>
　　　　　　　　<table id="Table1" runat="server" border="0" style="">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>SCORE</th>
                            <th>FACE</th>
                        </tr>
                    </thead>
                    <tbody>
　　　　　　　　　　    <tr runat="server" id="itemPlaceholder" />
                    </tbody>
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
    </div>
    </form>
</body>
</html>
