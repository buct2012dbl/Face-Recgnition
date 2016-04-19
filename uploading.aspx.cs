using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uploading : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if(FileUpload1.PostedFile.FileName!=null)
        {
            string savePath = Server.MapPath("~/image/");//指定上传文件在服务器上的保存路径
            if (FileUpload1.HasFile)
            {
                
                                                              //检查服务器上是否存在这个物理路径，如果不存在则创建
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath = savePath + "\\" + FileUpload1.FileName;
                FileUpload1.SaveAs(savePath);
                
            }
            
            Theme theme = new Theme(ImageData.returnbyte(savePath));
            ImageData.SaveQuery(theme);
        }
    }
}