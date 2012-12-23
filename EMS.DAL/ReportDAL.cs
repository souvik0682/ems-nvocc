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
        public static List<ImpBLChkLstEntity> GetImportBLCheckList()
        {
            string strExecution = "[report].[uspGetImportBLCheckList]";
            List<ImpBLChkLstEntity> lstEntity = new List<ImpBLChkLstEntity>();
            ImpBLChkLstEntity entity = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    entity = new ImpBLChkLstEntity(reader);
                    lstEntity.Add(entity);
                }
            }


            return lstEntity;
        }
    }
}
