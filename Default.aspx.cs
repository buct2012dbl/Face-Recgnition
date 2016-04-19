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
using System.Windows.Forms;
using sample;
using Neurotec.Biometrics;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    private NImage _image;
    private NBiometricClient _biometricClient;
    private bool? _isSegmentationActivated;
    private NLView faceview;
    private float _defaultMaxRoll;
    private float _defaultMaxYaw;
    private System.Windows.Forms.OpenFileDialog openFaceImageDlg;

    public NBiometricClient BiometricClient
    {
        get { return _biometricClient; }
        set
        {
            _biometricClient = value;
            _defaultMaxRoll = _biometricClient.FacesMaximalRoll;
            _defaultMaxYaw = _biometricClient.FacesMaximalYaw;
          // cbRollAngle=ListItem.FromString(_defaultMaxRoll.ToString());
         //  cbYawAngle.SelectedItem =ListItem.FromString(_defaultMaxYaw.ToString());
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
    protected void Page_Load(object sender, EventArgs e)
    {
        // try
        // {
        faceview = new NLView();
        Bitmap bit = new Bitmap("C:/img1.JPG");
        faceview.Image=bit;
        
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
       // }
       // catch (Exception ex)
       // {
       //     Utils.ShowException(ex);
       // }
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        openFaceImageDlg = new OpenFileDialog();
        openFaceImageDlg.Filter = NImages.GetOpenFileFilterString(true, true);
        if (openFaceImageDlg.ShowDialog() == DialogResult.OK)
        {
            if (_image != null) _image.Dispose();
            _image = null;

            try
            {
                // Read image
                _image = NImage.FromFile(openFaceImageDlg.FileName);
              //  DetectFace(_image);
            }
            catch (Exception ex)
            {
              //  Utils.ShowException(ex);
            }
        }
    }
}