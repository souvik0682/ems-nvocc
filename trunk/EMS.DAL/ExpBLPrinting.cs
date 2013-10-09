using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using System.Data.SqlClient;
using EMS.Entity;
using EMS.Utilities;

namespace EMS.DAL
{
    public class ExpBLPrintingDAL
    {
        public static BLPrintModel GetxpBLPrinting(ISearchCriteria searchCriteria = null)
        {
            if (searchCriteria != null)
            {
                string strExecution = "[exp].[prcGetBLPrintingCL]";
                using (DbQuery oDq = new DbQuery(strExecution))
                {
                    oDq.AddVarcharParam("@BLID", 50, searchCriteria.StringParams[0]);
                    DataSet ds = oDq.GetTables();
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {

                        var bLPrint = ds.Tables[0].DataTableToType<BLPrint>();
                        var itemDetails = ds.Tables[1].DataTableToCollectionType<ItemDetail>();
                        if (bLPrint == null) { return (BLPrintModel)null; }
                        return new BLPrintModel() { BLPrint = bLPrint, ItemDetails = itemDetails };
                    }
                }
            }
            else {
                return new BLPrintModel
                {
                    BLPrint = new BLPrint
                    {
                        AgentAddress = "AgentAddress",
                        AgentName = "AgentName",
                        BLClause = "BLClause",
                        Consignee = "Consignee",
                        ConsigneeName = "ConsigneeName",
                        ExpBLDate = "ExpBLDate",
                        ExpBLNo = "ExpBLNo",
                        FinalDelivery = "FinalDelivery",
                        fk_BLIssuePort = "fk_BLIssuePort",
                        fk_LocationID = "fk_LocationID",
                        fk_NVOCCID = "fk_NVOCCID",
                        FreightPayableAt = "FreightPayableAt",
                        FreightPrePayToPay = "FreightPrePayToPay",
                        GoodsDescription = "GoodsDescription",
                        GRWT = "GRWT",
                        LocationName = "LocationName",
                        MarksNumbers = "MarksNumbers",
                        NetWt = "NetWt",
                        NoofBLs = "NoofBLs",
                        Notify = "Notify",
                        NotifyName = "NotifyName",
                        PlaceofDischarge = "PlaceofDischarge",
                        PlaceofLoading = "PlaceofLoading",
                        PlaceOfReceipt = "PlaceOfReceipt",
                        ShipmentMode = "ShipmentMode",
                        ShipmentType = "ShipmentType",
                        Shipper = "Shipper",
                        ShipperName = "ShipperName",
                        VesselName = "VesselName",
                        VoyageNo = "VoyageNo"
                    },
                    ItemDetails = new List<ItemDetail> { 
                        new ItemDetail{CntrType="CntrType0",ContainerNo="ContainerNo0",GrossWeight="GrossWeight0",Package="Package0",SealNo="SealNo",Size="21"},
                        new ItemDetail{CntrType="CntrType1",ContainerNo="ContainerNo1",GrossWeight="GrossWeight1",Package="Package1",SealNo="SealNo1",Size="21"},
                        new ItemDetail{CntrType="CntrType2",ContainerNo="ContainerNo2",GrossWeight="GrossWeight2",Package="Package2",SealNo="SealNo2",Size="21"},
                        new ItemDetail{CntrType="CntrType3",ContainerNo="ContainerNo3",GrossWeight="GrossWeight3",Package="Package3",SealNo="SealNo3",Size="21"},
                        new ItemDetail{CntrType="CntrType",ContainerNo="ContainerNo3",GrossWeight="GrossWeight4",Package="Package4",SealNo="SealNo4",Size="21"},
                        new ItemDetail{CntrType="CntrType4",ContainerNo="ContainerNo4",GrossWeight="GrossWeight5",Package="Package5",SealNo="SealNo5",Size="21"},
                        new ItemDetail{CntrType="CntrType5",ContainerNo="ContainerNo5",GrossWeight="GrossWeight6",Package="Package6",SealNo="SealNo6",Size="21"},
                        new ItemDetail{CntrType="CntrType6",ContainerNo="ContainerNo6",GrossWeight="GrossWeight7",Package="Package7",SealNo="SealNo7",Size="21"},
                        new ItemDetail{CntrType="CntrType7",ContainerNo="ContainerNo7",GrossWeight="GrossWeight8",Package="Package8",SealNo="SealNo8",Size="21"}
                    }
                };
            
            
            }
            
            return (BLPrintModel)null;
        }
        public static DataSet GetxpBLPrintingDS(ISearchCriteria searchCriteria = null)
        {
            DataSet ds = new DataSet();
            if (searchCriteria != null)
            {
                string strExecution = "[exp].[prcGetBLPrintingCL]";
                using (DbQuery oDq = new DbQuery(strExecution))
                {
                    oDq.AddVarcharParam("@BLID", 50, searchCriteria.StringParams[0]);
                    ds = oDq.GetTables();
                }
            }

            return ds;
        }
    }
}
