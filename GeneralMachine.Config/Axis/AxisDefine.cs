using GeneralMachine.Common;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Config
{
    public class AxisDefine : SingletionProvider<AxisDefine>
    {
        public void Init()
        {
            if(MachineAxis.Count == 0)
            {
                foreach(Module module in Enum.GetValues(typeof(Module)))
                {
                    MachineAxis.Add(module, new Config.MachineAxis());
                    foreach(Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
                    {
                        MachineAxis[module].Z.Add(new Axis_RunParam());
                        MachineAxis[module].R.Add(new Axis_RunParam());
                    }
                }
            }
        }

        public Dictionary<Module, MachineAxis> MachineAxis = new Dictionary<Module, MachineAxis>();

        public static void Save()
        {
            SerializableHelper<AxisDefine> helper = new SerializableHelper<AxisDefine>(AxisDefine.Instance);
            helper.JsonSerialize(PathDefine.sPathAxis + "axis.json");
        }

        public static bool Load()
        {
            if(!File.Exists(PathDefine.sPathAxis + "axis.json"))
            {
                AxisDefine.Instance.Init();
                return true;
            }

            SerializableHelper<AxisDefine> helper = new SerializableHelper<AxisDefine>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathAxis + "axis.json");
            if (temp != null)
                AxisDefine.Instance = temp;
            else
                return false;
            return true;
        }
    }
}
