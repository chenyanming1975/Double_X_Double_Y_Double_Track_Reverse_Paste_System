using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using HalconDotNet;

namespace GeneralMachine.Common
{
    public static class MathHelper
    {
        /// <summary>
        /// 三点扩展法扩展点位数组-返回增加的扩展点位
        /// </summary>
        /// <param name="Points2Expand">待扩展的点阵</param>
        /// <param name="Origin">原点</param>
        /// <param name="XCoord">X终点坐标</param>
        /// <param name="YCoord">Y终点坐标</param>
        /// <param name="XCountIncluded">X方向个数</param>
        /// <param name="YCountIncluded">Y方向个数</param>
        /// <returns>返回增加的扩展点位</returns>
        public static PointF[] Expand2AddPoints(PointF[] Points2Expand, PointF Origin, PointF XCoord, PointF YCoord, short XCountIncluded, short YCountIncluded)
        {
            List<PointF> Expand2AddPoints = new List<PointF>();
            PointF[,] ExpandedPoints = new PointF[XCountIncluded, YCountIncluded];
            PointF TempX = new PointF();
            PointF TempY = new PointF();
            for (int j = 0; j < YCountIncluded; j++)
            {
                for (int i = 0; i < XCountIncluded; i++)
                {

                    ExpandedPoints[i, j] = new PointF();
                    if (XCountIncluded == 1)
                    {
                        TempX.X = Origin.X;
                        TempX.Y = Origin.Y;
                    }
                    else
                    {
                        TempX.X = (XCoord.X - Origin.X) * i / (XCountIncluded - 1) + Origin.X;
                        TempX.Y = (XCoord.Y - Origin.Y) * i / (XCountIncluded - 1) + Origin.Y;
                    }

                    if (YCountIncluded == 1)
                    {
                        TempY.X = Origin.X;
                        TempY.Y = Origin.Y;
                    }
                    else
                    {
                        TempY.X = (YCoord.X - Origin.X) * j / (YCountIncluded - 1) + Origin.X;
                        TempY.Y = (YCoord.Y - Origin.Y) * j / (YCountIncluded - 1) + Origin.Y;
                    }
                    ExpandedPoints[i, j].X = TempX.X + TempY.X - Origin.X;
                    ExpandedPoints[i, j].Y = TempX.Y + TempY.Y - Origin.Y;
                    if (i != 0 || j != 0)
                    {
                        for (int k = 0; k < Points2Expand.Length; k++)
                        {
                            Expand2AddPoints.Add(new PointF(Points2Expand[k].X + ExpandedPoints[i, j].X - Origin.X, Points2Expand[k].Y + ExpandedPoints[i, j].Y - Origin.Y));
                        }
                    }
                }
            }
            return Expand2AddPoints.ToArray();
        }

        /// <summary>
        /// 三点扩展法扩展点位数组-返回所有的点位
        /// </summary>
        /// <param name="Points2Expand">待扩展的点阵</param>
        /// <param name="Origin">原点</param>
        /// <param name="XCoord">X终点坐标</param>
        /// <param name="YCoord">Y终点坐标</param>
        /// <param name="XCountIncluded">X方向个数</param>
        /// <param name="YCountIncluded">Y方向个数</param>
        /// <returns>返回所有的点位</returns>
        public static PointF[] Expand2AllPoints(PointF Points2Expand, PointF Origin, PointF XCoord, PointF YCoord, short XCountIncluded, short YCountIncluded)
        {
            List<PointF> Expand2AllPoints = new List<PointF>();
            PointF[,] ExpandedPoints = new PointF[XCountIncluded, YCountIncluded];
            PointF TempX = new PointF();
            PointF TempY = new PointF();
            for (int j = 0; j < YCountIncluded; j++)
            {
                for (int i = 0; i < XCountIncluded; i++)
                {

                    ExpandedPoints[i, j] = new PointF();
                    if (XCountIncluded == 1)
                    {
                        TempX.X = Origin.X;
                        TempX.Y = Origin.Y;
                    }
                    else
                    {
                        TempX.X = (XCoord.X - Origin.X) * i / (XCountIncluded - 1) + Origin.X;
                        TempX.Y = (XCoord.Y - Origin.Y) * i / (XCountIncluded - 1) + Origin.Y;
                    }

                    if (YCountIncluded == 1)
                    {
                        TempY.X = Origin.X;
                        TempY.Y = Origin.Y;
                    }
                    else
                    {
                        TempY.X = (YCoord.X - Origin.X) * j / (YCountIncluded - 1) + Origin.X;
                        TempY.Y = (YCoord.Y - Origin.Y) * j / (YCountIncluded - 1) + Origin.Y;
                    }
                    ExpandedPoints[i, j].X = TempX.X + TempY.X - Origin.X;
                    ExpandedPoints[i, j].Y = TempX.Y + TempY.Y - Origin.Y;

                    Expand2AllPoints.Add(new PointF(Points2Expand.X + ExpandedPoints[i, j].X - Origin.X, Points2Expand.Y + ExpandedPoints[i, j].Y - Origin.Y));
                }
            }
            return Expand2AllPoints.ToArray();
        }

