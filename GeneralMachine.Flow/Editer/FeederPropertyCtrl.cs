using GeneralMachine.Common;
using GeneralMachine.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Flow.Editer
{
    public class FeederPropertyCtrl:StringConverter
    {
        public FeederPropertyCtrl()
        {
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        // Enable
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (ProgramFlow.SelectFlow == null) return new StandardValuesCollection(new List<string>());

            return new StandardValuesCollection(FeederDefine.Instance.GetFeederList(ProgramFlow.SelectFlow.Module));
        }

        /// <summary>
        /// Can't Edit
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
