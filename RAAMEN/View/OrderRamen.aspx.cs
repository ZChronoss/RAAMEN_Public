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
    public partial class OrderRamen : System.Web.UI.Page
    {
        RamenRepository rp = new RamenRepository();
        List<Ramen> cart;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }

            if (Session["Role"].ToString() != "Member")
            {
                Response.Redirect("~/View/Home.aspx");
            }

            List<Ramen> ramens = rp.GetRamens();
            RamenGridView.DataSource = ramens;
            RamenGridView.DataBind();
        }

        protected void RamenGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Order")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = RamenGridView.Rows[index];
                string ramenName = selectedRow.Cells[0].Text;

                //OrderLabel.Text = "You just ordered " + ramenName;
            }
        }
    }
}