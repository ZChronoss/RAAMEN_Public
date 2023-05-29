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
        TransactionHeaderRepository thr = new TransactionHeaderRepository();
        TransactionDetailRepository tdr = new TransactionDetailRepository();

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

            if(Session["Cart"] == null)
            {
                CartIsEmpty.Visible = true;
            }
            else
            {
                CartIsEmpty.Visible = false;
                List<Tuple<Ramen, int>> ramenCarts = (List<Tuple<Ramen, int>>)Session["Cart"];
                BindCartItem(ramenCarts);                
            }
        }

        protected void RamenGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Order")
            {
                CartIsEmpty.Visible = false;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = RamenGridView.Rows[index];
                string ramenName = selectedRow.Cells[0].Text;

                List<Tuple<Ramen, int>> ramenCarts = (List<Tuple<Ramen, int>>)Session["Cart"];
                Ramen ordRamen = rp.GetRamenByName(ramenName);
                
                if(Session["Cart"] != null)
                {
                    int ramenIdx = ramenCarts.FindIndex(x => x.Item1.Id == ordRamen.Id);
                    if(ramenIdx == -1) // if there is no item with the same id in cart
                    {
                        Tuple<Ramen, int> newCartItem = new Tuple<Ramen, int>(ordRamen, 1);
                        ramenCarts.Add(newCartItem);
                    }
                    else // if there is an item with the same id in cart
                    {
                        int incrementQty = ramenCarts[ramenIdx].Item2 + 1;
                        // update the quantity with a new tuple with the same ramen
                        ramenCarts[ramenIdx] = new Tuple<Ramen, int>(ordRamen, incrementQty);
                    }
                }
                else
                {
                    // initialize new item and put it in the cart

                    Tuple<Ramen, int> newCartItem = new Tuple<Ramen, int>(ordRamen, 1); 
                    ramenCarts = new List<Tuple<Ramen, int>>();
                    ramenCarts.Add(newCartItem);
                }

                BindCartItem(ramenCarts);                                     
                Session["Cart"] = ramenCarts;
            }
        }

        
        protected void CartGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "RemoveOrder")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = CartGridView.Rows[index];
                string ramenName = selectedRow.Cells[0].Text;
                Ramen delRamen = rp.GetRamenByName(ramenName);

                List<Tuple<Ramen, int>> ramenCarts = (List<Tuple<Ramen, int>>)Session["Cart"];
                int ramenIdx = ramenCarts.FindIndex(x => x.Item1.Id == delRamen.Id);
                int qty = ramenCarts[ramenIdx].Item2;

                if(qty > 1)
                {
                    ramenCarts[ramenIdx] = new Tuple<Ramen, int>(delRamen, qty - 1);
                }
                else
                {
                    ramenCarts.RemoveAt(ramenIdx);
                }

                if(ramenCarts.Count == 0)
                {
                    CartIsEmpty.Visible = true;
                }

                BindCartItem(ramenCarts);
                Session["Cart"] = ramenCarts;
            }
        }

        protected void ClearCartBtn_Click(object sender, EventArgs e)
        {
            List<Tuple<Ramen, int>> ramenCarts = (List<Tuple<Ramen, int>>)Session["Cart"];

            ClearCart(ramenCarts);

            Session["Cart"] = ramenCarts;
        }

        protected void BuyCartBtn_Click(object sender, EventArgs e)
        {
            List<Tuple<Ramen, int>> ramenCarts = (List<Tuple<Ramen, int>>)Session["Cart"];

            User cust = (User)Session["User"];
            int custId = cust.Id;

            // staffid '9' means unhandled
            Header newHeader = thr.insertHeader(custId, 9, DateTime.Now);
            foreach(Tuple<Ramen, int> ramen in ramenCarts)
            {
                tdr.insertDetail(newHeader.Id, ramen.Item1.Id, ramen.Item2);
            }

            ClearCart(ramenCarts);
            Session["Cart"] = ramenCarts;
        }

        private void ClearCart(List<Tuple<Ramen, int>> ramenCarts)
        {
            ramenCarts.Clear();
            BindCartItem(ramenCarts);
            CartIsEmpty.Visible = true;
        }

        private void BindCartItem(List<Tuple<Ramen, int>> ramenCarts)
        {
            CartGridView.DataSource = ramenCarts.Select(x => new
            {
                Name = x.Item1.Name,
                Qty = x.Item2
            });
            CartGridView.DataBind();
        }

    }
}