using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.Entity;
using EMS.DAL;
using System.Data;

namespace EMS.BLL
{
    public class ImportBLL
    {
        public List<ILine> GetAllLine()
        {
            return ImportBLDAL.GetAllLine();
        }

        public List<ILocation> GetLocation(int UserId)
        {
            return ImportBLDAL.GetLocation(UserId);
        }

        public DataTable GetVoyages(int VesselId)
        {
            return ImportBLDAL.GetVoyages(VesselId);
        }

        public int GetVesselId(string VesseleName)
        {
            return ImportBLDAL.GetVesselId(VesseleName);
        }

        public int GetUnitId(string UnitName)
        {
            return ImportBLDAL.GetUnitId(UnitName);
        }

        public int GetPortId(string PortCode)
        {
            return ImportBLDAL.GetPortId(PortCode);
        }

        public int GetDeliveryToId(string DeliveryTo)
        {
            return ImportBLDAL.GetDeliveryToId(DeliveryTo);
        }

        public DataTable GetCFSName(string CFSCode)
        {
            return ImportBLDAL.GetCFSName(CFSCode);
        }

        public int GetDefaultFreeDays(int NvoccId)
        {
            return ImportBLDAL.GetDefaultFreeDays(NvoccId);
        }

        public int GetPGRFreeDays(int LocationId)
        {
            return ImportBLDAL.GetPGRFreeDays(LocationId);
        }

        public int GetAddressId(string AddrName)
        {
            return ImportBLDAL.GetAddressId(AddrName);
        }

        public string GetAddress(string AddrName)
        {
            return ImportBLDAL.GetAddress(AddrName);
        }

        public DataTable GetContainerType()
        {
            return ImportBLDAL.GetContainerType();
        }

        //public DataTable GetISOCode()
        //{
        //    return ImportBLDAL.GetISOCode();
        //}

        public string GetISOCode(long LocationID, int ContainerSize)
        {
            return ImportBLDAL.GetISOCode(LocationID, ContainerSize);
        }

        public DataTable GetTareWeight(int ContainerTypeID)
        {
            return ImportBLDAL.GetTareWeight(ContainerTypeID);
        }

        public DataTable GetDefaultFreightPayableAt(int LocationId)
        {
            return ImportBLDAL.GetDefaultFreightPayableAt(LocationId);
        }

        public DataTable GetDefaultUnit(string UnitType, string UnitCode)
        {
            return ImportBLDAL.GetDefaultUnit(UnitType, UnitCode);
        }


        public int SaveImportBL(IBLHeader blHeader)
        {
            int blId = 0;
            int blFooterId = 0;

            blId = ImportBLDAL.SaveImportBL(blHeader);

            if (blId > 0)
            {
                if (!ReferenceEquals(blHeader.BLFooter, null))
                {
                    foreach (IBLFooter footer in blHeader.BLFooter)
                    {
                        footer.BLID = blId;
                        blFooterId = ImportBLDAL.SaveImportBLFooter(footer);
                    }
                }
            }

            return blId;
        }

        public IBLHeader GetBLHeaderinformation(long BlId)
        {
            return ImportBLDAL.GetBLHeaderinformation(BlId);
        }

        public List<IBLFooter> GetBLFooterInfo(long BlId)
        {
            return ImportBLDAL.GetBLFooterInfo(BlId);
        }

        public string GetVesselNameById(Int64 VesslId)
        {
            return ImportBLDAL.GetVesselNameById(VesslId);
        }

        public string GetPortNameById(Int64 PortId)
        {
            return ImportBLDAL.GetPortNameById(PortId);
        }

        public string GetCFSCodeById(int AddrId)
        {
            return ImportBLDAL.GetCFSCodeById(AddrId);
        }

        public string GetDeliveryToById(int DPTId)
        {
            return ImportBLDAL.GetDeliveryToById(DPTId);
        }

        public string GetUnitNameById(int UOMId)
        {
            return ImportBLDAL.GetUnitNameById(UOMId);
        }

        public string GetSurveyorNameById(int SurveyorId)
        {
            return ImportBLDAL.GetSurveyorNameById(SurveyorId);
        }

        public List<IBLHeader> GetImportBL(SearchCriteria searchCriteria)
        {
            return ImportBLDAL.GetImportBL(searchCriteria);
        }

        public bool IsDuplicateBL(string LineBLNo, Int64 VesselId, Int64 VoyageId, Int64 BLId)
        {
            return ImportBLDAL.IsDuplicateBL(LineBLNo, VesselId, VoyageId, BLId);
        }

        public bool IsDuplicateContainerNo(string CntrNo)
        {
            return ImportBLDAL.IsDuplicateContainerNo(CntrNo); ;
        }

        public DataTable GetOnHireContainers(string ContainerNo)
        {
            return ImportBLDAL.GetOnHireContainers(ContainerNo);
        }

        public DataTable GetContainerFromFooter(string ContainerNo, Int64 VesselId, Int64 VoyageId)
        {
            return ImportBLDAL.GetContainerFromFooter(ContainerNo, VesselId, VoyageId);
        }

        public void DeleteBLFooter(int FooterId)
        {
            ImportBLDAL.DeleteBLFooter(FooterId);
        }

        public DataTable GetSurveyor(long LocationId)
        {
            return ImportBLDAL.GetSurveyor(LocationId);
        }

        public DataSet GetBLQuery(string ImpBLNo, int ActivityType)
        {
            return ImportBLDAL.GetBLQuery(ImpBLNo,ActivityType);
        }

        public int SaveBLQActivity(string Activity, int BLID)
        {
            return ImportBLDAL.SaveBLQActivity(Activity, BLID);
        }

        public int SaveUploadedDocument(Int64 BLId, int DocName, string DocType, byte[] DocImage, DateTime UploadDate)
        {
            return ImportBLDAL.SaveUploadedDocument(BLId, DocName, DocType, DocImage, UploadDate);
        }

        public int DeleteUploadedDocument(Int64 DocId)
        {
            return ImportBLDAL.DeleteUploadedDocument(DocId);
        }

        public int SaveSubmittedDocument(Int64 BLId, string Param)
        {
            return ImportBLDAL.SaveSubmittedDocument(BLId, Param);
        }

        public DataTable GetUploadedDocByID(int DocID)
        {
            return ImportBLDAL.GetUploadedDocByID(DocID);
        }

        public DataTable GetAllInvoice(Int64 BLId)
        {
            return ImportBLDAL.GetAllInvoice(BLId);
        }
    }
}
