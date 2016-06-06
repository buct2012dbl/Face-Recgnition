<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <title></title>
    <style>
       .box{width: 1000px;height: auto;padding-left:100px}
       .box ul{width: 100%;height: auto;float: left;}
       .box ul li{width: 180px;height: 80px;float: left;}
    </style>
    <style type="text/css"> 
       .bb 
       { 
           padding-bottom: 1px; 
           padding-top: 0px; 
       } 
       .list
       {
           padding-left:100px
       }
    </style> 
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <li><asp:LinkButton ID="detect" runat="server" OnClick="detect_Click">detect</asp:LinkButton></li>
              
            <li class="active"><asp:LinkButton ID="match" runat="server" OnClick="match_Click">Match</asp:LinkButton></li>
            <li><asp:LinkButton ID="facedata" runat="server" OnClick="facedata_Click">Facedata</asp:LinkButton></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
      
  </nav>
   </div>
   
  <div class="container">   
  <div class="row" style="">
      
  <div class="col-lg-6">
    <div class="input-group">
      <div style="position:absolute;z-index:999999;opacity:0;">
           <asp:FileUpload ID="FileUpload1" runat="server" Height="35px" Width="105px" onchange="test2();"/>
        </div>  
      <span class="input-group-btn">
        <asp:Button ID="Button2" runat="server" Text="Open Image" CssClass="btn btn-default"/>
      </span>
     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  placeholder="Search for..."></asp:TextBox>
      <span class="input-group-btn">
        <asp:Button ID="Button3" runat="server" Text="Match" CssClass="btn btn-default"  OnClick="Button1_Click"/>
      </span>
    </div><!-- /input-group -->
      
  </div><!-- /.col-lg-6 -->
   
  </div>

    
    
        <asp:ListView ID="ListView1" runat="server">
            <LayoutTemplate>
　　　　　　　　<table id="Table1" runat="server" border="1" style="" >
　　　　　　　　　　<thead>
                        <tr>
                            
                            <th>SCORE</th>
                            <th>ID</th>
                            <th>NAME</th>
                            <th>GENDER</th>
                            <th>BIRTH</th>
                            <th>PHONENUMBER</th>
                            <th>FACE</th>
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
                        <%#Eval("score") %>
                    </td>
                    <td>
　　　　　　　　　　　　<%#Eval("id") %>
　　　　　　　　　　</td>
　　　　　　　　　　<td>
　　　　　　　　　　　　<%#Eval("name") %>
　　　　　　　　　　</td>
                    <td>
                        <%#Eval("gender") %>
                    </td>
                    <td>
                        <%#Eval("birth") %>
                    </td>
                    <td>
                        <%#Eval("phonenumber") %>
                    </td>
　　　　　　　　　　<td>
　　　　　　　　　　　　<asp:Image ID="img" runat="server" Width="120" Height="90" ImageUrl='<%#Eval("id","showimage.aspx?ID={0}") %>' />
　　　　　　　　　　</td>
　　　　　　　　　　
　　　　　　　　</tr>
　　　　　　</ItemTemplate>
        </asp:ListView>
        

        </div>
        <script type ="text/javascript">
            function test2() {
                var title = document.getElementById("FileUpload1").value;
                document.getElementById("TextBox1").value = title;
            }
        </script>
    </form>
</body>
</html>
