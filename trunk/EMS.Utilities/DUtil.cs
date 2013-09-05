
namespace EMS.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;

        public static class DataUtilityExtention
        {
            public static string CheckError(this SqlParameter param)
            {
                string ErrorMessage = string.Empty;
                if (param != null)
                {
                    ErrorMessage = Convert.ToString(param.Value);
                    if (!String.IsNullOrEmpty(ErrorMessage) && ErrorMessage.Length > 0) { throw new Exception(ErrorMessage); }
                }
                return ErrorMessage;
            }
            public static string[] GetOutValue(this SqlParameter[] param)
            {
                string val = string.Empty;
                if (param != null && param.Count() > 0)
                {
                    string[] arr = new string[param.Count()];
                    for (int i = 0; i < param.Count(); i++)
                    {
                        arr[i] = Convert.ToString(param[i].Value);
                    }
                }
                return (string[])null;
            }
            public static T DataTableToType<T>(this DataTable dt) where T : class
            {
                T t = default(T);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    foreach (DataRow dr in dt.Rows)
                    {
                        t = (T)Activator.CreateInstance(typeof(T), null);
                        foreach (var p in properties)
                        {
                            try
                            {
                                t.GetType().GetProperty(p.Name).SetValue(t, dr[p.Name], null);
                            }
                            catch { }
                        }
                        break;
                    }
                }

                return t;
            }
            public static IList<T> DataTableToCollectionType<T>(this DataTable dt) where T : class
            {
                IList<T> t = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    t = new List<T>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var temp = (T)Activator.CreateInstance(typeof(T), null);
                        foreach (var p in properties)
                        {
                            try
                            {
                                if(p.PropertyType.Name.ToLower().Equals("char")){
                                temp.GetType().GetProperty(p.Name).SetValue(temp, Convert.ToString(dr[p.Name])[0], null);
                                }else{
                                  temp.GetType().GetProperty(p.Name).SetValue(temp, dr[p.Name], null);
                                }
                            }
                            catch { }
                        }
                        t.Add(temp);
                    }
                }

                return t;
            }

            public static T DataTableToType<T>(this DataTable dt, params string[] columnNames) where T : class
            {
                T t = default(T);
                if (dt != null && dt.Rows.Count > 0 && columnNames != null && columnNames.Count() > 0)
                {
                    var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        t = (T)Activator.CreateInstance(typeof(T), null);
                        i = 0;
                        foreach (var p in properties)
                        {
                            try
                            {
                                t.GetType().GetProperty(p.Name).SetValue(t, dr[columnNames[i++]], null);
                            }
                            catch { }
                        }
                        break;
                    }
                }

                return t;
            }
            public static IList<T> DataTableToCollectionType<T>(this DataTable dt, params string[] columnNames) where T : class
            {
                IList<T> t = null;
                if (dt != null && dt.Rows.Count > 0 && columnNames != null && columnNames.Count() > 0)
                {
                    var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    t = new List<T>();
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        var temp = (T)Activator.CreateInstance(typeof(T), null);
                        i = 0;
                        foreach (var p in properties)
                        {
                            try
                            {
                                temp.GetType().GetProperty(p.Name).SetValue(temp, Convert.ToString(dr[columnNames[i++]]), null);
                            }
                            catch { }
                        }
                        t.Add(temp);
                    }
                }

                return t;
            }
        }
    

}
