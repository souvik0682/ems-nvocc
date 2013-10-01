using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using EMS.Utilities;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using System.Collections.Specialized;
using EMS.Utilities.Cryptography;
using System.Text;
namespace EMS.WebApp.Reports.Export
{
    public partial class ExpBLPrint : System.Web.UI.Page
    {
        public IList<BLPrintModel> Model { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            }
        }
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLine.SelectedIndex > 0)
            {

                Filler.FillData(ddlLocation, CommonBLL.GetLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lng = ddlLine.SelectedValue.ToLong();
            if (lng > 0)
            {
                Filler.FillData(ddlBlNo, CommonBLL.GetBLHeaderByBLNo(lng), "ImpLineBLNo", "ImpLineBLNo", "Bl. No.");
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ISearchCriteria iSearchCriteria = new SearchCriteria();
            iSearchCriteria.StringParams = new List<string>() { ddlBlNo.SelectedValue };
           // iSearchCriteria = null;
            var temp = ExpBLPrintingBLL.GetxpBLPrinting(iSearchCriteria);
            Model = temp==null?(List<BLPrintModel>)null:new List<BLPrintModel>() {temp  };
        }
        public int StartCount=0;
        public int EndCount = 0;
        public string GetTable(IList<ItemDetail> model)
        {
            StringBuilder sb = new StringBuilder();
            if (model != null)
            {
                if (StartCount == 0) {  EndCount = model.Count > 5 ? 5 : model.Count; }
                sb.Append(@" <table style='width: 100%; margin-top: 20px' border='1' cellpadding='0' cellspacing='0'>");
                sb.Append(@"<tr><th style='width: 20%'>Container No</th>");
                sb.Append(@"<th style='width: 20%'>Size / Type</th>");
                sb.Append(@" <th style='width: 20%'>Seal No</th>");
                sb.Append(@"<th style='width: 20%'>Package</th>");
                sb.Append(@"<th style='width: 20%'>Gross Weight</th></tr>");
                for (int cnt = StartCount; cnt < EndCount; cnt++)
                {
                    sb.AppendFormat(@"<tr><td>{0} </td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", model[cnt].ContainerNo, model[cnt].Size, model[cnt].SealNo, model[cnt].Package, model[cnt].GrossWeight);
                }
                sb.Append(@"</table>");
                if (model.Count > 5) {
                    EndCount = model.Count;
                    StartCount = 5;
                }

            } return model != null ? sb.ToString() : string.Empty;
        }
    }
}