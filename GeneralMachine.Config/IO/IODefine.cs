using GeneralMachine.Common;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneralMachine.Common.CommonHelper;

namespace GeneralMachine.Config
{

    /// <summary>
    /// IO定义
    /// </summary>
    public class IODefine: SingletionProvider<IODefine>
    {
        public void Init()
        {
            if(Inputs.Keys.Count == 0)
            {
                foreach(CardNo cardNo in Enum.GetValues(typeof(CardNo)))
                {
                    this.Inputs.Add(cardNo, new List<IOInput>());
                    for(int i =0; i < 4; ++i)
                    {
                        foreach(InputNo input in Enum.GetValues(typeof(InputNo)))
                        {
                            var io = new IOInput();
                            io.CardNo = cardNo;
                            io.AxisNo = i;
                            io.InputNo = input;
                            io.Name = $"{cardNo.ToString()}{i}-{input.ToString()}";
                            this.Inputs[cardNo].Add(io);
                        }
                    }
                }
            }

            if(Outputs.Keys.Count == 0)
            {
                foreach (CardNo cardNo in Enum.GetValues(typeof(CardNo)))
                {
                    this.Outputs.Add(cardNo, new List<IOOutput>());
                    for (int i = 0; i < 4; ++i)
                    {
                        foreach (OutputNo ouput in Enum.GetValues(typeof(OutputNo)))
                        {
                            var io = new IOOutput();
                            io.CardNo = cardNo;
                            io.AxisNo = i;
                            io.OuputNo = ouput;
                            io.DefaultState = false;
                            io.Name = $"{cardNo.ToString()}{i}-{ouput.ToString()}";
                            this.Outputs[cardNo].Add(io);
                        }
                    }
                }
            }

            if(TrackIO.Count ==0)
            {
                foreach(Track track in Enum.GetValues(typeof(Track)))
                {
                    this.TrackIO.Add(track, new TrackIO());
                }
            }

            if (MachineIO.Count == 0)
            {
                foreach (Module module in Enum.GetValues(typeof(Module)))
                {
                    this.MachineIO.Add(module, new MachineIO());

                    foreach(Nozzle nozzle in Enum.GetValues(typeof(Nozzle)))
                    {
                        this.MachineIO[module].VaccumCheck.Add( new IOInput());
                        this.MachineIO[module].VaccumSuck.Add(new IOOutput());
                        this.MachineIO[module].VaccumPO.Add(new IOOutput());
                    }

                    foreach(Feeder feeder in Enum.GetValues(typeof(Feeder)))
                    {
                        this.MachineIO[module].FeederExit.Add(new IOInput());
                        this.MachineIO[module].LabelReach.Add(new IOInput());
                    }
                }
            }
        }

        public Dictionary<CardNo, List<IOInput>> Inputs = new Dictionary<CardNo, List<IOInput>>();
        public Dictionary<CardNo, List<IOOutput>> Outputs = new Dictionary<CardNo, List<IOOutput>>();

        #region IO点 定义
        /// <summary>
        /// 轨道IO
        /// </summary>
        public Dictionary<Track, TrackIO> TrackIO = new Dictionary<Track, TrackIO>();

        /// <summary>
        /// 模组IO
        /// </summary>
        public Dictionary<Module, MachineIO> MachineIO = new Dictionary<Module, MachineIO>();

        /// <summary>
        /// 其他IO
        /// </summary>
        public OtherIO OtherIO = new OtherIO();
        #endregion

        public static void Save()
        {
            SerializableHelper<IODefine> helper = new SerializableHelper<IODefine>(IODefine.Instance);
            helper.JsonSerialize(PathDefine.sPathIO + "IO定义.json");
        }

        public static bool Load()
        {
            if(!File.Exists(PathDefine.sPathIO + "IO定义.json"))
            {
                IODefine.Instance.Init();
                return true;
            }

            SerializableHelper<IODefine> helper = new SerializableHelper<IODefine>();
            var temp = helper.DeJsonSerialize(PathDefine.sPathIO + "IO定义.json");
            if (temp != null)
                IODefine.Instance = temp;
            else
                return false;
            return true;
        }

        public static void Init(object IODefine)
        {
            Type type = IODefine.GetType();
            var propertys = type.GetProperties();
            for (int i = 0; i < propertys.Length; ++i)
            {
                object obj = null;
                if (propertys[i].PropertyType == typeof(IOOutput))
                {
                    obj = propertys[i].GetValue(IODefine);
                    var ouput = obj as IOOutput;
                    if(ouput.DefaultState)
                        ouput.SetIO(ouput.DefaultState);
                }
                else if (propertys[i].PropertyType == typeof(List<IOOutput>))
                {
                    obj = propertys[i].GetValue(IODefine);
                    List<IOOutput> outputs = obj as List<IOOutput>;
                    foreach (IOOutput output in outputs)
                    {
                        if (output.DefaultState)
                            output.SetIO(output.DefaultState);
                    }
                }
            }
        }
    }
}
