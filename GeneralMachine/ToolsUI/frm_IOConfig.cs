using GeneralMachine.Config;
using GeneralMachine.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine.IO
{
    public partial class frm_IOConfig : Form
    {
        public frm_IOConfig()
        {
            InitializeComponent();
        }

        private void frm_IOConfig_Load(object sender, EventArgs e)
        {
            IODefine.Load();
            this.propertyGrid1.SelectedObject = IODefine.Instance.MachineIO[Module.Front];
            this.propertyGrid2.SelectedObject = IODefine.Instance.MachineIO[Module.After];
            this.propertyGrid3.SelectedObject = IODefine.Instance.TrackIO[Config.Track.ForntTrack];
            this.propertyGrid4.SelectedObject = IODefine.Instance.TrackIO[Config.Track.AfterTrack];
            this.propertyGrid5.SelectedObject = IODefine.Instance.OtherIO;
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            IODefine.Save();
        }

        private IOInput GetInput(string name, string match1 = "", string match2 = "")
        {
            Type cardType = typeof(CardNo);
            IOInput find = null;
            foreach (CardNo cardNo in Enum.GetValues(cardType))
            {
                find = IODefine.Instance.Inputs[cardNo].Find((input) =>
                {
                    if (input.Text.Contains(name) && input.Text.Contains(match1) && input.Text.Contains(match2))
                    {
                        return true;
                    }
                    return false;
                }) as IOInput;
            }

            return find;
        }

        private IOOutput GetOutput(string name, string match1 = "", string match2 = "")
        {
            Type cardType = typeof(CardNo);
            IOOutput find = null;
            foreach (CardNo cardNo in Enum.GetValues(cardType))
            {
                find = IODefine.Instance.Outputs[cardNo].Find((input) =>
                {
                    if (input.Text.Contains(name) && input.Text.Contains(match1) && input.Text.Contains(match2))
                    {
                        return true;
                    }
                    return false;
                }) as IOOutput;
            }

            return find;
        }

        private void GetIO(string match,object IO_Object)
        {
            Type attrType = typeof(DisplayNameAttribute);
            foreach (System.Reflection.PropertyInfo p in IO_Object.GetType().GetProperties())
            {
                var obj = p.GetValue(IO_Object);
                if (p.PropertyType == typeof(IOInput))
                {
                    #region IO自动获取
                    string displayName = (p.GetCustomAttributes(attrType, true)[0] as DisplayNameAttribute).DisplayName;
                    var result = GetInput(displayName, match);
                    if (result != null)
                        p.SetValue(IO_Object, result);
                    #endregion
                }
                else if (p.PropertyType == typeof(IOOutput))
                {
                    #region IO自动获取
                    string displayName = (p.GetCustomAttributes(attrType, true)[0] as DisplayNameAttribute).DisplayName;
                    var result = GetOutput(displayName, match);
                    if (result != null)
                        p.SetValue(IO_Object, result);
                    #endregion
                }
                else if (p.PropertyType == typeof(List<IOInput>))
                {
                    #region
                    List<IOInput> list = new List<IOInput>();
                    string displayName = (p.GetCustomAttributes(attrType, true)[0] as DisplayNameAttribute).DisplayName;
                    for(int i = 0; i < (obj as List<IOInput>).Count; ++i)
                    {
                        var result = GetInput(displayName, match, (i+1).ToString());
                        if (result != null)
                            list.Add(result);
                    }

                    if(list.Count == (obj as List<IOInput>).Count)
                    {
                        p.SetValue(IO_Object, list);
                    }
                    #endregion
                }
                else if (p.PropertyType == typeof(List<IOOutput>))
                {
                    #region
                    List<IOOutput> list = new List<IOOutput>();
                    string displayName = (p.GetCustomAttributes(attrType, true)[0] as DisplayNameAttribute).DisplayName;
                    for (int i = 0; i < (obj as List<IOOutput>).Count; ++i)
                    {
                        var result = GetOutput(displayName, match, (i + 1).ToString());
                        if (result != null)
                            list.Add(result);
                    }

                    if (list.Count == (obj as List<IOOutput>).Count)
                    {
                        p.SetValue(IO_Object, list);
                    }
                    #endregion
                }

                Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(IO_Object));
            }
        }

        private void bAutoMatchIO_Click(object sender, EventArgs e)
        {
            //this.GetIO("A",IODefine.Instance.MachineIO[Module.Front]);
            //this.GetIO("B", IODefine.Instance.MachineIO[Module.Front]);
            //this.GetIO("", IODefine.Instance.TraceIO);
        }
    }
}
