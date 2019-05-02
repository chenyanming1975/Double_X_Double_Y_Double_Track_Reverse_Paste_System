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
    public class AxisUIEditor : TypeConverter
    {
        public AxisUIEditor() { }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return string.Empty;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            Axis_RunParam AXIS = new Axis_RunParam();
            AXIS.CardNO = (CardNo)propertyValues["CardNO"];
            AXIS.AxisNo = (int)propertyValues["AxisNo"];
            AXIS.HomeDirection = (bool)propertyValues["HomeDirection"];
            AXIS.HomeMode = (HomeMode)propertyValues["HomeMode"];
            AXIS.AxisRatio = (double)propertyValues["AxisRatio"];
            AXIS.Source = (AxisSource)propertyValues["Source"];
            AXIS.MinDiff = (double)propertyValues["MinDiff"];

            if (AXIS.MinDiff > 1)
                AXIS.MinDiff = 1;
            else if (AXIS.MinDiff <= 0.01)
                AXIS.MinDiff = 0.01;

            if (AXIS.AxisNo > 3)
                AXIS.AxisNo = 3;
            else if (AXIS.AxisNo < 0)
                AXIS.AxisNo = 0;

            return AXIS;
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
