using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace Catalogos.Utils
{
    public static class Exportar
    {
        public static void ExportarIEnumerable<T>(this IEnumerable<T> data, ref StringBuilder sb)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            sb.Append("<table border='" + "2px" + "'b>");
            sb.Append("<tr>");
            foreach (PropertyDescriptor prop in props)
            {
                sb.Append("<th><b><font face=Arial size=2>" + prop.Name + "</font></b></th>");
            }
            sb.Append("</tr>");

            foreach (T item in data)
            {
                sb.Append("<tr>");
                foreach (PropertyDescriptor prop in props)
                {
                    object value = prop.GetValue(item);
                    sb.Append("<td><font face=Arial size=" + "14px" + ">" + value.ToString() + "</font></td>");
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");
        }

        public static JObject GetTranslations()
        {
            var resourceObject = new JObject();

            var resourceSet = Resources.Names.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();
            while (enumerator.MoveNext())
            {
                resourceObject.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
            }

            return resourceObject;
        }
    }
}