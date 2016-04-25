using MySql.Data.MySqlClient;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;
using Neurotec.Licensing;
using sample;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class Default4 : System.Web.UI.Page
{
   // private NBiometricClient _biometricClient;
    private NSubject _subject1;
    private NSubject _subject2;
    private NFaceView faceView1 = new NFaceView();
    public static object locker = new object();
    private NBiometricEngine nle = new NBiometricEngine();
    System.Collections.Generic.List<btnode> btlist = new List<btnode>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        const int Port = 5000;
        const string Address = "/local";
        const string Components = "Biometrics.FaceExtraction,Biometrics.FaceMatching,Biometrics.FaceDetection,Devices.Cameras,Biometrics.FaceSegmentsDetection";
        foreach (string component in Components.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            NLicense.ObtainComponents(Address, Port, component);
        }
        /*
        _biometricClient = new NBiometricClient { BiometricTypes = NBiometricType.Face, UseDeviceManager = true };
        _biometricClient.Initialize();
        */
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        String sqlcommand = "select *  from resultshow";
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        conn.Open();
        MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        ListView1.DataSource = ds;
        ListView1.DataBind();

    }
    /*
    private void OnCreationCompleted(IAsyncResult r)
    {
        lock (locker)
        {
            try
            {
                NBiometricStatus status = _biometricClient.EndCreateTemplate(r);
                if (status != NBiometricStatus.Ok)
                {
                    MessageBox.Show(string.Format("The template was not extracted: {0}.", status));
                }
                // EnableVerifyButton();
            }
            catch (Exception ex)
            {
                Utils.ShowException(ex);
            }
        }
    }*/
    private string OpenImageTemplate(NFaceView faceView, out NSubject subject)
    {
        string savePath = Server.MapPath("~/image/");//指定上传文件在服务器上的保存路径;
        if (FileUpload1.PostedFile.FileName != null)
        {
            
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

       
        }
        subject = null;
        faceView.Face = null;
    //    msgLabel.Text = string.Empty;
        string fileLocation = string.Empty;

      //  openFileDialog.FileName = null;
     //   openFileDialog.Title = @"Open Template";
      //  if (openFileDialog.ShowDialog() == DialogResult.OK) // load template
        {
            fileLocation = savePath;

            // Check if given file is a template
            try
            {
                subject = NSubject.FromFile(savePath);
              //  EnableVerifyButton();
            }
            catch { }

            // If file is not a template, try to load it as image
            if (subject == null)
            {
                // Create a face object
                NFace face = new NFace() { FileName = fileLocation };
                faceView.Face = face;
                subject = new NSubject();
                subject.Faces.Add(face);

                // Extract a template from the subject
                nle.CreateTemplate(subject);
            }
        }
        return fileLocation;
    }
    /*
    int score;
    private void OnVerifyCompleted(IAsyncResult r)
    {
        lock (locker)
        {

            try
            {
                NBiometricStatus status = _biometricClient.EndVerify(r);
                if (status == NBiometricStatus.Ok || status == NBiometricStatus.MatchNotFound)
                {
                    //get matching score
                    score = _subject1.MatchingResults[0].Score;
                    // string msg = string.Format("Score of matched templates: {0}", score);
                    //msgLabel.Text = msg;
                    // MessageBox.Show(msg);
                }
                else MessageBox.Show(status.ToString());
            }
            catch (Exception ex)
            {
                Utils.ShowException(ex);
            }
        }
        
    }*/
    protected void Button1_Click(object sender, EventArgs e)
    {
        deleteresultshow();
        NMatcher nm = new NMatcher();
        
        OpenImageTemplate(faceView1, out _subject1);
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        conn.Open();
        String sqlcommand = String.Format("select data from imgdata");
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        MySqlDataReader msr = cmd.ExecuteReader();
        while(msr.Read())
        {
            
            

            byte[] blob = (byte[])msr.GetValue(0);
            Neurotec.Images.NImage img = ImageData.getNImageDataFromBytes(blob);
            NFace face = new NFace();
            face.Image = img;
            NSubject subject = new NSubject();
            subject.Faces.Add(face);
            //   _biometricClient.BeginCreateTemplate(subject, OnCreationCompleted, subject);
            nle.CreateTemplate(subject);
         //   Thread.Sleep(500);
         
            if (_subject1 != null && subject != null)
            {
               
                int score1=nm.Verify(_subject1.GetTemplateBuffer(), subject.GetTemplateBuffer());
             //   _biometricClient.BeginVerify(_subject1,subject, OnVerifyCompleted, null);
               
                btlist.Add(new btnode(blob, score1));

            }
                
            
            
        }
       
        conn.Close();
        msr.Close();
        
        int maxscore=0;
        int temp = 0;
        string str = "";
        for(int i=0;i<btlist.Count;i++)
        {
            sqlcommand = "insert into resultshow(score,data) values(@score,@data)";
            cmd = new MySqlCommand(sqlcommand, conn);
            MySqlParameter[] param = new MySqlParameter[2];
            param[0] = new MySqlParameter("@score", btlist[i].score);
            param[1] = new MySqlParameter("@data", btlist[i].blob);
            for (int j = 0; j < param.Length; j++)
                cmd.Parameters.Add(param[j]);
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                StreamWriter sw = new StreamWriter("C:\\myerror.txt", true, Encoding.Default);
                sw.Write(ex.Message);
                sw.Close();
                //一旦发生错误程序就停止运行，等待用户发现
                Console.Read();
            }
            
            conn.Close();
            conn.Dispose();
            msr.Dispose();/*
            if (btlist[i].score>maxscore)
            {
                maxscore = btlist[i].score;
                temp = i;
            }*/
         //   str += btlist[i].score + "   ";


        }
       // MessageBox.Show(str);
        /*
        sqlcommand = "insert into resultshow(score,data) values(@score,@data)";
        cmd = new MySqlCommand(sqlcommand, conn);  
        MySqlParameter[] param = new MySqlParameter[2];
        param[0] = new MySqlParameter("@score",btlist[temp].score);
        param[1] = new MySqlParameter("@data", btlist[temp].blob);
        for (int i = 0; i < param.Length; i++)
            cmd.Parameters.Add(param[i]);
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            StreamWriter sw = new StreamWriter("C:\\myerror.txt", true, Encoding.Default);
            sw.Write(ex.Message);
            sw.Close();
            //一旦发生错误程序就停止运行，等待用户发现
            Console.Read();
        }
       
        conn.Close();
        conn.Dispose();
        msr.Dispose();
        */
        Response.Redirect("Default5.aspx", false);
    }
    private void deleteresultshow()
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=test;uid=root;password=root");
        string sqlcommand = "TRUNCATE TABLE resultshow;";
        MySqlCommand cmd = new MySqlCommand(sqlcommand, conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        conn.Dispose();
    }
    class btnode
    {
        public byte[] blob;
        public int score;
        public btnode(byte[] blob,int score)
        {
            this.blob=new byte[blob.Length];
            blob.CopyTo(this.blob, 0);
            this.score = score;

        }
    }
}