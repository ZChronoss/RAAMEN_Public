using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;

namespace RAAMEN.Factory
{
    public class TransactionDetailFactory
    {
        public static Detail CreateDetail(int headerId, int ramenId, int qty)
        {
            Detail newDetail = new Detail()
            {
                Headerid = headerId,
                Ramenid = ramenId,
                Quantity = qty
            };

            return newDetail;
        }
    }
}