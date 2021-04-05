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
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select c.full_name,od.order_number,d.dish_name,r.name,a.area from order_dish od inner join dishes d on d.dish_code=od.dish_code inner join orders o on o.order_number=od.order_number inner join customers c on c.customer_id=o.customer_id inner join addresses a on a.address_id=o.delivery_point inner join restaurants r on r.restaurant_id=od.restaurant";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("DishSearch");

            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                dt.Load(odr);
            }

            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string order_number = ddlOrder.SelectedValue;
            string customer_id = ddlCustomer.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            string query1 = "select c.full_name,od.order_number,d.dish_name,r.name,a.area from order_dish od inner join dishes d on d.dish_code=od.dish_code inner join orders o on o.order_number=od.order_number inner join customers c on c.customer_id=o.customer_id inner join addresses a on a.address_id=o.delivery_point inner join restaurants r on r.restaurant_id=od.restaurant where o.order_number='" + order_number + "'";
            string query2 = "select c.full_name,od.order_number,d.dish_name,r.name,a.area from order_dish od inner join dishes d on d.dish_code=od.dish_code inner join orders o on o.order_number=od.order_number inner join customers c on c.customer_id=o.customer_id inner join addresses a on a.address_id=o.delivery_point inner join restaurants r on r.restaurant_id=od.restaurant where c.customer_id=" + customer_id;

            if (ddlOrder.SelectedIndex==0)
            {
                cmd.CommandText = query2;
            }
            else
            {
                cmd.CommandText = query1;
            }
            
            DataTable dt = new DataTable("DishSearch");

            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                dt.Load(odr);
            }

            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedIndex == 0)
            {
                ddlOrder.Items.Clear();
            }
            else
            {
                int customer_id = int.Parse(ddlCustomer.SelectedValue);

                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection(constr);
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ORDER_NUMBER FROM ORDERS where customer_id=" + customer_id;
                cmd.CommandType = CommandType.Text;

                ddlOrder.DataSource = cmd.ExecuteReader();
                ddlOrder.DataTextField = "order_number";
                ddlOrder.DataValueField = "order_number";
                ddlOrder.DataBind();
                ddlOrder.Items.Insert(0, new ListItem("Select Order"));   
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            ddlCustomer.SelectedIndex = 0;
            ddlOrder.Items.Clear();       }

        protected void ddlCustomer_DataBound(object sender, EventArgs e)
        {
            ddlCustomer.Items.Insert(0, new ListItem("Select Customer"));
        }
    }
}