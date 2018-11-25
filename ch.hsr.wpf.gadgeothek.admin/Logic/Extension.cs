using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ch.hsr.wpf.gadgeothek.admin.Logic
{
    public static class Extension
    {
        /// <summary>
        /// Replace properties of an object with the properties of another object.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void Replace(this object target, object source) {
            if (target.GetType() == source.GetType()) {
                PropertyInfo[] properties = target.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties) {
                    propertyInfo.SetValue(target, propertyInfo.GetValue(source));
                }
            } else {
                throw new ArgumentException("Target and Source are not of the same type.");
            }
        }
    }
}
