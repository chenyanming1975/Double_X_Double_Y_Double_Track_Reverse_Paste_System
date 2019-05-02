namespace GeneralMachine
{
    partial class TrackStateCtronl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_ManualTrackMode = new System.Windows.Forms.ComboBox();
            this.bOutput = new System.Windows.Forms.Button();
            this.bInput = new System.Windows.Forms.Button();
            this.gTrack = new System.Windows.Forms.GroupBox();
            this.lConyer1 = new System.Windows.Forms.Label();
            this.lConyer3 = new System.Windows.Forms.Label();
            this.lFlowINOUT = new System.Windows.Forms.Label();
            this.lConyer5 = new System.Windows.Forms.Label();
            this.lBeforeGive = new System.Windows.Forms.Label();
            this.lAfterRequest = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.gTrack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_ManualTrackMode);
            this.groupBox1.Controls.Add(this.bOutput);
            this.groupBox1.Controls.Add(this.bInput);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 110);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "手动进出板";
            // 
            // cb_ManualTrackMode
            // 
            this.cb_ManualTrackMode.FormattingEnabled = true;
            this.cb_ManualTrackMode.Location = new System.Drawing.Point(29, 48);
            this.cb_ManualTrackMode.Name = "cb_ManualTrackMode";
            this.cb_ManualTrackMode.Size = new System.Drawing.Size(112, 29);
            this.cb_ManualTrackMode.TabIndex = 9;
            this.cb_ManualTrackMode.Text = "左进左出";
            // 
            // bOutput
            // 
            this.bOutput.Location = new System.Drawing.Point(226, 48);
            this.bOutput.Name = "bOutput";
            this.bOutput.Size = new System.Drawing.Size(75, 31);
            this.bOutput.TabIndex = 8;
            this.bOutput.Text = "出板";
            this.bOutput.UseVisualStyleBackColor = true;
            this.bOutput.Click += new System.EventHandler(this.bOutput_Click);
            // 
            // bInput
            // 
            this.bInput.Location = new System.Drawing.Point(147, 48);
            this.bInput.Name = "bInput";
            this.bInput.Size = new System.Drawing.Size(75, 31);
            this.bInput.TabIndex = 7;
            this.bInput.Text = "进板";
            this.bInput.UseVisualStyleBackColor = true;
            this.bInput.Click += new System.EventHandler(this.bInput_Click);
            // 
            // gTrack
            // 
            this.gTrack.Controls.Add(this.lConyer1);
            this.gTrack.Controls.Add(this.lConyer3);
            this.gTrack.Controls.Add(this.lFlowINOUT);
            this.gTrack.Controls.Add(this.lConyer5);
            this.gTrack.Controls.Add(this.lBeforeGive);
            this.gTrack.Controls.Add(this.lAfterRequest);
            this.gTrack.Controls.Add(this.pictureBox1);
            this.gTrack.Dock = System.Windows.Forms.DockStyle.Top;
            this.gTrack.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gTrack.Location = new System.Drawing.Point(0, 0);
            this.gTrack.Name = "gTrack";
            this.gTrack.Size = new System.Drawing.Size(335, 90);
            this.gTrack.TabIndex = 12;
            this.gTrack.TabStop = false;
            this.gTrack.Text = "模组轨道";
            // 
            // lConyer1
            // 
            this.lConyer1.BackColor = System.Drawing.Color.White;
            this.lConyer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lConyer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lConyer1.Location = new System.Drawing.Point(79, 37);
            this.lConyer1.Name = "lConyer1";
            this.lConyer1.Size = new System.Drawing.Size(31, 29);
            this.lConyer1.TabIndex = 339;
            this.lConyer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lConyer3
            // 
            this.lConyer3.BackColor = System.Drawing.Color.White;
            this.lConyer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lConyer3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lConyer3.Location = new System.Drawing.Point(196, 37);
            this.lConyer3.Name = "lConyer3";
            this.lConyer3.Size = new System.Drawing.Size(31, 29);
            this.lConyer3.TabIndex = 341;
            this.lConyer3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lFlowINOUT
            // 
            this.lFlowINOUT.AutoSize = true;
            this.lFlowINOUT.BackColor = System.Drawing.Color.White;
            this.lFlowINOUT.Location = new System.Drawing.Point(116, 41);
            this.lFlowINOUT.Name = "lFlowINOUT";
            this.lFlowINOUT.Size = new System.Drawing.Size(74, 21);
            this.lFlowINOUT.TabIndex = 0;
            this.lFlowINOUT.Text = "左进右出";
            // 
            // lConyer5
            // 
            this.lConyer5.BackColor = System.Drawing.Color.White;
            this.lConyer5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lConyer5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lConyer5.Location = new System.Drawing.Point(233, 37);
            this.lConyer5.Name = "lConyer5";
            this.lConyer5.Size = new System.Drawing.Size(31, 29);
            this.lConyer5.TabIndex = 343;
            this.lConyer5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lBeforeGive
            // 
            this.lBeforeGive.AutoSize = true;
            this.lBeforeGive.BackColor = System.Drawing.Color.White;
            this.lBeforeGive.Location = new System.Drawing.Point(15, 41);
            this.lBeforeGive.Name = "lBeforeGive";
            this.lBeforeGive.Size = new System.Drawing.Size(58, 21);
            this.lBeforeGive.TabIndex = 2;
            this.lBeforeGive.Text = "前有板";
            // 
            // lAfterRequest
            // 
            this.lAfterRequest.AutoSize = true;
            this.lAfterRequest.BackColor = System.Drawing.Color.White;
            this.lAfterRequest.Location = new System.Drawing.Point(270, 41);
            this.lAfterRequest.Name = "lAfterRequest";
            this.lAfterRequest.Size = new System.Drawing.Size(58, 21);
            this.lAfterRequest.TabIndex = 7;
            this.lAfterRequest.Text = "后要板";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::GeneralMachine.Properties.Resources.conveyor;
            this.pictureBox1.Location = new System.Drawing.Point(3, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(329, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // TrackStateCtronl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gTrack);
            this.Name = "TrackStateCtronl";
            this.Size = new System.Drawing.Size(335, 212);
            this.Enter += new System.EventHandler(this.TrackStateCtronl_Enter);
            this.groupBox1.ResumeLayout(false);
            this.gTrack.ResumeLayout(false);
            this.gTrack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_ManualTrackMode;
        private System.Windows.Forms.Button bOutput;
        private System.Windows.Forms.Button bInput;
        public System.Windows.Forms.GroupBox gTrack;
        private System.Windows.Forms.Label lConyer1;
        private System.Windows.Forms.Label lConyer3;
        private System.Windows.Forms.Label lFlowINOUT;
        private System.Windows.Forms.Label lConyer5;
        private System.Windows.Forms.Label lBeforeGive;
        private System.Windows.Forms.Label lAfterRequest;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
