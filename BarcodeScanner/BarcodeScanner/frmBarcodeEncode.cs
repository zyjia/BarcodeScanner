using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

public partial class frmBarcodeEncode : Form
    {
        public frmBarcodeEncode()
        {
            InitializeComponent();
        }

        private void frmSample_Load(object sender, EventArgs e)
        {
            cboEncoding.SelectedIndex = 2;
            cboVersion.SelectedIndex = 6;
            cboCorrectionLevel.SelectedIndex = 1;
        }

     
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEncode_Click_1(object sender, EventArgs e)
        {
            if (txtEncodeData.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请输入要生成二维码的内容！");
                return;
            }
        // 1.设置QR二维码的规格
        ZXing.QrCode.QrCodeEncodingOptions qrEncodeOption = new ZXing.QrCode.QrCodeEncodingOptions();
        qrEncodeOption.CharacterSet = "UTF-8"; // 设置编码格式，否则读取'中文'乱码
        int scale =Convert.ToInt16(txtSize.Text);
        qrEncodeOption.CharacterSet = "UTF-8"; // 设置编码格式，否则读取'中文'乱码
        qrEncodeOption.Height = scale;
        qrEncodeOption.Width = scale;
        qrEncodeOption.Margin = 1; // 设置周围空白边距

        int version = Convert.ToInt16(cboVersion.Text);
        //选择编码修正方式，修正百分比越高内容越精确
        string errorCorrect = cboCorrectionLevel.Text;
        if (errorCorrect == "L")
            qrEncodeOption.ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L;//水平 7% 的字码可被修正
        else if (errorCorrect == "M")
            qrEncodeOption.ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M;//水平 15% 的字码可被修正
        else if (errorCorrect == "Q")
            qrEncodeOption.ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.Q;//水平 25% 的字码可被修正
        else if (errorCorrect == "H")
            qrEncodeOption.ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H;//水平 30% 的字码可被修正

        // 2.生成条形码图片并保存
        ZXing.BarcodeWriter wr = new BarcodeWriter();
        wr.Format = BarcodeFormat.QR_CODE; // 二维码
        wr.Options = qrEncodeOption;
        Bitmap img = wr.Write(this.txtEncodeData.Text.Trim());

        picEncode.Image = img;
        }


        //保存图片
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            saveFileDialog1.Title = "Save";
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
               
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }

                fs.Close();
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1 ;
            DialogResult r = printDialog1.ShowDialog();
            if ( r == DialogResult.OK ) {
                printDocument1.Print();
            }            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(picEncode.Image,0,0);          
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;               
                picDecode.Image = new Bitmap(fileName);
                
            }
        }

        private void btnDecode_Click_1(object sender, EventArgs e)
        {           
            try
            {                

            System.Drawing.Bitmap barcodeBitmap = new Bitmap(picDecode.Image);
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


            var decodedString = reader.Decode(source);
            if (decodedString == null)
            {
                //Console.WriteLine("Decode failed.");
                MessageBox.Show("读取失败");
            }
            var writer = new BarcodeWriter
            {
                Format = decodedString.BarcodeFormat,
                Options = { Width = 200, Height = 50, Margin = 4 },
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };
            txtDecodedData.Text = decodedString.Text;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
     }
