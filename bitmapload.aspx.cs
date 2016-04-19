using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Neurotec.Biometrics.Gui;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

public partial class bitmapload : System.Web.UI.Page
{
    private NLView faceview;
    protected void Page_Load(object sender, EventArgs e)
    {
        faceview = new NLView();
        Bitmap bit = new Bitmap("D:/img2.JPG");
        faceview.Image=bit;
        MemoryStream ms = new MemoryStream();
        faceview.Image.Save(ms, ImageFormat.Jpeg);
        ms.WriteTo(Response.OutputStream);
        ms.Dispose();
        bit.Dispose();
        faceview.Dispose();
    }
}