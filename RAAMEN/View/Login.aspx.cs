using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RAAMEN.Repository;
using RAAMEN.Model;

namespace RAAMEN.View
{
    public partial class Login : System.Web.UI.Page
    {
        UserRepository ur = new UserRepository();
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["User"] != null)
            {
                Response.Redirect("~/View/Home.aspx");
            }
        }

        private void emptyTxtBox(String username, String password)
        {
            if(username.Length == 0)
            {
                UsernameEmpty.Text = "Username must be filled!";
                UsernameEmpty.Visible = true;
            }
            else
            {
                UsernameEmpty.Visible = false;
            }

            if(password.Length == 0)
            {
                PasswordEmpty.Text = "Password must be filled!";
                PasswordEmpty.Visible = true;
            }
            else
            {
                PasswordEmpty.Visible = false;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            String username = UsernameTextBox.Text;
            String password = PasswordTextBox.Text;

            if(username.Length == 0 || password.Length == 0)
            {
                emptyTxtBox(username, password);
                return;
            }

            User user = ur.Login(username, password);
            if(user != null)
            {
                NoUser.Visible = false;

                if (RememberMe.Checked)
                {
                    HttpCookie cookie = new HttpCookie("user_cookie");
                    cookie.Value = user.Id.ToString();
                    cookie.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie);
                }

                string role = ur.getRole(user.Roleid);
                Session["User"] = user;
                Session["Role"] = role;
                Session["Username"] = user.Username;
                Session["UserID"] = user.Id;

                if (role.Equals("Member"))
                {
                    Response.Redirect("~/View/OrderRamen.aspx");
                }
                else
                {
                    Response.Redirect("~/View/Home.aspx");
                }
            }
            else
            {
                NoUser.Text = "User does not exist";
                NoUser.Visible = true;
            }
        }
    }
}