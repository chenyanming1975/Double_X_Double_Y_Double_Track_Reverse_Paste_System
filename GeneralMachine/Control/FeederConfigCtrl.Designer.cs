namespace GeneralMachine
{
    partial class FeederConfigCtrl
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
            this.components = new System.ComponentModel.Container();
            this.bMoveCam = new System.Windows.Forms.Button();
            this.bMoveNz = new System.Windows.Forms.Button();
            this.bUpdate = new System.Windows.Forms.Button();
            this.bRecordNZ = new System.Windows.Forms.Button();
            this.bMoveSuckHeight = new System.Windows.Forms.Button();
            this.bSuckTest = new System.Windows.Forms.Button();
            this.bRecordCam = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bAddRow = new System.Windows.Forms.Button();
            this.bDeleteRow = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyFeeder = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dGV_FeederLabelInfo = new System.Windows.Forms.DataGridView();
            this.enableSuckDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.labelSensorIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suckXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suckYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zHeightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suckAngleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.feederLabelInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nozzleRadio1 = new GeneralMachine.BasicUI.NozzleRadio();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_FeederLabelInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feederLabelInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bMoveCam
            // 
            this.bMoveCam.Dock = System.Windows.Forms.DockStyle.Top;
            this.bMoveCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMoveCam.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bMoveCam.Location = new System.Drawing.Point(0, 0);
            this.bMoveCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bMoveCam.Name = "bMoveCam";
            this.bMoveCam.Size = new System.Drawing.Size(67, 30);
            this.bMoveCam.TabIndex = 0;
            this.bMoveCam.Text = "到相机";
            this.bMoveCam.UseVisualStyleBackColor = true;
            this.bMoveCam.Click += new System.EventHandler(this.bMoveCam_Click);
            // 
            // bMoveNz
            // 
            this.bMoveNz.Dock = System.Windows.Forms.DockStyle.Top;
            this.bMoveNz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMoveNz.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bMoveNz.Location = new System.Drawing.Point(0, 30);
            this.bMoveNz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bMoveNz.Name = "bMoveNz";
            this.bMoveNz.Size = new System.Drawing.Size(67, 30);
            this.bMoveNz.TabIndex = 1;
            this.bMoveNz.Text = "到吸嘴";
            this.bMoveNz.UseVisualStyleBackColor = true;
            this.bMoveNz.Click += new System.EventHandler(this.bMoveNz_Click);
            // 
            // bUpdate
            // 
            this.bUpdate.BackColor = System.Drawing.Color.Yellow;
            this.bUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdate.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bUpdate.Location = new System.Drawing.Point(0, 296);
            this.bUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(67, 30);
            this.bUpdate.TabIndex = 2;
            this.bUpdate.Text = "更新";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // bRecordNZ
            // 
            this.bRecordNZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bRecordNZ.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bRecordNZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRecordNZ.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRecordNZ.Location = new System.Drawing.Point(0, 266);
            this.bRecordNZ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bRecordNZ.Name = "bRecordNZ";
            this.bRecordNZ.Size = new System.Drawing.Size(67, 30);
            this.bRecordNZ.TabIndex = 3;
            this.bRecordNZ.Text = "吸嘴记录";
            this.bRecordNZ.UseVisualStyleBackColor = false;
            this.bRecordNZ.Click += new System.EventHandler(this.bRecordNZ_Click);
            // 
            // bMoveSuckHeight
            // 
            this.bMoveSuckHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.bMoveSuckHeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMoveSuckHeight.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bMoveSuckHeight.Location = new System.Drawing.Point(0, 60);
            this.bMoveSuckHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bMoveSuckHeight.Name = "bMoveSuckHeight";
            this.bMoveSuckHeight.Size = new System.Drawing.Size(67, 30);
            this.bMoveSuckHeight.TabIndex = 4;
            this.bMoveSuckHeight.Text = "到吸高";
            this.bMoveSuckHeight.UseVisualStyleBackColor = true;
            this.bMoveSuckHeight.Click += new System.EventHandler(this.bMoveSuckHeight_Click);
            // 
            // bSuckTest
            // 
            this.bSuckTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.bSuckTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSuckTest.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bSuckTest.Location = new System.Drawing.Point(0, 90);
            this.bSuckTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSuckTest.Name = "bSuckTest";
            this.bSuckTest.Size = new System.Drawing.Size(67, 30);
            this.bSuckTest.TabIndex = 5;
            this.bSuckTest.Text = "吸单张";
            this.bSuckTest.UseVisualStyleBackColor = true;
            this.bSuckTest.Click += new System.EventHandler(this.bSuckTest_Click);
            // 
            // bRecordCam
            // 
            this.bRecordCam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bRecordCam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bRecordCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRecordCam.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRecordCam.Location = new System.Drawing.Point(0, 236);
            this.bRecordCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bRecordCam.Name = "bRecordCam";
            this.bRecordCam.Size = new System.Drawing.Size(67, 30);
            this.bRecordCam.TabIndex = 6;
            this.bRecordCam.Text = "相机记录";
            this.bRecordCam.UseVisualStyleBackColor = false;
            this.bRecordCam.Click += new System.EventHandler(this.bRecordCam_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bAddRow);
            this.panel2.Controls.Add(this.bDeleteRow);
            this.panel2.Controls.Add(this.bRecordCam);
            this.panel2.Controls.Add(this.bSuckTest);
            this.panel2.Controls.Add(this.bMoveSuckHeight);
            this.panel2.Controls.Add(this.bRecordNZ);
            this.panel2.Controls.Add(this.bUpdate);
            this.panel2.Controls.Add(this.bMoveNz);
            this.panel2.Controls.Add(this.bMoveCam);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(467, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(67, 326);
            this.panel2.TabIndex = 1;
            // 
            // bAddRow
            // 
            this.bAddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bAddRow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bAddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddRow.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bAddRow.Location = new System.Drawing.Point(0, 176);
            this.bAddRow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bAddRow.Name = "bAddRow";
            this.bAddRow.Size = new System.Drawing.Size(67, 30);
            this.bAddRow.TabIndex = 8;
            this.bAddRow.Text = "添加";
            this.bAddRow.UseVisualStyleBackColor = false;
            this.bAddRow.Click += new System.EventHandler(this.bAddRow_Click);
            // 
            // bDeleteRow
            // 
            this.bDeleteRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bDeleteRow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bDeleteRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDeleteRow.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bDeleteRow.Location = new System.Drawing.Point(0, 206);
            this.bDeleteRow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bDeleteRow.Name = "bDeleteRow";
            this.bDeleteRow.Size = new System.Drawing.Size(67, 30);
            this.bDeleteRow.TabIndex = 7;
            this.bDeleteRow.Text = "删除";
            this.bDeleteRow.UseVisualStyleBackColor = false;
            this.bDeleteRow.Click += new System.EventHandler(this.bDeleteRow_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 38);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(546, 366);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertyFeeder);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(538, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Feeder固有属性";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyFeeder
            // 
            this.propertyFeeder.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyFeeder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyFeeder.Location = new System.Drawing.Point(4, 5);
            this.propertyFeeder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.propertyFeeder.Name = "propertyFeeder";
            this.propertyFeeder.Size = new System.Drawing.Size(530, 327);
            this.propertyFeeder.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dGV_FeederLabelInfo);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(538, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Feeder点位设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dGV_FeederLabelInfo
            // 
            this.dGV_FeederLabelInfo.AllowUserToAddRows = false;
            this.dGV_FeederLabelInfo.AllowUserToDeleteRows = false;
            this.dGV_FeederLabelInfo.AutoGenerateColumns = false;
            this.dGV_FeederLabelInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_FeederLabelInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enableSuckDataGridViewCheckBoxColumn,
            this.labelSensorIndexDataGridViewTextBoxColumn,
            this.suckXDataGridViewTextBoxColumn,
            this.suckYDataGridViewTextBoxColumn,
            this.zHeightDataGridViewTextBoxColumn,
            this.suckAngleDataGridViewTextBoxColumn});
            this.dGV_FeederLabelInfo.DataSource = this.feederLabelInfoBindingSource;
            this.dGV_FeederLabelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV_FeederLabelInfo.Location = new System.Drawing.Point(4, 5);
            this.dGV_FeederLabelInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dGV_FeederLabelInfo.Name = "dGV_FeederLabelInfo";
            this.dGV_FeederLabelInfo.RowTemplate.Height = 23;
            this.dGV_FeederLabelInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGV_FeederLabelInfo.Size = new System.Drawing.Size(463, 326);
            this.dGV_FeederLabelInfo.TabIndex = 0;
            // 
            // enableSuckDataGridViewCheckBoxColumn
            // 
            this.enableSuckDataGridViewCheckBoxColumn.DataPropertyName = "EnableSuck";
            this.enableSuckDataGridViewCheckBoxColumn.HeaderText = "启用";
            this.enableSuckDataGridViewCheckBoxColumn.Name = "enableSuckDataGridViewCheckBoxColumn";
            this.enableSuckDataGridViewCheckBoxColumn.Width = 80;
            // 
            // labelSensorIndexDataGridViewTextBoxColumn
            // 
            this.labelSensorIndexDataGridViewTextBoxColumn.DataPropertyName = "LabelSensorIndex";
            this.labelSensorIndexDataGridViewTextBoxColumn.HeaderText = "到位光纤";
            this.labelSensorIndexDataGridViewTextBoxColumn.Name = "labelSensorIndexDataGridViewTextBoxColumn";
            this.labelSensorIndexDataGridViewTextBoxColumn.Width = 80;
            // 
            // suckXDataGridViewTextBoxColumn
            // 
            this.suckXDataGridViewTextBoxColumn.DataPropertyName = "SuckX";
            this.suckXDataGridViewTextBoxColumn.HeaderText = "X";
            this.suckXDataGridViewTextBoxColumn.Name = "suckXDataGridViewTextBoxColumn";
            this.suckXDataGridViewTextBoxColumn.Width = 60;
            // 
            // suckYDataGridViewTextBoxColumn
            // 
            this.suckYDataGridViewTextBoxColumn.DataPropertyName = "SuckY";
            this.suckYDataGridViewTextBoxColumn.HeaderText = "Y";
            this.suckYDataGridViewTextBoxColumn.Name = "suckYDataGridViewTextBoxColumn";
            this.suckYDataGridViewTextBoxColumn.Width = 60;
            // 
            // zHeightDataGridViewTextBoxColumn
            // 
            this.zHeightDataGridViewTextBoxColumn.DataPropertyName = "ZHeight";
            this.zHeightDataGridViewTextBoxColumn.HeaderText = "物料厚度";
            this.zHeightDataGridViewTextBoxColumn.Name = "zHeightDataGridViewTextBoxColumn";
            this.zHeightDataGridViewTextBoxColumn.Width = 80;
            // 
            // suckAngleDataGridViewTextBoxColumn
            // 
            this.suckAngleDataGridViewTextBoxColumn.DataPropertyName = "SuckAngle";
            this.suckAngleDataGridViewTextBoxColumn.HeaderText = "吸标角度";
            this.suckAngleDataGridViewTextBoxColumn.Name = "suckAngleDataGridViewTextBoxColumn";
            this.suckAngleDataGridViewTextBoxColumn.Width = 80;
            // 
            // feederLabelInfoBindingSource
            // 
            this.feederLabelInfoBindingSource.DataSource = typeof(GeneralMachine.Config.FeederLabelInfo);
            // 
            // nozzleRadio1
            // 
            this.nozzleRadio1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nozzleRadio1.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nozzleRadio1.Location = new System.Drawing.Point(0, 0);
            this.nozzleRadio1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nozzleRadio1.Name = "nozzleRadio1";
            this.nozzleRadio1.SelectNz = GeneralMachine.Config.Nozzle.Nz1;
            this.nozzleRadio1.Size = new System.Drawing.Size(546, 38);
            this.nozzleRadio1.TabIndex = 3;
            // 
            // FeederConfigCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.nozzleRadio1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FeederConfigCtrl";
            this.Size = new System.Drawing.Size(546, 404);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_FeederLabelInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feederLabelInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bMoveCam;
        private System.Windows.Forms.Button bMoveNz;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Button bRecordNZ;
        private System.Windows.Forms.Button bMoveSuckHeight;
        private System.Windows.Forms.Button bSuckTest;
        private System.Windows.Forms.Button bRecordCam;
        private System.Windows.Forms.Panel panel2;
        private BasicUI.NozzleRadio nozzleRadio1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid propertyFeeder;
        private System.Windows.Forms.DataGridView dGV_FeederLabelInfo;
        private System.Windows.Forms.BindingSource feederLabelInfoBindingSource;
        private System.Windows.Forms.Button bDeleteRow;
        private System.Windows.Forms.Button bAddRow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enableSuckDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelSensorIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn suckXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn suckYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zHeightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn suckAngleDataGridViewTextBoxColumn;
    }
}
