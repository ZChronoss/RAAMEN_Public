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
    public partial class TransactionDetail : System.Web.UI.Page
    {
        TransactionDetailRepository tdr = new TransactionDetailRepository();
        TransactionHeaderRepository thr = new TransactionHeaderRepository();
        UserRepository ur = new UserRepository();
        RamenRepository rr = new RamenRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/View/Login.aspx");
            }

            string role = Session["Role"].ToString();
            if (role == "Staff")
            {
                Response.Redirect("~/View/Home.aspx");
            }
            
            int trId = Convert.ToInt32(Request["ID"]);
            List<Detail> details = tdr.GetDetailsByHeaderId(trId);

            Header header = thr.GetHeader(trId);

            TrId.Text = header.Id.ToString();
            Staff.Text = ur.getUser(header.Staffid).Username;
            Date.Text = header.Date.ToString();

            if(role == "Admin")
            {
                if(header.Staffid == 9)
                {
                    Response.Redirect("~/View/Home.aspx");
                }

                Customer.Text = ur.getUser(header.Customerid).Username;
                Customer.Visible = true;
                CustomerLabel.Visible = true;
            }

            TrDetailGridView.DataSource = details.Select(x => new
            {
                Name = rr.GetRamen(x.Ramenid).Name,
                Qty = x.Quantity
            });
            TrDetailGridView.DataBind();
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/History.aspx");
        }
    }
}