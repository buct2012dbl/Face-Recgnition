using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Neurotec.Biometrics.Gui;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using Neurotec.Licensing;
using Neurotec.Media;
using System.Windows.Forms;
using sample;
using Neurotec.Biometrics;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data;

public partial class Default3 : System.Web.UI.Page
{
    private NImage _image;
    private NGrayscaleImage _Gimage;
    private NBiometricClient _biometricClient;
    private bool? _isSegmentationActivated;
    private NLView faceview;
    private float _defaultMaxRoll;
    private float _defaultMaxYaw;
    
    private static Neurotec.Biometrics.Gui.NFaceView nFaceView1= new Neurotec.Biometrics.Gui.NFaceView();
    private static NFace NF = new NFace();
    private NLExtractor nle = new NLExtractor();
    //  private NleDetectionDetails detectiondetail = new NleDetectionDetails();
    public NBiometricClient BiometricClient
    {
        get { return _biometricClient; }
        set
        {
            _biometricClient = value;
            _defaultMaxRoll = _biometricClient.FacesMaximalRoll;
            _defaultMaxYaw = _biometricClient.FacesMaximalYaw;
           // cbRollAngle.SelectedItem = _defaultMaxRoll;
           // cbYawAngle.SelectedItem = _defaultMaxYaw;
        }
    }
    private void DetectFace(NImage image)
    {
        if (image == null) return;

        SetBiometricClientParams();
        // Detect asynchroniously all faces that are suitable for face recognition in the image
        _biometricClient.BeginDetectFaces(image, OnDetectDone, null);
    }
    public static Func<Object, DataSet> isLogin = session => session as DataSet ?? new DataSet() { DataSetName = "a"};
    protected void Page_Load(object sender, EventArgs e)
    {
        const int Port = 5000;
        const string Address = "/local";
        const string Components = "Biometrics.FaceExtraction,Biometrics.FaceMatching,Biometrics.FaceDetection,Devices.Cameras,Biometrics.FaceSegmentsDetection";
        foreach (string component in Components.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            NLicense.ObtainComponents(Address, Port, component);
        }
        _biometricClient = new NBiometricClient { BiometricTypes = NBiometricType.Face, UseDeviceManager = true };
       _biometricClient.Initialize();
        this.FileUpload1.Attributes.Add("onchange", "test2();");

        float item = _biometricClient.FacesMaximalRoll;
        List<float> items = new List<float>();
        for (float i = 15; i <= 180; i += 15)
        {
            items.Add((i));
        }
        if (!items.Contains(item))
            items.Add(item);
        items.Sort();

        int index = items.IndexOf(item);
        for (int i = 0; i != items.Count; i++)
        {
            cbRollAngle.Items.Add(items[i].ToString());
        }
        cbRollAngle.SelectedIndex = index;

        item = _biometricClient.FacesMaximalYaw;
        items.Clear();
        for (float i = 15; i <= 90; i += 15)
        {
            items.Add((i));
        }
        if (!items.Contains(item))
            items.Add(item);
        items.Sort();

        index = items.IndexOf(item);
        for (int i = 0; i != items.Count; i++)
        {
            cbYawAngle.Items.Add(items[i].ToString());
        }
        cbYawAngle.SelectedIndex = index;
        DataSet set = isLogin(Session["result"]);
        if(set.DataSetName=="result")
        {
            ListView1.DataSource = set;
            ListView1.DataBind();
        }
    }
    private void SetBiometricClientParams()
    {

                
        _biometricClient.FacesMaximalRoll = float.Parse(cbRollAngle.SelectedItem.Value);
        _biometricClient.FacesMaximalYaw = float.Parse(cbYawAngle.SelectedItem.Value);

        if (!_isSegmentationActivated.HasValue)
            _isSegmentationActivated = NLicense.IsComponentActivated("Biometrics.FaceSegmentsDetection");

        _biometricClient.FacesDetectAllFeaturePoints = _isSegmentationActivated.Value;
        _biometricClient.FacesDetectBaseFeaturePoints = _isSegmentationActivated.Value;
    }
    private void OnDetectDone(IAsyncResult r)
    {
       
            NF = _biometricClient.EndDetectFaces(r);
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //  System.Windows.Forms.OpenFileDialog openFaceImageDlg = new OpenFileDialog();

        //   openFaceImageDlg.ShowDialog();

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

        // openFaceImageDlg.Filter = NImages.GetOpenFileFilterString(true, true);
        // if (openFaceImageDlg.ShowDialog() == DialogResult.OK)
        // {
        //    if (_image != null) _image.Dispose();
        //    _image = null;

        //      try
        //    {
        //   TextBox1.Text = FileUpload1.PostedFile.FileName;
        // Read image
        _image = NImage.FromFile(savePath);
        //        DetectFace(_image);
        //    }
        //    catch (Exception ex)
         //   {
         //        Utils.ShowException(ex);
         //   }
            NleFace nlef;
        
        nle.DetectFace(_image.ToGrayscale(),out nlef);
        nle.DetectAllFeaturePoints = true;
        nle.DetectBlink = true;
        nle.DetectEmotion = true;
        nle.DetectExpression = true;
        nle.DetectGender = true;
        nle.DetectGlasses = true;
        nle.DetectDarkGlasses = true;
        nle.DetectMouthOpen = true;
        nle.MaxRollAngleDeviation = short.Parse(cbRollAngle.SelectedValue);
        nle.MaxYawAngleDeviation = short.Parse(cbYawAngle.SelectedValue);
        string Blink = "";
        string Emotion = "";
        string Expression = "";
        string Gender = "";
        string Glasses = "";
        string Mouth = "";
        
        NleDetectionDetails detail = nle.DetectFacialFeatures(_image.ToGrayscale(), nlef);
        NleDetectionDetails detail2;
        NleExtractionStatus Status;
        nle.Extract(_image.ToGrayscale(),out detail2, out Status);
        List<NLFeaturePoint> points = new List<NLFeaturePoint>();
        
        points.Add(detail.LeftEyeCenter);
        points.Add(detail.MouthCenter);
        points.Add(detail.RightEyeCenter);
        for(int i= 0;i<detail.Points.Length;i++)
           points.Add(detail.Points[i]);
        Bitmap bit = new Bitmap(savePath);
        
        Graphics g = Graphics.FromImage(bit);
        Brush b = new SolidBrush(Color.Green);
        Pen p = new Pen(b);
        for (int i = 0; i< points.Count; i++)
        {
            g.DrawRectangle(p, points.ElementAt(i).X-2.5f, points.ElementAt(i).Y-2.5f,5,5);
        }
        
        string dirpath = "D:/img2.JPG";
        if(System.IO.File.Exists(dirpath) == true)
        {
            System.IO.File.Delete(dirpath);
        }
        
        bit.Save(dirpath,System.Drawing.Imaging.ImageFormat.Jpeg);
        if (detail2.EmotionAngerConfidence > 50 && detail2.EmotionAngerConfidence != 254 && detail2.EmotionAngerConfidence != 255)
            Emotion += " Anger ";
        if (detail2.EmotionDisgustConfidence > 50 && detail2.EmotionDisgustConfidence != 254 && detail2.EmotionDisgustConfidence != 255)
            Emotion += " Disgust ";
        if (detail2.EmotionFearConfidence > 50 && detail2.EmotionFearConfidence != 254 && detail2.EmotionFearConfidence != 255)
            Emotion += " Fear ";
        if (detail2.EmotionHappinessConfidence > 50 && detail2.EmotionHappinessConfidence!= 254 && detail2.EmotionHappinessConfidence != 255)
            Emotion += " Happyness ";
        if (detail2.EmotionNeutralConfidence > 50 && detail2.EmotionNeutralConfidence != 254 && detail2.EmotionNeutralConfidence != 255)
            Emotion += " Netral ";
        if (detail2.EmotionSadnessConfidence > 50 && detail2.EmotionSadnessConfidence != 254 && detail2.EmotionSadnessConfidence != 255)
            Emotion += " Sadness ";
        if (detail2.EmotionSurpriseConfidence > 50 && detail2.EmotionSurpriseConfidence != 254 && detail2.EmotionSurpriseConfidence != 255)
            Emotion += " Surprise ";
        Expression += detail2.Expression.ToString();
        
        if (detail2.DarkGlassesConfidence != 254 && detail2.DarkGlassesConfidence != 255)
        {
            if (detail2.DarkGlassesConfidence > 50)
                Glasses += " wearing glasses ";
            else
                Glasses += " not wearing glasses ";
        }
        if (detail2.BlinkConfidence != 254 && detail2.BlinkConfidence != 255)
        {
            if (detail2.BlinkConfidence > 50)
                Blink += " Eye open ";
            else
                Blink += " Eye close ";
        }
        Gender += detail2.Gender.ToString();
        if (detail2.MouthOpenConfidence != 254 && detail2.MouthOpenConfidence!= 255)
        {
            if (detail2.MouthOpenConfidence < 51)
                Mouth += " Mouth close ";
            else
                Mouth += " Mouth open ";
        }
        //Response.Write("<script>alert('"+Blink+Emotion+Expression+Glasses+Gender+Mouth+"')</script>");
        //  Response.Write("<script>alert('" + detail2.BlinkConfidence + Emotion + Expression + Gender + detail2.MouthOpenConfidence + "')</script>");
        DataSet dst = new DataSet();
        dst.DataSetName = "result";
        DataTable dt = new DataTable();
        dst.Tables.Add(dt);
        dt.Columns.Add("blink");
        dt.Columns.Add("emotion");
        dt.Columns.Add("expression");
        dt.Columns.Add("glasses");
        dt.Columns.Add("gender");
        dt.Columns.Add("mouth");

        DataRow row1 = dt.NewRow();
        row1["blink"] = Blink;
        row1["emotion"] = Emotion;
        row1["expression"] = Expression;
        row1["glasses"] = detail2.DarkGlassesConfidence;
        row1["gender"] = Gender;
        row1["mouth"] = Mouth;
        dt.Rows.Add(row1);
       // dst.Tables.Add(dt);
        Session["result"] = dst;
        ListView1.DataSource = dst;
        ListView1.DataBind();
        _image.Dispose();
        bit.Dispose();
        g.Dispose();
    }
    protected void detect_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default3.aspx");
    }
    protected void match_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default4.aspx");
    }
    protected void facedata_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploading.aspx");
    }
}
