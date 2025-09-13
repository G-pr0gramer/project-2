using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                BindGrid();
                ShowActivePanel();
            }
        }


        private void ShowActivePanel()
        {
            pnlCategories.Visible = ActivePanel == "Categories";
            pnlBrands.Visible = ActivePanel == "Brands";
            pnlProducts.Visible = ActivePanel == "Products";
            pnlinsert.Visible = ActivePanel == "Insert";
        
        }
        private string ActivePanel
        {
            get { return ViewState["ActivePanel"] as string ?? ""; }
            set { ViewState["ActivePanel"] = value; }
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string imagepath = "";


            if (image.HasFile)
            {
                // گرفتن نام فایل
                string fileName = Path.GetFileName(image.FileName);

                // مسیر ذخیره در سرور
                string savePath = Server.MapPath("~/pic/") + fileName;

                // ذخیره فایل
                image.SaveAs(savePath);

                // مسیر نسبی برای ذخیره در دیتابیس
                imagepath = "pic/" + fileName;
            }
            SqlDataSource2.InsertParameters["name"].DefaultValue = pronameinput.Text;
            SqlDataSource2.InsertParameters["category"].DefaultValue = procategoryinput.Text;
            SqlDataSource2.InsertParameters["brand"].DefaultValue = probrandinput.Text;
            SqlDataSource2.InsertParameters["supplier"].DefaultValue = prosupplierinput.Text;
            SqlDataSource2.InsertParameters["stock"].DefaultValue = stock.Text;
            SqlDataSource2.InsertParameters["status"].DefaultValue = status.Text;
            SqlDataSource2.InsertParameters["image"].DefaultValue = imagepath;

            SqlDataSource2.Insert();
            BindGrid();
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
            ActivePanel = "Categories";
        }

        protected void btnBrands_Click(object sender, EventArgs e)
        {
            pnlCategories.Visible = false;
            pnlBrands.Visible = true;
            pnlProducts.Visible = false;
            pnlinsert.Visible = false;
            ActivePanel = "brand";
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
            ActivePanel = "Products";
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
                int id = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("id"));
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM proname WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                BindGrid();
            }
            if (e.CommandName == "CustomEdit")
            {
                int id = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("id"));
                DataTable dt = GetSampleData();
                DataRow row = dt.Select("id=" + id).FirstOrDefault();

                if (row != null)
                {
                    hfEditId.Value = id.ToString();
                    txtEditName.Text = row["name"].ToString();
                    txtEditCategory.Text = row["category"].ToString();
                    txtEditBrand.Text = row["brand"].ToString();
                    txtEditSupplier.Text = row["supplier"].ToString();
                    txtEditStock.Text = row["stock"].ToString();
                    txtEditStatus.Text = row["status"].ToString();

                    pnlEdit.Visible = true;
                }
            }
        }
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfEditId.Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE proname 
             SET name=@name, category=@category, brand=@brand, supplier=@supplier,
                 stock=@stock, status=@status
             WHERE id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", txtEditName.Text);
                cmd.Parameters.AddWithValue("@category", txtEditCategory.Text);
                cmd.Parameters.AddWithValue("@brand", txtEditBrand.Text);
                cmd.Parameters.AddWithValue("@supplier", txtEditSupplier.Text);
                cmd.Parameters.AddWithValue("@stock", txtEditStock.Text);
                cmd.Parameters.AddWithValue("@status", txtEditStatus.Text);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlEdit.Visible = false;
           
            BindGrid(); 
        }
    }
}