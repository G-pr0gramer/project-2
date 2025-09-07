<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main1.aspx.cs" Inherits="sazegar_project.main1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>main</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="style.css" rel="stylesheet" />
</head>
<body dir="rtl">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <form id="form1" runat="server">
        <div class="container">
            <div class="main-content">
                <div class="main-head-content">
                    <asp:LinkButton ID="btnCategories" runat="server" CssClass="head-part-a col-md-4" OnClick="btnCategories_Click">
                            <div class="head-part">
                                دسته بندی ها
                            </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnBrands" runat="server" CssClass="head-part-a col-md-4" OnClick="btnBrands_Click">
                            <div class="head-part">
                                برند ها
                            </div>
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnProducts" runat="server" CssClass="head-part-a col-md-4" OnClick="btnProducts_Click">
                            <div class="head-part">
                                کالا ها
                            </div>
                    </asp:LinkButton>
                </div>
               
                 
                   <asp:ScriptManager ID="ScriptManager1" runat="server" />
            

            </div>
           
             <asp:Panel ID="pnlCategories" runat="server" CssClass="content-panel" Visible="false">
     <h3>مدیریت دسته بندی‌ها</h3>
         <div class="search-container">
        <div class="input-text col-md-6">
            <asp:Label ID="Label1" runat="server" Text="یک عبارت بنویسید:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="input form form-control"></asp:TextBox>
        </div>
        <div class="buttons col-md-6">
            <asp:Button ID="Button1" runat="server" Text=" نمایش همه " CssClass="btn btn-secondary" />
            <asp:Button ID="Button2" runat="server" Text="جتسجو" CssClass="btn btn-primary" />
        </div>
    </div>
 </asp:Panel>
 <asp:Panel ID="pnlBrands" runat="server" CssClass="content-panel" Visible="false">
     <h3>مدیریت  ‌برندها</h3>
      <div class="mid-content">
     <div class="mid-main-content">
         <div class="col-md-4">
             <asp:Button ID="Button3" runat="server" Text="افزودن" CssClass="btn btn-success" />
         </div>
         <div class="col-md-4">
             <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form form-control" />
         </div>
         <div class="col-md-4" style="display: flex; flex-direction: column; align-items: end;">
             <p style="margin: 0;">
                 دریافت اطلاعات کالا
             </p>
             <img src="pic/logo.png" alt="Alternate Text" style="width: 30px; height: 30px;" />
         </div>
     </div>
 </div>
 </asp:Panel>
 <asp:Panel ID="pnlProducts" runat="server" CssClass="content-panel" Visible="false">
     <h3>مدیریت کالاها</h3>
                 <div class="input-section" style="margin-top: 60px;">
                <div class="input-section-content">
                    
                    
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="5" AllowSorting="true" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای نمایش وجود ندارد">
        <Columns>
            <telerik:GridBoundColumn DataField="Id" HeaderText="شناسه" UniqueName="Id" />
            <telerik:GridImageColumn DataImageUrlFields="Image" HeaderText="تصویر" UniqueName="Image" ImageHeight="50px" ImageWidth="50px"/>
            <telerik:GridBoundColumn DataField="ProductName" HeaderText="نام کالا" UniqueName="ProductName" />
            <telerik:GridBoundColumn DataField="Brand" HeaderText="نام برند" UniqueName="Brand" />
            <telerik:GridBoundColumn DataField="Category" HeaderText="نام دسته بندی" UniqueName="Category" />
            <telerik:GridBoundColumn DataField="Supplier" HeaderText="تأمین کننده" UniqueName="Supplier" />
            <telerik:GridBoundColumn DataField="Stock" HeaderText="موجودی" UniqueName="Stock" />
            <telerik:GridBoundColumn DataField="Status" HeaderText="وضعیت" UniqueName="Status" />
            <telerik:GridButtonColumn CommandName="Edit" Text="ویرایش" UniqueName="EditColumn" />
            <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="DeleteColumn" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
 </asp:Panel>
            
        </div>
            
    </form>
   
</body>
</html>
