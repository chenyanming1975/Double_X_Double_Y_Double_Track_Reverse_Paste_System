using GeneralMachine.Common;
using GeneralMachine.Config;
using GeneralMachine.Flow.Nodes;
using GeneralMachine.Vision;
using NationalInstruments.Vision;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Flow.Tool
{
    #region 视觉计算参数
    /// <summary>
    /// 流程视觉计算参数
    /// </summary>
    public class ResultItem
    {
        public ResultKey Key = ResultKey.DownVision;

        /// <summary>
        /// 拍照点位置
        /// </summary>
        public PointF CaptruePos = new PointF();

        /// <summary>
        /// 拍照相机
        /// </summary>
        public Camera Camera = Camera.Top;

        public int PCBIndex = 0;
        public int PCSIndex = 0;
        public Nozzle NZIndex = Nozzle.Nz1;
        public Mark Mark = Mark.Mark点1;
        public VisionImage image = null;
        public Shape ROI = null;
        /// <summary>
        /// 视觉方法
        /// </summary>
        public string funcName = string.Empty;

        public Vision.VisionResult Result = null;
    }

    /// <summary>
    /// 视觉结果查询关键字
    /// </summary>
    public enum ResultKey
    {
        [EnumDescription("Mark拍照")]
        Mark,
        [EnumDescription("读大板码")]
        PanelCode,
        [EnumDescription("Label拍照")]
        DownVision,
        [EnumDescription("读小板码")]
        PCSCode,
        [EnumDescription("Badmark拍照")]
        Badmark,
    }

    #endregion

    /// <summary>
    /// 视觉计算线程,所有视觉在此计算
    /// </summary>
    public class VisionCalHelper : Common.SingletionProvider<VisionCalHelper>
    {
        /// <summary>
        /// 加载程序中需要用到的方法
        /// </summary>
        public ConcurrentDictionary<string, Vision.VisionFlow> VisionList = new ConcurrentDictionary<string, Vision.VisionFlow>();

        /// <summary>
        /// 设置曝光和光源
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="funcName"></param>
        public void SetShutterAndLight(Module module, Camera camera, string funcName)
        {
            Task.Factory.StartNew(() =>
            {
                var vision = this.GetFunc(funcName);
                if (vision != null)
                {
                    CameraDefine.Instance.Camera[module][camera].Exposure = vision.Exprouse;
                    LightParam param = new LightParam();
                    param.bBlue = vision.BlueUsed;
                    param.bRed = vision.RedUsed;
                    param.bGreen = vision.GreenUsed;
                    param.B_Value = vision.BlueValue;
                    param.R_Value = vision.RedValue;
                    param.G_Value = vision.GreenValue;
                    CameraDefine.Instance.Light(module, camera, param);
                }
            });
        }

        /// <summary>
        /// 获得视觉流程
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VisionFlow GetFunc(string name)
        {
            if (this.VisionList.ContainsKey(name))
                return this.VisionList[name];
            return null;
        }

        /// <summary>
        /// 释放所有已安装的视觉库
        /// </summary>
        public void ClearVisionList()
        {
            for (int i = 0; i < this.VisionList.Values.Count; ++i)
            {
                this.VisionList.Values.ElementAt(i).Dispose();
            }

            this.VisionList.Clear();
        }

        /// <summary>
        /// 安装和更新需要用到的视觉库
        /// </summary>
        public string InstallVision(List<string> nameList)
        {
            string rtn = string.Empty;
            for (int i = 0; i < nameList.Count; ++i)
            {
                rtn += this.InstallVision(nameList[i]);
            }

            return rtn;
        }

        /// <summary>
        /// 安装和更新需要用到的视觉库
        /// </summary>
        public string InstallVision(string name)
        {
            var flow = VisionFlow.Load(VisionToolCtrl.sPathVision + name);

            if (flow == null)
            {
                return $"导入 视觉方法[{name}] 失败\r\n";
            }

            if (!this.VisionList.ContainsKey(name))
                this.VisionList.TryAdd(name, flow);
            else
            {
                this.VisionList[name]?.Dispose();
                this.VisionList[name] = flow;
            }

            return string.Empty;
        }

        /// <summary>
        /// 从库中导入直接计算
        /// </summary>
        /// <param name="name"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public VisionResult DetectUI(string name, VisionImage image, Shape roi)
        {
            VisionResult result = new VisionResult();
            var flow = VisionFlow.Load(VisionToolCtrl.sPathVision + name);
            if (flow != null)
            {
                result = flow.Detect(image, roi);
            }

            flow?.Dispose();
            return result;
        }

        /// <summary>
        /// 同/异步计算视觉, 同步情况下可以出发报警
        /// </summary>
        /// <param name="module">所属模组</param>
        /// <param name="item">计算参数</param>
        /// <param name="image">计算图片</param>
        /// <param name="IsAsync">是否同步,同步可报警提示</param>
        public void VisionDetect(Module module, ResultItem item, VisionImage image, int offset = 0)
        {
            if(SystemConfig.Instance.General.RunMode == RunMode.TestRun)
            {
                item.Result = new VisionResult();
                item.Result.State = VisionResultState.OK;
                if (module == Module.Front)
                    Module1Handler.Invoke(this, item); // 计算
                else
                    Module2Handler.Invoke(this, item); // 计算
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();
                var func = GetFunc(item.funcName);
                if (image == null)
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Info, $"下视觉 相机{item.Camera} 拍照-图片采集失败!!!");
                }

                if (func != null)
                {
                    item.Result = func.Detect(image, item.ROI, offset);
                    if (module == Module.Front)
                        Module1Handler.Invoke(this, item); // 计算
                    else
                        Module2Handler.Invoke(this, item); // 计算

                    sw.Stop();
                    VisionCaledEvent?.Invoke(module, item, sw.ElapsedMilliseconds);
                }
                else
                {
                    MsgHelper.Instance.AddMessage(MsgLevel.Error, "找不到指定的视觉方法!!!");
                }
            }
        }

        /// <summary>
        /// module1 处理
        /// </summary>
        public static event EventHandler<ResultItem> Module1Handler;

        /// <summary>
        /// module2 处理
        /// </summary>
        public static event EventHandler<ResultItem> Module2Handler;

        /// <summary>
        /// 显示图片
        /// </summary>
        public event Action<Module, Camera, VisionImage> ImageShowEvent;

        /// <summary>
        /// 视觉结果已处理
        /// </summary>
        public event Action<Module, ResultItem, long> VisionCaledEvent;

        public void ImageShow(Module module, Camera camera, VisionImage image)
        {
            if (this.ImageShowEvent == null)
            {
                image?.Dispose();
            }
            else
            {
                this.ImageShowEvent.Invoke(module, camera, image);
            }
        }
    }
}
