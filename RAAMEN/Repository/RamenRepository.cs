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
        public void insertRamen(int meatId, string name, string broth, int price)
        {
            Ramen newRamen = RamenFactory.createRamen(meatId, name, broth, price);
            db.Ramen1.Add(newRamen);
            db.SaveChanges();
        }

        public void updateRamen(int ramenId, int meatId, string name, string broth, int price)
        {
            Ramen updRamen = GetRamen(ramenId);

            updRamen.Name = name;
            updRamen.Meatid = meatId;
            updRamen.Broth = broth;
            updRamen.Price = price.ToString();
            db.SaveChanges();
        }

        public void deleteRamen(int id)
        {
            Ramen delRamen = GetRamen(id);

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

        public Ramen GetRamen(int id)
        {
            Ramen retRamen = db.Ramen1.Where(x => x.Id == id).FirstOrDefault();
            return retRamen;
        }
    }
}