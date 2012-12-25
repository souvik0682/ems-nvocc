using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Entity;
using EMS.DAL;

namespace EMS.BLL
{
    public class ContainerTranBLL
    {
        public DataTable GetContainerTransactionList(SearchCriteria searchCriteria, int ID)
        {
            return ContainerTranDAL.GetContainerTransactionList(searchCriteria, ID);
        }

        public static DataTable GetContainerTransactionListFiltered(int Status, int LocationId)
        {
            return ContainerTranDAL.GetContainerTransactionListFiltered(Status,LocationId);
        }
    }
}
