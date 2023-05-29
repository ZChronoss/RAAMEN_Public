using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;
using RAAMEN.Factory;

namespace RAAMEN.Repository
{
    public class TransactionDetailRepository
    {
        Database1Entities db = new Database1Entities();

        public void insertDetail(int headerId, int ramenId, int qty)
        {
            Detail newDetail = TransactionDetailFactory.CreateDetail(headerId, ramenId, qty);

            db.Details.Add(newDetail);
            db.SaveChanges();
        }

        public List<Detail> GetDetailsByHeaderId(int headerId)
        {
            List<Detail> details = db.Details.Where(x => x.Headerid == headerId).ToList();
            return details;
        }
    }
}