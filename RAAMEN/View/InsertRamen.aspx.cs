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
    public partial class InsertRamen : System.Web.UI.Page
    {
        RamenRepository rr = new RamenRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }

            if (Session["Role"].ToString() == "Member")
            {
                Response.Redirect("~/View/OrderRamen.aspx");
            }

            if (!IsPostBack)
            {
                List<Meat> meats = rr.GetMeats();
                MeatDropDown.DataSource = meats;
                MeatDropDown.DataTextField = "Name";
                MeatDropDown.DataValueField = "Id";
                MeatDropDown.DataBind();
            }
            //MeatDropDown.Items.Insert(0, new ListItem("--Select--", String.Empty));
        }

        private bool nameValid(string name)
        {
            if(name.Length != 0 && name.Contains("Ramen"))
            {
                NameEmpty.Visible = false;
                return true;
            }

            NameEmpty.Text = "Name must contains 'Ramen' and can not be empty";
            NameEmpty.Visible = true;
            return false;
        }

        private bool meatValid(int id)
        {
            if(id != 0)
            {
                MeatEmpty.Visible = false;
                return true;
            }

            MeatEmpty.Text = "Meat must be chosen!";
            MeatEmpty.Visible = true;
            return false;
        }

        protected void InsertBtn_Click(object sender, EventArgs e)
        {
            string meat = MeatDropDown.SelectedItem.Text;
            InsertBtn.Text = meat;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ManageRamen.aspx");
        }
    }
}