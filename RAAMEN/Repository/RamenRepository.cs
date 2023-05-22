using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;
using RAAMEN.Factory;
using RAAMEN.Repository;

namespace RAAMEN.Repository
{
    public class RamenRepository
    {
        Database1Entities db = new Database1Entities();
        MeatRepository mr = new MeatRepository();
        public void insertRamen(string meatName, string name, string broth, int price)
        {
            Meat meat = db.Meats.Where(x => x.Name == meatName).FirstOrDefault();

            Ramen newRamen = RamenFactory.createRamen(meat.Id, name, broth, price);
            db.Ramen1.Add(newRamen);
            db.SaveChanges();
        }

        public void deleteRamen(int id)
        {
            Ramen delRamen = db.Ramen1.Find(id);

            db.Ramen1.Remove(delRamen);
            db.SaveChanges();
        }

        public List<Ramen> GetRamens()
        {
            List<Ramen> ramens = db.Ramen1.ToList();
            return ramens;
        }

        public List<Meat> GetMeats()
        {
            List<Meat> meats = mr.GetMeats();
            return meats;
        }
    }
}