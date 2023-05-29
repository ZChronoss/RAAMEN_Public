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
            User curUser = (User)Session["User"];
            if (Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }


            if (IsPostBack == false)
            {
                UsernameTextBox.Text = curUser.Username;
                EmailTextBox.Text = curUser.Email;
                GenderList.SelectedValue = GenderList.Items.FindByText(curUser.Gender).ToString();
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

        private bool passwordValid(string pass)
        {
            User curUser = (User)Session["User"];
            string realPass = curUser.Password;

            if(pass == realPass)
            {
                PasswordValid.Visible = false;
                return true;
            }

            PasswordValid.Text = "Password incorrect!";
            PasswordValid.Visible = true;
            return false;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"].ToString());
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string gender = GenderList.SelectedItem.Text;
            string pass = PasswordTextBox.Text;

            if (usernameValid(username) && emailValid(email) && genderValid(gender) && passwordValid(pass))
            {
                //ErrorLabel.Text = "Halo";
                //ErrorLabel.Visible = true;
                User sameUser = ur.getUserByEmail(email);
                if (sameUser != null && userId != sameUser.Id)
                {
                    ErrorLabel.Text = "User with the same email already exists!";
                    ErrorLabel.Visible = true;
                }
                else
                {
                    ErrorLabel.Visible = false;
                    ur.UpdateUser(userId, username, email, gender);


                    User curUser = ur.getUser(userId);
                    Session["User"] = curUser;
                    UsernameTextBox.Text = curUser.Username;
                    EmailTextBox.Text = curUser.Email;
                    GenderList.SelectedValue = GenderList.Items.FindByText(curUser.Gender).ToString();
                }
            }
        }
    }
}