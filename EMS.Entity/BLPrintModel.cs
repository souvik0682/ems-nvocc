using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    //public class BLPrint
    //{
    //    public string Shipper { get; set; }
    //    public string Consignee { get; set; }
    //    public string NotifyAddress { get; set; }
    //    public string PreCarriedBy { get; set; }
    //    public string PlaceOfReceipt { get; set; }
    //    public string VesselVoyageNo { get; set; }
    //    public string PortOfLoading { get; set; }
    //    public string PortOfDischarge { get; set; }
    //    public string PlaceOfDelivary { get; set; }
    //    public string FreightDetails { get; set; }
    //    public string FreightPayable { get; set; }
    //    public string NumerOfOriginal { get; set; }
    //    public string PlaceDateOfIssue { get; set; }
    //    public string BLNo { get; set; }
    //    public string RefNo { get; set; }
    //    public string DailyDemurage { get; set; }
    //    public string Applicable { get; set; }  
    //}

    //public class ItemDetail {
    //    public string MarksNNos { get; set; }
    //    public string NumberNKindOfPackage { get; set; }
    //    public double GrossWeight { get; set; }
    //    public double NetWeight { get; set; }
    //    public string Measurement { get; set; }
    //}

    public class BLPrint
    {
        public string fk_LocationID { get; set; }
        public string LocationName { get; set; }
        public string fk_NVOCCID { get; set; }
        public string ExpBLNo { get; set; }
        public string ExpBLDate { get; set; }
        public string fk_BLIssuePort { get; set; }
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string Notify { get; set; }

        public string ShipperName { get; set; }
        public string ConsigneeName { get; set; }
        public string NotifyName { get; set; }

        public string PlaceOfReceipt { get; set; }
        public string PlaceofLoading { get; set; }
        public string PlaceofDischarge { get; set; }
        public string FinalDelivery { get; set; }
        public string MarksNumbers { get; set; }
        public string GoodsDescription { get; set; }
        public string FreightPrePayToPay { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public string FreightPayableAt { get; set; }
        public string BLClause { get; set; }
        public string ShipmentType { get; set; }
        public string ShipmentMode { get; set; }
        public string NoofBLs { get; set; }
        public string GRWT { get; set; }
        public string NetWt { get; set; }


    }
    public class BLPrintModel
    {
        public BLPrint BLPrint { get; set; }
        public IList<ItemDetail> ItemDetails { get; set; }
    }
    public class ItemDetail
    {
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string CntrType { get; set; }
        public string SealNo { get; set; }
        public string GrossWeight { get; set; }
        public string Package { get; set; }
    }
}
