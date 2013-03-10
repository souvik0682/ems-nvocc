using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Entity;
using EMS.DAL;

namespace EMS.BLL
{
    public class MoneyReceiptsBLL
    {

        public List<MoneyReceiptEntity> GetMoneyReceiptsRearranged(SearchCriteria searchCriteria)
        {
            return MoneyReceiptDAL.GetMoneyReceiptsRearranged(searchCriteria);
        }

        public List<MoneyReceiptEntity> GetMoneyReceipts(SearchCriteria searchCriteria)
        {
            return MoneyReceiptDAL.GetMoneyReceipts(searchCriteria);
        }

        public BLInformations GetBLInformation(string blNo)
        {
            return MoneyReceiptDAL.GetBLInformation(blNo);
        }

        public List<InvoiceTypeEntity> GetInvoiceTypes()
        {
            return MoneyReceiptDAL.GetInvoiceTypes();
        }

        public List<InvoiceDetailsEntity> GetInvoiceDetails(int invoiceTypeId)
        {
            return MoneyReceiptDAL.GetInvoiceDetails(invoiceTypeId);
        }

        public int SaveMoneyReceipts(List<MoneyReceiptEntity> moneyReceipts)
        {
            return MoneyReceiptDAL.SaveMoneyReceipts(moneyReceipts);
        }

        public int SaveMoneyReceipt(MoneyReceiptEntity moneyReceipt)
        {
            return MoneyReceiptDAL.SaveMoneyReceipt(moneyReceipt);
        }

        public void DeleteMoneyReceipts(string mrNo)
        {
            MoneyReceiptDAL.DeleteMoneyReceipts(mrNo);
        }
    }
}
