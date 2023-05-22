using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;

namespace RAAMEN.Factory
{
    public class RamenFactory
    {
        public static Ramen createRamen(int meatId, string name, string broth, int price)
        {
            Ramen newRamen = new Ramen()
            {
                Meatid = meatId,
                Name = name,
                Broth = broth,
                Price = price.ToString()
            };

            return newRamen;
        }
    }
}