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

    [DataContract(IsReference = true)]
    public partial class SlotCostEntity : EMS.Common.ISlotCost
    {

        private long _pk_SlotCostID;

        private System.Nullable<long> _fk_SlotID;

        private string _CargoType;

        private string _CntrSize;

        private string _SpecialType;

        private System.Nullable<int> _Srl;

        private System.Nullable<int> _fk_ContainerTypeID;

        private System.Nullable<decimal> _ContainerRate;

        private System.Nullable<decimal> _RatePerTon;

        private System.Nullable<decimal> _RatePerCBM;

        private IList<EMS.Common.ISlotCost> _lstSlotCost;

        private int _fk_UserAdded;

        private System.Nullable<int> _fk_UserLastEdited;

        private System.DateTime _AddedOn;

        private System.Nullable<System.DateTime> _EditedOn;

        //public IList<ISlotCost> GetSlotCost(DataTable dt)
        //{
        //    int srl1 = 0;
        //    IList<ISlotCost> tempLst = null;
        //    if (dt != null)
        //    {
        //        tempLst = new List<ISlotCost>();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            tempLst.Add(new SlotCostEntity()
        //            {
        //                SrlNo = srl1 + 1,
        //                SlotCostID = dr["SlotCostID"].ToLong(),
        //                SlotID = dr["SlotID"].ToLong(),
        //                ContainerTypeID = dr["ContainerTypeID"].ToNullInt(),
        //                CntrSize = dr["CntrSize"].ToString(),
        //                RatePerCBM = dr["RatePerCBM"].ToDecimal(),
        //                RatePerTon = dr["RatePerTon"].ToDecimal(),
        //                ContainerRate = dr["ContainerRate"].ToDecimal(),
        //                SpecialType = dr["SpecialType"].ToString(),
        //                CargoType = dr["CargoType"].ToString()
        //            }
        //            );
        //            srl1++;
        //        }
        //    }
        //    return tempLst;
        //}


        public SlotCostEntity()
        {

        }

        [DataMember]

        public System.Nullable<int> SrlNo
        {
            get
            {
                return this._Srl;
            }
            set
            {
                if ((this._Srl != value))
                {
                    this._Srl = value;
                }
            }
        }

        [DataMember]
        public string CargoType
        {
            get
            {
                return this._CargoType;
            }
            set
            {
                if ((this._CargoType != value))
                {
                    this._CargoType = value;
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
        public long SlotCostID
        {
            get
            {
                return this._pk_SlotCostID;
            }
            set
            {
                if ((this._pk_SlotCostID != value))
                {
                    this._pk_SlotCostID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<long> SlotID
        {
            get
            {
                return this._fk_SlotID;
            }
            set
            {
                if ((this._fk_SlotID != value))
                {
                    this._fk_SlotID = value;
                }
            }
        }

        [DataMember]
        public string SpecialType
        {
            get
            {
                return this._SpecialType;
            }
            set
            {
                if ((this._SpecialType != value))
                {
                    this._SpecialType = value;
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
        public System.Nullable<System.Decimal> ContainerRate
        {
            get
            {
                return this._ContainerRate;
            }
            set
            {
                if ((this._ContainerRate != value))
                {
                    this._ContainerRate = value;
                }
            }
        }

        public System.Nullable<System.Decimal> RatePerTon
        {
            get
            {
                return this._RatePerTon;
            }
            set
            {
                if ((this._RatePerTon != value))
                {
                    this._RatePerTon = value;
                }
            }
        }

        public System.Nullable<System.Decimal> RatePerCBM
        {
            get
            {
                return this._RatePerCBM;
            }
            set
            {
                if ((this._RatePerCBM != value))
                {
                    this._RatePerCBM = value;
                }
            }
        }


        public IList<EMS.Common.ISlotCost> lstSlotCost
        {
            get
            {
                return this._lstSlotCost;
            }
            set
            {
                if ((this._lstSlotCost != value))
                {
                    this._lstSlotCost = value;
                }
            }
        }

    }
}

