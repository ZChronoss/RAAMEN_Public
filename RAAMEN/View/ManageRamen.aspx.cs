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
    public partial class ManageRamen : System.Web.UI.Page
    {
        RamenRepository rr = new RamenRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }

            if(Session["Role"].ToString() == "Member")
            {
                Response.Redirect("~/View/OrderRamen.aspx");
            }

            ListToGridView();
        }

        private void ListToGridView()
        {
            List<Ramen> ramens = rr.GetRamens();
            RamenGridView.DataSource = ramens;
            RamenGridView.DataBind();
        }

        protected void RamenGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = RamenGridView.Rows[e.RowIndex];
            int id = Convert.ToInt32(row.Cells[0].Text);

            rr.deleteRamen(id);
            ListToGridView();
        }

        protected void RamenGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = RamenGridView.Rows[e.NewEditIndex];
            int id = Convert.ToInt32(row.Cells[0].Text);

            Response.Redirect("~/View/UpdateRamen.aspx?ID=" + id);
        }

        protected void InsertRamen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/InsertRamen.aspx");
        }
    }
}