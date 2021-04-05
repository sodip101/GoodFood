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
    public partial class Restaurants : System.Web.UI.Page
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
            cmd.CommandText = "SELECT * FROM Restaurants";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("Restaurants");

            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                dt.Load(odr);
            }

            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string restaurantName = (row.Cells[2].Controls[0] as TextBox).Text;
                int restaurantAddress = int.Parse((row.Cells[3].Controls[0] as TextBox).Text);
                string restaurantContact = (row.Cells[4].Controls[0] as TextBox).Text;

                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (OracleConnection con = new OracleConnection(constr))
                {
                    using (OracleCommand cmd = new OracleCommand("update restaurants set name='" + restaurantName + "',contact_number='" + restaurantContact + "',address=" + restaurantAddress + " where restaurant_id=" + ID))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                GridView1.EditIndex = -1;
                this.BindGrid();
            }
            catch (Exception)
            {
                CustomValidatorGrid.IsValid = false;
            }
   
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string restaurantName = txtRestaurantName.Text;
            int restaurantAddress = int.Parse(ddlAddresses.SelectedValue);
            string restaurantContact = txtRestaurantContact.Text;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("INSERT INTO restaurants(name,address,contact_number)VALUES('" + restaurantName + "','" + restaurantAddress + "','" + restaurantContact + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    txtRestaurantName.Text = "";
                    txtRestaurantContact.Text = "";
                    ddlAddresses.SelectedIndex = 0;
                }
            }
            this.BindGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("DELETE FROM restaurants WHERE restaurant_id=" + ID))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this Row?');";
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtRestaurantContact.Text = "";
            txtRestaurantName.Text = "";
            ddlAddresses.SelectedIndex = 0;
        }


        protected void ddlAddresses_DataBound(object sender, EventArgs e)
        {
            ddlAddresses.Items.Insert(0, new ListItem("Select Address"));
        }
    }
}