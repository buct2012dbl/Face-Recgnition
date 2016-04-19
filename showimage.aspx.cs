using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class showimage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string id = Request.QueryString["ID"];
        /*
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        String sqlcommand = String.Format("select data from imgdata where id='{0}'", id);
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        conn.Open();
        MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        Response.ContentType = "image/jpeg";
        byte[] byteFileStream = Convert.FromBase64String(ds.Tables[0].Rows[0][0].ToString().Trim());
        Response.BinaryWrite(byteFileStream);
        Response.End();
        */
        byte[] blob = ImageData.setSelect(id);
        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(blob);
        Response.End();
    }
}