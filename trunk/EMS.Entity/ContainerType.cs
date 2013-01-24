using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Utilities;
namespace EMS.Common
{
    public partial class ContainerType : EMS.Common.IContainerType
    {

        private int _pk_ContainerTypeID;

        private string _ContainerAbbr;

        private string _CotainerDesc;

        private System.Nullable<int> _TareWeight20;

        private System.Nullable<int> _TareWeight40;

        private System.Nullable<int> _CntrHTFT;

        private System.Nullable<int> _CntrHTIN;

        private string _ISO20;

        private string _ISO40;

        private System.Nullable<int> _fk_UserAdded;

        private System.Nullable<int> _fk_UserLastEdited;

        private System.Nullable<System.DateTime> _AddedOn;

        private System.Nullable<System.DateTime> _EditedOn;

        

        public ContainerType()
        {
           
        }

        public ContainerType(DataTableReader dr)
        {
            if (dr != null) {
                ContainerTypeID = dr["pk_ContainerTypeID"].ToInt();
                ContainerAbbr = dr["ContainerAbbr"].ToString();
                CotainerDesc = dr["CotainerDesc"].ToString();
                TareWeight20 = dr["TareWeight20"].ToNullInt();
                TareWeight40 = dr["TareWeight40"].ToNullInt();
                CntrHTFT = dr["CntrHTFT"].ToNullInt();
                CntrHTIN = dr["CntrHTIN"].ToNullInt();
                ISO20 = dr["ISO20"].ToString();
                ISO40 = dr["ISO40"].ToString();
                UserAdded = dr["fk_UserAdded"].ToNullInt();
                UserLastEdited = dr["fk_UserLastEdited"].ToNullInt();
                AddedOn = dr["AddedOn"].ToNullDateTime();
                EditedOn = dr["EditedOn"].ToNullDateTime();
            }
        }
        public int ContainerTypeID
        {
            get
            {
                return this._pk_ContainerTypeID;
            }
            set
            {
                if ((this._pk_ContainerTypeID != value))
                {
                    this._pk_ContainerTypeID = value;
                }
            }
        }
        public string ContainerAbbr
        {
            get
            {
                return this._ContainerAbbr;
            }
            set
            {
                if ((this._ContainerAbbr != value))
                {
                    
                    this._ContainerAbbr = value;
                }
            }
        }
        public string CotainerDesc
        {
            get
            {
                return this._CotainerDesc;
            }
            set
            {
                if ((this._CotainerDesc != value))
                {
                    this._CotainerDesc = value;
                }
            }
        }
        public System.Nullable<int> TareWeight20
        {
            get
            {
                return this._TareWeight20;
            }
            set
            {
                if ((this._TareWeight20 != value))
                {
                    this._TareWeight20 = value;
                }
            }
        }

        public System.Nullable<int> TareWeight40
        {
            get
            {
                return this._TareWeight40;
            }
            set
            {
                if ((this._TareWeight40 != value))
                {
                    this._TareWeight40 = value;
                }
            }
        }
        public System.Nullable<int> CntrHTFT
        {
            get
            {
                return this._CntrHTFT;
            }
            set
            {
                if ((this._CntrHTFT != value))
                {
                    this._CntrHTFT = value;
                }
            }
        }
        public System.Nullable<int> CntrHTIN
        {
            get
            {
                return this._CntrHTIN;
            }
            set
            {
                if ((this._CntrHTIN != value))
                {
                    this._CntrHTIN = value;
                }
            }
        }

        public string ISO20
        {
            get
            {
                return this._ISO20;
            }
            set
            {
                if ((this._ISO20 != value))
                {
                    this._ISO20 = value;
                }
            }
        }
        public string ISO40
        {
            get
            {
                return this._ISO40;
            }
            set
            {
                if ((this._ISO40 != value))
                {
                    this._ISO40 = value;
                }
            }
        }
        public System.Nullable<int> UserAdded
        {
            get
            {
                return this._fk_UserAdded;
            }
            set
            {
                if ((this._fk_UserAdded != value))
                {
                    this._fk_UserAdded = value;
                }
            }
        }

        public System.Nullable<int> UserLastEdited
        {
            get
            {
                return this._fk_UserLastEdited;
            }
            set
            {
                if ((this._fk_UserLastEdited != value))
                {
                    this._fk_UserLastEdited = value;
                }
            }
        }
        public System.Nullable<System.DateTime> AddedOn
        {
            get
            {
                return this._AddedOn;
            }
            set
            {
                if ((this._AddedOn != value))
                {
                    this._AddedOn = value;
                }
            }
        }
        public System.Nullable<System.DateTime> EditedOn
        {
            get
            {
                return this._EditedOn;
            }
            set
            {
                if ((this._EditedOn != value))
                {
                    this._EditedOn = value;
                }
            }
        }
    }
}