        /// <summary>
        /// 坐标转换-根据两个Mark点
        /// </summary>
        /// <param name="POINTS">点阵</param>
        /// <param name="Mark1">Mark1</param>
        /// <param name="Mark2">Mark2</param>
        /// <param name="newMark1">新的Mark1</param>
        /// <param name="newMark2">新的Mark2</param>
        /// <param name="RotationValue">旋转角度</param>
        /// <returns>返回新的点阵</returns>
        public static PointF[] TransformPointsForm2Mark(PointF[] POINTS, PointF Mark1, PointF Mark2, PointF newMark1, PointF newMark2, ref double RotationValue)
        {
            //double R = 0;
            PointF Temp = new PointF();
            PointF[] ReturnPoints = new PointF[POINTS.Length];
            RotationValue = GetAngle(newMark1, newMark2) - GetAngle(Mark1, Mark2);
            RotationValue *= -1;
            for (int i = 0; i < POINTS.Length; i++)
            {
                Temp.X = POINTS[i].X + (newMark1.X - Mark1.X);
                Temp.Y = POINTS[i].Y + (newMark1.Y - Mark1.Y);
                PtRotate(Temp, newMark1, RotationValue, out ReturnPoints[i]);
            }
            return ReturnPoints;
        }

        /// <summary>
        /// 坐标转换-根据两个Mark点(带缩放比例)
        /// </summary>
        /// <param name="POINTS">点阵</param>
        /// <param name="Mark1">Mark1</param>
        /// <param name="Mark2">Mark2</param>
        /// <param name="newMark1">新的Mark1</param>
        /// <param name="newMark2">新的Mark2</param>
        /// <param name="BaseIndex">基准 0-两点中点 1-Mark1 2-Mark2</param>
        /// <param name="ScalRatio">缩放比例</param>
        /// <param name="RotationValue">旋转角度</param>
        /// <returns>返回新的点阵</returns>
        public static PointF[] TransformPointsForm2Mark_Scale(PointF[] POINTS, PointF Mark1, PointF Mark2, PointF newMark1, PointF newMark2, short BaseIndex, ref double ScalRatio_X, ref double ScalRatio_Y, ref double RotationValue)
        {
            PointF Temp = new PointF();
            PointF[] ReturnPoints = new PointF[POINTS.Length];
            RotationValue = GetAngle(newMark1, newMark2) - GetAngle(Mark1, Mark2);
            RotationValue *= -1;
            //胀缩比
            PointF newMark20 = new PointF();//Mark2 旋转后的点
            PointF BasePoint = new PointF();
            PointF PasteXY = new PointF();
            if (0 == BaseIndex)
            {
                BasePoint.X = (newMark1.X + newMark2.X) / 2;
                BasePoint.Y = (newMark1.Y + newMark2.Y) / 2;
            }
            if (1 == BaseIndex)
            {
                BasePoint.X = newMark1.X;
                BasePoint.Y = newMark1.Y;
            }
            if (2 == BaseIndex)
            {
                BasePoint.X = newMark2.X;
                BasePoint.Y = newMark2.Y;
            }
            PtRotate(newMark2, newMark1, RotationValue, out newMark20);
            ScalRatio_X = (newMark20.X - newMark1.X) / (Mark2.X - Mark1.X);
            ScalRatio_Y = (newMark20.Y - newMark1.Y) / (Mark2.Y - Mark1.Y);
            for (int i = 0; i < POINTS.Length; i++)
            {
                Temp.X = POINTS[i].X + (newMark1.X - Mark1.X);
                Temp.Y = POINTS[i].Y + (newMark1.Y - Mark1.Y);
                PtRotate(Temp, newMark1, RotationValue, out ReturnPoints[i]);
                PasteXY.X = (float)((ReturnPoints[i].X - BasePoint.X) * ScalRatio_X + BasePoint.X);
                PasteXY.Y = (float)((ReturnPoints[i].Y - BasePoint.Y) * ScalRatio_Y + BasePoint.Y);
                ReturnPoints[i].X = PasteXY.X;
                ReturnPoints[i].Y = PasteXY.Y;
            }
            return ReturnPoints;
        }

