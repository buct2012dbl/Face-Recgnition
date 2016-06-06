using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Text;
using System.Web.UI.WebControls;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class ImageData
{
   
    public ImageData()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
   
    public static byte[] returnbyte(string strpath)
    {
        // 以二进制方式读文件
        FileStream fsMyfile = new FileStream(strpath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        // 创建一个二进制数据流读入器，和打开的文件关联
        BinaryReader brMyfile = new BinaryReader(fsMyfile);
        // 把文件指针重新定位到文件的开始
        brMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
        byte[] bytes = brMyfile.ReadBytes(Convert.ToInt32(fsMyfile.Length.ToString()));
        // 关闭以上new的各个对象
        brMyfile.Close();
        return bytes;
    }
    public static byte[] Getbyte(System.Drawing.Image img)
    {
        MemoryStream stream = new MemoryStream();
        img.Save(stream, ImageFormat.Jpeg);
        byte[] mydata = new byte[stream.Length];
        mydata = stream.ToArray();
        stream.Close();
        return mydata;
    }
    public static System.Drawing.Image getImageDataFromBytes(byte[] blob)
    {
        /*
        openconn();
        string sql = "select IMGDATA from t_img where imgID=100";
        MySqlCommand comm = new MySqlCommand(sql,conn);
        MySqlDataReader msr = comm.ExecuteReader();
        Byte[] blob = new Byte[(msr.GetBytes(2, 0, null, 0, int.MaxValue))];
        msr.GetBytes(2, 0, blob, 0, blob.Length);
        */
        System.IO.MemoryStream ms = new System.IO.MemoryStream(blob);
        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
        return img;
    }
    public static Neurotec.Images.NImage getNImageDataFromBytes(byte[] blob)
    {
        /*
        openconn();
        string sql = "select IMGDATA from t_img where imgID=100";
        MySqlCommand comm = new MySqlCommand(sql,conn);
        MySqlDataReader msr = comm.ExecuteReader();
        Byte[] blob = new Byte[(msr.GetBytes(2, 0, null, 0, int.MaxValue))];
        msr.GetBytes(2, 0, blob, 0, blob.Length);
        */
        Neurotec.IO.NMemoryStream ms = new Neurotec.IO.NMemoryStream(blob);
        //  System.IO.MemoryStream ms = new System.IO.MemoryStream(blob);
        Neurotec.Images.NImage img = Neurotec.Images.NImage.FromStream(ms);
        return img;
    }
    public static bool SaveQuery(Theme theme)
    {
         
     
        MySqlParameter[] param = new MySqlParameter[6];
       
      //  param[0] = new MySqlParameter("@id", theme.ID);
        param[0] = new MySqlParameter("@data", theme.data);
        param[1] = new MySqlParameter("@name", theme.name);
        param[2] = new MySqlParameter("@gender", theme.gender);
        param[3] = new MySqlParameter("@birth", theme.birth);
        param[4] = new MySqlParameter("@id", theme.ID);
        param[5] = new MySqlParameter("@phonenumber", theme.phonenumber);
        inserttotest(param);
        return true;
    }
    private static void inserttotest(MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        string sqlcommand = "insert into imgdata(data,id,name,gender,birth,phonenumber) values(@data,@id,@name,@gender,@birth,@phonenumber)";
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        for(int i= 0;i<param.Length;i++)
           cmd.Parameters.Add(param[i]);
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (System.Exception e)
        {
            StreamWriter sw = new StreamWriter("C:\\myerror.txt", true, Encoding.Default);
            sw.Write(e.Message);
            sw.Close();
            //一旦发生错误程序就停止运行，等待用户发现
            Console.Read();
        }
        conn.Close();
    }
    public static byte[] setSelectfromresult(String uderid)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=resultshow;uid=root;password=root");
        conn.Open();
        String sqlcommand = String.Format("select data from imgdata where id='{0}'", uderid);
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        MySqlDataReader msr = cmd.ExecuteReader();
        msr.Read();

        // byte[] blob = new byte[(msr.GetBytes(2, 0, null, 0, int.MaxValue))];
        // msr.GetBytes(2, 0, blob, 0, blob.Length);
        byte[] blob = (byte[])msr.GetValue(0);
        conn.Close();
        msr.Close();
        msr.Dispose();
        //String xmlStr = Encoding.Unicode.GetString(blob);
        //query.LoadFromString(xmlStr);
        return blob;
    }
    public static byte[] setSelect(String uderid)
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        conn.Open();
        String sqlcommand = String.Format("select data from imgdata where id='{0}'", uderid);
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        MySqlDataReader msr = cmd.ExecuteReader();
        msr.Read();
        
        // byte[] blob = new byte[(msr.GetBytes(2, 0, null, 0, int.MaxValue))];
        // msr.GetBytes(2, 0, blob, 0, blob.Length);
        byte [] blob = (byte[]) msr.GetValue(0);
        conn.Close();
        msr.Close();
        msr.Dispose();
        //String xmlStr = Encoding.Unicode.GetString(blob);
        //query.LoadFromString(xmlStr);
        return blob;
    }
    /// <summary>
    /// 文章类辅助插入数据库操作
    /// </summary>
    
    
}
public class Theme
{
    public string ID;
    public byte[] data;
    public string name;
    public string gender;
    public string birth;
    public string phonenumber;
    //        public int dc_len;
    //        public string 
    //        public string dc_title;
    //        public string dc_titleTokens;
    //        public string dc_descriptionTokens;
    //        public string dc_description;
    public Theme(byte[] data,string id,string name,string gender,string birth,string phonenumber)
    {

        this.ID = id;
        this.data = data;
        this.name = name;
        this.gender = gender;
        this.birth = birth;
        this.phonenumber = phonenumber;
        //            dc_len = 0;
        //            dc_title = string.Empty;
        //            dc_titleTokens = string.Empty;
        //            dc_descriptionTokens = string.Empty;
        //            dc_description = string.Empty;
    }
}