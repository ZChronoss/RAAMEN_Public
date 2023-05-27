using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Factory;
using RAAMEN.Model;

namespace RAAMEN.Repository
{
    public class TransactionHeaderRepository
    {
        Database1Entities db = new Database1Entities();
        public Header insertHeader(int custId, int staffId, DateTime date)
        {
            Header newHeader = TransactionHeaderFactory.CreateHeader(custId, staffId, date);

            db.Headers.Add(newHeader);
            db.SaveChanges();

            return newHeader;
        }
    }
}