//' (c) Copyright Microsoft Corporation.
//' This source is subject to the Microsoft Public License.
//' See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
//' All other rights reserved.
//'*-------------------------------*
//'*                               *
//'*      Mahsa Hassankashi        *
//'*     info@mahsakashi.com       *
//'*   http://www.mahsakashi.com   * 
//'*     kashi_mahsa@yahoo.com     * 
//'*                               *
//'*                               *
//'*-------------------------------*
//' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService {

    public AutoComplete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCountryList(string prefixText, int count)
    {

        AppCodeClass ac = new AppCodeClass();
        
        //return GetCompletionList1(prefixText, count);
        string sql = "Select * from mstCountry Where CountryName like @prefixText";
       // SqlDataAdapter da = new SqlDataAdapter(sql, "Data Source=DILP-PC;Initial Catalog=NVOCC;Integrated Security=True;Pooling=true;Connection Timeout=30;Max Pool Size=40;Min Pool Size=5");
        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["CountryName"].ToString(), i);
            i++;
        }
        return items;


      
    }

    [WebMethod]
    public string[] GetPortList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();
        
        string sql = "select PortName+','+PortCode port from DSR.dbo.mstPort where PortName like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["port"].ToString(), i);
            i++;
        }
        return items;


      
    }

    
}

public class AppCodeClass
{
    public string ConnectionString = "Data Source=WIN-SERVER;Initial Catalog=Liner;User Id=sa;Password=P@ssw0rd;Pooling=true;Connection Timeout=30;Max Pool Size=40;Min Pool Size=5";
}

