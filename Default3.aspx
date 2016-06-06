<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
           <li class="active"><asp:LinkButton ID="detect" runat="server" OnClick="detect_Click">detect</asp:LinkButton></li>
              
            <li><asp:LinkButton ID="match" runat="server" OnClick="match_Click">Match</asp:LinkButton></li>
            <li><asp:LinkButton ID="facedata" runat="server" OnClick="facedata_Click">Facedata</asp:LinkButton></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
      
  </nav>
  <div class="container">   
  <div class="row" style="">
      
  <div class="col-lg-6">
    <div class="input-group">
      <div style="position:absolute;z-index:999999;opacity:0;">
           <asp:FileUpload ID="FileUpload1" runat="server" Height="35px" Width="105px"/>
      </div>  
      <span class="input-group-btn">
        <asp:Button ID="Button1" runat="server" Text="Open Image" CssClass="btn btn-default"/>
      </span>
     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  placeholder="Search for..."></asp:TextBox>
      <span class="input-group-btn">
        <asp:Button ID="Button2" runat="server" Text="Detect" CssClass="btn btn-default"  OnClick="Button1_Click"/>
      </span>
    </div><!-- /input-group -->
      
  </div><!-- /.col-lg-6 -->
   
  </div>  
       <br />
      
             
     <span class="label label-default">Max roll angle deviation</span>
     <asp:DropDownList ID="cbRollAngle" runat="server"></asp:DropDownList>
      &nbsp&nbsp&nbsp
      <span class="label label-default">Max yaw angle deviation</span>
     <asp:DropDownList ID="cbYawAngle" runat="server"></asp:DropDownList>     
       <div id="fontzoom">
          
              
             
      </div>
       <br />
       <asp:Image ID="Image1" runat="server" ImageUrl="~/bitmapload.aspx"/>
       <br />
       <asp:ListView ID="ListView1" runat="server">
           <LayoutTemplate>
　　　　　　　　<table id="Table1" runat="server" border="1" style="" >
　　　　　　　　　　<thead>
                        <tr>
                            <th>Blink</th>
                            <th>Emotion</th>
                            <th>Expression</th>
                            <th>Glasses</th>
                            <th>Gender</th>
                            <th>Mouth</th>
                        </tr>
                    </thead>
                    <tbody>
　　　　　　　　　　    <tr runat="server" id="itemPlaceholder" />
                    </tbody>
　　　　　　　　</table>
　　　　　　</LayoutTemplate>
　　　　　　<ItemTemplate>
　　　　　　　　<tr runat="server" id="Tr"  style="align-content:center" >
                    <td>
　　　　　　　　　　　　<%#Eval("blink") %>
　　　　　　　　　　</td>
　　　　　　　　　　<td>
　　　　　　　　　　　　<%#Eval("emotion") %>
　　　　　　　　　　</td>
　　　　　　　　　　<td>
　　　　　　　　　　　　<%#Eval("expression") %>
　　　　　　　　　　</td>
                    <td>
                        <%#Eval("glasses") %>
                    </td>
                    <td>
                        <%#Eval("gender") %>
                    </td>
　                  <td>
                       <%#Eval("mouth") %>
　                  </td>　　　　　　　
            　　</tr>
　　　　　　</ItemTemplate>
       </asp:ListView>
      
  </div>
      
         
        
    <script src="js/jquery.js"></script>
     <script type="text/javascript">
    function HandleFileButtonClick()
    {
        //document.form1.FileUpload1.click();
        
        var a = document.getElementById("FileUpload1");
        a.click();
        //var b = document.getElementById("TextBox1");
        

       
    }
    function test2() {
        var title = document.getElementById("FileUpload1").value;
        document.getElementById("TextBox1").value = title;
    }
    </script>
    <script >
        $(document).ready(function () {
            var $navbar = $('#navbar');
            $navbar.on('click', 'li', function (e) {
                $(this).addClass('active').siblings().removeClass('active');
            })
        })
        
    </script>
    </form>
</body>
</html>
