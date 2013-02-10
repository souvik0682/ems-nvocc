using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    [Serializable]
    public class BLFooterEntity : IBLFooter
    {
        public long BLID
        {
            get;
            set;
        }

        public long BLFooterID
        {
            get;
            set;
        }

        public string CntrNo
        {
            get;
            set;
        }

        public string CntrSize
        {
            get;
            set;
        }

        public int ContainerTypeID
        {
            get;
            set;
        }

        public string ContainerType { get; set; }

        public string ContainerAbbr { get; set; }

        public string SealNo
        {
            get;
            set;
        }

        public string Comodity
        {
            get;
            set;
        }

        public decimal GrossWeight
        {
            get;
            set;
        }

        public int Package
        {
            get;
            set;
        }

        public decimal CargoWtTon
        {
            get;
            set;
        }

        public string SOC
        {
            get;
            set;
        }

        public string ISOCode
        {
            get;
            set;
        }

        public decimal Temperature
        {
            get;
            set;
        }

        public string TempUnit
        {
            get;
            set;
        }

        public string CustomSeal
        {
            get;
            set;
        }

        public string CAgent
        {
            get;
            set;
        }

        public decimal TempMax
        {
            get;
            set;
        }

        public decimal TempMin
        {
            get;
            set;
        }

        public string PCSTemp
        {
            get;
            set;
        }

        public string DIMCode
        {
            get;
            set;
        }

        public decimal ODLength
        {
            get;
            set;
        }

        public decimal ODWidth
        {
            get;
            set;
        }

        public decimal ODHeight
        {
            get;
            set;
        }

        public string Cargo
        {
            get;
            set;
        }

        public string Stowage
        {
            get;
            set;
        }

        public string CallNo
        {
            get;
            set;
        }

        public string IMCO
        {
            get;
            set;
        }

        public int MovementID
        {
            get;
            set;
        }

        public DateTime DateLastMoved
        {
            get;
            set;
        }

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

        public decimal TareWeight { get; set; }
        public bool Waiver { get; set; }
        public bool LCLDuplicate { get; set; }
        

        public BLFooterEntity()
        {
        }

        public BLFooterEntity(DataTableReader reader)
        {
            this.BLFooterID = Convert.ToInt32(reader["BLFooterID"]);
            this.BLID = Convert.ToInt32(reader["BLID"]);
            this.CAgent = Convert.ToString(reader["CAgent"]);
            this.CallNo = Convert.ToString(reader["CallNo"]);
            this.Cargo = Convert.ToString(reader["Cargo"]);
            this.CargoWtTon = Convert.ToDecimal(reader["CargoWtTon"]);
            this.CntrNo = Convert.ToString(reader["CntrNo"]);
            this.CntrSize = Convert.ToString(reader["CntrSize"]);
            this.Comodity = Convert.ToString(reader["Comodity"]);
            this.ContainerTypeID = Convert.ToInt32(reader["ContainerTypeID"]);
            this.CustomSeal = Convert.ToString(reader["CustomSeal"]);
            //this.DateLastMoved = Convert.ToDateTime(reader["DateLastMoved"]);
            this.DIMCode = Convert.ToString(reader["DIMCode"]);
            this.GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);
            this.IMCO = Convert.ToString(reader["IMCO"]);
            this.ISOCode = Convert.ToString(reader["ISOCode"]);
            //this.MovementID = Convert.ToInt32(reader["MovementID"]);
            this.ODHeight = Convert.ToDecimal(reader["ODHeight"]);
            this.ODLength = Convert.ToDecimal(reader["ODLength"]);
            this.ODWidth = Convert.ToDecimal(reader["ODWidth"]);
            this.Package = Convert.ToInt32(reader["Package"]);
            this.PCSTemp = Convert.ToString(reader["PCSTemp"]);
            this.SealNo = Convert.ToString(reader["SealNo"]);
            this.SOC = Convert.ToString(reader["SOC"]);
            this.Stowage = Convert.ToString(reader["Stowage"]);
            this.Temperature = Convert.ToDecimal(reader["Temperature"]);
            this.TempMax = Convert.ToDecimal(reader["TempMax"]);
            this.TempMin = Convert.ToDecimal(reader["TempMin"]);
            this.TempUnit = Convert.ToString(reader["TempUnit"]);
            this.ContainerType = Convert.ToString(reader["ContainerAbbr"]);

            this.TareWeight = Convert.ToDecimal(reader["TareWeight"]);
            this.Waiver = Convert.ToBoolean(reader["Waiver"]);
            this.LCLDuplicate = Convert.ToBoolean(reader["LCLDuplicate"]);
        }
    }
}
