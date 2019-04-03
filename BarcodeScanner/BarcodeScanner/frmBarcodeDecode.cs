using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

public class frmBarcodeDecode : System.Windows.Forms.Form {
    
    [DllImport("kernel32.dll", EntryPoint="GetShortPathNameW")]
    static extern int GetShortPathName(string longPath, System.Text.StringBuilder ShortPath, int bufferSize);
    
    public frmBarcodeDecode() {
        // This call is required by the Windows Form Designer.
        this.InitializeComponent();
        // Add any initialization after the InitializeComponent() call
    }
    
    // Form overrides dispose to clean up the component list.
    protected override void Dispose(bool disposing) {
        if (disposing) {
            if (!(components == null)) {
                components.Dispose();
            }
            
        }
        
        base.Dispose(disposing);
    }
    
    // Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;
    
    // NOTE: The following procedure is required by the Windows Form Designer
    // It can be modified using the Windows Form Designer.  
    // Do not modify it using the code editor.
    internal System.Windows.Forms.Label Label1;
    
    internal System.Windows.Forms.TextBox txtFrom;
    
    internal System.Windows.Forms.Button btnFrom;
    
    internal System.Windows.Forms.FolderBrowserDialog fldFrom;
    
    internal System.Windows.Forms.Button btnProcess;
    
    internal System.Windows.Forms.ProgressBar ProgressBar1;
    
    internal System.Windows.Forms.CheckBox chkUseBarcodeZones;
    
    internal System.Windows.Forms.ComboBox cbBarcodeType;
    
    internal System.Windows.Forms.Label lblType;
    
    internal System.Windows.Forms.Label Label2;
    
    internal System.Windows.Forms.ComboBox cbDirection;
    
    internal System.Windows.Forms.CheckBox chkShowTime;
    
    internal System.Windows.Forms.CheckBox chkCSV;
    
    internal System.Windows.Forms.CheckBox chkLog;
    
    internal System.Windows.Forms.Panel pnCS;
    
    internal Label Label5;
    
