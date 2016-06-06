<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploading.aspx.cs" Inherits="uploading" %>

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
              
            <li><asp:LinkButton ID="match" runat="server" OnClick="match_Click">Match</asp:LinkButton></li>
            <li class="active"><asp:LinkButton ID="facedata" runat="server" OnClick="facedata_Click">Facedata</asp:LinkButton></li>
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
                             <asp:FileUpload ID="FileUpload1" runat="server" Height="35px" Width="105px" onchange="test2()"/>
                       </div>  
                       <span class="input-group-btn">
                             <asp:Button ID="Button2" runat="server" Text="Open Image" CssClass="btn btn-default"/>
                       </span>
                       <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  placeholder="Search for..."></asp:TextBox>
                       
                 </div><!-- /input-group -->
                 
                 <span class="label-default">name</span>
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                 <span class="label-default">gender</span>
                 <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                 <span class="label-default">birth</span>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="Date"  placeholder=""></asp:TextBox>
                 <span class="label-default">ID</span>
                 <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                 <span class="label-default">phonenumber</span>
                 <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                 <span class="input-group-btn">
                             <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btn btn-default"  OnClick="Button1_Click"/>
                       </span>
                 
      
             </div><!-- /.col-lg-6 -->
   
         </div>
    
   </div>
   <script type="text/javascript">
    function HandleFileButtonClick()
    {
        //document.form1.FileUpload1.click();
        
        var a = document.getElementById("FileUpload1");
        a.click();
        //var b = document.getElementById("TextBox1");
        

       
    }
    function test2()
    {
        var title = document.getElementById("FileUpload1").value;
        document.getElementById("TextBox1").value = title;
    }
    </script>
    </form>
</body>
</html>
