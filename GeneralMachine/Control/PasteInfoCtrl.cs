using GeneralMachine.Config;
using GeneralMachine.Flow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine
{
    public partial class PasteInfoCtrl : Control
    {
        public PasteInfoCtrl()
        {
            InitializeComponent();
            this.BackColor = Color.White;
        }

        private Module module = Module.Front;
        private int size = 10;

        #region 对外接口
        public void ChangedXYRegion()
        {
            pasteRegion = new RectangleF();
            var runDate = SystemEntiy.Instance.FlowMachine[this.module].RunData;
            float minX = pasteRegion.X;
            float maxX = pasteRegion.X;
            float minY = pasteRegion.Y;
            float maxY = pasteRegion.Y;
            if (runDate.BoardCount> 0 && runDate[0].PCSCount > 0)
            {
                minX = maxX = runDate[0][0].UpPastePt.X;
                minY = maxY = runDate[0][0].UpPastePt.Y;
            }

            int row = runDate.BoardCount;
            int maxColumn = 0;
            for (int i = 0; i < runDate.BoardCount; ++i)
            {
                int curColumn = runDate[i].PCSCount;
                if (curColumn > maxColumn)
                    maxColumn = curColumn;

                for (int j = 0; j < curColumn; ++j)
                {
                    if (runDate[i][j].UpPastePt.X < minX)
                    {
                        minX = runDate[i][j].UpPastePt.X;
                    }

                    if (runDate[i][j].UpPastePt.X > maxX)
                    {
                        maxX = runDate[i][j].UpPastePt.X;
                    }

                    if (runDate[i][j].UpPastePt.Y < minY)
                    {
                        minY = runDate[i][j].UpPastePt.Y;
                    }

                    if (runDate[i][j].UpPastePt.Y > maxY)
                    {
                        maxY = runDate[i][j].UpPastePt.Y;
                    }
                }
            }

            pasteRegion = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            int rowSize = (int)(pasteRegion.Width / maxColumn);
            int colSize = (int)(pasteRegion.Height / row);
            this.size = 2*(rowSize > colSize ? colSize : rowSize);
            if (this.size > 30)
                this.size = 30;
        }
        #endregion

        public Module CurModule
        {
            get { return this.module; }
            set { this.module = value; }
        }

        #region 贴附区域显示比例
        /// <summary>
        /// 贴附区域坐标
        /// </summary>
        private RectangleF pasteRegion = new RectangleF();
        private Rectangle uiRegion = new Rectangle();
        /// <summary>
        /// 计算贴附区域
        /// </summary>
      
        /// <summary>
        /// 获得UI区域的坐标
        /// </summary>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        private Point AdjuestPt(PointF wroldPt)
        {
            Point point = new Point();

            try
            {
                float zoomx = (wroldPt.X - this.pasteRegion.X) / this.pasteRegion.Width;  // 缩放比例
                float zoomy = (wroldPt.Y - this.pasteRegion.Y) / this.pasteRegion.Height;

                point.X = (int)(uiRegion.X + this.uiRegion.Width * zoomx);
                point.Y = (int)(uiRegion.Y + this.uiRegion.Height * zoomy);
            }
            catch
            {
            }

            return point;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            uiRegion = new Rectangle(50, 100, this.Width - 100, this.Height - 200);
            base.OnSizeChanged(e);
        }
        #endregion

        #region 描绘贴附区域
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (SystemEntiy.Instance.FlowMachine != null && SystemEntiy.Instance.FlowMachine.ContainsKey(this.module)
                && SystemEntiy.Instance.FlowMachine[this.module].Program != null)
            {
                var runDate = SystemEntiy.Instance.FlowMachine[this.module].RunData;

                for (int i = 0; i < runDate.BoardCount; ++i)
                {
                    int column = runDate[i].PCSCount;
                    for (int j = 0; j < column; ++j)
                    {
                        this.DrawCircle(pe.Graphics, runDate[i][j].UpPastePt, i, j, this.GetColor(runDate[i][j].iPasteState), this.size);
                    }
                }
            }

            base.OnPaint(pe);
        }

        private void DrawAxis(Graphics e)
        {
            Pen pen = new Pen(Color.Red,5);
            e.DrawLine(pen, new Point(50, 50), new Point(50, this.Height - 50));
        }

        /// <summary>
        /// 绘制圆形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="index"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        private void DrawCircle(Graphics g, PointF wroldPt, int pcbIndex, int pcsIndex, Color color, int size = 10)
        {
            Point uiPt = AdjuestPt(wroldPt);
            Rectangle rect = new Rectangle(uiPt, new Size(size, size));
            g.FillEllipse(new SolidBrush(color), rect);
            g.DrawEllipse(new Pen(Color.Gray, 1.4F), rect);
            g.DrawString($"{pcbIndex + 1}-{pcsIndex + 1}", this.Font, new SolidBrush(Color.Brown), uiPt.X - size, rect.Y - size);
        }

        public Color GetColor(int state)
        {
            switch(state)
            {
                case 1:
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Red;
                default:
                    return Color.Yellow;
            }
        }
        #endregion
    }
}