    internal TextBox txtStartFile;
    internal CheckBox chkqrcode;
    internal Button brtGen;
    internal Button btnConvert;
    internal Button btnConvertFile;
    internal System.Windows.Forms.TextBox txtOutput;
    
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent() {
            this.Label1 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.btnFrom = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.fldFrom = new System.Windows.Forms.FolderBrowserDialog();
            this.btnProcess = new System.Windows.Forms.Button();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkUseBarcodeZones = new System.Windows.Forms.CheckBox();
            this.cbBarcodeType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.chkShowTime = new System.Windows.Forms.CheckBox();
            this.chkCSV = new System.Windows.Forms.CheckBox();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.pnCS = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtStartFile = new System.Windows.Forms.TextBox();
            this.chkqrcode = new System.Windows.Forms.CheckBox();
            this.brtGen = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnConvertFile = new System.Windows.Forms.Button();
            this.pnCS.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(29, 12);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "目录";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(65, 11);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(744, 21);
            this.txtFrom.TabIndex = 5;
            // 
            // btnFrom
            // 
            this.btnFrom.Location = new System.Drawing.Point(816, 9);
            this.btnFrom.Name = "btnFrom";
            this.btnFrom.Size = new System.Drawing.Size(34, 24);
            this.btnFrom.TabIndex = 7;
            this.btnFrom.Text = "...";
            this.btnFrom.UseVisualStyleBackColor = true;
            this.btnFrom.Click += new System.EventHandler(this.btnFrom_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(19, 165);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(831, 238);
            this.txtOutput.TabIndex = 13;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(186, 134);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(153, 25);
            this.btnProcess.TabIndex = 14;
            this.btnProcess.Text = "开始解码";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(19, 409);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(831, 21);
            this.ProgressBar1.TabIndex = 15;
            // 
            // chkUseBarcodeZones
            // 
            this.chkUseBarcodeZones.AutoSize = true;
            this.chkUseBarcodeZones.Location = new System.Drawing.Point(437, 5);
            this.chkUseBarcodeZones.Name = "chkUseBarcodeZones";
            this.chkUseBarcodeZones.Size = new System.Drawing.Size(126, 16);
            this.chkUseBarcodeZones.TabIndex = 16;
            this.chkUseBarcodeZones.Text = "Use Barcode Zones";
            this.chkUseBarcodeZones.UseVisualStyleBackColor = true;
            // 
            // cbBarcodeType
            // 
            this.cbBarcodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBarcodeType.FormattingEnabled = true;
            this.cbBarcodeType.Items.AddRange(new object[] {
            "All",
            "Code39",
            "Code128",
            "EAN"});
            this.cbBarcodeType.Location = new System.Drawing.Point(46, 3);
            this.cbBarcodeType.Name = "cbBarcodeType";
            this.cbBarcodeType.Size = new System.Drawing.Size(153, 20);
            this.cbBarcodeType.TabIndex = 17;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(4, 8);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(29, 12);
            this.lblType.TabIndex = 18;
            this.lblType.Text = "类型";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(218, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(29, 12);
            this.Label2.TabIndex = 19;
            this.Label2.Text = "方向";
            // 
            // cbDirection
            // 
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Items.AddRange(new object[] {
            "All",
            "Vertical",
            "Horizontal"});
            this.cbDirection.Location = new System.Drawing.Point(284, 3);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(146, 20);
            this.cbDirection.TabIndex = 20;
            // 
            // chkShowTime
            // 
            this.chkShowTime.AutoSize = true;
            this.chkShowTime.Checked = true;
            this.chkShowTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTime.Location = new System.Drawing.Point(728, 42);
            this.chkShowTime.Name = "chkShowTime";
            this.chkShowTime.Size = new System.Drawing.Size(72, 16);
            this.chkShowTime.TabIndex = 21;
            this.chkShowTime.Text = "解码时间";
            this.chkShowTime.UseVisualStyleBackColor = true;
            // 
            // chkCSV
            // 
            this.chkCSV.AutoSize = true;
            this.chkCSV.Checked = true;
            this.chkCSV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCSV.Location = new System.Drawing.Point(728, 64);
            this.chkCSV.Name = "chkCSV";
            this.chkCSV.Size = new System.Drawing.Size(90, 16);
            this.chkCSV.TabIndex = 22;
            this.chkCSV.Text = "生成CSV文件";
            this.chkCSV.UseVisualStyleBackColor = true;
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = true;
            this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLog.Location = new System.Drawing.Point(632, 42);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(72, 16);
            this.chkLog.TabIndex = 23;
            this.chkLog.Text = "显示日志";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // pnCS
            // 
            this.pnCS.Controls.Add(this.cbDirection);
            this.pnCS.Controls.Add(this.cbBarcodeType);
            this.pnCS.Controls.Add(this.lblType);
            this.pnCS.Controls.Add(this.Label2);
            this.pnCS.Controls.Add(this.chkUseBarcodeZones);
            this.pnCS.Location = new System.Drawing.Point(19, 42);
            this.pnCS.Name = "pnCS";
            this.pnCS.Size = new System.Drawing.Size(589, 30);
            this.pnCS.TabIndex = 25;
            this.pnCS.Visible = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(24, 110);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(35, 12);
            this.Label5.TabIndex = 27;
            this.Label5.Text = "Start";
            // 
            // txtStartFile
            // 
            this.txtStartFile.Location = new System.Drawing.Point(65, 107);
            this.txtStartFile.Name = "txtStartFile";
            this.txtStartFile.Size = new System.Drawing.Size(758, 21);
            this.txtStartFile.TabIndex = 28;
            // 
            // chkqrcode
            // 
            this.chkqrcode.AutoSize = true;
            this.chkqrcode.Checked = true;
            this.chkqrcode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkqrcode.Location = new System.Drawing.Point(632, 64);
            this.chkqrcode.Name = "chkqrcode";
            this.chkqrcode.Size = new System.Drawing.Size(60, 16);
            this.chkqrcode.TabIndex = 29;
            this.chkqrcode.Text = "QRCode";
            this.chkqrcode.UseVisualStyleBackColor = true;
            // 
            // brtGen
            // 
            this.brtGen.Location = new System.Drawing.Point(25, 134);
            this.brtGen.Name = "brtGen";
            this.brtGen.Size = new System.Drawing.Size(155, 25);
            this.brtGen.TabIndex = 30;
            this.brtGen.Text = "生成二维码";
            this.brtGen.UseVisualStyleBackColor = true;
            this.brtGen.Click += new System.EventHandler(this.brtGen_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(365, 134);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(153, 25);
            this.btnConvert.TabIndex = 31;
            this.btnConvert.Text = "格式转换(目录)";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnConvertFile
            // 
            this.btnConvertFile.Location = new System.Drawing.Point(539, 134);
            this.btnConvertFile.Name = "btnConvertFile";
            this.btnConvertFile.Size = new System.Drawing.Size(153, 25);
            this.btnConvertFile.TabIndex = 32;
            this.btnConvertFile.Text = "格式转换(文件)";
            this.btnConvertFile.UseVisualStyleBackColor = true;
            this.btnConvertFile.Visible = false;
            this.btnConvertFile.Click += new System.EventHandler(this.btnConvertFile_Click);
            // 
            // frmBarcodeDecode
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(939, 465);
            this.Controls.Add(this.btnConvertFile);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.brtGen);
            this.Controls.Add(this.chkqrcode);
            this.Controls.Add(this.txtStartFile);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.pnCS);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.chkCSV);
            this.Controls.Add(this.chkShowTime);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnFrom);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.Name = "frmBarcodeDecode";
            this.Text = "条码解码器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnCS.ResumeLayout(false);
            this.pnCS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    
    private AppSetting oAppSetting = new AppSetting();
    
