using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;

namespace RAAMEN.Repository
{
    public class MeatRepository
    {
        Database1Entities db = new Database1Entities();
        public List<Meat> GetMeats()
        {
            return db.Meats.ToList();
        }
    }
}