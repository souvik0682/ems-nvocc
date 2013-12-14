using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
namespace EMS.Entity
{
    [Serializable]
    public class EqpOnHire : EMS.Common.IEqpOnHire 
    {


        private long _pk_HireID;

        private char _OnOffHire;

        private int _fk_NVOCCID;

        private int _fk_CompanyID;

        private int _fk_LocationID;

        private System.Nullable<long> _fk_VesselID;

        private System.Nullable<long> _fk_VoyageID;

        private string _HireReference;

        private System.Nullable<System.DateTime> _HireReferenceDate;

        private System.Nullable<System.DateTime> _ValidTill;

        private string _LinePrefix;

        private System.Nullable<long> _fk_ReturnedPortID;

        private string _Narration;

        private System.Nullable<System.DateTime> _StatusRef;

        private string _ReleaseRefNo;

        private System.Nullable<System.DateTime> _ReleaseRefDate;

        private System.Nullable<int> _TEUs;

        private System.Nullable<int> _FEUs;

        private int _fk_UserAdded;

        private System.Nullable<int> _fk_UserLastEdited;

        private System.DateTime _AddedOn;

        private System.Nullable<System.DateTime> _EditedOn;
        private  IList<EMS.Common.IEqpOnHireContainer> _lstEqpOnHireContainer;
        private int _fk_LeaseID;



        public EqpOnHire()
        {

        }


        public long HireID
        {
            get
            {
                return this._pk_HireID;
            }
            set
            {
                if ((this._pk_HireID != value))
                {
                    this._pk_HireID = value;
                }
            }
        }

        public Int32 LeaseID
        {
            get
            {
                return this._fk_LeaseID;
            }
            set
            {
                if ((this._fk_LeaseID != value))
                {
                    this._fk_LeaseID = value;
                }
            }
        }

        public char OnOffHire
        {
            get
            {
                return this._OnOffHire;
            }
            set
            {
                if ((this._OnOffHire != value))
                {
                    this._OnOffHire = value;
                }
            }
        }


        public int NVOCCID
        {
            get
            {
                return this._fk_NVOCCID;
            }
            set
            {
                if ((this._fk_NVOCCID != value))
                {
                    this._fk_NVOCCID = value;
                }
            }
        }


        public int CompanyID
        {
            get
            {
                return this._fk_CompanyID;
            }
            set
            {
                if ((this._fk_CompanyID != value))
                {
                    this._fk_CompanyID = value;
                }
            }
        }


        public int LocationID
        {
            get
            {
                return this._fk_LocationID;
            }
            set
            {
                if ((this._fk_LocationID != value))
                {
                    this._fk_LocationID = value;
                }
            }
        }
                
        public System.Nullable<long> VesselID
        {
            get
            {
                return this._fk_VesselID;
            }
            set
            {
                if ((this._fk_VesselID != value))
                {
                    this._fk_VesselID = value;
                }
            }
        }


        public System.Nullable<long> VoyageID
        {
            get
            {
                return this._fk_VoyageID;
            }
            set
            {
                if ((this._fk_VoyageID != value))
                {
                    this._fk_VoyageID = value;
                }
            }
        }


        public string HireReference
        {
            get
            {
                return this._HireReference;
            }
            set
            {
                if ((this._HireReference != value))
                {
                    this._HireReference = value;
                }
            }
        }


        public System.Nullable<System.DateTime> HireReferenceDate
        {
            get
            {
                return this._HireReferenceDate;
            }
            set
            {
                if ((this._HireReferenceDate != value))
                {
                    this._HireReferenceDate = value;
                }
            }
        }


        public System.Nullable<System.DateTime> ValidTill
        {
            get
            {
                return this._ValidTill;
            }
            set
            {
                if ((this._ValidTill != value))
                {
                    this._ValidTill = value;
                }
            }
        }


        public string LinePrefix
        {
            get
            {
                return this._LinePrefix;
            }
            set
            {
                if ((this._LinePrefix != value))
                {
                    this._LinePrefix = value;
                }
            }
        }


        public System.Nullable<long> ReturnedPortID
        {
            get
            {
                return this._fk_ReturnedPortID;
            }
            set
            {
                if ((this._fk_ReturnedPortID != value))
                {
                    this._fk_ReturnedPortID = value;
                }
            }
        }


        public string Narration
        {
            get
            {
                return this._Narration;
            }
            set
            {
                if ((this._Narration != value))
                {
                    this._Narration = value;
                }
            }
        }


        public System.Nullable<System.DateTime> StatusRef
        {
            get
            {
                return this._StatusRef;
            }
            set
            {
                if ((this._StatusRef != value))
                {
                    this._StatusRef = value;
                }
            }
        }


        public string ReleaseRefNo
        {
            get
            {
                return this._ReleaseRefNo;
            }
            set
            {
                if ((this._ReleaseRefNo != value))
                {
                    this._ReleaseRefNo = value;
                }
            }
        }


        public System.Nullable<System.DateTime> ReleaseRefDate
        {
            get
            {
                return this._ReleaseRefDate;
            }
            set
            {
                if ((this._ReleaseRefDate != value))
                {
                    this._ReleaseRefDate = value;
                }
            }
        }

        public System.Nullable<int> TEUs
        {
            get
            {
                return this._TEUs;
            }
            set
            {
                if ((this._TEUs != value))
                {
                    this._TEUs = value;
                }
            }
        }
        public System.Nullable<int> FEUs
        {
            get
            {
                return this._FEUs;
            }
            set
            {
                if ((this._FEUs != value))
                {
                    this._FEUs = value;
                }
            }
        }        
        public int UserAdded
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


        public System.DateTime AddedOn
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
        public IList<EMS.Common.IEqpOnHireContainer> LstEqpOnHireContainer
        {
            get
            {
                return this._lstEqpOnHireContainer;
            }
            set
            {
                if ((this._lstEqpOnHireContainer != value))
                {
                    this._lstEqpOnHireContainer = value;
                }
            }
        }


    }

}
