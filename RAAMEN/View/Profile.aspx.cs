using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using RAAMEN.Model;
using RAAMEN.Repository;

namespace RAAMEN.View
{
    public partial class Profile : System.Web.UI.Page
    {
        UserRepository ur = new UserRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }

            List<String> gender = new List<String>
            {
                "Female",
                "Male"
            };

            if (!IsPostBack)
            {
                int userId = Convert.ToInt32(Session["UserID"].ToString());
                User updUser = ur.getUser(userId);
                UsernameTextBox.Text = updUser.Username;
                EmailTextBox.Text = updUser.Email;
                GenderList.SelectedValue = gender.Where(x => x == updUser.Gender).FirstOrDefault();
            }
        }

        private bool usernameValid(string username)
        {
            if (username.Length >= 5 && username.Length <= 15)
            {
                string trimUname = Regex.Replace(username, @"\s", "");

                if (trimUname.All(Char.IsLetter))
                {
                    UsernameValid.Visible = false;
                    return true;
                    // Kalo format username betul
                }
            }

            UsernameValid.Text = "Username's length must be between 5 and 15 characters and contains alphabet and space only!";
            UsernameValid.Visible = true;
            return false;
            // Kalo format username salah
        }

        private bool emailValid(string email)
        {
            if (email.EndsWith(".com"))
            {
                EmailValid.Visible = false;
                return true;
            }

            EmailValid.Text = "Email must ends with '.com'";
            EmailValid.Visible = true;
            return false;
        }

        private bool genderValid(string gender)
        {
            if (gender.Equals("Male") || gender.Equals("Female"))
            {
                GenderValid.Visible = false;
                return true;
            }

            GenderValid.Text = "Gender must be chosen!";
            GenderValid.Visible = true;
            return false;
        }

        private bool passwordValid(int userId, string pass)
        {          
            User updUser = ur.getUser(userId);  
            string realPass = updUser.Password;
            
            if (pass.Equals(realPass) && pass.Length != 0)
            {
                return true;
            }

            PasswordValid.Text = "Password incorrect!";
            PasswordValid.Visible = true;
            return false;
        }

        protected void UpdateProfBtn_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"].ToString());
            
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string gender = GenderList.SelectedItem.Text;
            string pass = PasswordTextBox.Text;

            if(usernameValid(username) && emailValid(email) && genderValid(gender) && passwordValid(userId, pass))
            {
                bool upd = ur.UpdateUser(userId, username, email, gender);

                if (!upd)
                {
                    ErrorLabel.Text = "User with the same email already exists!";
                    ErrorLabel.Visible = true;
                }
                else
                {
                    Response.Redirect("~/View/Profile.aspx");
                }
            }
        }
    }
}