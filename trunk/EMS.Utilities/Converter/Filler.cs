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
namespace EMS.Utilities
{
    public  class Messenger {
        public static string SendMessage(string msgs, string state, string title, string isredirect)
        {
            return string.Format("ShowMessage('{0}',{1},'{2}','{3}')", msgs, state, title, isredirect);
        }
    }
    public static class Checker {

        public static string StringRequired(this string obj)
        {
            if (string.IsNullOrEmpty(obj) && obj.Length == 0)
            throw new Exception("value can't be Null Or Empty.");
            return obj;
        }
        public static int IntRequired(this object obj)
        {
            var val = obj.ToInt();
            if( val>0)
                    return val;
            throw new Exception("value can't be less than equal to 0");
        }
        public static long LongRequired(this object obj)
        {
            var val = obj.ToLong();
            if (val > 0)
                return val;
            throw new Exception("value can't be less than equal to 0");
        }

    }
   
    public static class Filler {


        public static void FillTitles(DropDownList ddl)
        {         
                if (ddl != null) {
                    ddl.Items.Add(new ListItem("Title",""));
                    ddl.Items.Add(new ListItem("Mr.", "Mr."));
                    ddl.Items.Add(new ListItem("Mrs.", "Mrs."));
                    ddl.Items.Add(new ListItem("Master", "Master"));
                    ddl.Items.Add(new ListItem("Miss", "Miss"));
            }            
        }

