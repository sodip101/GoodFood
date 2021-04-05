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
    public partial class LPoints : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
                lpDate.Visible = false;
            }
        }

        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM loyalty_points";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("Loyalty_Points");

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
            int dishCode = int.Parse(ddlDishes.SelectedValue);
            string date = txtLPDate.Text;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("INSERT INTO loyalty_points(dish_code,loyalty_points_date)VALUES(" + dishCode + ",'" + date + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    txtLPDate.Text = "";
                    ddlDishes.SelectedIndex = 0;
                }
            }
            this.BindGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                int dishCode = int.Parse((row.Cells[2].Controls[0] as TextBox).Text);
                string date = DateTime.Parse((row.Cells[3].Controls[0] as TextBox).Text).ToString("dd-MMM-yy");


                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (OracleConnection con = new OracleConnection(constr))
                {
                    using (OracleCommand cmd = new OracleCommand("update loyalty_points set dish_code=" + dishCode + ",loyalty_points_date='" + date + "' where dish_date_id=" + ID))
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
                using (OracleCommand cmd = new OracleCommand("DELETE FROM loyalty_points WHERE dish_date_id=" + ID))
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

        protected void lpDate_SelectionChanged(object sender, EventArgs e)
        {
            txtLPDate.Text = lpDate.SelectedDate.ToString("dd-MMM-yy");
            lpDate.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtLPDate.Text = "";
            ddlDishes.SelectedIndex = 0;
            lpDate.SelectedDates.Clear();
            lpDate.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (lpDate.Visible)
            {
                lpDate.Visible = false;
            }
            else
            {
                lpDate.Visible = true;
            }
        }

        protected void ddlDishes_DataBound(object sender, EventArgs e)
        {
            ddlDishes.Items.Insert(0, new ListItem("Select Dish"));
        }
    }
}