        /// <summary>
        /// 坐标转换-根据两个Mark点
        /// </summary>
        /// <param name="POINTS">点阵</param>
        /// <param name="Mark1">Mark1</param>
        /// <param name="Angle1">Mark2</param>
        /// <param name="newMark1">新的Mark1</param>
        /// <param name="newAngle1">新的Mark2</param>
        /// <param name="RotationValue">旋转角度</param>
        /// <returns>返回新的点阵</returns>
        public static PointF[] TransformPointsForm1Mark1Angle(PointF[] POINTS, PointF Mark1, double Angle1, PointF newMark1, double newAngle1, ref double RotationValue)
        {
            PointF Temp = new PointF();
            PointF[] ReturnPoints = new PointF[POINTS.Length];
            RotationValue = newAngle1 - Angle1;
            RotationValue *= -1;
            for (int i = 0; i < POINTS.Length; i++)
            {
                Temp.X = POINTS[i].X + (newMark1.X - Mark1.X);
                Temp.Y = POINTS[i].Y + (newMark1.Y - Mark1.Y);
                PtRotate(Temp, newMark1, RotationValue, out ReturnPoints[i]);
            }
            return ReturnPoints;
        }

        /// <summary>
        /// 坐标转换-根据1个Mark点(不带角度旋转)
        /// </summary>
        /// <param name="POINTS">点阵</param>
        /// <param name="Mark1">Mark1</param>
        /// <param name="newMark1">新的Mark1</param>
        /// <returns>返回新的点阵</returns>
        public static PointF[] TransformPointsForm1Mark(PointF[] POINTS, PointF Mark1, PointF newMark1)
        {
            PointF[] ReturnPoints = new PointF[POINTS.Length];
            for (int i = 0; i < POINTS.Length; i++)
            {
                ReturnPoints[i].X = POINTS[i].X + (newMark1.X - Mark1.X);
                ReturnPoints[i].Y = POINTS[i].Y + (newMark1.Y - Mark1.Y);
            }
            return ReturnPoints;
        }

        /// <summary>
        /// 两角度相减求差角（逆时针为正 -180到180）
        /// </summary>
        /// <param name="Angle1">角度1</param>
        /// <param name="Angle2">角度2</param>
        /// <returns>角度差</returns>
        private static double SubAngle(double Angle1, double Angle2)
        {
            double AngleSub = Angle1 - Angle2;

            if (AngleSub > 180)
            {
                AngleSub = AngleSub - 360;
            }
            else if (AngleSub < -180)
            {
                AngleSub = AngleSub + 360;
            }
            return AngleSub;
        }

        /// <summary>
        /// 将角度限制在-180~180之间
        /// </summary>
        /// <param name="Angle"></param>
        /// <param name="RIndex"></param>
        /// <returns></returns>
        private static double DegreeNormal(double Angle, short RIndex)
        {
            if (Angle < -360)
            {
                Angle = Angle + (int)(Angle / 360) * 360;
            }
            if (Angle > 360)
            {
                Angle = Angle - (int)(Angle / 360) * 360;
            }
            if (Angle > 180)
            {
                Angle = Angle - 360;
            }
            if (Angle < -180)
            {
                Angle = Angle + 360;
            }
            return Angle;
        }

        /// <summary>
        /// 点绕点旋转
        /// </summary>
        /// <param name="PTtoRotate"></param>
        /// <param name="RotateCenter"></param>
        /// <param name="RotatethetaAngle"></param>
        /// <param name="PTRotated"></param>
        public static void PtRotate(PointF PTtoRotate, PointF RotateCenter, double RotatethetaAngle, out PointF PTRotated)//点绕点旋转算法（逆时针为正）
        {
            double deg = RotatethetaAngle / 180.0 * Math.PI;
            PTRotated = new Point();
            PTRotated.X = (PTtoRotate.X - RotateCenter.X) * (float)Math.Cos(deg) + (PTtoRotate.Y - RotateCenter.Y) * (float)Math.Sin(deg) + RotateCenter.X;
            PTRotated.Y = -(PTtoRotate.X - RotateCenter.X) * (float)Math.Sin(deg) + (PTtoRotate.Y - RotateCenter.Y) * (float)Math.Cos(deg) + RotateCenter.Y;
        }

        public static double GetDist(PointF start, PointF end)
        {
            return Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }

