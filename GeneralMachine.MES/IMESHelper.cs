using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.MES
{
    /// <summary>
    /// MES 上传接口
    /// 1. 对应各个 厂区的MES上传
    /// 2. 插件化模式, 从指定文件夹根据名称导入
    /// 3. 
    /// </summary>
    public interface IMESHelper
    {
        /// <summary>
        /// 获得MES 设置控件
        /// </summary>
        /// <returns></returns>
        Form MES_ConfigureForm();

        /// <summary>
        /// 上传信息
        /// </summary>
        /// <returns></returns>
        bool UploadMessage();

        /// <summary>
        /// 上传报警信息
        /// </summary>
        /// <returns></returns>
        bool UploadAlarmMessage();

        /// <summary>
        /// 上传爆料信息
        /// </summary>
        /// <returns></returns>
        bool UploadRejectMessage();

        /// <summary>
        /// 上传生产完成信息
        /// </summary>
        /// <returns></returns>
        bool UploadProductMessage();
    }
}
