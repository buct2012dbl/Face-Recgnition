using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        string sqlcommand = "insert into user values(@username,@password)";
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        if (isnameexist(TextBox1.Text.Trim()))
        {
            Response.Write("<script>alert('username has been registed')");
        }
        //MySqlParameter[] param = new MySqlParameter[2];
        //for (int i = 0; i < 2; i++)
        cmd.Parameters.Add(new MySqlParameter("username", TextBox1.Text.Trim()));
        cmd.Parameters.Add(new MySqlParameter("password", TextBox2.Text.Trim()));
        conn.Open();
       // try
       // {
            cmd.ExecuteNonQuery();
       // }
       // catch (System.Exception ex)
        //{
        //    StreamWriter sw = new StreamWriter("C:\\myerror.txt", true, Encoding.Default);
        //    sw.Write(ex.Message);
        //    sw.Close();
            //一旦发生错误程序就停止运行，等待用户发现
        //    Console.Read();
       // }
        conn.Close();
        Response.Write("<script>alert('succeed')</script>");
    }
    private bool isnameexist(string name)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        conn.Open();
        String sqlcommand = String.Format("select * from user");
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        MySqlDataReader msr = cmd.ExecuteReader();
        while (msr.Read())
        {
            string username = (string)msr.GetValue(0);
            if (name == username)
            {
                return true;
            }
        }
        return false;
    }
}