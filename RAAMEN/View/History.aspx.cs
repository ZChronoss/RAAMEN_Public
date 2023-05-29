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
    public partial class History : System.Web.UI.Page
    {
        TransactionHeaderRepository thr = new TransactionHeaderRepository();
        UserRepository ur = new UserRepository();
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

            List<Header> headerList = new List<Header>();

            if(role == "Member")
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                headerList = thr.GetHeadersForCust(userId);

                if(headerList.Count == 0)
                {
                    EmptyLabel.Text = "You haven't done any transaction!";
                    EmptyLabel.Visible = true;
                }
            }

            if(role == "Admin")
            {
                headerList = thr.GetHandledHeader();

                if (headerList.Count == 0)
                {
                    EmptyLabel.Text = "No transaction has been made!";
                    EmptyLabel.Visible = true;
                }
            }

            HistoryGridView.DataSource = headerList.Select(x => new 
            {
                Id = x.Id,
                Staff = ur.getUser(x.Staffid).Username,
                Date = x.Date
            });
            HistoryGridView.DataBind();
        }

        protected void HistoryGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Detail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = HistoryGridView.Rows[index];
                int trId = Convert.ToInt32(selectedRow.Cells[0].Text);

                Response.Redirect("~/View/TransactionDetail.aspx?ID=" + trId);
            }
        }
    }
}