using GeneralMachine.Config;

namespace GeneralMachine.Flow.Editer
{
    partial class PasteListSetCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gPasteList = new System.Windows.Forms.GroupBox();
            this.dGVPaste = new System.Windows.Forms.DataGridView();
            this.pasteParamBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gEnable = new System.Windows.Forms.GroupBox();
            this.rbNoSelect = new System.Windows.Forms.RadioButton();
            this.rbSelect = new System.Windows.Forms.RadioButton();
            this.rbPasteRight = new System.Windows.Forms.RadioButton();
            this.rbPasteLeft = new System.Windows.Forms.RadioButton();
            this.rbPasteNULL = new System.Windows.Forms.RadioButton();
            this.rbPasteAll = new System.Windows.Forms.RadioButton();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bAutoByX = new System.Windows.Forms.Button();
            this.bAutoByY = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bDeleteSelect = new System.Windows.Forms.Button();
            this.bSetSelect = new System.Windows.Forms.Button();
            this.cbFeeder = new System.Windows.Forms.ComboBox();
            this.numAngle = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gOffset = new System.Windows.Forms.GroupBox();
            this.bInOffset = new System.Windows.Forms.Button();
            this.numOffsetY = new System.Windows.Forms.NumericUpDown();
            this.numOffsetX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canPasteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OffsetX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OffsetY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pasteAngleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Feeder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.badmarkIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barCodeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gPasteList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVPaste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pasteParamBindingSource)).BeginInit();
            this.gEnable.SuspendLayout();
            this.toolPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).BeginInit();
            this.gOffset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetX)).BeginInit();
            this.SuspendLayout();
            // 
            // gPasteList
            // 
            this.gPasteList.Controls.Add(this.dGVPaste);
            this.gPasteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gPasteList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gPasteList.Location = new System.Drawing.Point(0, 0);
            this.gPasteList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gPasteList.Name = "gPasteList";
            this.gPasteList.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gPasteList.Size = new System.Drawing.Size(515, 505);
            this.gPasteList.TabIndex = 1;
            this.gPasteList.TabStop = false;
            this.gPasteList.Text = "贴附列表";
            // 
            // dGVPaste
            // 
            this.dGVPaste.AllowUserToAddRows = false;
            this.dGVPaste.AllowUserToDeleteRows = false;
            this.dGVPaste.AllowUserToOrderColumns = true;
            this.dGVPaste.AutoGenerateColumns = false;
            this.dGVPaste.BackgroundColor = System.Drawing.Color.White;
            this.dGVPaste.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVPaste.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.Column1,
            this.canPasteDataGridViewCheckBoxColumn,
            this.OffsetX,
            this.OffsetY,
            this.pasteAngleDataGridViewTextBoxColumn,
            this.Feeder,
            this.badmarkIDDataGridViewTextBoxColumn,
            this.barCodeIDDataGridViewTextBoxColumn});
            this.dGVPaste.DataSource = this.pasteParamBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGVPaste.DefaultCellStyle = dataGridViewCellStyle1;
            this.dGVPaste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVPaste.Location = new System.Drawing.Point(3, 20);
            this.dGVPaste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dGVPaste.Name = "dGVPaste";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVPaste.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dGVPaste.RowHeadersVisible = false;
            this.dGVPaste.RowTemplate.Height = 23;
            this.dGVPaste.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVPaste.Size = new System.Drawing.Size(509, 481);
            this.dGVPaste.TabIndex = 0;
            // 
            // pasteParamBindingSource
            // 
            this.pasteParamBindingSource.DataSource = typeof(GeneralMachine.Flow.Nodes.PasteParam);
            // 
            // gEnable
            // 
            this.gEnable.Controls.Add(this.rbNoSelect);
            this.gEnable.Controls.Add(this.rbSelect);
            this.gEnable.Controls.Add(this.rbPasteRight);
            this.gEnable.Controls.Add(this.rbPasteLeft);
            this.gEnable.Controls.Add(this.rbPasteNULL);
            this.gEnable.Controls.Add(this.rbPasteAll);
            this.gEnable.Dock = System.Windows.Forms.DockStyle.Top;
            this.gEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gEnable.Location = new System.Drawing.Point(0, 0);
            this.gEnable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gEnable.Name = "gEnable";
            this.gEnable.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gEnable.Size = new System.Drawing.Size(185, 155);
            this.gEnable.TabIndex = 5;
            this.gEnable.TabStop = false;
            this.gEnable.Text = "贴附模式";
            // 
            // rbNoSelect
            // 
            this.rbNoSelect.AutoSize = true;
            this.rbNoSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbNoSelect.Location = new System.Drawing.Point(3, 125);
            this.rbNoSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbNoSelect.Name = "rbNoSelect";
            this.rbNoSelect.Size = new System.Drawing.Size(179, 21);
            this.rbNoSelect.TabIndex = 5;
            this.rbNoSelect.TabStop = true;
            this.rbNoSelect.Text = "只贴不选中";
            this.rbNoSelect.UseVisualStyleBackColor = true;
            // 
            // rbSelect
            // 
            this.rbSelect.AutoSize = true;
            this.rbSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbSelect.Location = new System.Drawing.Point(3, 104);
            this.rbSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbSelect.Name = "rbSelect";
            this.rbSelect.Size = new System.Drawing.Size(179, 21);
            this.rbSelect.TabIndex = 4;
            this.rbSelect.TabStop = true;
            this.rbSelect.Text = "只贴选中";
            this.rbSelect.UseVisualStyleBackColor = true;
            // 
            // rbPasteRight
            // 
            this.rbPasteRight.AutoSize = true;
            this.rbPasteRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPasteRight.Location = new System.Drawing.Point(3, 83);
            this.rbPasteRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbPasteRight.Name = "rbPasteRight";
            this.rbPasteRight.Size = new System.Drawing.Size(179, 21);
            this.rbPasteRight.TabIndex = 3;
            this.rbPasteRight.TabStop = true;
            this.rbPasteRight.Text = "只贴右Feeder";
            this.rbPasteRight.UseVisualStyleBackColor = true;
            this.rbPasteRight.CheckedChanged += new System.EventHandler(this.rbPasteRight_CheckedChanged);
            // 
            // rbPasteLeft
            // 
            this.rbPasteLeft.AutoSize = true;
            this.rbPasteLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPasteLeft.Location = new System.Drawing.Point(3, 62);
            this.rbPasteLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbPasteLeft.Name = "rbPasteLeft";
            this.rbPasteLeft.Size = new System.Drawing.Size(179, 21);
            this.rbPasteLeft.TabIndex = 2;
            this.rbPasteLeft.TabStop = true;
            this.rbPasteLeft.Text = "只贴左Feeder";
            this.rbPasteLeft.UseVisualStyleBackColor = true;
            this.rbPasteLeft.CheckedChanged += new System.EventHandler(this.rbPasteLeft_CheckedChanged);
            // 
            // rbPasteNULL
            // 
            this.rbPasteNULL.AutoSize = true;
            this.rbPasteNULL.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPasteNULL.Location = new System.Drawing.Point(3, 41);
            this.rbPasteNULL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbPasteNULL.Name = "rbPasteNULL";
            this.rbPasteNULL.Size = new System.Drawing.Size(179, 21);
            this.rbPasteNULL.TabIndex = 1;
            this.rbPasteNULL.TabStop = true;
            this.rbPasteNULL.Text = "全不贴";
            this.rbPasteNULL.UseVisualStyleBackColor = true;
            this.rbPasteNULL.CheckedChanged += new System.EventHandler(this.rbPasteNULL_CheckedChanged);
            // 
            // rbPasteAll
            // 
            this.rbPasteAll.AutoSize = true;
            this.rbPasteAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPasteAll.Location = new System.Drawing.Point(3, 20);
            this.rbPasteAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbPasteAll.Name = "rbPasteAll";
            this.rbPasteAll.Size = new System.Drawing.Size(179, 21);
            this.rbPasteAll.TabIndex = 0;
            this.rbPasteAll.TabStop = true;
            this.rbPasteAll.Text = "全贴";
            this.rbPasteAll.UseVisualStyleBackColor = true;
            this.rbPasteAll.CheckedChanged += new System.EventHandler(this.rbPasteAll_CheckedChanged);
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.groupBox2);
            this.toolPanel.Controls.Add(this.groupBox1);
            this.toolPanel.Controls.Add(this.gOffset);
            this.toolPanel.Controls.Add(this.bClose);
            this.toolPanel.Controls.Add(this.gEnable);
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolPanel.Location = new System.Drawing.Point(515, 0);
            this.toolPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(185, 505);
            this.toolPanel.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bAutoByX);
            this.groupBox2.Controls.Add(this.bAutoByY);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 376);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(185, 92);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "整体设置";
            // 
            // bAutoByX
            // 
            this.bAutoByX.BackColor = System.Drawing.Color.Yellow;
            this.bAutoByX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bAutoByX.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bAutoByX.Location = new System.Drawing.Point(3, 22);
            this.bAutoByX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bAutoByX.Name = "bAutoByX";
            this.bAutoByX.Size = new System.Drawing.Size(179, 33);
            this.bAutoByX.TabIndex = 7;
            this.bAutoByX.Text = "按X坐标排序";
            this.bAutoByX.UseVisualStyleBackColor = false;
            this.bAutoByX.Click += new System.EventHandler(this.bAutoByX_Click);
            // 
            // bAutoByY
            // 
            this.bAutoByY.BackColor = System.Drawing.Color.Yellow;
            this.bAutoByY.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bAutoByY.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bAutoByY.Location = new System.Drawing.Point(3, 55);
            this.bAutoByY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bAutoByY.Name = "bAutoByY";
            this.bAutoByY.Size = new System.Drawing.Size(179, 33);
            this.bAutoByY.TabIndex = 6;
            this.bAutoByY.Text = "按Y坐标排序";
            this.bAutoByY.UseVisualStyleBackColor = false;
            this.bAutoByY.Click += new System.EventHandler(this.bAutoByY_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bDeleteSelect);
            this.groupBox1.Controls.Add(this.bSetSelect);
            this.groupBox1.Controls.Add(this.cbFeeder);
            this.groupBox1.Controls.Add(this.numAngle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 242);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(185, 134);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "整体设置";
            // 
            // bDeleteSelect
            // 
            this.bDeleteSelect.BackColor = System.Drawing.Color.Yellow;
            this.bDeleteSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bDeleteSelect.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bDeleteSelect.Location = new System.Drawing.Point(3, 97);
            this.bDeleteSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bDeleteSelect.Name = "bDeleteSelect";
            this.bDeleteSelect.Size = new System.Drawing.Size(179, 33);
            this.bDeleteSelect.TabIndex = 6;
            this.bDeleteSelect.Text = "删除选中";
            this.bDeleteSelect.UseVisualStyleBackColor = false;
            this.bDeleteSelect.Click += new System.EventHandler(this.bDeleteSelect_Click);
            // 
            // bSetSelect
            // 
            this.bSetSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bSetSelect.Location = new System.Drawing.Point(130, 26);
            this.bSetSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bSetSelect.Name = "bSetSelect";
            this.bSetSelect.Size = new System.Drawing.Size(47, 56);
            this.bSetSelect.TabIndex = 5;
            this.bSetSelect.Text = "设置";
            this.bSetSelect.UseVisualStyleBackColor = false;
            this.bSetSelect.Click += new System.EventHandler(this.bSetSelect_Click);
            // 
            // cbFeeder
            // 
            this.cbFeeder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeeder.FormattingEnabled = true;
            this.cbFeeder.Items.AddRange(new object[] {
            "左Feeder",
            "右Feeder"});
            this.cbFeeder.Location = new System.Drawing.Point(54, 57);
            this.cbFeeder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbFeeder.Name = "cbFeeder";
            this.cbFeeder.Size = new System.Drawing.Size(70, 25);
            this.cbFeeder.TabIndex = 5;
            // 
            // numAngle
            // 
            this.numAngle.DecimalPlaces = 2;
            this.numAngle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numAngle.Location = new System.Drawing.Point(54, 26);
            this.numAngle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numAngle.Name = "numAngle";
            this.numAngle.Size = new System.Drawing.Size(70, 26);
            this.numAngle.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Feeder:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "R:";
            // 
            // gOffset
            // 
            this.gOffset.Controls.Add(this.bInOffset);
            this.gOffset.Controls.Add(this.numOffsetY);
            this.gOffset.Controls.Add(this.numOffsetX);
            this.gOffset.Controls.Add(this.label2);
            this.gOffset.Controls.Add(this.label1);
            this.gOffset.Dock = System.Windows.Forms.DockStyle.Top;
            this.gOffset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gOffset.Location = new System.Drawing.Point(0, 155);
            this.gOffset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gOffset.Name = "gOffset";
            this.gOffset.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gOffset.Size = new System.Drawing.Size(185, 87);
            this.gOffset.TabIndex = 8;
            this.gOffset.TabStop = false;
            this.gOffset.Text = "整体偏移";
            // 
            // bInOffset
            // 
            this.bInOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bInOffset.Location = new System.Drawing.Point(130, 21);
            this.bInOffset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bInOffset.Name = "bInOffset";
            this.bInOffset.Size = new System.Drawing.Size(47, 61);
            this.bInOffset.TabIndex = 4;
            this.bInOffset.Text = "设置";
            this.bInOffset.UseVisualStyleBackColor = false;
            this.bInOffset.Click += new System.EventHandler(this.bInOffset_Click);
            // 
            // numOffsetY
            // 
            this.numOffsetY.DecimalPlaces = 2;
            this.numOffsetY.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numOffsetY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numOffsetY.Location = new System.Drawing.Point(54, 56);
            this.numOffsetY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numOffsetY.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numOffsetY.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numOffsetY.Name = "numOffsetY";
            this.numOffsetY.Size = new System.Drawing.Size(70, 26);
            this.numOffsetY.TabIndex = 3;
            // 
            // numOffsetX
            // 
            this.numOffsetX.DecimalPlaces = 2;
            this.numOffsetX.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numOffsetX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numOffsetX.Location = new System.Drawing.Point(54, 21);
            this.numOffsetX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numOffsetX.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numOffsetX.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numOffsetX.Name = "numOffsetX";
            this.numOffsetX.Size = new System.Drawing.Size(70, 26);
            this.numOffsetX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.Yellow;
            this.bClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bClose.Location = new System.Drawing.Point(0, 472);
            this.bClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(185, 33);
            this.bClose.TabIndex = 7;
            this.bClose.Text = "关闭";
            this.bClose.UseVisualStyleBackColor = false;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.FillWeight = 80F;
            this.iDDataGridViewTextBoxColumn.HeaderText = "流程ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 80;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MapID";
            this.Column1.FillWeight = 80F;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // canPasteDataGridViewCheckBoxColumn
            // 
            this.canPasteDataGridViewCheckBoxColumn.DataPropertyName = "CanPaste";
            this.canPasteDataGridViewCheckBoxColumn.FillWeight = 80F;
            this.canPasteDataGridViewCheckBoxColumn.HeaderText = "是否贴附";
            this.canPasteDataGridViewCheckBoxColumn.Name = "canPasteDataGridViewCheckBoxColumn";
            this.canPasteDataGridViewCheckBoxColumn.Width = 80;
            // 
            // OffsetX
            // 
            this.OffsetX.DataPropertyName = "OffsetX";
            this.OffsetX.FillWeight = 80F;
            this.OffsetX.HeaderText = "偏移X";
            this.OffsetX.Name = "OffsetX";
            this.OffsetX.Width = 80;
            // 
            // OffsetY
            // 
            this.OffsetY.DataPropertyName = "OffsetY";
            this.OffsetY.FillWeight = 80F;
            this.OffsetY.HeaderText = "偏移Y";
            this.OffsetY.Name = "OffsetY";
            this.OffsetY.Width = 80;
            // 
            // pasteAngleDataGridViewTextBoxColumn
            // 
            this.pasteAngleDataGridViewTextBoxColumn.DataPropertyName = "PasteAngle";
            this.pasteAngleDataGridViewTextBoxColumn.FillWeight = 80F;
            this.pasteAngleDataGridViewTextBoxColumn.HeaderText = "贴附角度";
            this.pasteAngleDataGridViewTextBoxColumn.Name = "pasteAngleDataGridViewTextBoxColumn";
            this.pasteAngleDataGridViewTextBoxColumn.Width = 80;
            // 
            // Feeder
            // 
            this.Feeder.DataPropertyName = "Feeder";
            this.Feeder.HeaderText = "Feeder";
            this.Feeder.Name = "Feeder";
            // 
            // badmarkIDDataGridViewTextBoxColumn
            // 
            this.badmarkIDDataGridViewTextBoxColumn.DataPropertyName = "BadmarkID";
            this.badmarkIDDataGridViewTextBoxColumn.FillWeight = 80F;
            this.badmarkIDDataGridViewTextBoxColumn.HeaderText = "坏点ID";
            this.badmarkIDDataGridViewTextBoxColumn.Name = "badmarkIDDataGridViewTextBoxColumn";
            this.badmarkIDDataGridViewTextBoxColumn.Width = 80;
            // 
            // barCodeIDDataGridViewTextBoxColumn
            // 
            this.barCodeIDDataGridViewTextBoxColumn.DataPropertyName = "BarCodeID";
            this.barCodeIDDataGridViewTextBoxColumn.FillWeight = 80F;
            this.barCodeIDDataGridViewTextBoxColumn.HeaderText = "条码ID";
            this.barCodeIDDataGridViewTextBoxColumn.Name = "barCodeIDDataGridViewTextBoxColumn";
            this.barCodeIDDataGridViewTextBoxColumn.Width = 80;
            // 
            // PasteListSetCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 505);
            this.Controls.Add(this.gPasteList);
            this.Controls.Add(this.toolPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PasteListSetCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PasteListSetCtrl_Load);
            this.gPasteList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVPaste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pasteParamBindingSource)).EndInit();
            this.gEnable.ResumeLayout(false);
            this.gEnable.PerformLayout();
            this.toolPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).EndInit();
            this.gOffset.ResumeLayout(false);
            this.gOffset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffsetX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gPasteList;
        private System.Windows.Forms.GroupBox gEnable;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.RadioButton rbPasteRight;
        private System.Windows.Forms.RadioButton rbPasteLeft;
        private System.Windows.Forms.RadioButton rbPasteNULL;
        private System.Windows.Forms.RadioButton rbPasteAll;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.DataGridView dGVPaste;
        private System.Windows.Forms.GroupBox gOffset;
        private System.Windows.Forms.NumericUpDown numOffsetY;
        private System.Windows.Forms.NumericUpDown numOffsetX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bInOffset;
        private System.Windows.Forms.RadioButton rbNoSelect;
        private System.Windows.Forms.RadioButton rbSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFeeder;
        private System.Windows.Forms.NumericUpDown numAngle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bSetSelect;
        private System.Windows.Forms.Button bDeleteSelect;
        private System.Windows.Forms.BindingSource pasteParamBindingSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bAutoByX;
        private System.Windows.Forms.Button bAutoByY;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn canPasteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OffsetX;
        private System.Windows.Forms.DataGridViewTextBoxColumn OffsetY;
        private System.Windows.Forms.DataGridViewTextBoxColumn pasteAngleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Feeder;
        private System.Windows.Forms.DataGridViewTextBoxColumn badmarkIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barCodeIDDataGridViewTextBoxColumn;
    }
}
