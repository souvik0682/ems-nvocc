using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using EMS.BLL;


namespace EMS.WebApp.Transaction
{
    /// <summary>
    /// Summary description for DocDownload
    /// </summary>
    public class DocDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            if (context.Request.QueryString["id"] != null)
            {
                ImportBLL oImportBL = new ImportBLL();
                int id = Convert.ToInt32(context.Request.QueryString["id"].ToString());
                string name =context.Request.QueryString["n"].ToString();
                DataTable dt = oImportBL.GetUploadedDocByID(id);

                if (dt != null)
                {

                    Byte[] bytes = (Byte[])dt.Rows[0]["Image"];
                    context.Response.Buffer = true;
                    context.Response.Charset = "";
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    context.Response.ContentType = dt.Rows[0]["Type"].ToString();// dt.Rows[0]["ContentType"].ToString();
                    context.Response.AddHeader("content-disposition", "attachment;filename=" + name);
                    context.Response.BinaryWrite(bytes);
                    context.Response.Flush();
                    context.Response.End();

                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}