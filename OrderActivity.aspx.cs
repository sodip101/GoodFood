using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CW
{
    public partial class OrderActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
                startDate.Visible = false;
                endDate.Visible = false;
            }
        }
        private void BindGrid()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int customer_id = int.Parse(ddlCustomer.SelectedValue);
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            if (DateTime.Parse(startDate)<DateTime.Parse(endDate))
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection(constr);
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select r.name AS Restaurant,COUNT(d.dish_code) AS Dish_Count FROM orders o inner join order_dish od on od.order_number=o.order_number inner join dishes d on d.dish_code=od.dish_code inner join customers c on c.customer_id=o.customer_id inner join restaurants r on r.restaurant_id=od.restaurant where o.customer_id=" + customer_id + " AND o.date_time between '" + startDate + "' AND '" + endDate + "' AND ROWNUM < 6 GROUP BY r.name ORDER BY COUNT(d.dish_code) desc";

                cmd.CommandType = CommandType.Text;


                DataTable dt = new DataTable("OrderActivity");

                using (OracleDataReader odr = cmd.ExecuteReader())
                {
                    dt.Load(odr);
                }

                con.Close();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                ddlCustomer.SelectedIndex = 0;
                txtStartDate.Text = "";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            ddlCustomer.SelectedIndex = 0;
            startDate.Visible = false;
            endDate.Visible = false;
            startDate.SelectedDates.Clear();
            endDate.SelectedDates.Clear();
        }

        protected void startDate_SelectionChanged(object sender, EventArgs e)
        {
            txtStartDate.Text = startDate.SelectedDate.ToString("dd-MMM-yy");
            startDate.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (startDate.Visible)
            {
                startDate.Visible = false;
            }
            else
            {
                startDate.Visible = true;
            }
        }

        protected void ddlCustomer_DataBound(object sender, EventArgs e)
        {
            ddlCustomer.Items.Insert(0, new ListItem("Select Customer"));
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (endDate.Visible)
            {
                endDate.Visible = false;
            }
            else
            {
                endDate.Visible = true;
            }
        }

        protected void endDate_SelectionChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = endDate.SelectedDate.ToString("dd-MMM-yy");
            endDate.Visible = false;
        }

        protected void CustomValidatorStartDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;

            if (DateTime.Parse(startDate).CompareTo(DateTime.Parse(endDate))>0)
            {
                args.IsValid = false;            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}