using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.Utilities;
using System.Runtime.Serialization;
using System.Data;
namespace EMS.Entity
{
    
    [DataContract(IsReference=true)]
    public partial class EqpOnHireContainer : EMS.Common.IEqpOnHireContainer 
    {        

        private System.Nullable<int> _fk_NVOCCID;

        private long _pk_HireContainerID;
        
        private System.Nullable<long> _fk_HireID;

        private string _ContainerNo;

        private string _ContainerAbbr;

        private System.Nullable<System.DateTime> _ValidTill;

        private System.Nullable<long> _fk_ReturnPortID;

        private System.Nullable<int> _fk_ContainerTypeID;

        private int _fk_UserAdded;

        private System.Nullable<int> _fk_UserLastEdited;

        private System.DateTime _AddedOn;

        private System.Nullable<System.DateTime> _EditedOn;

        private string _CntrSize;

        private System.Nullable<System.DateTime> _ActualOnHireDate;

        private System.Nullable<System.DateTime> _IGMDate;

        private System.Nullable<long> _IGMNo;

        private string _LGNo;

        private System.Nullable<long> _fk_MovementOptID;
        public IList<IEqpOnHireContainer> GetEqpOnHireContainerList(DataTable dt)
        {
            IList<IEqpOnHireContainer> tempLst = null;
            if (dt != null) {
                tempLst = new List<IEqpOnHireContainer>();
                foreach(DataRow dr in dt.Rows) {
                    tempLst.Add(new EqpOnHireContainer(){
                    HireContainerID = dr["HireContainerID"].ToLong(),
                    HireID = dr["HireID"].ToLong(),
                    ContainerNo = dr["ContainerNo"].ToString(),
                    ContainerTypeID = dr["ContainerTypeID"].ToNullInt(),
                    //ContainerAbbr = dr["ContainerAbbr"].ToString(),
                    CntrSize = dr["CntrSize"].ToString(),
                    ActualOnHireDate = dr["ActualOnHireDate"].ToNullDateTime(),                    
                    IGMDate = dr["IGMDate"].ToNullDateTime(),
                    IGMNo = dr["IGMNo"].ToNullInt(),
                    LGNo = dr["LGNo"].ToString()
                }
                    );
            }}
            return tempLst; 
        } 


        public EqpOnHireContainer()
        {
            
        }
        
        [DataMember]
        public System.Nullable<int> NVOCCID
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
        [DataMember]
        public long HireContainerID
        {
            get
            {
                return this._pk_HireContainerID;
            }
            set
            {
                if ((this._pk_HireContainerID != value))
                {
                    this._pk_HireContainerID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<long> HireID
        {
            get
            {
                return this._fk_HireID;
            }
            set
            {
                if ((this._fk_HireID != value))
                {
                    this._fk_HireID = value;
                }
            }
        }

        [DataMember]
        public string ContainerNo
        {
            get
            {
                return this._ContainerNo;
            }
            set
            {
                if ((this._ContainerNo != value))
                {
                    this._ContainerNo = value;
                }
            }
        }


        [DataMember]
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

        [DataMember]
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
        [DataMember]        
        public System.Nullable<long> ReturnPortID
        {
            get
            {
                return this._fk_ReturnPortID;
            }
            set
            {
                if ((this._fk_ReturnPortID != value))
                {
                    this._fk_ReturnPortID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> ContainerTypeID
        {
            get
            {
                return this._fk_ContainerTypeID;
            }
            set
            {
                if ((this._fk_ContainerTypeID != value))
                {
                    this._fk_ContainerTypeID = value;
                }
            }
        }

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public string CntrSize
        {
            get
            {
                return this._CntrSize;
            }
            set
            {
                if ((this._CntrSize != value))
                {
                    this._CntrSize = value;                    
                }
            }
        }

        [DataMember]
        public System.Nullable<System.DateTime> ActualOnHireDate
        {
            get
            {
                return this._ActualOnHireDate;
            }
            set
            {
                if ((this._ActualOnHireDate != value))
                {
                    this._ActualOnHireDate = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<System.DateTime> IGMDate
        {
            get
            {
                return this._IGMDate;
            }
            set
            {
                if ((this._IGMDate != value))
                {
                    this._IGMDate = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<long> IGMNo
        {
            get
            {
                return this._IGMNo;
            }
            set
            {
                if ((this._IGMNo != value))
                {
                    this._IGMNo = value;
                }
            }
        }

        [DataMember]
        public string LGNo
        {
            get
            {
                return this._LGNo;
            }
            set
            {
                if ((this._LGNo != value))
                {
                    this._LGNo = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<long> MovementOptID
        {
            get
            {
                return this._fk_MovementOptID;
            }
            set
            {
                if ((this._fk_MovementOptID != value))
                {
                    this._fk_MovementOptID = value;
                }
            }
        }

    }
}
