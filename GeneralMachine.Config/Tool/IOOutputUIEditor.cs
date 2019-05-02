using GeneralMachine.Motion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace GeneralMachine.Config
{
    class IOOutputUIEditor : TypeConverter
    {
        public IOOutputUIEditor() { }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //将对象转换为字符串
            if ((destinationType == typeof(string)) && value != null && value != null)
            {
                return ((IOOutput)value).ToString();
            }

            //生成设计时的构造器代码 
            if (destinationType == typeof(InstanceDescriptor) && value != null)
            {
                IOOutput p = (IOOutput)value;
                ConstructorInfo ctor = typeof(IOOutput).GetConstructor(new Type[] { typeof(CardNo), typeof(int), typeof(OutputNo) });
                return new InstanceDescriptor(ctor, new object[] { p.CardNo, p.AxisNo, p.OuputNo });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            IOOutput io = new IOOutput();
            io.CardNo = (CardNo)propertyValues["CardNo"];
            io.AxisNo = (int)propertyValues["AxisNo"];
            io.OuputNo = (OutputNo)propertyValues["OuputNo"];
            return io;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value != null)
            {
                return TypeDescriptor.GetProperties(value, attributes);
            }

            return base.GetProperties(context, value, attributes);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
