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

public partial class Default3 : System.Web.UI.Page
{
    private NImage _image;
    private NGrayscaleImage _Gimage;
    private NBiometricClient _biometricClient;
    private bool? _isSegmentationActivated;
    private NLView faceview;
    private float _defaultMaxRoll;
    private float _defaultMaxYaw;
    private System.Windows.Forms.OpenFileDialog openFaceImageDlg;
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
      //  openFaceImageDlg = new OpenFileDialog();
      //  openFaceImageDlg.ShowDialog();
        
       
       // openFaceImageDlg.Filter = NImages.GetOpenFileFilterString(true, true);
       // if (openFaceImageDlg.ShowDialog() == DialogResult.OK)
       // {
        //    if (_image != null) _image.Dispose();
        //    _image = null;

      //      try
        //    {
         //   TextBox1.Text = FileUpload1.PostedFile.FileName;
                // Read image
                _image = NImage.FromFile("D:/img1.JPG");
        //        DetectFace(_image);
        //    }
        //    catch (Exception ex)
         //   {
         //        Utils.ShowException(ex);
         //   }
            NleFace nlef;
        
        nle.DetectFace(_image.ToGrayscale(),out nlef);
        nle.DetectAllFeaturePoints = true;
        NleDetectionDetails detail = nle.DetectFacialFeatures(_image.ToGrayscale(), nlef);
        List<NLFeaturePoint> points = new List<NLFeaturePoint>();
        points.Add(detail.LeftEyeCenter);
        points.Add(detail.MouthCenter);
        points.Add(detail.RightEyeCenter);
        for(int i= 0;i<detail.Points.Length;i++)
           points.Add(detail.Points[i]);
        Bitmap bit = new Bitmap("D:/img1.JPG");
        
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
       
        
    }
}
