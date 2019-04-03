
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class frmTxt2Dpl : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components;
        private string filePath = string.Empty;
        private readonly string filePlayed = "*played*0";
        private readonly string fileTitle = "*title*";
        private readonly string fileUrl = "*file*";
        private int i = 1;
        private Label label1;
        private Label label2;
        private readonly string str1 = "DAUMPLAYLIST\n";
        private readonly string str2 = "playname = \n";
        private readonly string str3 = "topindex = 0\n";

        public frmTxt2Dpl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Multiselect = true,
                Title = "请选择文件",
                Filter = "文本文件(txt)|*.txt"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.filePath = dialog.FileName;
                this.label2.Text = this.filePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.filePath))
            {
                this.label1.Text = "未选择文件";
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    FileName = "temp.dpl",
                    Filter = "电视源(dpl)|*.dpl",
                    RestoreDirectory = true
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    new FileStream(dialog.FileName, FileMode.Create).Close();
                }
                this.WriteFile(dialog.FileName, this.str1);
                this.WriteFile(dialog.FileName, this.str2);
                this.WriteFile(dialog.FileName, this.str3);
                StreamReader reader = new StreamReader(this.filePath);
                string str = "";
                while (str != null)
                {
                    str = reader.ReadLine();
                    if ((str != null) && !str.Equals(""))
                    {
                        char[] separator = new char[] { ',' };
                        string[] strArray = str.Split(separator);
                        if (strArray.Length == 2)
                        {
                            string info = this.i.ToString() + this.fileUrl + strArray[1] + "\n";
                            this.WriteFile(dialog.FileName, info);
                            string str3 = this.i.ToString() + this.fileTitle + Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(strArray[0])) + "\n";
                            this.WriteFile(dialog.FileName, str3);
                            string str4 = this.i.ToString() + this.filePlayed + "\n";
                            this.WriteFile(dialog.FileName, str4);
                            this.i++;
                        }
                    }
                }
                reader.Close();
                this.label1.Text = "转换完成";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            base.SuspendLayout();
            this.button1.Location = new Point(0x16, 0x29);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0x16, 0x9b);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "开始转换";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0xeb);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x69);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 12);
            this.label2.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(120, 0x116);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Name = "Form1";
            this.Text = "txtTodpl";
            base.Load += new EventHandler(this.Form1_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void WriteFile(string writeFilePath, string info)
        {
            FileStream stream;
            if (!File.Exists(writeFilePath))
            {
                stream = new FileStream(writeFilePath, FileMode.Create);
            }
            else
            {
                stream = new FileStream(writeFilePath, FileMode.Append);
            }
            StreamWriter writer1 = new StreamWriter(stream);
            writer1.Write(info);
            writer1.Flush();
            writer1.Close();
            stream.Close();
        }
    }