    private System.IO.StreamWriter oLogFile;
    
    private bool bStartFile = true;
    
    private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
        oAppSetting.SetValue("FromPath", txtFrom.Text);
        oAppSetting.SetValue("StartFile", txtStartFile.Text);
        oAppSetting.SetValue("BarcodeType", cbBarcodeType.SelectedIndex.ToString());
        oAppSetting.SetValue("Direction", cbDirection.SelectedIndex.ToString());
        oAppSetting.SetValue("UseBarcodeZones", ( chkUseBarcodeZones.Checked ? "1" : "0" ));
        oAppSetting.SetValue("Log", ( chkLog.Checked ? "1" : "0" ));
        oAppSetting.SetValue("ShowTime", ( chkShowTime.Checked ? "1" : "0" ));
        oAppSetting.SetValue("CSV", ( chkCSV.Checked ? "1" : "0" ));
        oAppSetting.SaveData();
    }
    
    private void Form1_Load(object sender, System.EventArgs e) {
        oAppSetting.LoadData();
        txtFrom.Text = oAppSetting.GetValue("FromPath");
        txtStartFile.Text = oAppSetting.GetValue("StartFile");
        string sBarcodeType = oAppSetting.GetValue("BarcodeType");
        if ((sBarcodeType != "")) {
            cbBarcodeType.SelectedIndex = int.Parse(sBarcodeType);
        }
        else {
            cbBarcodeType.SelectedIndex = 0;
        }
        
        string sDirection = oAppSetting.GetValue("Direction");
        if ((sDirection != "")) {
            cbDirection.SelectedIndex = int.Parse(sDirection);
        }
        else {
            cbDirection.SelectedIndex = 0;
        }
        
        oAppSetting.GetValue("UseBarcodeZones") ;
        chkUseBarcodeZones.Checked = true;
        chkLog.Checked = (oAppSetting.GetValue("Log") != "0");
        chkShowTime.Checked = (oAppSetting.GetValue("ShowTime") != "0");
        chkCSV.Checked = (oAppSetting.GetValue("CSV") != "0");
        oAppSetting.GetValue("Bytescout") ;
        oAppSetting.GetValue("Fast") ;
    }
    
    public static string GetShortName(string sLongFileName) {
        if ((sLongFileName.Length < 250)) {
            return sLongFileName;
        }
        
        long lRetVal;
        int iLen = 1024;
        System.Text.StringBuilder sShortPathName = new System.Text.StringBuilder(iLen);
        lRetVal = frmBarcodeDecode.GetShortPathName(sLongFileName, sShortPathName, iLen);
        string sRet = sShortPathName.ToString();
        if ((sRet != "")) {
            return sRet;
        }
        else {
            return sLongFileName;
        }
        
    }
    
    private void btnFrom_Click(object sender, System.EventArgs e) {
        fldFrom.SelectedPath = txtFrom.Text;
        fldFrom.ShowDialog();
        txtFrom.Text = fldFrom.SelectedPath;
    }
    
    private void btnProcess_Click(object sender, System.EventArgs e) {
        btnProcess.Enabled = false;
        string sFromPath = txtFrom.Text;
        if (!Directory.Exists(sFromPath)) {
            btnProcess.Enabled = true;
            //MsgBox("Folder does not exist");
            return;
        }
        
        if (chkCSV.Checked) {
            string sLogFileName = (System.DateTime.Now.Month.ToString() + ("-" 
                        + (System.DateTime.Now.Day.ToString() + ("-" 
                        + (System.DateTime.Now.Year.ToString() + ("_" 
                        + (System.DateTime.Now.Hour.ToString() + ("-" 
                        + (System.DateTime.Now.Minute.ToString() + ("-" 
                        + (System.DateTime.Now.Second.ToString() + ".csv")))))))))));
            string sLogFilePath = System.IO.Path.Combine(sFromPath, sLogFileName);
            oLogFile = new System.IO.StreamWriter(sLogFilePath);
        }
        
        if ((txtStartFile.Text != "")) {
            bStartFile = false;
        }
        
        txtOutput.Text = "";
        txtOutput.AppendText(("Starting..." + "\r\n"));
        this.ProccessFolder(sFromPath);
        txtOutput.AppendText("Done!");
        btnProcess.Enabled = true;
        if (chkCSV.Checked) {
            oLogFile.Close();
        }
        
    }
    
    void ProccessFolder(string sFolderPath) {
        string sFromPath = txtFrom.Text;
        string[] oFiles = Directory.GetFiles(sFolderPath);
        ProgressBar1.Maximum = oFiles.Length;
        for (int i = 0; (i 
                    <= (oFiles.Length - 1)); i++) {
            string sFromFilePath = oFiles[i];
            if ((txtStartFile.Text != "")) {
                if ((txtStartFile.Text.ToLower().Trim() == sFromFilePath.ToLower())) {
                    bStartFile = true;
                }
                
            }
            
            if (bStartFile) {
                FileInfo oFileInfo = new FileInfo(frmBarcodeDecode.GetShortName(sFromFilePath));
                string sExt = this.PadExt(oFileInfo.Extension);
                if (((sExt == "JPG") 
                            || ((sExt == "GIF") 
                            || ((sExt == "PNG") 
                            || ((sExt == "BMP") 
                            || (sExt == "TIF")))))) {
                            if (chkqrcode.Checked)
                            {
                                this.qr_ReadBarcode(sFromFilePath);
                            }
                            else
                            {
                                this.ReadBarcode(sFromFilePath);
                            }
                        }
                
            }
            
            ProgressBar1.Value = i;
            Application.DoEvents();
        }
        
        ProgressBar1.Value = 0;
        string[] oFolders = Directory.GetDirectories(sFolderPath);
        for (int i = 0; (i 
                    <= (oFolders.Length - 1)); i++) {
            string sChildFolder = oFolders[i];
            int iPos = sChildFolder.LastIndexOf("\\");
            string sFolderName = sChildFolder.Substring((iPos + 1));
            this.ProccessFolder(sChildFolder);
        }
        
    }


    private void qr_ReadBarcode(string sFromFilePath)
    {
        System.Drawing.Bitmap barcodeBitmap = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(sFromFilePath);
        string sFromPath = txtFrom.Text;
        string sFileName = sFromFilePath.Replace((sFromPath + "\\"), "");
        DateTime dtStart = DateTime.Now;

        var source = new BitmapLuminanceSource(barcodeBitmap);

        // using http://zxingnet.codeplex.com/
        // PM> Install-Package ZXing.Net
        var reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
        {
            AutoRotate = true,
            TryInverted = true,
            Options = new DecodingOptions
            {
                TryHarder = true,
                //PureBarcode = true,
                /*PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.CODE_128
                    //BarcodeFormat.EAN_8,
                    //BarcodeFormat.CODE_39,
                    //BarcodeFormat.UPC_A
                }*/
            }
        };

        //var newhint = new KeyValuePair<DecodeHintType, object>(DecodeHintType.ALLOWED_EAN_EXTENSIONS, new Object());
        //reader.Options.Hints.Add(newhint);

        var result = reader.Decode(source);
        if (result == null)
        {
            //Console.WriteLine("Decode failed.");
            this.WriteLog((sFileName + ("," + "Decode failed.")));
        }
        string sSec = "";
        if (chkShowTime.Checked)
        {
            sSec = ('\t' + DateTime.Now.Subtract(dtStart).TotalSeconds.ToString("#0.00"));
        }
        if (chkLog.Checked)
        {
            txtOutput.AppendText((sFileName + ('\t'
                            + (result.Text
                            + (sSec + "\r\n")))));
            txtOutput.AppendText("条码格式:" + result.BarcodeFormat + "\r\n");

        }

        this.WriteLog((sFileName + ('\t' + result.Text)));
        this.WriteLog(("条码格式:" + ('\t' + result.BarcodeFormat)));

        var writer = new BarcodeWriter
        {
            Format = result.BarcodeFormat,
            Options = { Width = 200, Height = 50, Margin = 4 },
            Renderer = new ZXing.Rendering.BitmapRenderer()
        };
        //生成条码图片
        //var barcodeImage = writer.Write(result.Text);
      



    }


    private void ConvertCP(string sFromFilePath, Encoding encode)
    {
        if (sFromFilePath.Trim().Equals(""))
            return;
        Encoding oriEncode;
        #region 编码识别

        IdentifyEncoding ie = new IdentifyEncoding();
        FileInfo fi = new FileInfo(sFromFilePath);
        string encodingName = string.Empty;
        string message = string.Empty;
        encodingName = ie.GetEncodingName(fi);
        fi = null;

        if (encodingName.ToLower() == "other")
        {
            message = string.Format("\r\n{0}文件格式不正确或已损坏。 ", sFromFilePath);
            txtOutput.AppendText((sFromFilePath + ('\t'
                                    + (message ))));
            return;
        }
        else
        {
            oriEncode = Encoding.GetEncoding(encodingName);
        }

        #endregion
        string text = File.ReadAllText(sFromFilePath, oriEncode);
        File.WriteAllText(sFromFilePath, text, encode);
        txtOutput.AppendText(sFromFilePath);
    }

    private void ReadBarcode(string sFromFilePath) {
        string sFromPath = txtFrom.Text;
        string sFileName = sFromFilePath.Replace((sFromPath + "\\"), "");
        System.Drawing.Bitmap oImage = null;
        try {
            oImage =(System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(sFromFilePath);
        }
        catch (Exception ex) {
            if (chkLog.Checked) {
                txtOutput.AppendText((sFileName + ('\t' + ("Could not open" + "\r\n"))));
            }
            
            this.WriteLog((sFileName + ("," + "Could not open")));
            return;
        }
        
        System.Collections.ArrayList barcodes = new System.Collections.ArrayList();
        int iScans = 100;
        DateTime dtStart = DateTime.Now;
        BarcodeImaging.UseBarcodeZones = chkUseBarcodeZones.Checked;
        BarcodeImaging.BarcodeType oBarcodeType = BarcodeImaging.BarcodeType.All;
        switch (cbBarcodeType.Text) {
            case "Code39":
                oBarcodeType = BarcodeImaging.BarcodeType.Code39;
                break;
            case "Code128":
                oBarcodeType = BarcodeImaging.BarcodeType.Code128;
                break;
            case "EAN":
                oBarcodeType = BarcodeImaging.BarcodeType.EAN;
                break;
        }
        switch (cbDirection.Text) {
            case "All":
                BarcodeImaging.FullScanBarcodeTypes = oBarcodeType;
                BarcodeImaging.FullScanPage(ref barcodes, oImage, iScans);
                break;
            case "Vertical":
                BarcodeImaging.ScanPage(ref barcodes, oImage, iScans, BarcodeImaging.ScanDirection.Horizontal, oBarcodeType);
                break;
            case "Horizontal":
                BarcodeImaging.ScanPage(ref barcodes, oImage, iScans, BarcodeImaging.ScanDirection.Vertical, oBarcodeType);
                break;
        }
        string sSec = "";
        if (chkShowTime.Checked) {
            sSec = ('\t' + DateTime.Now.Subtract(dtStart).TotalSeconds.ToString("#0.00"));
        }
        
        if ((barcodes.Count == 0)) {
            if (chkLog.Checked) {
                txtOutput.AppendText((sFileName + ('\t' + ("Failed" 
                                + (sSec + "\r\n")))));
            }
            
            this.WriteLog((sFileName + ("," + "Failed")));
        }
        else {
            foreach (object bc in barcodes) {
                if (chkLog.Checked) {
                    txtOutput.AppendText((sFileName + ('\t' 
                                    + (bc 
                                    + (sSec + "\r\n")))));
                }
                
                this.WriteLog((sFileName + ("," + bc)));
            }
            
        }
        
        oImage.Dispose();
    }
    
    private void WriteLog(string sLine) {
        if (chkCSV.Checked) {
            oLogFile.WriteLine(sLine);
        }
        
    }
    
    private string PadExt(string s) {
        s = s.ToUpper();
        if ((s.Length > 3)) {
            s = s.Substring(1, 3);
        }
        
        return s;
    }

    private void brtGen_Click(object sender, EventArgs e)
    {
        new frmBarcodeEncode().Show();
    }

    private void btnConvert_Click(object sender, EventArgs e)
    {
        btnProcess.Enabled = false;
        string sFromPath = txtFrom.Text;
        if (!Directory.Exists(sFromPath))
        {
            btnProcess.Enabled = true;
            return;
        }


        if ((txtStartFile.Text != ""))
        {
            bStartFile = false;
        }
        txtOutput.Text = "";
        txtOutput.AppendText(("Starting..." + "\r\n"));
        this.ConvertFolder(sFromPath);
        txtOutput.AppendText("Done!");
        btnProcess.Enabled = true;

    }


    void ConvertFolder(string sFolderPath)
    {
        string sFromPath = txtFrom.Text;
        string[] oFiles = Directory.GetFiles(sFolderPath);
        ProgressBar1.Maximum = oFiles.Length;
        for (int i = 0; (i
                    <= (oFiles.Length - 1)); i++)
        {
            string sFromFilePath = oFiles[i];
            if ((txtStartFile.Text != ""))
            {
                if ((txtStartFile.Text.ToLower().Trim() == sFromFilePath.ToLower()))
                {
                    bStartFile = true;
                }

            }

            if (bStartFile)
            {
                FileInfo oFileInfo = new FileInfo(frmBarcodeDecode.GetShortName(sFromFilePath));
                //string sExt = this.PadExt(oFileInfo.Extension);
                string sExt = oFileInfo.Extension.ToUpper().Replace(".","");
                if (((sExt == "MD")
                            || ((sExt == "TXT")
                            || ((sExt == "HTML")
                            || ((sExt == "HTM")
                            || ((sExt == "PY")
                            || (sExt == "PHP")))))))
                {
                    //UTF8Encoding(true)有BOM
                    this.ConvertCP(sFromFilePath, new System.Text.UTF8Encoding(true));
                }

            }

            ProgressBar1.Value = i;
            Application.DoEvents();
        }

        ProgressBar1.Value = 0;
        string[] oFolders = Directory.GetDirectories(sFolderPath);
        for (int i = 0; (i
                    <= (oFolders.Length - 1)); i++)
        {
            string sChildFolder = oFolders[i];
            int iPos = sChildFolder.LastIndexOf("\\");
            string sFolderName = sChildFolder.Substring((iPos + 1));
            this.ConvertFolder(sChildFolder);
        }

    }

    private void btnConvertFile_Click(object sender, EventArgs e)
    {
        new MainForm().Show();
    }
}