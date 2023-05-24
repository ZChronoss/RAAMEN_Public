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

        public void Register(string username, string email, string gender, string password)
        {
            User newUser = UserFactory.CreateUser(username, email, gender, password, 2);
            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public User Login(String username, String password)
        {
            User user = db.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            return user;
        }

        public void UpdateUser(int userId, string username, string email, string gender)
        {
            User updUser = getUser(userId);
            updUser.Username = username;
            updUser.Email = email;
            updUser.Gender = gender;

            db.SaveChanges();
        }

        public User getUserByEmail(string email)
        {
            User user = db.Users.Where(x => x.Email == email).FirstOrDefault();
            return user;
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
            User retUser = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return retUser;
        }
    }
}