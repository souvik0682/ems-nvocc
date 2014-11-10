using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EMS.BLL;

namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ReferenceEquals(Request.QueryString["Id"], null))
            {
                ViewState["ID"] = Request.QueryString["Id"].ToString();
            }
        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ViewState["ID"]);

            if (FileUploadControl.HasFile)
            {
                try
                {
                    string originalFilename = Path.GetFileName(FileUploadControl.FileName);
                    string filename = new Guid().ToString();
                    FileUploadControl.SaveAs(Server.MapPath("~/Forwarding/Transaction/EstimateFiles/") + filename);
                    hdnFileName.Value = filename;

                    //Update file name in database
                    JobBLL.SaveEstimateFile(id, filename, originalFilename);

                    ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script type='text/javascript'>sendValue();</script>");
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
    }
}