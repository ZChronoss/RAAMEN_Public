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

        public void HandleOrder(int trId, int staffId)
        {
            Header header = db.Headers.Where(x => x.Id == trId).FirstOrDefault();

            header.Staffid = staffId;
            db.SaveChanges();
        }

        public Header GetHeader(int id)
        {
            Header header = db.Headers.Where(x => x.Id == id).FirstOrDefault();
            return header;
        }

        public List<Header> GetHandledHeader()
        {
            List<Header> headers = db.Headers.Where(x => x.Staffid != 9).ToList();
            return headers;
        }

        public List<Header> GetHeadersForCust(int custId)
        {
            List<Header> headerList = db.Headers.Where(x => x.Customerid == custId).ToList();
            return headerList;
        }

        public List<Header> GetAllHeaders()
        {
            List<Header> headerList = db.Headers.ToList();
            return headerList;
        }
    }
}