using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;

namespace RAAMEN.Repository
{
    public class RoleRepository
    {
        Database1Entities db = new Database1Entities();

        public string getRole(int id)
        {
            Role role = db.Roles.Where(x => x.Id == id).FirstOrDefault();
            return role.name;
        }
        
    }
}