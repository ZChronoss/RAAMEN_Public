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
    public partial class UpdateRamen : System.Web.UI.Page
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
                MeatDropDown.Items.Insert(0, new ListItem("--Select--", String.Empty));

                string ramenId = Request["ID"];
                Ramen curRamen = rr.GetRamen(Convert.ToInt32(ramenId));

                NameTextBox.Text = curRamen.Name;
                MeatDropDown.SelectedValue = curRamen.Meatid.ToString();
                BrothTextBox.Text = curRamen.Broth;
                PriceTextBox.Text = curRamen.Price;
            }
        }

        private bool nameValid(string name)
        {
            if (name.Length != 0 && name.Contains("Ramen"))
            {
                NameEmpty.Visible = false;
                return true;
            }

            NameEmpty.Text = "Name must contains 'Ramen' and can not be empty";
            NameEmpty.Visible = true;
            return false;
        }

        private bool meatValid(string id)
        {
            if (!id.Equals(String.Empty))
            {
                MeatEmpty.Visible = false;
                return true;
            }

            MeatEmpty.Text = "Meat must be chosen!";
            MeatEmpty.Visible = true;
            return false;
        }

        private bool brothValid(string broth)
        {
            if (broth.Length != 0)
            {
                BrothEmpty.Visible = false;
                return true;
            }

            BrothEmpty.Text = "Broth must be filled!";
            BrothEmpty.Visible = true;
            return false;
        }

        private bool priceValid(String price)
        {
            if (!price.Equals(String.Empty) && Convert.ToInt32(price) >= 3000)
            {
                PriceEmpty.Visible = false;
                return true;
            }

            PriceEmpty.Text = "Price must be at least 3000!";
            PriceEmpty.Visible = true;
            return false;
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            int ramenId = Convert.ToInt32(Request["ID"]);
            string name = NameTextBox.Text;
            string meat = MeatDropDown.SelectedValue;
            string broth = BrothTextBox.Text;
            String price = PriceTextBox.Text;

            if (nameValid(name) && meatValid(meat) && brothValid(broth) && priceValid(price))
            {
                rr.updateRamen(ramenId, Convert.ToInt32(meat), name, broth, Convert.ToInt32(price));
                Response.Redirect("~/View/ManageRamen.aspx");
            }
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ManageRamen.aspx");
        }
    }
}