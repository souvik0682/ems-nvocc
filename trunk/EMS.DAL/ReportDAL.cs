using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity.Report;

namespace EMS.DAL
{
    public sealed class ReportDAL
    {
        public static List<ImpBLChkLstEntity> GetImportBLCheckList(int lineId, int locId,Int64 voyageId, Int64 vesselId)
        {
            string strExecution = "[report].[uspGetImportBLCheckList]";
            List<ImpBLChkLstEntity> lstEntity = new List<ImpBLChkLstEntity>();
            ImpBLChkLstEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCId", lineId);
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddBigIntegerParam("@VoyageId", voyageId);
                oDq.AddBigIntegerParam("@VesselId", vesselId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpBLChkLstEntity(reader);
                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }

        public static List<ImpRegisterEntity> GetImportRegister(int lineId, int locId, Int64 voyageId, Int64 vesselId)
        {
            string strExecution = "[report].[uspGetImportRegister]";
            List<ImpRegisterEntity> lstEntity = new List<ImpRegisterEntity>();
            ImpRegisterEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@NVOCCId", lineId);
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddBigIntegerParam("@VoyageId", voyageId);
                oDq.AddBigIntegerParam("@VesselId", vesselId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpRegisterEntity(reader);
                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }
    }
}
