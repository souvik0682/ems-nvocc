using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using EMS.Entity;

namespace EMS.DAL
{
    public class ImportBLDAL
    {
        public static List<ILine> GetAllLine()
        {
            string strExecution = "uspGetAllLine";

            List<ILine> lstLine = new List<ILine>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCID", 0);
                oDq.AddVarcharParam("@NVOCCName", 100, "");
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ILine line = new LineEntity(reader);
                    lstLine.Add(line);
                }
                reader.Close();
            }
            return lstLine;
        }

        public static List<ILocation> GetLocation(int UserId)
        {
            string strExecution = "uspGetLocationForImportBL";
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", UserId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ILocation loc = new LocationEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        public static DataTable GetVoyages(int VesseleId)
        {
            string strExecution = "uspGetVoyage";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@fk_VesselID", VesseleId);

                dt = oDq.GetTable();
            }

            return dt;
        }

        public static int GetVesselId(string VesseleName)
        {
            string strExecution = "uspGetVesselIdByVesselName";
            int vesselId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@VesselName", 60, VesseleName);

                vesselId = Convert.ToInt32(oDq.GetScalar());
            }

            return vesselId;
        }

        public static int GetUnitId(string UnitName)
        {
            string strExecution = "uspGetUnitIdByUnitName";
            int unitId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@UnitName", 30, UnitName);

                unitId = Convert.ToInt32(oDq.GetScalar());
            }

            return unitId;
        }

        public static int GetPortId(string PortCode)
        {
            string strExecution = "uspGetPortIdByPortCode";
            int portId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@PortCode", 6, PortCode);

                portId = Convert.ToInt32(oDq.GetScalar());
            }

            return portId;
        }

        public static int GetDeliveryToId(string DeliveryTo)
        {
            string strExecution = "uspGetDeliveryToIdByName";
            int deliveryToId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@DeliveryTo", 60, DeliveryTo);

                deliveryToId = Convert.ToInt32(oDq.GetScalar());
            }

            return deliveryToId;
        }

        public static DataTable GetCFSName(string CFSCode)
        {
            string strExecution = "uspGetCFSNameByCode";
            DataTable CFS = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@CFSCode", 10, CFSCode);

                CFS = oDq.GetTable();
            }

            return CFS;
        }

        public static int GetDefaultFreeDays(int NvoccId)
        {
            string strExecution = "uspGetDefaultFreeDays";
            int DefaultFreeDays = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NvoccId", NvoccId);
                object val = oDq.GetScalar();

                if (val != DBNull.Value)
                    DefaultFreeDays = Convert.ToInt32(val);
            }

            return DefaultFreeDays;
        }

        public static int GetPGRFreeDays(int LocationId)
        {
            string strExecution = "uspGetPGRFreeDays";
            int PGRFreeDays = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocationId", LocationId);
                object val = oDq.GetScalar();

                if (val != DBNull.Value)
                    PGRFreeDays = Convert.ToInt32(val);
            }

            return PGRFreeDays;
        }

        public static int GetAddressId(string AddrName)
        {
            string strExecution = "uspGetAddrIdByAddrName";
            int addressId = 0;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@AddrName", 60, AddrName);

                addressId = Convert.ToInt32(oDq.GetScalar());
            }

            return addressId;
        }

        public static string GetAddress(string AddrName)
        {
            string strExecution = "uspGetAddrByAddrName";
            string address = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@AddrName", 60, AddrName);

                address = Convert.ToString(oDq.GetScalar());
            }

            return address;
        }

        public static DataTable GetContainerType()
        {
            string strExecution = "uspGetContainerType";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }

            return dt;
        }

        //public static DataTable GetISOCode()
        //{
        //    string strExecution = "uspGetISOCode";
        //    DataTable dt = new DataTable();


        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        dt = oDq.GetTable();
        //    }

        //    return dt;
        //}

        public static string GetISOCode(long LocationID, int ContainerSize)
        {
            string strExecution = "uspGetISOCode";
            string ISO = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationID", LocationID);
                oDq.AddIntegerParam("@ContainerSize", ContainerSize);

                ISO = Convert.ToString(oDq.GetScalar());
            }

            return ISO;
        }

        public static DataTable GetTareWeight(int ContainerTypeID)
        {
            string strExecution = "uspGetTareWeight";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@ContainerTypeID", ContainerTypeID);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetDefaultFreightPayableAt(int LocationId)
        {
            string strExecution = "uspGetDefaultFreightPayableAt";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocationId", LocationId);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetDefaultUnit(string UnitType, string UnitCode)
        {
            string strExecution = "uspGetUnitByUnitCode";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@UnitType", 1, UnitType);
                oDq.AddVarcharParam("@UnitCode", 3, UnitCode);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static int SaveImportBL(IBLHeader blHeader)
        {
            string strExecution = "uspSaveImportBLHeader";
            int blId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", blHeader.BLID);
                oDq.AddBigIntegerParam("@BLIssuePortID", blHeader.BLIssuePortID);
                oDq.AddVarcharParam("@CACode", 10, blHeader.CACode.ToUpper());
                oDq.AddVarcharParam("@BillOfEntryNo", 50, blHeader.BillOfEntryNo.ToUpper());
                oDq.AddVarcharParam("@CargoMovement", 2, blHeader.CargoMovement.ToUpper());
                oDq.AddVarcharParam("@CargoNature", 2, blHeader.CargoNature.ToUpper());
                oDq.AddVarcharParam("@CargoType", 1, blHeader.CargoType.ToUpper());
                oDq.AddIntegerParam("@DetentionFree", blHeader.DetentionFree);
                oDq.AddVarcharParam("@DetentionSlabType", 1, blHeader.DetentionSlabType.ToUpper());
                oDq.AddBooleanParam("@DOLock", blHeader.DOLock);
                oDq.AddVarcharParam("@DOLockReason", 100, blHeader.DOLockReason.ToUpper());
                oDq.AddBooleanParam("@DPT", blHeader.DPT);
                oDq.AddBigIntegerParam("@FinalDestination", blHeader.FinalDestination);
                oDq.AddBooleanParam("@FreeOut", blHeader.FreeOut);
                oDq.AddVarcharParam("@FreightType", 2, blHeader.FreightType.ToUpper());
                oDq.AddBooleanParam("@HazFlag", blHeader.HazFlag);
                oDq.AddVarcharParam("@FreigthToCollect", 60, blHeader.FreigthToCollect.ToString().ToUpper());
                oDq.AddVarcharParam("@GrossWeight", 60, blHeader.GrossWeight.ToString().ToUpper());
                oDq.AddDateTimeParam("@IGMBLDate", blHeader.IGMBLDate);
                oDq.AddVarcharParam("@IGMBLNumber", 60, blHeader.IGMBLNumber.ToUpper());
                oDq.AddVarcharParam("@IMOCode", 60, blHeader.IMOCode.ToUpper());
                oDq.AddVarcharParam("@ImpLineBLNo", 60, blHeader.ImpLineBLNo.ToUpper());
                oDq.AddDateTimeParam("@ImpLineBLDate", blHeader.ImpLineBLDate);
                oDq.AddBigIntegerParam("@ImpVesselID", blHeader.ImpVesselID);
                oDq.AddBigIntegerParam("@ImpVoyageID", blHeader.ImpVoyageID);
                oDq.AddVarcharParam("@ItemLineNo", 6, blHeader.ItemLineNo.ToUpper());
                oDq.AddVarcharParam("@ItemLinePrefix", 8, blHeader.ItemLinePrefix.ToUpper());
                oDq.AddVarcharParam("@ItemSubLineNo", 6, blHeader.ItemSubLineNo.ToUpper());
                oDq.AddVarcharParam("@ItemType", 2, blHeader.ItemType.ToUpper());
                oDq.AddVarcharParam("@LineBLType", 2, blHeader.LineBLType.ToUpper());
                oDq.AddIntegerParam("@LocationID", blHeader.LocationID);
                oDq.AddVarcharParam("@MLOCode", 16, blHeader.MLOCode.ToUpper());
                oDq.AddIntegerParam("@NVOCCID", blHeader.NVOCCID);
                oDq.AddIntegerParam("@NoofFEU", blHeader.NoofFEU);
                oDq.AddIntegerParam("@NoofTEU", blHeader.NoofTEU);
                oDq.AddVarcharParam("@PackageDetail", 30, blHeader.PackageDetail.ToUpper());
                oDq.AddBooleanParam("@PartBL", blHeader.PartBL);
                oDq.AddIntegerParam("@PGR_FreeDays", blHeader.PGR_FreeDays);
                oDq.AddBigIntegerParam("@PortDischarge", blHeader.PortDischarge);
                oDq.AddBigIntegerParam("@PortLoading", blHeader.PortLoading);
                oDq.AddBooleanParam("@Reefer", blHeader.Reefer);
                oDq.AddBigIntegerParam("@StockLocationID", blHeader.StockLocationID);
                oDq.AddIntegerParam("@SurveyorAddressID", blHeader.SurveyorAddressID); //NOT FOUND
                oDq.AddBooleanParam("@TaxExemption", blHeader.TaxExemption);
                oDq.AddVarcharParam("@TPBondNo", 10, blHeader.TPBondNo.ToUpper());
                oDq.AddVarcharParam("@TransportMode", 1, blHeader.TransportMode.ToUpper());
                oDq.AddIntegerParam("@UnitOfVolume", blHeader.UnitOfVolume);
                oDq.AddIntegerParam("@UnitOfWeight", blHeader.UnitOfWeight);
                oDq.AddIntegerParam("@UnitPackage", blHeader.UnitPackage);
                oDq.AddVarcharParam("@UNOCode", 5, blHeader.UNOCode.ToUpper());
                oDq.AddVarcharParam("@Volume", 60, blHeader.Volume.ToString().ToUpper());
                oDq.AddVarcharParam("@WaiverPercent", 60, blHeader.WaiverPercent.ToString().ToUpper()); //NOT FOUND
                oDq.AddVarcharParam("@WaiverType", 60, blHeader.WaiverType.ToUpper()); //NOT FOUND
                oDq.AddVarcharParam("@TranShipment", 500, blHeader.TranShipment.ToUpper());
                oDq.AddVarcharParam("@ShipperInformation", 500, blHeader.ShipperInformation.ToUpper());
                oDq.AddVarcharParam("@ShipperName", 60, blHeader.ShipperName.ToUpper()); //NOT FOUND
                oDq.AddVarcharParam("@MarksNumbers", 500, blHeader.MarksNumbers.ToUpper());
                oDq.AddVarcharParam("@GoodDescription", 60, blHeader.GoodDescription.ToUpper());
                oDq.AddVarcharParam("@ConsigneeInformation", 500, blHeader.ConsigneeInformation.ToUpper());
                oDq.AddVarcharParam("@ConsigneeName", 60, blHeader.ConsigneeName.ToUpper()); //NOT FOUND
                oDq.AddVarcharParam("@BLComment", 60, blHeader.BLComment.ToUpper());
                oDq.AddVarcharParam("@NotifyName", 60, blHeader.NotifyName.ToUpper()); //NOT FOUND
                oDq.AddVarcharParam("@NotifyPartyInformation", 500, blHeader.NotifyPartyInformation.ToUpper());
                oDq.AddDateTimeParam("@dtAdded", blHeader.dtAdded);
                oDq.AddDateTimeParam("@dtEdited", blHeader.dtEdited);
                oDq.AddBooleanParam("@RsStatus", blHeader.RsStatus);
                oDq.AddBooleanParam("@ONBR", blHeader.ONBR);
                oDq.AddIntegerParam("@CompanyID", blHeader.CompanyID);
                oDq.AddBooleanParam("@Billing", blHeader.Billing); //NOT FOUND
                oDq.AddBigIntegerParam("@NumberPackage", blHeader.NumberPackage);
                oDq.AddDateTimeParam("@WaiverDate", blHeader.WaiverDate); //NOT FOUND
                oDq.AddVarcharParam("@Commodity", 20, blHeader.Commodity.ToUpper());
                oDq.AddVarcharParam("@LineBLVesselDetail", 100, blHeader.LineBLVesselDetail.ToUpper());
                oDq.AddVarcharParam("@CANTo", 60, blHeader.CANTo.ToUpper());
                oDq.AddIntegerParam("@WaiverFk_UserID", blHeader.WaiverFk_UserID); //NOT FOUND
                oDq.AddBigIntegerParam("@UserAdded", blHeader.UserAdded);
                oDq.AddBigIntegerParam("@UserEdited", blHeader.UserEdited);
                if (!string.IsNullOrEmpty(blHeader.WaiverLetterUpload))
                    oDq.AddVarcharParam("@WaiverLetterUpload", 60, blHeader.WaiverLetterUpload.ToUpper()); //NOT FOUND
                oDq.AddVarcharParam("@CFSNomination", 1, blHeader.CFSNomination.ToUpper());
                oDq.AddIntegerParam("@AddressCFSId", blHeader.AddressCFSId);
                oDq.AddBigIntegerParam("@PortFrtPayableID", blHeader.PortFrtPayableID);
                oDq.AddIntegerParam("@DPTId", blHeader.DPTId);

                blId = Convert.ToInt32(oDq.GetScalar());
            }

            return blId;
        }

        public static int SaveImportBLFooter(IBLFooter blFooter)
        {
            string strExecution = "uspSaveImportBLFooter";
            int blFooterId = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", blFooter.BLID);
                oDq.AddBigIntegerParam("@BLFooterID", blFooter.BLFooterID);
                oDq.AddVarcharParam("@CAgent", 15, blFooter.CAgent.ToUpper());
                oDq.AddVarcharParam("@CallNo", 1, blFooter.CallNo.ToUpper());
                oDq.AddVarcharParam("@Cargo", 3, blFooter.Cargo.ToUpper());
                oDq.AddVarcharParam("@CargoWtTon", 10, blFooter.CargoWtTon.ToString().ToUpper());
                oDq.AddVarcharParam("@CntrNo", 11, blFooter.CntrNo.ToUpper());
                oDq.AddVarcharParam("@CntrSize", 2, blFooter.CntrSize.ToUpper());
                oDq.AddVarcharParam("@Comodity", 60, blFooter.Comodity.ToUpper());
                oDq.AddIntegerParam("@ContainerTypeID", blFooter.ContainerTypeID);
                oDq.AddVarcharParam("@CustomSeal", 15, blFooter.CustomSeal.ToUpper());
                oDq.AddVarcharParam("@DIMCode", 3, blFooter.DIMCode.ToUpper());
                oDq.AddVarcharParam("@GrossWeight", 60, blFooter.GrossWeight.ToString().ToUpper());
                oDq.AddVarcharParam("@IMCO", 4, blFooter.IMCO.ToUpper()); ;
                oDq.AddVarcharParam("@ISOCode", 4, blFooter.ISOCode.ToUpper());
                oDq.AddVarcharParam("@ODHeight", 18, blFooter.ODHeight.ToString().ToUpper());
                oDq.AddVarcharParam("@ODLength", 18, blFooter.ODLength.ToString().ToUpper());
                oDq.AddVarcharParam("@ODWidth", 18, blFooter.ODWidth.ToString().ToUpper());
                oDq.AddIntegerParam("@Package", blFooter.Package);
                oDq.AddVarcharParam("@SealNo", 15, blFooter.SealNo.ToUpper());
                oDq.AddVarcharParam("@SOC", 1, blFooter.SOC.ToUpper());
                oDq.AddVarcharParam("@Stowage", 60, blFooter.Stowage.ToUpper());
                oDq.AddVarcharParam("@Temperature", 10, blFooter.Temperature.ToString().ToUpper());
                oDq.AddVarcharParam("@TempMax", 10, blFooter.TempMax.ToString().ToUpper());
                oDq.AddVarcharParam("@TempMin", 10, blFooter.TempMin.ToString().ToUpper());
                oDq.AddVarcharParam("@TempUnit", 1, blFooter.TempUnit.ToUpper());
                oDq.AddVarcharParam("@PCSTemp", 3, blFooter.PCSTemp.ToUpper());
                oDq.AddBooleanParam("@Waiver", blFooter.Waiver);
                oDq.AddBooleanParam("@LCLDuplicate", blFooter.LCLDuplicate);

                blFooterId = Convert.ToInt32(oDq.GetScalar());
            }

            return blFooterId;
        }

        public static IBLHeader GetBLHeaderinformation(long BlId)
        {
            string strExecution = "uspGetBLHeaderInfo";
            IBLHeader header = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", BlId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    header = new BLHeaderEntity(reader);
                }

                reader.Close();
            }

            return header;
        }

        public static List<IBLFooter> GetBLFooterInfo(long BlId)
        {
            string strExecution = "uspGetBLFooterInfo";
            List<IBLFooter> lstFooter = new List<IBLFooter>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", BlId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBLFooter footer = new BLFooterEntity(reader);
                    lstFooter.Add(footer);
                }

                reader.Close();
            }

            return lstFooter;
        }

        public static string GetVesselNameById(Int64 VesslId)
        {
            string strExecution = "uspGetVeselNameById";
            string vesselName = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@VesselID", VesslId);

                vesselName = Convert.ToString(oDq.GetScalar());
            }

            return vesselName;
        }

        public static string GetPortNameById(Int64 PortId)
        {
            string strExecution = "uspGetPortNameByPortId";
            string portName = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@PortId", PortId);

                portName = Convert.ToString(oDq.GetScalar());
            }

            return portName;
        }

        public static string GetCFSCodeById(int AddrId)
        {
            string strExecution = "uspGetCFSCodebyId";
            string portName = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AddrId", AddrId);

                portName = Convert.ToString(oDq.GetScalar());
            }

            return portName;
        }

        public static string GetDeliveryToById(int DPTId)
        {
            string strExecution = "uspGetDeliveryToById";
            string deliveryTo = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@DPTId", DPTId);

                deliveryTo = Convert.ToString(oDq.GetScalar());
            }

            return deliveryTo;
        }

        public static string GetUnitNameById(int UOMId)
        {
            string strExecution = "uspGetUnitNameById";
            string unitName = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UOMId", UOMId);

                unitName = Convert.ToString(oDq.GetScalar());
            }

            return unitName;
        }

        public static string GetSurveyorNameById(int SurveyorId)
        {
            string strExecution = "uspGetSurveyorNameById";
            string surveyorName = string.Empty;


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@SurveyorId", SurveyorId);

                surveyorName = Convert.ToString(oDq.GetScalar());
            }

            return surveyorName;
        }

        public static List<IBLHeader> GetImportBL(SearchCriteria searchCriteria)
        {
            string strExecution = "[uspGetImportBLList]";
            List<IBLHeader> lstBL = new List<IBLHeader>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@IGMBLNo", 100, searchCriteria.IGMBLNo);
                oDq.AddVarcharParam("@LineBLNo", 100, searchCriteria.LineBLNo);
                //oDq.AddVarcharParam("@ContainerNo", 100, searchCriteria.ContainerNo);
                oDq.AddVarcharParam("@POD", 100, searchCriteria.POD);
                oDq.AddVarcharParam("@POL", 100, searchCriteria.POL);
                oDq.AddVarcharParam("@Voyage", 100, searchCriteria.Voyage);
                oDq.AddVarcharParam("@Vessel", 100, searchCriteria.Vessel);
                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IBLHeader bl = new BLHeaderEntity(reader);
                    lstBL.Add(bl);
                }

                reader.Close();
            }

            return lstBL;
        }

        public static bool IsDuplicateBL(string LineBLNo, Int64 VesselId, Int64 VoyageId, Int64 BLId)
        {
            string strExecution = "uspIsDuplicateBL";
            string BLNo = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@LineBLNo", 60, LineBLNo);
                oDq.AddBigIntegerParam("@VesselId", VesselId);
                oDq.AddBigIntegerParam("@VoyageId", VoyageId);
                oDq.AddBigIntegerParam("@BLId", BLId);

                BLNo = Convert.ToString(oDq.GetScalar());
            }

            return (BLNo == string.Empty) ? false : true;
        }

        public static bool IsDuplicateContainerNo(string CntrNo)
        {
            string strExecution = "uspIsDuplicateContainerNo";
            string BLNo = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@CntrNo", 60, CntrNo);

                BLNo = Convert.ToString(oDq.GetScalar());
            }

            return (BLNo == string.Empty) ? false : true;
        }

        public static DataTable GetOnHireContainers(string ContainerNo)
        {
            string strExecution = "uspGetOnHireContainers";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@ContainerNo", 11, ContainerNo);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataTable GetContainerFromFooter(string ContainerNo, Int64 VesselId, Int64 VoyageId)
        {
            string strExecution = "uspGetContainerFromFooter";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@ContainerNo", 11, ContainerNo);
                oDq.AddBigIntegerParam("@VesselId", VesselId);
                oDq.AddBigIntegerParam("@VoyageId", VoyageId);
                dt = oDq.GetTable();
            }

            return dt;
        }

        public static void DeleteBLFooter(int FooterId)
        {
            string strExecution = "uspDeleteBLFooter";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@FooterID", FooterId);
                oDq.RunActionQuery();
            }
        }

        public static DataTable GetSurveyor(long LocationId)
        {
            string strExecution = "uspGetSurveyor";
            DataTable dt = new DataTable();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationId", LocationId);

                dt = oDq.GetTable();
            }

            return dt;
        }

        public static DataSet GetBLQuery(string BLNo)
        {
            string strExecution = "[trn].[getBLQuery]";
            DataSet ds = new DataSet();


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BLNo", 60, BLNo);
                ds = oDq.GetTables();
            }

            return ds;
        }

        public static int SaveBLQActivity(string Activity, int BLID)
        {
            string strExecution = "[trn].[ImpBLActivityAddEdit]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@BLID", BLID);
                oDq.AddNVarcharParam("@Activity", 4000, Activity);
                oDq.AddIntegerParam("@Result", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }
            return Result;
        }

        public static int SaveUploadedDocument(Int64 BLId, int DocName, byte[] DocImage, DateTime UploadDate)
        {
            string strExecution = "[trn].[docUpload]";
            int Res = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@fk_BLID", BLId);
                oDq.AddBigIntegerParam("@DocName", DocName);
                oDq.AddImageParam("@DocImg", DocImage);
                oDq.AddDateTimeParam("@UplodedDate", UploadDate);

                Res = oDq.RunActionQuery();
            }
            return Res;
        }

        public static int DeleteUploadedDocument(Int64 DocId)
        {
            string strExecution = "[trn].[DeleteUploadedDoc]";
            int Res = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@DocID", DocId);

                Res = Convert.ToInt32(oDq.GetScalar());
            }
            return Res;
        }
    }
}
