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
    public partial class OrderQueueDetail : System.Web.UI.Page
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

            if (Session["Role"].ToString() == "Member")
            {
                Response.Redirect("~/View/OrderRamen.aspx");
            }

            int trId = Convert.ToInt32(Request["ID"]);
            List<Detail> details = tdr.GetDetailsByHeaderId(trId);

            Header header = thr.GetHeader(trId);

            TrId.Text = header.Id.ToString();
            Staff.Text = ur.getUser(header.Staffid).Username;
            Customer.Text = ur.getUser(header.Customerid).Username;
            Date.Text = header.Date.ToString();

            TrDetailGridView.DataSource = details.Select(x => new
            {
                Name = rr.GetRamen(x.Ramenid).Name,
                Qty = x.Quantity
            });
            TrDetailGridView.DataBind();
        }
        protected void HandleButton_Click(object sender, EventArgs e)
        {
            int trId = Convert.ToInt32(Request["ID"]);
            int staffId = Convert.ToInt32(Session["UserID"]);
            thr.HandleOrder(trId, staffId);
            Response.Redirect("~/View/OrderQueue.aspx");
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/OrderQueue.aspx");
        }

    }
}