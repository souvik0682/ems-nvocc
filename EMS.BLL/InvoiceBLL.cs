using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.Cryptography;
using EMS.Utilities.ResourceManager;

namespace EMS.BLL
{
    public class InvoiceBLL
    {
        #region Invoice Type
        public DataTable GetInvoiceType()
        {
            return InvoiceDAL.GetInvoiceType();
        }
        #endregion

        #region Location
        public DataTable GetLocation()
        {
            return InvoiceDAL.GetLocation();
        }
        #endregion

        #region BL No
        public DataTable GetBLno()
        {
            return InvoiceDAL.GetBLno();
        }
        #endregion

        #region Gross Weight
        public DataTable GrossWeight(string BLno)
        {
            return InvoiceDAL.GrossWeight(BLno);
        }
        #endregion

        public DataTable TEU(string BLno)
        {
            return InvoiceDAL.TEU(BLno);
        }

        public DataTable FEU(string BLno)
        {
            return InvoiceDAL.FEU(BLno);
        }

        public DataTable Volume(string BLno)
        {
            return InvoiceDAL.Volume(BLno);
        }

        public DataTable BLdate(string BLno)
        {
            return InvoiceDAL.BLdate(BLno);
        }
    }
}
