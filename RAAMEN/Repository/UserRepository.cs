using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAAMEN.Model;
using RAAMEN.Factory;

namespace RAAMEN.Repository
{
    public class UserRepository
    {
        Database1Entities db = new Database1Entities();
        RoleRepository rp = new RoleRepository();

        public bool Register(string username, string email, string gender, string password)
        {
            User sameUser = db.Users.Where(x => x.Email == email).FirstOrDefault();
            if(sameUser != null)
            {
                return false;
            }

            User newUser = UserFactory.CreateUser(username, email, gender, password, 2);
            db.Users.Add(newUser);
            db.SaveChanges();

            return true;
        }

        public User Login(String username, String password)
        {
            User user = db.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            return user;
        }

        public bool UpdateUser(int userId, string username, string email, string gender)
        {
            User sameUser = db.Users.Where(x => x.Email == email).FirstOrDefault();

            if(sameUser != null && userId != sameUser.Id)
            {
                return false;
            }

            User updUser = getUser(userId);
            updUser.Username = username;
            updUser.Email = email;
            updUser.Gender = gender;

            db.SaveChanges();
            return true;
        }

        public string getRole(int id)
        {
            string role = rp.getRole(id);
            return role;
        }

        public List<User> getCustomers()
        {
            List<User> custs = db.Users.Where(x => x.Roleid == 2).ToList();
            return custs;
        }

        public List<User> getStaffs()
        {
            List<User> staffs = db.Users.Where(x => x.Roleid == 1).ToList();
            return staffs;
        }

        public User getUser(int id)
        {
            User retUser = db.Users.Find(id);
            return retUser;
        }
    }
}