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
    public partial class Addresses : System.Web.UI.Page
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
            cmd.CommandText = "SELECT * FROM Addresses";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("Addresses");

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
            string area = txtArea.Text;
            float longtitude = float.Parse(txtLongitude.Text);
            float latitude = float.Parse(txtLatitude.Text);

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("INSERT INTO addresses(area,longitude,latitude)VALUES('" + area + "'," + longtitude + "," + latitude +")"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    txtArea.Text = "";
                    txtLongitude.Text = "";
                    txtLatitude.Text = "";
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
                string area = (row.Cells[2].Controls[0] as TextBox).Text;
                float longtitude = float.Parse((row.Cells[3].Controls[0] as TextBox).Text);
                float latitude = float.Parse((row.Cells[4].Controls[0] as TextBox).Text);


                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (OracleConnection con = new OracleConnection(constr))
                {
                    using (OracleCommand cmd = new OracleCommand("update addresses set area='" + area + "',longitude=" + longtitude + ",latitude=" + latitude + " where address_id=" + ID))
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
                using (OracleCommand cmd = new OracleCommand("DELETE FROM addresses WHERE address_id=" + ID))
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
            txtArea.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
        }
    }
}