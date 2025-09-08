using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sazegar_project
{
    public partial class main1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadGrid1.DataSource = GetSampleData();
                RadGrid1.DataBind();
            }
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            SqlDataSource2.InsertParameters["name"].DefaultValue = textbox6.Text;
            SqlDataSource2.Insert();
        }
        private DataTable GetSampleData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Image");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Category");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Stock");
            dt.Columns.Add("Status");

            dt.Rows.Add(1, "pic/logo.png", "کالا 1", "برند 1", "دسته 1", "تأمین کننده 1", 10, "فعال");
            dt.Rows.Add(2, "pic/logo.png", "کالا 2", "برند 2", "دسته 2", "تأمین کننده 2", 5, "غیرفعال");

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
    }
}