        /// <summary>
        /// 根据两点求角度 单位：度
        /// </summary>
        /// <param name="Point1">点1</param>
        /// <param name="Point2">点2</param>
        /// <returns>返回角度 单位：度</returns>
        public static double GetAngle(PointF Point1, PointF Point2)
        {
            return GetAngle(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }

        public static double GetAngle(PointF line1S, PointF line1E, PointF line2S, PointF line2E)
        {
            HTuple rad = new HTuple();
            HTuple deg = new HTuple();
            HOperatorSet.AngleLl(line1S.Y, line1S.X, line1E.Y, line1E.X
                , line2S.Y, line2S.X, line2E.Y, line2E.X, out rad);
            HOperatorSet.TupleDeg(rad, out deg);
            double angle = deg.D;
            return angle;
        }
        public static double GetAngle(double px1, double py1, double px2, double py2)
        {
            //两点的x、y值
            HTuple rad = new HTuple();
            HTuple deg = new HTuple();
            HOperatorSet.AngleLl(py1, px1, py2, px2, 0, 0, 0, 1, out rad);
            HOperatorSet.TupleDeg(rad, out deg);
            double angle = deg.D;

            if (py1 == py2)// 0 和 180
            {
                angle = 0;
            }
            return angle;
        }
        /// <summary>
        /// 线性转换
        /// </summary>
        /// <param name="CaliPoint1">校验点1</param>
        /// <param name="CaliPoint2">校验点2</param>
        /// <param name="NewX">新点X V</param>
        /// <param name="NewY">算出新点Y N</param>
        /// <returns>0-OK 1-Fail</returns>
        public static short LineConvert(PointF CaliPoint1, PointF CaliPoint2, double NewX, out double NewY)
        {
            double ratio = 0;
            NewY = 0;
            try
            {
                ratio = (CaliPoint2.Y - CaliPoint1.Y) / (CaliPoint2.X - CaliPoint1.X);
                NewY = ratio * (NewX - CaliPoint1.X) + CaliPoint1.Y;
                return 0;
            }
            catch
            {
                return 1;
            }
        }
        
        /// <summary>
        /// 最小二乘法拟合圆
        /// </summary>
        /// <param name="_Points">待拟合的点</param>
        /// <param name="_centerX">中心X</param>
        /// <param name="_centerY">中心Y</param>
        /// <param name="_R">半径</param>
        /// <returns>true-OK false-Fail</returns>
        public static bool FitCircle(PointF[] _Points, out double _centerX, out double _centerY, out double _R)// 最小二乘法，得到圆心坐标和半径三个数据
        {
            int _PointNum = _Points.Length;
            if (_PointNum < 3)
            {
                _centerX = 0;
                _centerY = 0;
                _R = 0;
                return false;
            }
            float x1 = 0.0f;  //x一次方的初值
            float y1 = 0.0f;
            float x2 = 0.0f;  //x平方的初始值
            float y2 = 0.0f;
            float x3 = 0.0f;  //x立方的初始值
            float y3 = 0.0f;
            float x1y1 = 0.0f;
            float x1y2 = 0.0f;
            float x2y1 = 0.0f;

            for (int i = 0; i < _PointNum; i++)
            {
                x1 = x1 + _Points[i].X;
                y1 = y1 + _Points[i].Y;
                x2 = x2 + _Points[i].X * _Points[i].X;
                y2 = y2 + _Points[i].Y * _Points[i].Y;
                x3 = x3 + _Points[i].X * _Points[i].X * _Points[i].X;
                y3 = y3 + _Points[i].Y * _Points[i].Y * _Points[i].Y;
                x1y1 = x1y1 + _Points[i].X * _Points[i].Y;
                x1y2 = x1y2 + _Points[i].X * _Points[i].Y * _Points[i].Y;
                x2y1 = x2y1 + _Points[i].X * _Points[i].X * _Points[i].Y;
            }

            float C, D, E, G, H, N;
            float a, b, c;
            N = _PointNum;
            C = N * x2 - x1 * x1;
            D = N * x1y1 - x1 * y1;
            E = N * x3 + N * x1y2 - (x2 + y2) * x1;
            G = N * y2 - y1 * y1;
            H = N * x2y1 + N * y3 - (x2 + y2) * y1;
            a = (H * D - E * G) / (C * G - D * D);
            b = (H * C - E * D) / (D * D - G * C);
            c = -(a * x1 + b * y1 + x2 + y2) / N;

            float A, B, R;
            A = a / (-2);
            B = b / (-2);
            R = (float)Math.Sqrt(a * a + b * b - 4 * c) / 2;

            _centerX = A;
            _centerY = B;
            _R = R;
            return true;
        }

    }
}
