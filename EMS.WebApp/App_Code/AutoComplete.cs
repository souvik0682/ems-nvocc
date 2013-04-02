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

    [WebMethod]
    public string[] GetVesselList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "select (VesselName + ' | ' + CallSign) Name from dbo.trnVessel where VesselName like @prefixText";
        string sql = "select VesselName Name from dbo.trnVessel where VesselName like @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Name"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetPackageUnitList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        string sql = "SELECT [UnitName] FROM [dbo].[mstUOM] WHERE [UnitType] = 'P' AND UnitName LIKE @prefixText"; //[pk_UOMId] ,[UnitCode] ,
        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["UnitName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetWeightUnitList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        string sql = "SELECT [UnitName] FROM [dbo].[mstUOM] WHERE [UnitType] = 'W' AND UnitName LIKE @prefixText"; //[pk_UOMId] ,[UnitCode] ,
        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["UnitName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetVolumeUnitList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        string sql = "SELECT [UnitName] FROM [dbo].[mstUOM] WHERE [UnitType] = 'V' AND UnitName LIKE @prefixText"; //[pk_UOMId] ,[UnitCode] ,
        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["UnitName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetSurveyorList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [AddrName] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'SU' AND [AddrName] LIKE @prefixText";

        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'SU' AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetDeliveryToList(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [AddrName] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'IC' AND [AddrName] LIKE @prefixText";

        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'IC' AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetPortListBL(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        string sql = @"SELECT PortName collate SQL_Latin1_General_CP1_CI_AS+' | '+portCode collate SQL_Latin1_General_CP1_CI_AS+' | '+
                      (SELECT countryname FROM dbo.mstCountry WHERE countryabbr = LEFT(portCode collate SQL_Latin1_General_CP1_CI_AS,2)) Name
                      FROM DSR.dbo.mstPort where PortName LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Name"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetShipper(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [AddrName] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'IS' AND [AddrName] LIKE @prefixText";

        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'IS' AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetCANotice(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [AddrName] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'IS' AND [AddrName] LIKE @prefixText";

        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'IS' AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetConsignee(string prefixText, int count, string contextKey)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [AddrName] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'IC' AND [AddrName] LIKE @prefixText";

        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'IC' 
                        AND a.[fk_LocationID] = " + contextKey +
                        " AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetNParty(string prefixText, int count, string contextKey)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [AddrName] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'IN' AND [AddrName] LIKE @prefixText";

        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'IC'
                        AND a.[fk_LocationID] = " + contextKey + 
                        " AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetCFSCode(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        //string sql = "SELECT [CFSCode] FROM [DBO].[mstAddress] WHERE [AddrActive] = 1 AND [AddrType] = 'CF' AND [CFSCode] LIKE @prefixText";

        string sql = @"SELECT a.[CFSCode] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'CF' AND [CFSCode] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["CFSCode"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetInvoiceNo(string prefixText, int count)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();

        string sql = "select InvoiceNo from finInvoice WHERE InvoiceNo LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["InvoiceNo"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetChaList(string prefixText, int count, string contextKey)
    {
        count = 10;
        AppCodeClass ac = new AppCodeClass();
        
        string sql = @"SELECT a.[AddrName] FROM [DBO].[mstAddress] a 
                        INNER JOIN [dbo].[mstAddressType] at 
                        ON CAST(a.AddrType as int) = at.pk_AddrTypeID 
                        WHERE a.[AddrActive] = 1 AND at.[AddrType] = 'CH' 
                        AND a.[fk_LocationID] = " + contextKey + 
                        " AND [AddrName] LIKE @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, ac.ConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["AddrName"].ToString(), i);
            i++;
        }
        return items;
    }
}

public class AppCodeClass
{
    public string ConnectionString = "Data Source=WIN-SERVER;Initial Catalog=Liner;User Id=sa;Password=P@ssw0rd;Pooling=true;Connection Timeout=30;Max Pool Size=40;Min Pool Size=5";
    //public string ConnectionString = @"Data Source=DILP-PC;Initial Catalog=NVOCC;Integrated Security=true;Pooling=true;Connection Timeout=30;Max Pool Size=40;Min Pool Size=5";

    //public string ConnectionString = @"Data Source=SOUVIK-PC\SQLEXPRESS;Initial Catalog=NVOCC;Integrated Security=SSPI;";
}

