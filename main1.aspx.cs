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
                RadGrid1.DataSource = GetSampleData();

                BindGrid();
                switch (ActivePanel)
                {
                    case "Products":
                        pnlProducts.Visible = true;
                        break;
                    case "Categories":
                        pnlCategories.Visible = true;
                        break;
                    case "Brands":
                        pnlBrands.Visible = true;
                        break;
                    case "Insert":
                        pnlinsert.Visible = true;
                        break;
                }
            }
            if (RadGrid1.EditIndexes.Count > 0)
            {
                pnlProducts.Visible = true;
            }
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
                SqlDataSource2.InsertParameters["name"].DefaultValue =pronameinput.Text;
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
            if (e.CommandName == "Edit")
            {
                int id = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("id"));
            }
        }
        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            int id = (int)editedItem.GetDataKeyValue("id");
            string name = (editedItem["name"].Controls[0] as TextBox).Text;
            string brand = (editedItem["brand"].Controls[0] as TextBox).Text;
            string category = (editedItem["category"].Controls[0] as TextBox).Text;
            string supplier = (editedItem["supplier"].Controls[0] as TextBox).Text;
            string stock = (editedItem["stock"].Controls[0] as TextBox).Text;
            string status = (editedItem["status"].Controls[0] as TextBox).Text;
            string imagePath = (editedItem["image"].Controls[0] as TextBox).Text;
            FileUpload fileUpload = editedItem.FindControl("fileUploadEdit") as FileUpload;
            if (fileUpload != null && fileUpload.HasFile)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
                string savePath = Server.MapPath("~/pic/") + fileName;
                fileUpload.SaveAs(savePath);
                imagePath = "pic/" + fileName;
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE proname 
                         SET name=@name, brand=@brand, category=@category, supplier=@supplier, 
                             stock=@stock, status=@status, image=@image
                         WHERE id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@supplier", supplier);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@image", imagePath);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            pnlProducts.Visible = true;
            BindGrid();
            
        }

    }
}