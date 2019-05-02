namespace GeneralMachine.ProgramUI
{
    partial class MachineStateCtrl
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
            this.dGVMachineState = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dGVMachineState)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVMachineState
            // 
            this.dGVMachineState.AllowUserToAddRows = false;
            this.dGVMachineState.AllowUserToDeleteRows = false;
            this.dGVMachineState.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVMachineState.BackgroundColor = System.Drawing.Color.White;
            this.dGVMachineState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVMachineState.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dGVMachineState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVMachineState.Location = new System.Drawing.Point(0, 0);
            this.dGVMachineState.Name = "dGVMachineState";
            this.dGVMachineState.ReadOnly = true;
            this.dGVMachineState.RowHeadersVisible = false;
            this.dGVMachineState.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dGVMachineState.RowTemplate.Height = 23;
            this.dGVMachineState.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dGVMachineState.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dGVMachineState.Size = new System.Drawing.Size(237, 519);
            this.dGVMachineState.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "机器状态";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "前模组";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "后模组";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MachineStateCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dGVMachineState);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MachineStateCtrl";
            this.Size = new System.Drawing.Size(237, 519);
            this.Load += new System.EventHandler(this.MachineStateCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVMachineState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVMachineState;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
