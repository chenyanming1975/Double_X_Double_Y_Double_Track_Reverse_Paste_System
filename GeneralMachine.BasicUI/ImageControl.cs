using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.Vision.WindowsForms;
using NationalInstruments.Vision;
using GeneralMachine.Common;

namespace GeneralMachine
{
    public partial class ImageControl : UserControl
    {
        public ImageControl()
        {
            InitializeComponent();
        }

        #region 1-常用方法
        public static void ShowResult(ImageViewer View, double X, double Y, double R)
        {
            PointF Center = new PointF();
            Center.X = (float)X;
            Center.Y = (float)Y;
            //center cross
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X - 30, Center.Y), new PointContour(Center.X + 30, Center.Y)), Rgb32Value.YellowColor);
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X, Center.Y - 30), new PointContour(Center.X, Center.Y + 30)), Rgb32Value.YellowColor);
            PointF a = new PointF();
            PointF A = new PointF();
            a.X = Center.X + 30;
            a.Y = Center.Y;
            MathHelper.PtRotate(a, Center, R, out A);
            //Line Direction
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X, Center.Y), new PointContour(A.X, A.Y)), Rgb32Value.BlueColor);
        }
        public static void ShowResult(ImageViewer View, PointF Center, PointF A)
        {
            //center cross
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X - 30, Center.Y), new PointContour(Center.X + 30, Center.Y)), Rgb32Value.YellowColor);
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X, Center.Y - 30), new PointContour(Center.X, Center.Y + 30)), Rgb32Value.YellowColor);

            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X, Center.Y), new PointContour(A.X, A.Y)), Rgb32Value.BlueColor);
        }
        public static void ShowResult(ImageViewer View, PointF Center, double R)
        {
            //center cross
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X - 30, Center.Y), new PointContour(Center.X + 30, Center.Y)), Rgb32Value.YellowColor);
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X, Center.Y - 30), new PointContour(Center.X, Center.Y + 30)), Rgb32Value.YellowColor);
            PointF a = new PointF();
            PointF A = new PointF();
            a.X = Center.X + 30;
            a.Y = Center.Y;
            MathHelper.PtRotate(a, Center, R, out A);
            //Line Direction
            View.Image.Overlays.Default.AddLine(new LineContour(new PointContour(Center.X, Center.Y), new PointContour(A.X, A.Y)), Rgb32Value.BlueColor);
        }

        //显示刻度尺
        public static void ShowCross(ImageViewer viewer)
        {
            viewer.Image.Overlays.Default.AddLine(new LineContour(new PointContour(viewer.Image.Width / 2, 0), new PointContour(viewer.Image.Width / 2, viewer.Image.Height)), Rgb32Value.RedColor);
            viewer.Image.Overlays.Default.AddLine(new LineContour(new PointContour(0, viewer.Image.Height / 2), new PointContour(viewer.Image.Width, viewer.Image.Height / 2)), Rgb32Value.RedColor);
        }

        public  static void ClearCross(ImageViewer viewer)
        {
            viewer.Image.Overlays.Default.Clear();
        }
        #endregion
    }
}
