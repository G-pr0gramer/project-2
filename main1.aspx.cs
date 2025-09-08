using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace sazegar_project
{
    public partial class main1 : System.Web.UI.Page
    {
        string connStr = @"Data Source=.;Initial Catalog=products;Integrated Security=True;Encrypt=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadGrid1.DataSource = GetSampleData();
                RadGrid1.DataBind();
                BindGrid();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            SqlDataSource2.InsertParameters["name"].DefaultValue =pronameinput.Text;
            SqlDataSource2.InsertParameters["category"].DefaultValue = procategoryinput.Text;
            SqlDataSource2.InsertParameters["brand"].DefaultValue = probrandinput.Text;
            SqlDataSource2.InsertParameters["supplier"].DefaultValue = prosupplierinput.Text;
            SqlDataSource2.InsertParameters["stock"].DefaultValue = stock.Text;
            SqlDataSource2.InsertParameters["status"].DefaultValue = status.Text;
            SqlDataSource2.Insert();
        }
        private DataTable GetSampleData()
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM proname", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); // dt الان از دیتابیس پر می‌شود
            }

       
            foreach (DataRow row in dt.Rows)
            {
                string name = row["name"].ToString();
                Console.WriteLine(name); // چاپ نام هر کالا
            }
            return dt;
        }
        protected void btnCategories_Click(object sender, EventArgs e)
        {
            pnlCategories.Visible = true;
            pnlBrands.Visible = false;
            pnlProducts.Visible = false;
            pnlinsert.Visible = false;
        }

        protected void btnBrands_Click(object sender, EventArgs e)
        {
            pnlCategories.Visible = false;
            pnlBrands.Visible = true;
            pnlProducts.Visible = false;
            pnlinsert.Visible = false;
        }
        protected void db_pro_click(object sender, EventArgs e)
        {
            pnlCategories.Visible = false;
            pnlBrands.Visible = false;
            pnlProducts.Visible = false;
            pnlinsert.Visible = true;
        }

        protected void btnProducts_Click(object sender, EventArgs e)
        {
            pnlCategories.Visible = false;
            pnlBrands.Visible = false;
            pnlProducts.Visible = true;
            pnlinsert.Visible = false;
        }
        private void BindGrid()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM proname", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
            }
        }
        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("Id"));
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM proname WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                BindGrid();
            }
            if (e.CommandName == "Edit")
            {
                int id = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("id"));
            }
        }
            
    }
}