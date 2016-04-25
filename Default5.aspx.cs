using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        String sqlcommand = "select *  from resultshow order by score desc";
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        conn.Open();
        MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ListView1.DataSource = ds;
        ListView1.DataBind();
    }
}