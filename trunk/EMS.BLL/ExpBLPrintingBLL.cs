﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using System.Data.SqlClient;
using EMS.Entity;

namespace EMS.BLL
{
    public static class  ExpBLPrintingBLL
    {
        public static BLPrintModel GetxpBLPrinting(ISearchCriteria searchCriteria)
        {
            return EMS.DAL.ExpBLPrintingDAL.GetxpBLPrinting(searchCriteria);
        }
    }
}
