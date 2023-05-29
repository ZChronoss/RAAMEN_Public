using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RAAMEN.Model;
using RAAMEN.Repository;

namespace RAAMEN.View
{
    public partial class OrderQueue : System.Web.UI.Page
    {
        TransactionHeaderRepository thr = new TransactionHeaderRepository();
        UserRepository ur = new UserRepository();
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

            List<Header> headers = thr.GetAllHeaders();

            OrderQueueGridView.DataSource = headers.Select(x => new
            {
                Id = x.Id,
                Staff = ur.getUser(x.Staffid).Username,
                Customer = ur.getUser(x.Customerid).Username,
                Date = x.Date
            });
            OrderQueueGridView.DataBind();
        }

        protected void OrderQueueGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Handle")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = OrderQueueGridView.Rows[index];

                String staff = selectedRow.Cells[1].Text;

                if(staff != "Unhandled")
                {
                    Response.Write("<script>alert('This transaction already handled')</script>");
                }
                else
                {
                    int trId = Convert.ToInt32(selectedRow.Cells[0].Text);

                    Response.Redirect("~/View/OrderQueueDetail.aspx?ID=" + trId);
                }

            }
        }
    }
}