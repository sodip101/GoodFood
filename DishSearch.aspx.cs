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
    public partial class DishSearch : System.Web.UI.Page
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
            cmd.CommandText = "SELECT d.dish_name,d.local_name,dr.dish_rate,r.name as restaurant FROM dish_rate dr INNER JOIN dishes d ON d.dish_code = dr.dish_code INNER JOIN restaurants r ON r.restaurant_id = dr.restaurant_id";
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
            int dish_code = int.Parse(ddlDish.SelectedValue) ;
            string query = "SELECT d.dish_name,d.local_name,dr.dish_rate,r.name as restaurant FROM dish_rate dr INNER JOIN dishes d ON d.dish_code = dr.dish_code INNER JOIN restaurants r ON r.restaurant_id = dr.restaurant_id where d.dish_code="+dish_code;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = query;

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            ddlDish.SelectedIndex = 0;
        }

        protected void ddlDish_DataBound(object sender, EventArgs e)
        {
            ddlDish.Items.Insert(0, new ListItem("Select Dish"));
        }
    }
}