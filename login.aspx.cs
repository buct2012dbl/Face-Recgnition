using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        conn.Open();
        String sqlcommand = String.Format("select * from user");
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        MySqlDataReader msr = cmd.ExecuteReader();
        while(msr.Read())
        {
            string name = (string)msr.GetValue(0);
            string password = (string)msr.GetValue(1);
            name.Trim();
            password.Trim();
            if(name.Trim()==TextBox1.Text.Trim()&&password.Trim()==TextBox2.Text.Trim())
            {
                Response.Redirect("Default3.aspx");
                conn.Close();
                msr.Close();
                msr.Dispose();
                return;
            }
            
        }
        Response.Write("<script>alert('username or password is not exist');</script");



    }
}