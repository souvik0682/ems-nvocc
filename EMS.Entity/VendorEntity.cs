using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class VendorEntity : IVendor
    {
        #region IUser Members
        public int VendorId
        {
            get;
            set;
        }

        public string VendorType
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public int VendorSalutation
        {
            get;
            set;
        }

        public string VendorName
        {
            get;
            set;
        }

        public string VendorAddress
        {
            get;
            set;
        }

        public string CFSCode
        {
            get;
            set;
        }

        public int Terminalid
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

        public bool VendorActive
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }
        #endregion

        #region ICommon Members
        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }
        #endregion

         #region Constructors

        public VendorEntity()
        {

        }


        public VendorEntity(DataTableReader reader)
        {
            this.CFSCode = Convert.ToString(reader["CFSCode"]);
            this.CompanyID = Convert.ToInt32(reader["Company"]);
            //this.LocationID = Convert.ToInt32(reader["Location"]);
            this.LocationName = Convert.ToString(reader["Location"]);
            this.Terminalid = Convert.ToInt32(reader["Terminal"]);
            this.VendorAddress = Convert.ToString(reader["Address"]);
            this.VendorId = Convert.ToInt32(reader["Id"]);
            this.VendorName = Convert.ToString(reader["Name"]);
            this.VendorSalutation = Convert.ToInt32(reader["Salutation"]);
            this.VendorType = Convert.ToString(reader["Type"]);                        
        }       

        #endregion        
    }
}
