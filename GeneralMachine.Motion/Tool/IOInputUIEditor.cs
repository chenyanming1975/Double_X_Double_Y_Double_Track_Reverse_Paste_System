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

namespace GeneralMachine.Motion
{
    public class IOInputUIEditor : TypeConverter
    {
        public IOInputUIEditor() { }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //将对象转换为字符串
            if ((destinationType == typeof(string)) && value != null && value != null)
            {
                return ((IOInput)value).ToString();
            }

            //生成设计时的构造器代码 
            if (destinationType == typeof(InstanceDescriptor) && value != null)
            {
                IOInput p = (IOInput)value;
                ConstructorInfo ctor = typeof(IOInput).GetConstructor(new Type[] { typeof(CardNo), typeof(int), typeof(InputNo) });
                return new InstanceDescriptor(ctor, new object[] {p.CardNo, p.AxisNo, p.InputNo });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            IOInput io = new IOInput();
            io.CardNo = (CardNo)propertyValues["CardNo"];
            io.AxisNo = (int)propertyValues["AxisNo"];
            io.InputNo = (InputNo)propertyValues["InputNo"];
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
