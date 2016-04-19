<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <nav class="navbar navbar-inverse">  
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="Default2.aspx">FACE RECOGNITION</a>
        </div>
        <div id="navbar1" class="collapse navbar-collapse">
            
          <ul class="nav navbar-nav" id="navbar">
            <li><a href="#">Detect</a></li>
            <li><a id="about" href="#about">Match</a></li>
            <li class="active"><a href="#contact">Contact</a></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
      
  </nav>
        <asp:ListView ID="ListView1" runat="server"
　　　　　　>
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
　　　　　　　　　　　　<asp:Image ID="img" runat="server" Width="120" Height="90" ImageUrl='<%#Eval("id","showimage.aspx?ID={0}") %>' />
　　　　　　　　　　</td>
　　　　　　　　　　
　　　　　　　　</tr>
　　　　　　</ItemTemplate>
            
　　　　</asp:ListView>
    </form>
</body>
</html>
