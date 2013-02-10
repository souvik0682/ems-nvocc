using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IBLFooter : ICommon
    {
        Int64 BLID { get; set; }
        Int64 BLFooterID { get; set; }
        string CntrNo { get; set; }
        string CntrSize { get; set; }
        int ContainerTypeID { get; set; }
        string ContainerType { get; set; }
        string SealNo { get; set; }
        string Comodity { get; set; }
        decimal GrossWeight { get; set; }
        int Package { get; set; }
        decimal CargoWtTon { get; set; }
        string SOC { get; set; }
        string ISOCode { get; set; }
        decimal Temperature { get; set; }
        string TempUnit { get; set; }
        string CustomSeal { get; set; }
        string CAgent { get; set; }
        decimal TempMax { get; set; }
        decimal TempMin { get; set; }
        string PCSTemp { get; set; }
        string DIMCode { get; set; }
        decimal ODLength { get; set; }
        decimal ODWidth { get; set; }
        decimal ODHeight { get; set; }
        string Cargo { get; set; }
        string Stowage { get; set; }
        string CallNo { get; set; }
        string IMCO { get; set; }
        int MovementID { get; set; }
        DateTime DateLastMoved { get; set; }
        string ContainerAbbr { get; set; }

        decimal TareWeight { get; set; }
        bool Waiver { get; set; }
        bool LCLDuplicate { get; set; }
    }
}
