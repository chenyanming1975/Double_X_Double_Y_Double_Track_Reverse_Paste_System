using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using HalconDotNet;
using System.IO;
using NationalInstruments.Vision;
using Newtonsoft.Json;
using NationalInstruments.Vision.Analysis;
using System.Collections.ObjectModel;

namespace GeneralMachine.Vision
{
    public static class VisionHelper
    {
        /// <summary>
        /// 矩形结构体 适用于-1.相机解析度 2.ROI
        /// </summary>
        public struct RectangleRegion
        {
            public short TopLeftX;
            public short TopLeftY;
            public short Width;
            public short Height;
            public RectangleRegion(short topLeftX, short topLeftY, short width, short height)
            {
                this.TopLeftX = topLeftX;
                this.TopLeftY = topLeftY;
                this.Width = width;
                this.Height = height;
            }
        }

        /// <summary>
        /// 图像格式转换 NI->Halcon
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static HImage Image(VisionImage temp)
        {
            try
            {
                HImage rtnImage = new HImage();
                temp.BorderWidth = 0;
                rtnImage.GenImage1("byte", temp.Width, temp.Height, (HTuple)temp.StartPtr);
                return rtnImage;
            }
            catch (HalconException ex)
            {
                return new HImage();
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="hv_WindowHandle"></param>
        /// <param name="hv_String"></param>
        /// <param name="hv_CoordSystem"></param>
        /// <param name="hv_Row"></param>
        /// <param name="hv_Column"></param>
        /// <param name="hv_Color"></param>
        /// <param name="hv_Box"></param>
        public static void DispMessage(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
          HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {
            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow (same as if no second value is given)
            //       otherwise -> use given string as color string for the shadow color
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                try
                {
                    HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                    HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index));
                }
                catch (HalconException HDevExpDefaultException1)
                {
                }
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        /// <summary>
        /// 图像方向
        /// </summary>
        public enum PicDir : short
        {
            /// <summary>
            /// 左到右
            /// </summary>
            Left2Right = 0,
            /// <summary>
            ///  右到左
            /// </summary>
            Right2Left = 1,
            /// <summary>
            ///  上到下
            /// </summary>
            Up2Down = 2,
            /// <summary>
            ///  下到上
            /// </summary>
            Down2Up = 3,
        }

        /// <summary>
        /// 灰度方向
        /// </summary>
        public enum GrayDir : short
        {
            /// <summary>
            /// 白到黑
            /// </summary>
            White2Black = 0,
            /// <summary>
            ///  黑到白
            /// </summary>
            Black2White = 1,
        }

        /// <summary>
        /// 条码类型
        /// </summary>
        public enum BarcodeType : short
        {
            /// <summary>
            /// 1D条码-128
            /// </summary>
            Type_1D_128 = 0,
            /// <summary>
            /// 1D条码-39
            /// </summary>
            Type_1D_39 = 1,
            /// <summary>
            ///  DM码
            /// </summary>
            Type_2D_DataMatrix = 10,
            /// <summary>
            ///  QR码
            /// </summary>
            Type_2D_QR = 11,
        }

        /// <summary>
        /// 直线信息
        /// </summary>
        public struct Line_INFO
        {
            public double start_Row;//Y起点行坐标
            public double start_Col;//X起点列坐标
            public double end_Row; //Y终点行坐标
            public double end_Col;//X终点列坐标
            public double Nr;//行向量
            public double Nc;//列向量
            public double Dist;//距离

            public Line_INFO(double m_start_Row, double m_start_Col, double m_end_Row, double m_end_Col)
            {
                //r*Nr+c*Nc-Dist=0
                ///AX+BY+C=0        
                //A = Y2 - Y1
                //B = X1 - X2
                //C = X2*Y1 - X1*Y2
                this.start_Row = m_start_Row;
                this.start_Col = m_start_Col;
                this.end_Row = m_end_Row;
                this.end_Col = m_end_Col;
                this.Nr = m_start_Col - m_end_Col;
                this.Nc = m_end_Row - m_start_Row;
                this.Dist = m_start_Col * m_end_Row - m_end_Col * m_start_Row;
            }
        };

        /// <summary>
        /// 圆信息
        /// </summary>
        public struct Circle_INFO//圆数据
        {
            public double Row_center;
            public double Column_center;
            public double Radius;
            public double StartPhi;
            public double EndPhi;
            public string PointOrder;

            public Circle_INFO(double m_Row_center, double m_Column_center, double m_Radius, double m_StartPhi, double m_EndPhi, string m_PointOrder)
            {
                this.Row_center = m_Row_center;
                this.Column_center = m_Column_center;
                this.Radius = m_Radius;
                this.StartPhi = m_StartPhi;
                this.EndPhi = m_EndPhi;
                this.PointOrder = m_PointOrder;
            }
        }

        /// <summary>
        /// 测量信息
        /// </summary>
        public struct Metrology_INFO
        {
            public double Length1;
            public double Length2;
            public double Threshold;
            public double MeasureDis;

            [NonSerialized]
            public HTuple ParamName;
            [NonSerialized]
            public HTuple ParamValue;
            public int PointsOrder;
            public Metrology_INFO(double _length1, double _length2, double _threshold, double _measureDis, HTuple _paraName, HTuple _paraValue, int _pointsOrder)
            {
                this.Length1 = _length1;                        // 长/2
                this.Length2 = _length2;                        // 宽/2
                this.Threshold = _threshold;                    // 阈值
                this.MeasureDis = _measureDis;                  //间隔
                this.ParamName = _paraName;                     //参数名
                this.ParamValue = _paraValue;                   //参数值
                this.PointsOrder = _pointsOrder;                //点顺序 0位默认，1 顺时针，2 逆时针
            }
        }

        /// <summary>
        /// 轮廓模板
        /// </summary>
        public struct TempShape//
        {
            [NonSerialized]
            public HObject ModelContours;
            [NonSerialized]
            public HObject ModelContoursAffinTrans;
            [NonSerialized]
            public HTuple ModelID;

        }

        /// <summary>
        /// 轮廓匹配结果
        /// </summary>
        public struct MatchResults//轮廓匹配结果
        {
            public PointF XYCoord;
            public double Angle;
            public double Score;
            [NonSerialized]
            public HObject ContoursAffinTrans;//轮廓
            public MatchResults(PointF xyCoord, double angle, double score, HObject contoursAffinTrans)
            {
                this.XYCoord = xyCoord;
                this.Angle = angle;
                this.Score = score;
                this.ContoursAffinTrans = contoursAffinTrans;
            }
        }
        /// <summary>
        /// 图片类型转换
        /// </summary>
        /// <param name="hobject"></param>
        /// <param name="image"></param>
        public static void HobjectToHimage(HObject hobject, ref HImage image)
        {
            try
            {
                HTuple pointer, type, width, height;
                HOperatorSet.GetImagePointer1(hobject, out pointer, out type, out width, out height);
                image.GenImage1(type, width, height, pointer);
            }
            catch (Exception)
            {
            }
        }

        //预处理****************************************************************************************
        /// <summary>
        /// 图像增益
        /// </summary>
        /// <param name="gain">增益</param>
        /// <param name="offset">偏移</param>
        /// <param name="imageWidth">图片</param>
        /// <param name="imageHeight"></param>
        /// <param name="ho_Image"></param>
        /// <returns></returns>
        public static bool GainOffset(short gain, short offset, ref HObject ho_Image)
        {
            HObject imageTemp = null;
            HTuple imageWidth, imageHeight;
            HObject ho_object = null;
            try
            {
                HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
                HOperatorSet.GenImageConst(out imageTemp, "byte", imageWidth, imageHeight);
                HOperatorSet.AddImage(ho_Image, imageTemp, out ho_object, gain, -offset);
                ho_Image?.Dispose();
                ho_Image = ho_object;
                imageTemp.Dispose();
                return true;
            }
            catch (Exception)
            {
                if (imageTemp != null)
                {
                    imageTemp.Dispose();
                }
            }
            return false;
        }

        //line****************************************************************************************
        /// <summary>
        /// 找直线
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="Roi">Roi</param>
        /// <param name="picdir">图像方向:上下左右的方向</param>
        /// <param name="graydir">灰度方向:白到黑或者黑到白</param>
        /// <param name="StartPoint">直线的起点</param>
        /// <param name="EndPoint">直线的终点</param>
        /// <param name="ho_line">直线</param>
        /// <returns></returns>
        public static bool DetectLine(HObject img, RectangleRegion Roi, PicDir picdir, GrayDir graydir, out PointF StartPoint, out PointF EndPoint, out HObject ho_line)
        {
            ho_line = new HObject();
            StartPoint = new PointF();
            EndPoint = new PointF();
            HXLDCont m_MeasureXLD = new HXLDCont();
            HTuple m_row, m_col;
            Line_INFO outline;
            Line_INFO tempLine = new Line_INFO();
            Metrology_INFO m_MetrologyInfo = new Metrology_INFO();
            #region Info
            m_MetrologyInfo.Length2 = 5;
            m_MetrologyInfo.Threshold = 80;
            m_MetrologyInfo.ParamName = new HTuple();
            m_MetrologyInfo.ParamName.Append("measure_transition");
            m_MetrologyInfo.ParamName.Append("measure_select");
            m_MetrologyInfo.ParamName.Append("measure_distance");
            m_MetrologyInfo.ParamValue = new HTuple();
            if (picdir == PicDir.Left2Right)
            {
                m_MetrologyInfo.Length1 = Roi.Width / 2;
                tempLine.start_Col = Roi.TopLeftX + Roi.Width / 2;
                tempLine.start_Row = Roi.TopLeftY;
                tempLine.end_Col = Roi.TopLeftX + Roi.Width / 2;
                tempLine.end_Row = Roi.TopLeftY + Roi.Height;
            }
            if (picdir == PicDir.Right2Left)
            {
                m_MetrologyInfo.Length1 = Roi.Width / 2;
                tempLine.start_Col = Roi.TopLeftX + Roi.Width / 2;
                tempLine.start_Row = Roi.TopLeftY + Roi.Height;
                tempLine.end_Col = Roi.TopLeftX + Roi.Width / 2;
                tempLine.end_Row = Roi.TopLeftY;
            }
            if (picdir == PicDir.Up2Down)
            {
                m_MetrologyInfo.Length1 = Roi.Height / 2;
                tempLine.start_Col = Roi.TopLeftX + Roi.Width;
                tempLine.start_Row = Roi.TopLeftY + Roi.Height / 2;
                tempLine.end_Col = Roi.TopLeftX;
                tempLine.end_Row = Roi.TopLeftY + Roi.Height / 2;
            }
            if (picdir == PicDir.Down2Up)
            {
                m_MetrologyInfo.Length1 = Roi.Height / 2;
                tempLine.start_Col = Roi.TopLeftX;
                tempLine.start_Row = Roi.TopLeftY + Roi.Height / 2;
                tempLine.end_Col = Roi.TopLeftX + Roi.Width;
                tempLine.end_Row = Roi.TopLeftY + Roi.Height / 2;
            }
            if (graydir == GrayDir.Black2White)
            {
                m_MetrologyInfo.ParamValue.Append("positive");//"negative" "positive" "all" "uniform"
            }
            if (graydir == GrayDir.White2Black)
            {
                m_MetrologyInfo.ParamValue.Append("negative");//"negative" "positive" "all" "uniform"
            }
            m_MetrologyInfo.ParamValue.Append("last");//"first" "last" "all"
            m_MetrologyInfo.ParamValue.Append(15);
            #endregion
            if (MeasureLine(img, tempLine, m_MetrologyInfo, out outline, out m_row, out m_col, out m_MeasureXLD))
            {
                StartPoint.X = (float)outline.start_Col;
                StartPoint.Y = (float)outline.start_Row;
                EndPoint.X = (float)outline.end_Col;
                EndPoint.Y = (float)outline.end_Row;
                HOperatorSet.GenRegionLine(out ho_line, StartPoint.Y, StartPoint.X, EndPoint.Y, EndPoint.X);
                //Form_Main.sub_1_imageSet.imageSet.DisplayObject(m_MeasureXLD);
                m_MeasureXLD.Dispose();
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 找直线实现
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="inLine">直线信息</param>
        /// <param name="inMetrology">测量信息</param>
        /// <param name="outLine">计算出的直线</param>
        /// <param name="outR"></param>
        /// <param name="outC"></param>
        /// <param name="outMeasureXLD"></param>
        /// <returns></returns>
        private static bool MeasureLine(HObject image, Line_INFO inLine, Metrology_INFO inMetrology, out Line_INFO outLine, out HTuple outR, out HTuple outC, out HXLDCont outMeasureXLD)
        {
            HImage inImage = new HImage();
            HTuple pointer, type, width, height;

            HMetrologyModel hMetrologyModel = new HMetrologyModel();
            try
            {
                HOperatorSet.GetImagePointer1(image, out pointer, out type, out width, out height);
                inImage.GenImage1(type, width, height, pointer);
                outLine = new Line_INFO();
                HTuple lineResult = new HTuple();
                HTuple lineInfo = new HTuple();
                lineInfo.Append(new HTuple(new double[] { inLine.start_Row, inLine.start_Col, inLine.end_Row, inLine.end_Col }));
                hMetrologyModel.AddMetrologyObjectGeneric(new HTuple("line"), lineInfo, new HTuple(inMetrology.Length1),
                    new HTuple(inMetrology.Length2), new HTuple(1), new HTuple(inMetrology.Threshold)
                    , inMetrology.ParamName, inMetrology.ParamValue);
                hMetrologyModel.ApplyMetrologyModel(inImage);
                inImage.Dispose();
                inImage = null;
                outMeasureXLD = hMetrologyModel.GetMetrologyObjectMeasures("all", "all", out outR, out outC);
                lineResult = hMetrologyModel.GetMetrologyObjectResult(new HTuple("all"), new HTuple("all"), new HTuple("result_type"), new HTuple("all_param"));
                if (lineResult.TupleLength() >= 4)
                {
                    outLine = new Line_INFO(lineResult[0].D, lineResult[1].D, lineResult[2].D, lineResult[3].D);
                    return true;
                }
                else
                {
                    HXLDCont temp = new HXLDCont();
                    temp.GenContourPolygonXld(outR, outC);
                    double a, b, c, d, e, f, g;
                    temp.FitLineContourXld("tukey", -1, 0, 5, 2, out a, out b, out c, out d, out e, out f, out g);
                    outLine = new Line_INFO(a, b, c, d);
                }
                hMetrologyModel.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                inImage?.Dispose();
                outLine = new Line_INFO();
                outR = new HTuple();
                outC = new HTuple();
                outMeasureXLD = new HXLDCont();
                hMetrologyModel.Dispose();
                return false;
                //异常写入日志文件
                // MessageBox.Show(ex.Message);
            }
        }

        //Circle****************************************************************************************
        /// <summary>
        /// 找圆-使用ROI-RectangleRegion
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="rectRoi">ROI</param>
        /// <param name="MinRadius">最小半径</param>
        /// <param name="MaxRadius">最大半径</param>
        /// <param name="Center">中心点</param>
        /// <param name="Radius">半径</param>
        /// <param name="ho_Contcircle">圆轮廓</param>
        /// <param name="ho_cross">圆中心</param>
        /// <returns></returns>
        public static bool DetectCircle(HObject img, RectangleRegion rectRoi, double MinRadius, double MaxRadius, out PointF Center, out double Radius, out HObject ho_Contcircle, out HObject ho_cross)
        {
            //*************************************************************************************************
            ho_Contcircle = null;
            ho_cross = null;
            Center = new PointF();
            Radius = 0;
            HObject ho_ImageROI, Roi, hCircles, countour;
            HOperatorSet.GenEmptyObj(out ho_ImageROI);
            HOperatorSet.GenEmptyObj(out hCircles);
            HOperatorSet.GenEmptyObj(out countour);
            HOperatorSet.GenRectangle1(out Roi, rectRoi.TopLeftY, rectRoi.TopLeftX, rectRoi.TopLeftY + rectRoi.Height, rectRoi.Width);
            //*************************************************************************************************
            HTuple hv_numbers, CoutourType;//返回拟合圆个数
            try
            {
                HOperatorSet.ReduceDomain(img, Roi, out ho_ImageROI);   //获取图像区域 形态学处理后
                HOperatorSet.EdgesSubPix(ho_ImageROI, out ho_ImageROI, "canny", 1.5, 50, 75);
                HOperatorSet.SegmentContoursXld(ho_ImageROI, out ho_ImageROI, "lines_circles", 5, 4, 2);
                HOperatorSet.SelectShapeXld(ho_ImageROI, out ho_ImageROI, "circularity", "and", 0.5, 1);
                HOperatorSet.SelectShapeXld(ho_ImageROI, out ho_ImageROI, "outer_radius", "and", MinRadius, MaxRadius);
                HOperatorSet.CountObj(ho_ImageROI, out hv_numbers);
                if (hv_numbers.D != 0)
                {
                    for (int i = 0; i < hv_numbers.D; i++)
                    {
                        HOperatorSet.SelectObj(ho_ImageROI, out countour, i);
                        HOperatorSet.GetContourAttribXld(countour, "cont_approx", out CoutourType);
                        if (CoutourType.D != -1)//-1=line 0- 1-circle
                        {
                            HOperatorSet.ConcatObj(hCircles, countour, out hCircles);
                        }
                    }
                    HOperatorSet.CountObj(hCircles, out hv_numbers);
                    if (hv_numbers.D == 0)
                    {
                        Roi.Dispose();
                        ho_ImageROI.Dispose();
                        hCircles.Dispose();
                        countour.Dispose();
                        return false;
                    }
                    HTuple hv_Row, hv_Column, hv_Radius, hv_startPhi, hv_endPhi, hv_PointOrder;
                    HOperatorSet.FitCircleContourXld(hCircles, "geometric", -1, 2, 0, 3, 2, out hv_Row, out hv_Column, out hv_Radius, out hv_startPhi, out hv_endPhi, out hv_PointOrder);
                    hv_numbers = new HTuple(hv_Row.TupleLength());
                    if (hv_numbers.D == 0)
                    {
                        Roi.Dispose();
                        ho_ImageROI.Dispose();
                        hCircles.Dispose();
                        countour.Dispose();
                        return false;
                    }
                    Center.X = (float)(hv_Column[0].D);
                    Center.Y = (float)(hv_Row[0].D);
                    Radius = hv_Radius[0].D;
                    HOperatorSet.GenCircleContourXld(out ho_Contcircle, hv_Row[0], hv_Column[0], hv_Radius[0], 0, 6.28, "positive", 1);
                    HOperatorSet.GenCrossContourXld(out ho_cross, hv_Row[0], hv_Column[0], 10, 0);
                    Roi.Dispose();
                    ho_ImageROI.Dispose();
                    hCircles.Dispose();
                    countour.Dispose();
                    return true;
                }
                else
                {
                    Roi.Dispose();
                    ho_ImageROI.Dispose();
                    hCircles.Dispose();
                    countour.Dispose();
                    return false;
                }

            }
            catch
            {
                Roi.Dispose();
                ho_ImageROI.Dispose();
                hCircles.Dispose();
                countour.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 找圆-使用ROI-HObject
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="Roi">ROI</param>
        /// <param name="MinRadius">最小半径</param>
        /// <param name="MaxRadius">最大半径</param>
        /// <param name="Center">中心点</param>
        /// <param name="Radius">半径</param>
        /// <param name="ho_Contcircle">圆轮廓</param>
        /// <param name="ho_cross">圆中心</param>
        /// <returns></returns>
        public static bool DetectCircle(HObject img, HObject Roi, double MinRadius, double MaxRadius, out PointF Center, out double Radius, out HObject ho_Contcircle, out HObject ho_cross)
        {
            //*************************************************************************************************
            ho_Contcircle = null;
            ho_cross = null;
            Center = new PointF();
            Radius = 0;
            HObject ho_ImageROI, hCircles, countour;
            HOperatorSet.GenEmptyObj(out ho_ImageROI);
            HOperatorSet.GenEmptyObj(out hCircles);
            HOperatorSet.GenEmptyObj(out countour);
            //*************************************************************************************************

            HTuple hv_numbers, CoutourType;//返回拟合圆个数
            try
            {
                HOperatorSet.ReduceDomain(img, Roi, out ho_ImageROI);   //获取图像区域 形态学处理后
                HOperatorSet.EdgesSubPix(ho_ImageROI, out ho_ImageROI, "canny", 1.5, 50, 75);
                HOperatorSet.SegmentContoursXld(ho_ImageROI, out ho_ImageROI, "lines_circles", 5, 4, 2);
                HOperatorSet.SelectShapeXld(ho_ImageROI, out ho_ImageROI, "circularity", "and", 0.5, 1);
                HOperatorSet.SelectShapeXld(ho_ImageROI, out ho_ImageROI, "outer_radius", "and", MinRadius, MaxRadius);
                HOperatorSet.CountObj(ho_ImageROI, out hv_numbers);
                if (hv_numbers.D != 0)
                {
                    for (int i = 1; i < hv_numbers.D + 1; i++)
                    {
                        HOperatorSet.SelectObj(ho_ImageROI, out countour, (HTuple)i);
                        HOperatorSet.GetContourGlobalAttribXld(countour, "cont_approx", out CoutourType);
                        if (CoutourType.D != -1)//-1=line 0- 1-circle
                        {
                            HOperatorSet.ConcatObj(hCircles, countour, out hCircles);
                        }
                    }
                    HOperatorSet.CountObj(hCircles, out hv_numbers);
                    if (hv_numbers.D == 0)
                    {
                        ho_ImageROI.Dispose();
                        hCircles.Dispose();
                        countour.Dispose();
                        return false;
                    }
                    HTuple hv_Row, hv_Column, hv_Radius, hv_startPhi, hv_endPhi, hv_PointOrder;
                    HOperatorSet.FitCircleContourXld(hCircles, "geometric", -1, 2, 0, 3, 2, out hv_Row, out hv_Column, out hv_Radius, out hv_startPhi, out hv_endPhi, out hv_PointOrder);
                    hv_numbers = new HTuple(hv_Row.TupleLength());
                    if (hv_numbers.D == 0)
                    {
                        ho_ImageROI.Dispose();
                        hCircles.Dispose();
                        countour.Dispose();
                        return false;
                    }
                    Center.X = (float)(hv_Column[0].D);
                    Center.Y = (float)(hv_Row[0].D);
                    Radius = hv_Radius[0].D;
                    HOperatorSet.GenCircleContourXld(out ho_Contcircle, hv_Row[0], hv_Column[0], hv_Radius[0], 0, 6.28, "positive", 1);
                    HOperatorSet.GenCrossContourXld(out ho_cross, hv_Row[0], hv_Column[0], 10, 0);
                    ho_ImageROI.Dispose();
                    hCircles.Dispose();
                    countour.Dispose();
                    return true;
                }
                else
                {
                    ho_ImageROI.Dispose();
                    hCircles.Dispose();
                    countour.Dispose();
                    return false;
                }

            }
            catch (HalconException info)
            {
                ho_ImageROI.Dispose();
                hCircles.Dispose();
                countour.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 卡尺找圆
        /// </summary>
        /// <param name="inImage">输入图像</param>
        /// <param name="inCircle">输入圆</param>
        /// <param name="inMetrology">输入形态学</param>
        /// <param name="outCircle">输出圆</param>
        /// <param name="outR">输出行坐标</param>
        /// <param name="outC">输出列坐标</param>
        /// <param name="outMeasureXLD">输出检测轮廓</param>
        public static void MeasureCircle(HImage inImage, Circle_INFO inCircle, Metrology_INFO inMetrology, out Circle_INFO outCircle, out HTuple outR, out HTuple outC, out HXLDCont outMeasureXLD)
        {
            HMetrologyModel hMetrologyModel = new HMetrologyModel();

            try
            {
                outCircle = new Circle_INFO();
                HTuple CircleResult = new HTuple();
                HTuple CircleInfo = new HTuple();
                CircleInfo.Append(new HTuple(new double[] { inCircle.Row_center, inCircle.Column_center, inCircle.Radius }));
                hMetrologyModel.AddMetrologyObjectGeneric(new HTuple("circle"), CircleInfo, new HTuple(inMetrology.Length1),
                    new HTuple(inMetrology.Length2), new HTuple(1), new HTuple(inMetrology.Threshold)
                    , inMetrology.ParamName, inMetrology.ParamValue);

                hMetrologyModel.ApplyMetrologyModel(inImage);
                outMeasureXLD = hMetrologyModel.GetMetrologyObjectMeasures("all", "all", out outR, out outC);

                CircleResult = hMetrologyModel.GetMetrologyObjectResult(new HTuple("all"), new HTuple("all"), new HTuple("result_type"), new HTuple("all_param"));
                if (CircleResult.TupleLength() >= 3)
                {
                    outCircle.Row_center = CircleResult[0].D;
                    outCircle.Column_center = CircleResult[1].D;
                    outCircle.Radius = CircleResult[2].D;
                }
                hMetrologyModel.Dispose();
            }
            catch (Exception ex)
            {
                outCircle = new Circle_INFO();
                outR = new HTuple();
                outC = new HTuple();
                outMeasureXLD = new HXLDCont();
                hMetrologyModel.Dispose();
            }
        }

        //Pattern****************************************************************************************
        /// <summary>
        /// 轮廓匹配
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rectRoi"></param>
        /// <param name="ModelID"></param>
        /// <param name="MaxReturns"></param>
        /// <param name="MinScore"></param>
        /// <param name="MinAngle"></param>
        /// <param name="MaxAngle"></param>
        /// <param name="matchresults"></param>
        /// <returns></returns>
        public static bool FindShapeTemplate(HObject img, RectangleRegion rectRoi, HTuple ModelID, int MaxReturns, double MinScore, double MinAngle, double MaxAngle, out MatchResults[] matchresults)//, double MaxOverlap, double Greediness)
        {
            HObject Roi, ModelContours = null, imgreduced = null;
            HOperatorSet.GenRectangle1(out Roi, rectRoi.TopLeftY, rectRoi.TopLeftX, rectRoi.TopLeftY + rectRoi.Height, rectRoi.TopLeftX + rectRoi.Width);
            HTuple homMat2D = null, a, roir, roic;
            HTuple FindRow, FindColumn, FindAngle, FindScore;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                HOperatorSet.GetShapeModelContours(out ModelContours, ModelID, 1);
                HOperatorSet.FindShapeModel(imgreduced, ModelID, new HTuple(MinAngle).TupleRad(), new HTuple(Math.Abs(MaxAngle - MinAngle)).TupleRad(), MinScore, MaxReturns, 0.5, "least_squares", 0, 0.9, out FindRow, out FindColumn, out FindAngle, out FindScore);
                if (FindRow.Length == 0)
                {
                    matchresults = null;
                    Roi?.Dispose();
                    Roi = null;
                    ModelContours?.Dispose();
                    ModelContours = null;
                    imgreduced?.Dispose();
                    imgreduced = null;
                    return false;
                }
                else
                {
                    matchresults = new MatchResults[FindRow.Length];
                    for (int i = 0; i < FindRow.Length; i++)
                    {
                        matchresults[i].XYCoord = new PointF();
                        matchresults[i].XYCoord.X = float.Parse(FindColumn[i].D.ToString());
                        matchresults[i].XYCoord.Y = float.Parse(FindRow[i].D.ToString());
                        matchresults[i].Angle = FindAngle[i].D;
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, FindRow[i], FindColumn[i], FindAngle[i], out homMat2D);
                        HOperatorSet.AffineTransContourXld(ModelContours, out matchresults[i].ContoursAffinTrans, homMat2D);
                    }
                    Roi?.Dispose();
                    Roi = null;
                    ModelContours?.Dispose();
                    ModelContours = null;
                    imgreduced?.Dispose();
                    imgreduced = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                matchresults = null;
                Roi?.Dispose();
                Roi = null;
                ModelContours?.Dispose();
                ModelContours = null;
                imgreduced?.Dispose();
                imgreduced = null;
                return false;
            }

        }

        /// <summary>
        /// 轮廓匹配
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Roi"></param>
        /// <param name="ModelID"></param>
        /// <param name="MaxReturns"></param>
        /// <param name="MinScore"></param>
        /// <param name="MinAngle"></param>
        /// <param name="MaxAngle"></param>
        /// <param name="matchresults"></param>
        /// <returns></returns>
        public static bool FindShapeTemplate(HObject img, HObject Roi, HTuple ModelID, int MaxReturns, double MinScore, double MinAngle, double MaxAngle, out MatchResults[] matchresults)//, double MaxOverlap, double Greediness)
        {
            HTuple homMat2D = null, a, roir, roic;
            HObject ModelContours = null, imgreduced = null;
            HTuple FindRow, FindColumn, FindAngle, FindScore;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                HOperatorSet.GetShapeModelContours(out ModelContours, ModelID, 1);
                HOperatorSet.FindShapeModel(imgreduced, ModelID, new HTuple(MinAngle).TupleRad(), new HTuple(MaxAngle).TupleRad(), 
                    MinScore, MaxReturns, 0.5, "least_squares", 4, 0.5, 
                    out FindRow, out FindColumn, out FindAngle, out FindScore);
                if (FindRow.Length == 0)
                {
                    matchresults = null;
                    ModelContours.Dispose();
                    imgreduced.Dispose();
                    return false;
                }
                else
                {
                    matchresults = new MatchResults[FindRow.Length];
                    for (int i = 0; i < FindRow.Length; i++)
                    {
                        matchresults[i].XYCoord = new PointF();
                        matchresults[i].XYCoord.X = float.Parse(FindColumn[i].D.ToString());
                        matchresults[i].XYCoord.Y = float.Parse(FindRow[i].D.ToString());
                        matchresults[i].Angle = FindAngle[i].D;
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, FindRow[i], FindColumn[i], FindAngle[i], out homMat2D);
                        HOperatorSet.AffineTransContourXld(ModelContours, out matchresults[i].ContoursAffinTrans, homMat2D);
                        ModelContours.Dispose();
                        imgreduced.Dispose();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                matchresults = null;
                ModelContours.Dispose();
                imgreduced.Dispose();
                return false;
            }

        }

        /// <summary>
        /// 创建轮廓匹配模板
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Roi"></param>
        /// <param name="ModeID"></param>
        /// <param name="contoursAffinTrans"></param>
        /// <returns></returns>
        public static bool CreatShapeTemplate(HObject img, HObject Roi, out HTuple ModeID, out HObject contoursAffinTrans)
        {
            contoursAffinTrans = null;
            ModeID = null;
            HObject imgreduced = null;
            HTuple homMat2D = null;
            HObject ModelContours = null;
            HTuple roir, roic, a;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                HOperatorSet.CreateShapeModel(imgreduced, "auto", new HTuple(-45).TupleRad(), new HTuple(90).TupleRad(), "auto", "none", "use_polarity", "auto", "auto", out ModeID);
                HOperatorSet.GetShapeModelContours(out ModelContours, ModeID, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, roir, roic, 0, out homMat2D);
                HOperatorSet.AffineTransContourXld(ModelContours, out contoursAffinTrans, homMat2D);
                imgreduced.Dispose();
                ModelContours.Dispose();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    imgreduced.Dispose();
                    ModelContours.Dispose();
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        public static bool CreatShapeTemplateEx(HObject img, HObject Roi, double startAngle, double endAngle, out HTuple ModeID, out HObject contoursAffinTrans)
        {
            contoursAffinTrans = null;
            ModeID = null;
            HObject imgreduced = null;
            HTuple homMat2D = null;
            HObject ModelContours = null;
            HTuple roir, roic, a;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                HOperatorSet.CreateShapeModel(imgreduced, "auto", new HTuple(startAngle).TupleRad(), new HTuple(endAngle - startAngle).TupleRad(), "auto", "none", "use_polarity", "auto", "auto", out ModeID);
                HOperatorSet.GetShapeModelContours(out ModelContours, ModeID, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, roir, roic, 0, out homMat2D);
                HOperatorSet.AffineTransContourXld(ModelContours, out contoursAffinTrans, homMat2D);
                imgreduced.Dispose();
                ModelContours.Dispose();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    imgreduced.Dispose();
                    ModelContours.Dispose();
                }
                catch (Exception)
                {
                }
                return false;
            }
        }


        /// <summary>
        /// 创建变形匹配模板
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Roi"></param>
        /// <param name="ModeID"></param>
        /// <param name="contoursAffinTrans"></param>
        /// <returns></returns>
        public static bool CreatDeformableTemplate(HObject img, HObject Roi, out HTuple ModeID, out HObject contoursAffinTrans)
        {
            contoursAffinTrans = null;
            ModeID = null;
            HObject imgreduced = null;
            HTuple homMat2D = null;
            HObject ModelContours = null;
            HTuple roir, roic, a;

            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                HOperatorSet.CreateLocalDeformableModel(imgreduced, "auto", (new HTuple(-10)).TupleRad()
              , (new HTuple(20)).TupleRad(), "auto", 0.9, 1.1, "auto", 0.9, 1.1, "auto",
              "none", "use_polarity", "auto", "auto", new HTuple(), new HTuple(), out ModeID);
                HOperatorSet.GetDeformableModelContours(out ModelContours, ModeID, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, roir, roic, 0, out homMat2D);
                HOperatorSet.AffineTransContourXld(ModelContours, out contoursAffinTrans, homMat2D);
                imgreduced.Dispose();
                ModelContours.Dispose();
                return true;
            }
            catch (Exception)
            {
                imgreduced.Dispose();
                ModelContours.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 变形匹配
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Roi"></param>
        /// <param name="ModelID"></param>
        /// <param name="MaxReturns"></param>
        /// <param name="MinScore"></param>
        /// <param name="MinAngle"></param>
        /// <param name="MaxAngle"></param>
        /// <param name="MinScal"></param>
        /// <param name="MaxScal"></param>
        /// <param name="matchresults"></param>
        /// <returns></returns>
        public static bool FindDeformableTemplate(HObject img, HObject Roi, HTuple ModelID, int MaxReturns, double MinScore, double MinAngle, double MaxAngle, double MinScal, double MaxScal, out MatchResults[] matchresults)//, double MaxOverlap, double Greediness)
        {
            HTuple homMat2D = null, a, roir, roic;
            HTuple hv_Smoothness = 25;
            HObject ho_VectorField = null, ho_ImageRectified = null, ModelContours = null, imgreduced = null;
            HTuple FindRow, FindColumn, FindAngle, FindScore;

            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);//image_rectified
                HOperatorSet.GetDeformableModelContours(out ModelContours, ModelID, 1);
                HOperatorSet.FindLocalDeformableModel(imgreduced, out ho_ImageRectified, out ho_VectorField,
                out ModelContours, ModelID, (new HTuple(MinAngle)).TupleRad(), (new HTuple(MaxAngle)).TupleRad()
                , MinScal, MaxScal, MinScal, MaxScal, MinScore, MaxReturns, 0.7, 0, 0.4, ((new HTuple("image_rectified")).TupleConcat(
                "vector_field")).TupleConcat("deformed_contours"), ((new HTuple("deformation_smoothness")).TupleConcat(
                "expand_border")).TupleConcat("subpixel"), hv_Smoothness.TupleConcat(
                (new HTuple(0)).TupleConcat(1)), out FindScore, out FindRow, out FindColumn);
                if (FindRow.Length == 0)
                {
                    matchresults = null;
                    ho_VectorField.Dispose();
                    ho_ImageRectified.Dispose();
                    ModelContours.Dispose();
                    imgreduced.Dispose();
                    return false;
                }
                else
                {
                    FindAngle = new double[FindRow.Length];
                    matchresults = new MatchResults[FindRow.Length];
                    for (int i = 0; i < FindRow.Length; i++)
                    {
                        matchresults[i].XYCoord = new PointF();
                        matchresults[i].XYCoord.X = float.Parse(FindColumn[i].D.ToString());
                        matchresults[i].XYCoord.Y = float.Parse(FindRow[i].D.ToString());
                        matchresults[i].Angle = FindAngle[i].D;
                        //HOperatorSet.VectorAngleToRigid(0, 0, 0, FindRow[i], FindColumn[i], FindAngle[i], out homMat2D);
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, 0, 0, 0, out homMat2D);
                        HOperatorSet.AffineTransContourXld(ModelContours, out matchresults[i].ContoursAffinTrans, homMat2D);
                    }
                    ho_VectorField.Dispose();
                    ho_ImageRectified.Dispose();
                    ModelContours.Dispose();
                    imgreduced.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                matchresults = null;
                ho_VectorField.Dispose();
                ho_ImageRectified.Dispose();
                ModelContours.Dispose();
                imgreduced.Dispose();
                return false;
            }

        }

        /// <summary>
        /// 变形匹配
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="Roi">Roi</param>
        /// <param name="ModelID">变形匹配模板</param>
        /// <param name="MaxReturns">最大返回个数</param>
        /// <param name="MinScore">最小分数</param>
        /// <param name="MinAngle">最小角度</param>
        /// <param name="MaxAngle">最大角度</param>
        /// <param name="MinScal">最小缩放率</param>
        /// <param name="MaxScal">最大缩放率</param>
        /// <param name="matchresults">匹配结果</param>
        /// <returns></returns>
        public static bool FindDeformableTemplate(HObject img, RectangleRegion rectRoi, HTuple ModelID, int MaxReturns, double MinScore, double MinAngle, double MaxAngle, double MinScal, double MaxScal, out MatchResults[] matchresults)//, double MaxOverlap, double Greediness)
        {
            HTuple homMat2D = null, a, roir, roic;
            HTuple hv_Smoothness = 25;
            HObject Roi, ho_VectorField = null, ho_ImageRectified = null, ModelContours = null, imgreduced = null;
            HTuple FindRow, FindColumn, FindAngle, FindScore;
            HOperatorSet.GenRectangle1(out Roi, rectRoi.TopLeftY, rectRoi.TopLeftX, rectRoi.TopLeftY + rectRoi.Height, rectRoi.Width);
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);//image_rectified
                HOperatorSet.GetDeformableModelContours(out ModelContours, ModelID, 1);
                HOperatorSet.FindLocalDeformableModel(imgreduced, out ho_ImageRectified, out ho_VectorField,
                out ModelContours, ModelID, (new HTuple(MinAngle)).TupleRad(), (new HTuple(MaxAngle)).TupleRad()
                , MinScal, MaxScal, MinScal, MaxScal, MinScore, MaxReturns, 0.7, 0, 0.4, ((new HTuple("image_rectified")).TupleConcat(
                "vector_field")).TupleConcat("deformed_contours"), ((new HTuple("deformation_smoothness")).TupleConcat(
                "expand_border")).TupleConcat("subpixel"), hv_Smoothness.TupleConcat(
                (new HTuple(0)).TupleConcat(1)), out FindScore, out FindRow, out FindColumn);
                if (FindRow.Length == 0)
                {
                    matchresults = null;
                    Roi.Dispose();
                    ho_VectorField.Dispose();
                    ho_ImageRectified.Dispose();
                    ModelContours.Dispose();
                    imgreduced.Dispose();
                    return false;
                }
                else
                {
                    FindAngle = new double[FindRow.Length];
                    matchresults = new MatchResults[FindRow.Length];
                    for (int i = 0; i < FindRow.Length; i++)
                    {
                        matchresults[i].XYCoord = new PointF();
                        matchresults[i].XYCoord.X = float.Parse(FindColumn[i].D.ToString());
                        matchresults[i].XYCoord.Y = float.Parse(FindRow[i].D.ToString());
                        matchresults[i].Angle = FindAngle[i].D;
                        //HOperatorSet.VectorAngleToRigid(0, 0, 0, FindRow[i], FindColumn[i], FindAngle[i], out homMat2D);
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, 0, 0, 0, out homMat2D);
                        HOperatorSet.AffineTransContourXld(ModelContours, out matchresults[i].ContoursAffinTrans, homMat2D);
                    }
                    Roi.Dispose();
                    ho_VectorField.Dispose();
                    ho_ImageRectified.Dispose();
                    ModelContours.Dispose();
                    imgreduced.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                matchresults = null;
                Roi.Dispose();
                ho_VectorField.Dispose();
                ho_ImageRectified.Dispose();
                ModelContours.Dispose();
                imgreduced.Dispose();
                return false;
            }

        }

        /// <summary>
        /// 创建NCC模板
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="Roi">ROI</param>
        /// <param name="ModeID">NCC模板</param>
        /// <returns></returns>
        public static bool CreatNCCTemplate(HObject img, HObject Roi, out HTuple ModeID)
        {
            ModeID = null;
            HObject imgreduced = null;
            HObject ModelContours = null;
            HTuple roir, roic, a;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                var start = new HTuple(-45);
                var step = new HTuple(90);

                HOperatorSet.CreateNccModel(imgreduced, "auto", start.TupleRad(), step, "auto", "use_polarity", out ModeID);
                imgreduced.Dispose();
                ModelContours?.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CreatNCCTemplate(HObject img, HObject Roi, double angleStart, double angleEnd, out HTuple ModeID)
        {
            ModeID = null;
            HObject imgreduced = null;
            HObject ModelContours = null;
            HTuple roir, roic, a;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);
                HOperatorSet.CreateNccModel(imgreduced, "auto", angleStart, angleEnd - angleStart, "auto", "use_polarity", out ModeID);
                imgreduced.Dispose();
                ModelContours?.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// NCC模板匹配-Roi-HObject
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Roi"></param>
        /// <param name="ModelID"></param>
        /// <param name="MaxReturns"></param>
        /// <param name="MinScore"></param>
        /// <param name="MinAngle"></param>
        /// <param name="MaxAngle"></param>
        /// <param name="MinScal"></param>
        /// <param name="MaxScal"></param>
        /// <param name="matchresults"></param>
        /// <returns></returns>
        public static bool FindNccTemplate(HObject img, HObject Roi, HTuple ModelID, int MaxReturns, double MinScore, double MinAngle, double MaxAngle, out MatchResults[] matchresults)//, double MaxOverlap, double Greediness)
        {
            HTuple a, roir, roic;
            HObject imgreduced = null;
            HTuple FindRow, FindColumn, FindAngle, FindScore;
            try
            {
                HOperatorSet.AreaCenter(Roi, out a, out roir, out roic);
                HOperatorSet.ReduceDomain(img, Roi, out imgreduced);//image_rectified
                HOperatorSet.FindNccModel(imgreduced, ModelID, (new HTuple(MinAngle)).TupleRad(), (new HTuple(MaxAngle - MinAngle)).TupleRad(), MinScore, MaxReturns, 0.5, "true",
                0, out FindRow, out FindColumn, out FindAngle, out FindScore);
                if (FindRow.Length == 0)
                {
                    matchresults = null;
                    imgreduced.Dispose();
                    return false;
                }
                else
                {
                    matchresults = new MatchResults[FindRow.Length];
                    for (int i = 0; i < FindRow.Length; i++)
                    {
                        matchresults[i].XYCoord = new PointF();
                        matchresults[i].XYCoord.X = float.Parse(FindColumn[i].D.ToString());
                        matchresults[i].XYCoord.Y = float.Parse(FindRow[i].D.ToString());
                        matchresults[i].Angle = FindAngle[i].D;
                    }
                    imgreduced.Dispose();
                    return true;
                }
            }
            catch (HalconException ex)
            {
                Debug.WriteLine(ex.Message);
                matchresults = null;
                imgreduced?.Dispose();
                return false;
            }

        }

        /// <summary>
        /// NCC模板匹配-Roi-RectangleRegion
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rectRoi"></param>
        /// <param name="ModelID"></param>
        /// <param name="MaxReturns"></param>
        /// <param name="MinScore"></param>
        /// <param name="MinAngle"></param>
        /// <param name="MaxAngle"></param>
        /// <param name="MinScal"></param>
        /// <param name="MaxScal"></param>
        /// <param name="matchresults"></param>
        /// <returns></returns>
        public static bool FindNccTemplate(HObject img, RectangleRegion rectRoi, HTuple ModelID, int MaxReturns, double MinScore, double MinAngle, double MaxAngle, out MatchResults[] matchresults)//, double MaxOverlap, double Greediness)
        {
            try
            {
                HObject Roi;
                HOperatorSet.GenRectangle1(out Roi, rectRoi.TopLeftY, rectRoi.TopLeftX, rectRoi.TopLeftY + rectRoi.Height, rectRoi.Width);
                bool rtn = FindNccTemplate(img, Roi, ModelID, MaxReturns, MinScore, MinAngle, MaxAngle, out matchresults);
                Roi?.Dispose();
                return rtn;
            }
            catch (HalconException ex)
            {
                matchresults = null;
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        //面积计算****************************************************************************************
        public static bool AreaCount(HObject img, HObject Roi, bool IsWhite, out double areacount)
        {
            areacount = 0;
            HObject thresholdImg = null;
            HObject tempImage = null;
            HObject tempImage2 = null;
            HTuple area, cx, cy;
            try
            {
                HOperatorSet.ReduceDomain(img, Roi, out tempImage);//image_rectified
                if (!IsWhite)
                {
                    HOperatorSet.InvertImage(tempImage, out tempImage2);
                    HOperatorSet.Threshold(tempImage2, out thresholdImg, 128, 255);
                    tempImage2?.Dispose();
                }
                else
                {
                    HOperatorSet.Threshold(tempImage, out thresholdImg, 128, 255);
                }

                HOperatorSet.AreaCenter(thresholdImg, out area, out cy, out cx);
                areacount = area.D;
                tempImage?.Dispose();
                thresholdImg.Dispose();
                return true;
            }
            catch (Exception a)
            {
                thresholdImg?.Dispose();
                tempImage?.Dispose();
                return false;
            }
        }

        //几何运算****************************************************************************************
        /// <summary>
        /// 直线相交
        /// </summary>
        /// <param name="ln1_Pt1">直线1起点</param>
        /// <param name="ln1_Pt2">直线1终点</param>
        /// <param name="ln2_Pt1">直线2起点</param>
        /// <param name="ln2_Pt2">直线2终点</param>
        /// <param name="IntersectionPoint"></param>
        /// <returns></returns>
        public static bool LnsIntersection(PointF ln1_Pt1, PointF ln1_Pt2, PointF ln2_Pt1, PointF ln2_Pt2, out PointF IntersectionPoint)
        {
            IntersectionPoint = new PointF();
            HTuple PointX = 0; HTuple PointY = 0;
            HTuple OVERLAP = 0;
            try
            {
                HOperatorSet.IntersectionLines(ln1_Pt1.Y, ln1_Pt1.X, ln1_Pt2.Y, ln1_Pt2.X, ln2_Pt1.Y, ln2_Pt1.X, ln2_Pt2.Y, ln2_Pt2.X, out PointY, out PointX, out OVERLAP);
                IntersectionPoint.X = (float)PointX.D;
                IntersectionPoint.Y = (float)PointY.D;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  static double GetLineAngle(PointF ln1_Pt1, PointF ln1_Pt2)
        {
            HTuple phi = new HTuple();
            HOperatorSet.LineOrientation(ln1_Pt1.Y, ln1_Pt1.X, ln1_Pt2.Y, ln1_Pt2.X, out phi);
            return phi.D;
        }


        /// <summary>
        /// 最小二乘法找直线 y = kx + b
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y"></param>
        /// <param name="k"></param>
        /// <param name="d"></param>
        /// <param name="r">相关性</param>
        /// <returns></returns>
        public static bool FitLine(double[] X, double[] Y, out double k, out double b, out double r)
        {
            if(X.Length != Y.Length)
            {
                k = 0;
                b = 0;
                r = 0;
                return false;
            }

            double sumX = 0;
            double sumY = 0;
            double avgX = 0;
            double avgY = 0;
            double r1 = 0;
            double r2 = 0;

            for(int i =0; i < X.Length; ++i)
            {
                sumX += X[i];
                sumY += Y[i];
            }

            avgX = sumX / X.Length;
            avgY = sumY / Y.Length;

            double SPxy = 0;
            double SSx = 0;
            double SSy = 0;

            for (int i = 0; i < X.Length; ++i)
            {
                SPxy += (X[i] - avgX) * (Y[i] - avgY);
                SSx += (X[i] - avgX) * (X[i] - avgX);
                SSy += (Y[i] - avgY) * (Y[i] - avgY);
            }

            if(SSy == 0)
            {
                k = 0;
                b = 0;
                r = 1;
                return true;
            }

            k = SPxy / SSx;
            b = avgY - k * avgX;
            r1 = SPxy * SPxy;
            r2 = SSx * SSy;
            r = r1 / r2;
            return true;
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


        //条码识别****************************************************************************************
        /// <summary>
        /// 条码识别
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Roi"></param>
        /// <param name="type"></param>
        /// <param name="Barcode"></param>
        /// <returns></returns>
        public static bool ReadBarcode(HObject img, HObject Roi, BarcodeType type, out string Barcode, out HObject Box)
        {
            Barcode = "";
            HTuple BarCodeHandle, DecodedDataStrings;
            Box = null;

            try
            {
                HOperatorSet.ReduceDomain(img, Roi, out img);//image_rectified
                switch (type)
                {
                    case BarcodeType.Type_1D_128:
                        HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out BarCodeHandle);
                        HOperatorSet.FindBarCode(img, out Box, BarCodeHandle,
                                                 "Code 128", out DecodedDataStrings);
                        if (DecodedDataStrings.Length > 0)
                        {
                            Barcode = DecodedDataStrings.SArr[0];
                        }
                        break;
                    case BarcodeType.Type_1D_39:
                        HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out BarCodeHandle);
                        HOperatorSet.FindBarCode(img, out Box, BarCodeHandle,
                                                 "Code 39", out DecodedDataStrings);
                        if (DecodedDataStrings.Length > 0)
                        {
                            Barcode = DecodedDataStrings.SArr[0];
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case BarcodeType.Type_2D_DataMatrix:
                        HTuple ResultHandles_DataMatrix = null;
                        HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", new HTuple(), new HTuple(), out BarCodeHandle);
                        HOperatorSet.SetDataCode2dParam(BarCodeHandle, "default_parameters", "maximum_recognition");
                        HOperatorSet.FindDataCode2d(img, out Box, BarCodeHandle, "stop_after_result_num",
                            1/*找寻1个*/, out ResultHandles_DataMatrix, out DecodedDataStrings);
                        if (DecodedDataStrings.Length > 0)
                        {
                            Barcode = DecodedDataStrings.SArr[0];
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case BarcodeType.Type_2D_QR:
                        HTuple ResultHandles_QR = null;
                        HOperatorSet.CreateDataCode2dModel("QR Code", new HTuple(), new HTuple(), out BarCodeHandle);
                        HOperatorSet.FindDataCode2d(img, out Box, BarCodeHandle, new HTuple(), new HTuple(), out ResultHandles_QR, out DecodedDataStrings);
                        if (DecodedDataStrings.Length > 0)
                        {
                            Barcode = DecodedDataStrings.SArr[0];
                        }
                        else
                        {
                            return false;
                        }
                        break;
                }
                return true;
            }
            catch
            {
                Barcode = string.Empty;
                Box?.Dispose();
                Box = null;
                return false;
            }
        }


        /// <summary>
        /// 点绕点旋转算法
        /// </summary>
        /// <param name="PTtoRotate">需要旋转的像素点</param>
        /// <param name="RotateCenter">旋转中心</param>
        /// <param name="RotatethetaAngle">角度</param>
        /// <returns></returns>
        public static PointContour PtRotate(PointContour PTtoRotate, PointContour RotateCenter, double RotatethetaAngle)//点绕点旋转算法（逆时针为正）
        {
            double deg = RotatethetaAngle / 180.0 * Math.PI;
            var PTRotated = new PointContour();
            PTRotated.X = (PTtoRotate.X - RotateCenter.X) * Math.Cos(deg) + (PTtoRotate.Y - RotateCenter.Y) * Math.Sin(deg) + RotateCenter.X;
            PTRotated.Y = -(PTtoRotate.X - RotateCenter.X) * Math.Sin(deg) + (PTtoRotate.Y - RotateCenter.Y) * Math.Cos(deg) + RotateCenter.Y;
            return PTRotated;
        }
    }

    [Serializable]
    public class HalCali
    {
        /// <summary>
        /// 标定图像
        /// </summary>
        [JsonIgnore]
        public VisionImage CalibImage = null;
        /// <summary>
        /// 图像坐标
        /// </summary>
        public List<PointContour> ImageLocs = new List<PointContour>();
        /// <summary>
        /// 世界坐标
        /// </summary>
        public List<PointF> WorldLocs = new List<PointF>();

        /// <summary>
        /// 相机标定时相机的中心坐标
        /// </summary>
        public PointF CliabCenter = new PointF();

        /// <summary>
        /// 校验 产生矩阵
        /// </summary>
        /// <returns></returns>
        public short Cam_Calibration(string path, VisionImage image)
        {
            Collection<PointContour> Pixels = new Collection<PointContour>();
            Collection<PointContour> Wrolds = new Collection<PointContour>();
            try
            {
                for (int i = 0; i < this.ImageLocs.Count; ++i)
                {
                    Pixels.Add(new PointContour(this.ImageLocs[i].X, this.ImageLocs[i].Y));
                    Wrolds.Add(new PointContour(this.WorldLocs[i].X, this.WorldLocs[i].Y));
                }

                double rtn = Algorithms.LearnCalibrationPoints(image, Pixels, Wrolds);
                if (rtn < 999)
                    return -1;
                else
                {
                    image.WriteVisionFile(path);
                    this.LoadCalib(path);
                    return 0;
                }
            }
            catch { return -1; }
        }

        public void LoadCalib(string path)
        {
            if (!File.Exists(path)) return;
            this.CalibImage = new VisionImage();
            this.CalibImage.ReadVisionFile(path);

            // 中心点像素坐标 
            var center = new PointContour(this.CalibImage.Width / 2, this.CalibImage.Height / 2);

            var coordreport = Algorithms.ConvertPixelToRealWorldCoordinates(this.CalibImage, center);
            if (coordreport.Points.Count > 0)
            {
                this.CliabCenter = new PointF((float)coordreport.Points[0].X, (float)coordreport.Points[0].Y);
            }
        }

        /// <summary>
        /// 像数到世界坐标转换
        /// </summary>
        /// <param name="PixelPoint">像数坐标</param>
        /// <returns>世界坐标</returns>
        public PointF Pixel2World(PointContour PixelPoint)
        {
            var coordreport = Algorithms.ConvertPixelToRealWorldCoordinates(this.CalibImage, PixelPoint);
            if (coordreport.Points.Count <= 0)
                return new PointF((float)PixelPoint.X, (float)PixelPoint.Y);
            else
                return new PointF((float)coordreport.Points[0].X, (float)coordreport.Points[0].Y);
        }

        /// <summary>
        /// 世界到像数坐标转换
        /// </summary>
        /// <param name="WorldPoint">世界坐标</param>
        /// <returns>像数坐标</returns>
        public PointContour World2Pixel(PointF WorldPoint)
        {
            PointContour world = new PointContour((double)WorldPoint.X, (double)WorldPoint.Y);
            var coordreport = Algorithms.ConvertRealWorldToPixelCoordinates(this.CalibImage, world);
            if (coordreport.Points.Count <= 0)
                return new PointContour(this.CalibImage.Width / 2, this.CalibImage.Height / 2);
            else
                return coordreport.Points[0];
        }
    }
}
