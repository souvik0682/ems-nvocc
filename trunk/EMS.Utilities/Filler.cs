using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections;
using System.Globalization;
namespace EMS.Utilities
{
  
    public static class JDatetimeConverter
    {
        public static string JToDtString(this DateTime value)
        {
            if (value==null)
                return string.Empty;
            CultureInfo ci = CultureInfo.InvariantCulture;
            return value.ToString("yyyy-MM-dd hh:mm:ss.FFF",ci);
        }

        public static string JToUpper(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return value.ToUpper();
        }
    }



}
