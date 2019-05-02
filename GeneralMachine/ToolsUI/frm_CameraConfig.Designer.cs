namespace GeneralMachine
{
    partial class frm_CameraConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel6 = new System.Windows.Forms.Panel();
            this.imageSet = new NationalInstruments.Vision.WindowsForms.ImageViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bLinkLight = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bOpenCam = new System.Windows.Forms.Button();
            this.cb_CamList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bUpdate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.cameraRatio1 = new GeneralMachine.BasicUI.CameraRatio();
            this.moduleRadio1 = new GeneralMachine.BasicUI.ModuleRadio();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.imageSet);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Controls.Add(this.cb_CamList);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.bUpdate);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel6.Location = new System.Drawing.Point(578, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(394, 450);
            this.panel6.TabIndex = 9;
            // 
            // imageSet
            // 
            this.imageSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSet.Location = new System.Drawing.Point(0, 185);
            this.imageSet.Margin = new System.Windows.Forms.Padding(0);
            this.imageSet.Name = "imageSet";
            this.imageSet.ShowImageInfo = true;
            this.imageSet.ShowToolbar = true;
            this.imageSet.Size = new System.Drawing.Size(394, 265);
            this.imageSet.TabIndex = 284;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bLinkLight);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.bOpenCam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 31);
            this.panel1.TabIndex = 5;
            // 
            // button5
            // 
            this.bLinkLight.Dock = System.Windows.Forms.DockStyle.Left;
            this.bLinkLight.Location = new System.Drawing.Point(264, 0);
            this.bLinkLight.Name = "button5";
            this.bLinkLight.Size = new System.Drawing.Size(88, 31);
            this.bLinkLight.TabIndex = 4;
            this.bLinkLight.Text = "链接光源";
            this.bLinkLight.UseVisualStyleBackColor = true;
            this.bLinkLight.Click += new System.EventHandler(this.bLinkLight_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.Location = new System.Drawing.Point(198, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 31);
            this.button4.TabIndex = 3;
            this.button4.Text = "蓝光";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OpenBlue_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button3.Location = new System.Drawing.Point(132, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 31);
            this.button3.TabIndex = 2;
            this.button3.Text = "绿光";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OpenGreen_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.Location = new System.Drawing.Point(66, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 31);
            this.button2.TabIndex = 1;
            this.button2.Text = "红光";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OpenRed_Click);
            // 
            // button1
            // 
            this.bOpenCam.Dock = System.Windows.Forms.DockStyle.Left;
            this.bOpenCam.Location = new System.Drawing.Point(0, 0);
            this.bOpenCam.Name = "button1";
            this.bOpenCam.Size = new System.Drawing.Size(66, 31);
            this.bOpenCam.TabIndex = 0;
            this.bOpenCam.Text = "实时";
            this.bOpenCam.UseVisualStyleBackColor = true;
            this.bOpenCam.Click += new System.EventHandler(this.OpenCam_Click);
            // 
            // cb_CamList
            // 
            this.cb_CamList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_CamList.FormattingEnabled = true;
            this.cb_CamList.Location = new System.Drawing.Point(0, 125);
            this.cb_CamList.Name = "cb_CamList";
            this.cb_CamList.Size = new System.Drawing.Size(394, 29);
            this.cb_CamList.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(394, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "相机列表：";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 27);
            this.label1.TabIndex = 2;
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Red;
            this.bUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdate.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUpdate.Location = new System.Drawing.Point(0, 29);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(394, 42);
            this.bUpdate.TabIndex = 1;
            this.bUpdate.Text = "更新设置";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(394, 29);
            this.label6.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.propertyGrid1.Location = new System.Drawing.Point(0, 82);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(578, 368);
            this.propertyGrid1.TabIndex = 12;
            // 
            // cameraRatio1
            // 
            this.cameraRatio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cameraRatio1.Location = new System.Drawing.Point(0, 40);
            this.cameraRatio1.Name = "cameraRatio1";
            this.cameraRatio1.SelectCamera = GeneralMachine.Config.Camera.Top;
            this.cameraRatio1.Size = new System.Drawing.Size(578, 42);
            this.cameraRatio1.TabIndex = 11;
            // 
            // moduleRadio1
            // 
            this.moduleRadio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleRadio1.Location = new System.Drawing.Point(0, 0);
            this.moduleRadio1.Name = "moduleRadio1";
            this.moduleRadio1.Size = new System.Drawing.Size(578, 40);
            this.moduleRadio1.TabIndex = 10;
            // 
            // frm_CameraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(972, 450);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.cameraRatio1);
            this.Controls.Add(this.moduleRadio1);
            this.Controls.Add(this.panel6);
            this.Name = "frm_CameraConfig";
            this.Text = "相机配置";
            this.Load += new System.EventHandler(this.frm_CameraConfig_Load);
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Label label6;
        private BasicUI.ModuleRadio moduleRadio1;
        private BasicUI.CameraRatio cameraRatio1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_CamList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bOpenCam;
        public NationalInstruments.Vision.WindowsForms.ImageViewer imageSet;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button bLinkLight;
    }
}