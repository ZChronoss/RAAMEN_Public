using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RAAMEN.Model;
using RAAMEN.Repository;

namespace RAAMEN.View
{
    public partial class Home : System.Web.UI.Page
    {
        UserRepository ur = new UserRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }

            string role = Session["Role"].ToString();
            if(role == "Member")
            {
                Response.Redirect("~/View/OrderRamen.aspx");
            }

            RoleLabel.Text = "Hello " + role + " " + Session["Username"].ToString();

            if(role == "Staff")
            {
                ListNameLbl.Text = "Customers List";

                List<User> custList = ur.getCustomers();
                UserGridView.DataSource = custList;
                UserGridView.DataBind();
            }else if(role == "Admin")
            {
                ListNameLbl.Text = "Staffs List";

                List<User> staffList = ur.getStaffs();
                UserGridView.DataSource = staffList;
                UserGridView.DataBind();
            }
            
        }
    }
}