using HalconDotNet;
using NationalInstruments.Vision;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Vision
{
    /// <summary>
    /// 视觉流程
    /// </summary>
    public class VisionFlow : IDisposable
    {
        #region 固定值
        public string FlowName
        {
            get;
            set;
        } = "Name1";

        /// <summary>
        /// 相机曝光
        /// </summary>
        public int Exprouse
        {
            get;
            set;
        } = 100;

        #region 打光方案
        /// <summary>
        /// 红光
        /// </summary>
        public bool RedUsed
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 绿光
        /// </summary>
        public bool GreenUsed
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 蓝光
        /// </summary>
        public bool BlueUsed
        {
            get;
            set;
        } = false;

        public int RedValue
        {
            get;
            set;
        } = 80;

        public int GreenValue
        {
            get;
            set;
        } = 80;

        public int BlueValue
        {
            get;
            set;
        } = 80;
        #endregion

        /// <summary>
        /// X方程
        /// </summary>
        public string ResultX
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Y方程
        /// </summary>
        public string ResultY
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// 角度方程
        /// </summary>
        public string Angle
        {
            get;
            set;
        } = string.Empty;

        public string Area
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// 基准坐标系
        /// </summary>
        public string OrgCrood
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// 条码
        /// </summary>
        public string Code
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// 基准坐标系点
        /// </summary>
        public PointContour OrgPoint
        {
            get;
            set;
        } = new PointContour();

        /// <summary>
        /// 基础角度
        /// </summary>
        public double BaseAngle
        {
            get;
            set;
        } = 0;
        #endregion

        #region 对外接口
        /// <summary>
        /// 算法集合
        /// </summary>
        public List<Detect> Detects = new List<Detect>();

        public List<string> GetItem(ResultType type)
        {
            List<string> obj = new List<string>();
            foreach (Detect detect in Detects)
            {
                if ((detect.Type & type) == type)
                {
                    obj.Add(detect.UnitID);
                }
            }
            return obj;
        }

        public List<string> GetItem(Type type)
        {
            List<string> obj = new List<string>();
            foreach (Detect detect in Detects)
            {
                if (detect.GetType() == type)
                {
                    obj.Add(detect.UnitID);
                }
            }
            return obj;
        }
        #endregion

        /// <summary>
        /// 计算结果
        /// </summary>
        /// <returns></returns>
        public VisionResult Detect(VisionImage image, Shape InitRoi = null, int textOffset = 0)
        {
            VisionResult rtn = new VisionResult();
            rtn.State = VisionResultState.WaitCal;

            Dictionary<string, VisionResult> Result = new Dictionary<string, VisionResult>();

            foreach (Detect func in this.Detects)
            {
                var result = func.Detected(image, Result, this, InitRoi);
                Result.Add(func.UnitID, result);
                rtn.VisionDesr.AddRange(result.VisionDesr);
                if (result.State == VisionResultState.NG)
                {
                    rtn.State = VisionResultState.NG;
                    return rtn;
                }

            }

            OverlayTextOptions TEXT = new OverlayTextOptions();
            TEXT.FontName = "Consolas";
            TEXT.FontSize = 128;
            if (!string.IsNullOrEmpty(this.ResultX) && Result.ContainsKey(this.ResultX))
            {
                rtn.Point = new PointContour();
                rtn.Point.X = Result[this.ResultX].Point.X;
                image.Overlays.Default.AddText($"X:{rtn.Point.X:N2}", new PointContour(50 + textOffset, 100), Rgb32Value.BlueColor, TEXT);
            }

            if (!string.IsNullOrEmpty(this.ResultX) && Result.ContainsKey(this.ResultY))
            {
                rtn.Point.Y = Result[this.ResultY].Point.Y;
                image.Overlays.Default.AddText($"Y:{rtn.Point.Y:N2}", new PointContour(50 + textOffset, 180), Rgb32Value.BlueColor, TEXT);
            }

            if (!string.IsNullOrEmpty(this.Angle) && Result.ContainsKey(this.Angle))
            {
                rtn.Angle = Result[this.Angle].Angle;
                image.Overlays.Default.AddText($"Angle:{rtn.Angle:N2}", new PointContour(50 + textOffset, 260), Rgb32Value.BlueColor, TEXT);
            }

            if (!string.IsNullOrEmpty(this.Area) && Result.ContainsKey(this.Area))
            {
                rtn.Area = Result[this.ResultX].Area;
                image.Overlays.Default.AddText($"Area:{rtn.Area:N2}", new PointContour(50 + textOffset, 340), Rgb32Value.BlueColor, TEXT);
            }

            rtn.State = VisionResultState.OK;
            return rtn;
        }

        /// <summary>
        /// 异步执行 计算结果
        /// 在 async 包含的函数代码里执行就不会影响主逻辑
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Task<VisionResult> DetectAsync(VisionImage image, Shape roi = null)
        {
            return Task<VisionResult>.Factory.StartNew(() =>
            {
                return this.Detect(image, roi);
            });
        }

        public static VisionFlow Load(string pathName)
        {
            Common.SerializableHelper<VisionFlow> helper = new Common.SerializableHelper<VisionFlow>();
            var flow = helper.DeJsonSerialize(pathName + "//Vision.vp");

            try
            {
                if (flow != null && flow.Detects.Count > 0)
                {
                    foreach (Detect detect in flow.Detects)
                    {
                        if (detect.GetType() == typeof(DetectGeometric))
                        {
                            (detect as DetectGeometric).Temp.ReadVisionFile($"{pathName}\\{detect.UnitID}.bmp");
                        }
                        else if (detect.GetType() == typeof(DetectPttern))
                        {
                            (detect as DetectPttern).Temp.ReadVisionFile($"{pathName}\\{detect.UnitID}.bmp");
                        }
                        else if (detect.GetType() == typeof(DetectShapeMatch))
                        {
                            (detect as DetectShapeMatch).ShapeModel.ReadShapeModel($"{pathName}\\{detect.UnitID}.temp");
                        }
                    }
                }
            }
            catch (HalconException ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return flow;
        }

        public static bool Save(string pathName, VisionFlow flow)
        {
            if (flow == null)
                return false;

            Common.CommonHelper.CreatePath($"{pathName}{flow.FlowName}/");

            try
            {
                foreach (Detect detect in flow.Detects)
                {
                    if (detect.GetType() == typeof(DetectGeometric))
                    {
                        (detect as DetectGeometric).Temp.WriteVisionFile($"{pathName}{flow.FlowName}/{detect.UnitID}.bmp");
                    }
                    else if (detect.GetType() == typeof(DetectPttern))
                    {
                        (detect as DetectPttern).Temp.WriteVisionFile($"{pathName}{flow.FlowName}/{detect.UnitID}.bmp");
                    }
                    else if(detect.GetType() == typeof(DetectShapeMatch))
                    {
                        (detect as DetectShapeMatch).ShapeModel.WriteShapeModel($"{pathName}{flow.FlowName}/{detect.UnitID}.temp");
                    }
                }

            }
            catch { return false; }

            Common.SerializableHelper<VisionFlow> helper = new Common.SerializableHelper<VisionFlow>(flow);
            return helper.JsonSerialize(pathName + flow.FlowName + "/Vision.vp");
        }

        public void Dispose()
        {
            foreach (Detect detect in this.Detects)
            {
                if (detect.GetType() == typeof(DetectGeometric))
                {
                    (detect as DetectGeometric).Dispose();
                }
                else if (detect.GetType() == typeof(DetectPttern))
                {
                    (detect as DetectPttern).Dispose();
                }
                else if(detect.GetType() == typeof(DetectShapeMatch))
                {
                    (detect as DetectShapeMatch).Dispose();
                }
            }
        }
    }
}

