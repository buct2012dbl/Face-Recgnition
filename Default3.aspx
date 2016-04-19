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
            <li class="active"><a href="#">Detect</a></li>
            <li><a id="about" href="#about">Match</a></li>
            <li><a href="#contact">Contact</a></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
      
  </nav>
   <div class="container">   
  <div class="row" style="">
      
  <div class="col-lg-6">
    <div class="input-group">
        
      <span class="input-group-btn">
        <asp:Button ID="Button1" runat="server" Text="Open Image" CssClass="btn btn-default" OnClick="Button1_Click" OnClientClick="BrowseFolder()"/>
      </span>
     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"  placeholder="Search for..."></asp:TextBox>
   
     
    </div><!-- /input-group -->
      
  </div><!-- /.col-lg-6 -->
    
  </div>         
     <span class="label label-default">Max roll angle deviation</span>
     <asp:DropDownList ID="cbRollAngle" runat="server"></asp:DropDownList>
      &nbsp&nbsp&nbsp
      <span class="label label-default">Max yaw angle deviation</span>
     <asp:DropDownList ID="cbYawAngle" runat="server"></asp:DropDownList>     
       <div id="fontzoom">
          <asp:FileUpload ID="FileUpload1" runat="server" Visible="false"/>
              
             
      </div>
       <br />
       <asp:Image ID="Image1" runat="server" ImageUrl="~/bitmapload.aspx"/>
      
  </div>
      
         
        
    <script src="js/jquery.js"></script>
    <script type="text/javascript">
        (
        function BrowseFolder() {
                try {
                    var Message = "请选择文件夹";
                    var Shell = new ActiveXObject("Shell.Application");
                    var Folder = Shell.BrowseForFolder(0, Message, 0x0040, 0x11); //起始目录为：我的电脑
                    if (Folder != null) {
                        Folder = Folder.items(); // 返回 FolderItems 对象
                        Folder = Folder.item();
                        Folder = Folder.Path; // 返回路径
                        if (Folder.charAt(Folder.length - 1) != "\\") {
                            Folder = Folder + "\\";
                        }
                        var bb = document.getElementByIdx_x("<%=TextBox1.ClientID%>");                                               
                        //document.getElementByIdx_x("BackupPath").value = Folder;
                        bb.value = Folder;
                        return Folder;
                    }
                } catch (e) {
                    alert(e.message);
                }
            }
        )

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