        public static void FillData<T>(DropDownList ddl, IList<T> dataSource, string textProperty, string valueProperty,string defaultValue)
        {
           // string str = System.Configuration.ConfigurationSettings.AppSettings["Data"];
            if (dataSource!=null)
            {               
                ddl.DataTextField = textProperty;
                ddl.DataValueField = valueProperty;
                ddl.DataSource = dataSource;
                ddl.DataBind();      
            }
            ddl.Items.Insert(0, new ListItem(defaultValue,"0"));
        }
        public static void FillData(DropDownList ddl, DataTable dataSource, string textProperty, string valueProperty, string defaultValue)
        {            
           /// string str = System.Configuration.ConfigurationSettings.AppSettings["Data"];
            if (dataSource != null)
            {
                ddl.DataTextField = textProperty;
                ddl.DataValueField = valueProperty;
                ddl.DataSource = dataSource;
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem(defaultValue, "0"));
        }
        public static void FillMonth(DropDownList ddl)
        {
            string str = System.Configuration.ConfigurationSettings.AppSettings["Months"];
            if (!string.IsNullOrEmpty(str)) {
                var arr = str.Split(',');
                int i=1;
                ddl.Items.Add(new ListItem("Months",""));
                foreach (string s in arr) { 
                   ddl.Items.Add(new ListItem(s,(i++).ToString()));
                }
            }
        }
        public static void FillYear(DropDownList ddl)
        {
            string str = System.Configuration.ConfigurationSettings.AppSettings["Months"];
            if (!string.IsNullOrEmpty(str))
            {
                var arr = str.Split(',');
                int i = 2000;
                int cnt=i+10;
                ddl.Items.Add(new ListItem("Year", ""));
                for (int j = i; j < cnt;j++ )
                {
                    ddl.Items.Add(new ListItem(j.ToString()));
                }
            }
        }
        public static void GridDataBind(object source,GridView grd) {
            grd.DataSource = source;
            grd.DataBind();
        }
    }
    public static class JConverter
    {
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
       }
        public static object ChangeType(Type t, object value)
        {
            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }
        public static void RegisterTypeConverter<T, TC>() where TC : System.ComponentModel.TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
        public static T To<T>(this object text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }
    }
    public static class Converter
    {
        //public static TDerived CreateFromEnumerable<TDerived, T>(this IEnumerable<T> seq) where TDerived : List<T>, new()
        //{            
        //    TDerived outList = new TDerived();
        //    outList.AddRange(seq);
        //    return outList;
        //}

        public static string CreateFromEnumerable<T>(this IEnumerable<T> seq)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Root>");
            string name="",parent=typeof(T).Name;
                foreach (var n in seq)
                {
                    xml.AppendFormat("<{0}",parent);
                    foreach (var prop in typeof(T).GetProperties())
                    {                    
                    xml.AppendFormat(" {0}=\"{1}\" ", prop.Name, n.GetType().GetProperty(prop.Name).GetValue(n, null));
                    }                    
                    xml.AppendFormat("></{0}>", parent);
                }
                xml.Append("</Root>");
            return xml.ToString();
        }
        public static int ToInt(this object obj) {
            int temp=0;
            try {
                if (obj != null) {
                    temp = Convert.ToInt32(obj);
                }
            }catch{}
            return temp;
        }
        public static int? ToNullInt(this object obj)
        {
            int? temp = null;
            try
            {
                if (obj != null)
                {
                    temp = (int?)Convert.ToInt32(obj);
                }
            }
            catch { }
            return temp;
        }

        public static decimal ToDecimal(this object obj)
        {
            decimal temp = 0;
            try
            {
                if (obj != null)
                {
                    temp = Convert.ToDecimal(obj);
                }
            }
            catch { }
            return temp;
        }
        public static decimal? ToNullDecimal(this object obj)
        {
            decimal? temp = null;
            try
            {
                if (obj != null)
                {
                    temp = (decimal?)Convert.ToDecimal(obj);
                }
            }
            catch { }
            return temp;
        }

        public static long ToLong(this object obj)
        {
            long temp = 0;
            try
            {
                if (obj != null)
                {
                    temp = Convert.ToInt64(obj);
                }
            }
            catch { }
            return temp;
        }
        public static long? ToNullLong(this object obj)
        {
            long? temp = null;
            try
            {
                if (obj != null)
                {
                    temp = (long?)Convert.ToInt64(obj);
                }
            }
            catch { }
            return temp;
        }

        public static double ToDouble(this object obj)
        {
            double temp = 0;
            try
            {
                if (obj != null)
                {
                    temp = Convert.ToDouble(obj);
                }
            }
            catch { }
            return temp;
        }
        public static double? ToNullDouble(this object obj)
        {
            double? temp = null;
            try
            {
                if (obj != null)
                {
                    temp = (double?)Convert.ToDouble(obj);
                }
            }
            catch { }
            return temp;
        }
        public static DateTime ToDateTime(this object obj)
        {
            DateTime temp = new DateTime();
            try
            {
                if (obj != null)
                {
                    temp = (DateTime)Convert.ToDateTime(obj);
                }
            }
            catch { }
            return temp;
        }
        public static DateTime? ToNullDateTime(this object obj)
        {
            DateTime? temp = null;
            try
            {
                if (obj != null)
                {
                    temp = (DateTime?)Convert.ToDateTime(obj);
                }
            }
            catch { }
            return temp;
        }
        public static string ToNullDateTimeToString(this DateTime obj)
        {
            string temp = string.Empty;
            try
            {
                if (obj != null)
                {
                    if (obj == Convert.ToDateTime("01/01/1900"))
                        return temp;
                    return obj.ToShortDateString();
                }
            }
            catch { }
            return temp;
        }
        public static string ToNewString(this object obj)
        {
            string temp = string.Empty;
            try
            {
                if (obj != null)
                {
                    temp = Convert.ToString(obj);
                }
            }
            catch { }
            return temp;
        }

        public static string DataToValue<T>(this object obj) {            
            if(obj!=null){
                if(typeof(T).Equals(typeof(DateTime))){
                    try{
                        if (obj.ToDateTime() == Convert.ToDateTime("01/01/1900"))
                            return string.Empty;
                            return obj.ToDateTime().ToShortDateString();
                    }catch{}
                }
                return obj.ToString();
                }
            return string.Empty;
        }

        //public static T ObjectCreater<T>(this object obj,Dictionary<string,string> keyvalue)
        //{
        //    T t=default(T);
        //    if (obj != null)
        //    {
        //        t = (T)Activator.CreateInstance(typeof(T));
        //        var arr = t.GetType().GetProperties();
        //     foreach(var v in keyvalue)   {
        //         arr.FirstOrDefault(e=>e.Name==v.Key).SetValue(t, "", null);
        //     }
        //    }
        //    return t;
        //}

    }
    public class XMLGenerator<T> 
    {

        public string GenerateXMLFromEntity1(T t) {

            MemoryStream stream = new MemoryStream();

            //Serialize the Record object to a memory stream using DataContractSerializer.
            //XmlWriter w = XmlWriter.Create(ms, new XmlWriterSettings
            //{
            //    Indent = true,
            //    Encoding = new UTF8Encoding(false),
            //    IndentChars = "  ",
            //    OmitXmlDeclaration = true,
            //});
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, t);
            int count = (int)stream.Length; // saves object in memory stream
            byte[] arr = new byte[count];
            stream.Seek(0, SeekOrigin.Begin);
            // copy stream contents in byte array
            stream.Read(arr, 0, count);
            UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string
            return utf.GetString(arr).Trim();
           // return string.Empty;
        }
        public string GenerateXMLFromEntity(T t) {

            MemoryStream stream = null;
            TextWriter writer = null;
            try
            {
                stream = new MemoryStream(); // read xml in memory
                writer = new StreamWriter(stream, Encoding.Unicode);
                // get serialise object
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, t); // read object
                int count = (int)stream.Length; // saves object in memory stream
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array
                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null) stream.Close();
                if (writer != null) writer.Close();
            }


            return "";
            }
    }

}
