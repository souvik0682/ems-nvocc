using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class SlotEntity : EMS.Common.ISlot
    {
        #region ISlot Members

        public int SlotID
        {
            get;
            set;
        }

        public string SlotOperatorName
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

        public int SlotOperatorID
        {
            get;
            set;
        }

        public int LineID
        {
            get;
            set;
        }

        public string NVOCC
        {
            get;
            set;
        }

        public string POL
        {
            get;
            set;
        }

        public string POD
        {
            get;
            set;
        }

        public Int32 POLID
        {
            get;
            set;
        }

        public Int32 PODID
        {
            get;
            set;
        }

        public int MovOrigin
        {
            get;
            set;
        }

        //public int MovDestination
        //{
        //    get;
        //    set;
        //}

        public DateTime EffectDt
        {
            get;
            set;
        }

        public bool SlotStatus
        {
            get;
            set;
        }

        public string PODTerminal
        {
            get;
            set;
        }

        //private IList<EMS.Common.ISlotCost> _lstSlotCost;

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

        #region IBase<int> Members

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        
        #endregion

        #region Constructors

        public SlotEntity()
        {
            
        }

        public SlotEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "fk_SlotOperatorID"))
            {
                if (reader["fk_SlotOperatorID"] != DBNull.Value)
                    this.SlotOperatorID = Convert.ToInt32(reader["fk_SlotOperatorID"]);
            }

            if (ColumnExists(reader, "SlotOperatorName"))
            {
                if (reader["SlotOperatorName"] != DBNull.Value)
                    this.SlotOperatorName = Convert.ToString(reader["SlotOperatorName"]);
            }

            if (ColumnExists(reader, "LineName"))
            {
                if (reader["LineName"] != DBNull.Value)
                    this.NVOCC = Convert.ToString(reader["LineName"]);
            }

            if (ColumnExists(reader, "fk_LineID"))
            {
                if (reader["fk_LineID"] != DBNull.Value)
                    this.LineID = Convert.ToInt32(reader["fk_LineID"]);
            }

            if (ColumnExists(reader, "fk_SlotID"))
            {
                if (reader["fk_SlotID"] != DBNull.Value)
                    this.SlotID = Convert.ToInt32(reader["fk_SlotID"]);
            }

            if (ColumnExists(reader, "LoadPort"))
            {
                if (reader["LoadPort"] != DBNull.Value)
                    this.POL = Convert.ToString(reader["LoadPort"]);
            }

            if (ColumnExists(reader, "fk_POL"))
            {
                if (reader["fk_POL"] != DBNull.Value)
                    this.POLID = Convert.ToInt32(reader["fk_POL"]);
            }

            if (ColumnExists(reader, "DestPort"))
            {
                if (reader["DestPort"] != DBNull.Value)
                    this.POD = Convert.ToString(reader["DestPort"]);
            }

            if (ColumnExists(reader, "fk_POD"))
            {
                if (reader["fk_POD"] != DBNull.Value)
                    this.PODID = Convert.ToInt32(reader["fk_POD"]);
            }


            if (ColumnExists(reader, "PODTerminal"))
            {
                if (reader["PODTerminal"] != DBNull.Value)
                    this.PODTerminal = Convert.ToString(reader["PODTerminal"]);
            }

            if (ColumnExists(reader, "fk_MovOrigin"))
            {
                if (reader["fk_MovOrigin"] != DBNull.Value)
                    this.MovOrigin = Convert.ToInt32(reader["fk_MovOrigin"]);
            }

            //if (ColumnExists(reader, "fk_MovDest"))
            //{
            //    if (reader["fk_MovDest"] != DBNull.Value)
            //        this.MovDestination = Convert.ToInt32(reader["fk_MovDest"]);
            //}


            if (ColumnExists(reader, "effDate"))
            {
                if (reader["effDate"] != DBNull.Value)
                    this.EffectDt = Convert.ToDateTime(reader["effDate"]);
            }

        }

        //public IList<EMS.Common.ISlotCost> lstSlotCost
        //{
        //    get
        //    {
        //        return this._lstSlotCost;
        //    }
        //    set
        //    {
        //        if ((this._lstSlotCost != value))
        //        {
        //            this._lstSlotCost = value;
        //        }
        //    }
        //}

        #endregion

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }


}
