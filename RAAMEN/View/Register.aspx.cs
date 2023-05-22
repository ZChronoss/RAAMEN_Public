using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using RAAMEN.Repository;

namespace RAAMEN.View
{
    public partial class Register : System.Web.UI.Page
    {
        UserRepository ur = new UserRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Response.Redirect("~/View/Home.aspx");
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
            if (email.EndsWith(".com") && email.Length != 0)
            {
                EmailValid.Visible = false;
                return true;
            }

            EmailValid.Text = "Email must ends with '.com'!";
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

        private bool passwordValid(string password, string confPass)
        {
            if(password == confPass && password.Length != 0)
            {
                PasswordValid.Visible = false;
                return true;
            }

            PasswordValid.Text = "Password and Confirm Password must be the same!";
            PasswordValid.Visible = true;
            return false;
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            String username = UsernameTextBox.Text;
            String email = EmailTextBox.Text;
            String gender = GenderList.SelectedItem.Text;
            String password = PasswordTextBox.Text;
            String confPass = ConfPasswordTextBox.Text;

            if(usernameValid(username) && emailValid(email) && genderValid(gender) && passwordValid(password, confPass))
            {
                bool reg = ur.Register(username, email, gender, password);

                if (!reg)
                {
                    ErrorLabel.Text = "User with the same email already exists!";
                    ErrorLabel.Visible = true;
                }
                else
                {
                    Response.Redirect("~/View/Login.aspx");
                }
            }
        }
    }
}