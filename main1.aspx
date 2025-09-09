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
                <asp:LinkButton ID="db_pro" runat="server" Text="افزودن محصولات" CssClass="btn btn-success" OnClick="db_pro_click"> 
                </asp:LinkButton>
            </asp:Panel>

            <asp:Panel ID="pnlinsert" runat="server" CssClass="content-panel" Visible="false">
                <div style="margin-top: 50px;">
                    <h3>افزودن محصول به جدول دیتابیس</h3>
                    <div style="margin-top: 20px; display:flex;">
                        <div class="col-md-4">
                           
                                <label>نام محصول</label>
                                <asp:TextBox ID="pronameinput" runat="server" CssClass="pro-name-input  form-control"></asp:TextBox>
                                <label>دسته بندی </label>
                                <asp:TextBox ID="procategoryinput" runat="server" CssClass="pro-name-input  form-control"></asp:TextBox>
                                <label>عکس محصول</label>
                              <asp:FileUpload ID="image" runat="server" CssClass="pro-name-input" />
                          
                        </div>
                        <div class="col-md-4">
                            <label>نام برند</label>
                            <asp:TextBox ID="probrandinput" runat="server" CssClass="pro-name-input  form-control"></asp:TextBox>
                            <label>تامین کننده</label>
                            <asp:TextBox ID="prosupplierinput" runat="server" CssClass="pro-name-input  form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <div>

<label>تعداد </label>
<asp:TextBox ID="stock" runat="server" CssClass="pro-name-input  form-control"></asp:TextBox>
<div>
    <label>وضعیت</label>
    <asp:TextBox ID="status" runat="server" CssClass="pro-name-input  form-control"></asp:TextBox>

                        </div>

                                <asp:Button ID="insertb" runat="server" Text="افزودن محصول" CssClass="insertbutt btn btn-success" OnClick="btnInsert_Click" />
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


                        <telerik:RadGrid ID="RadGrid1" OnItemCommand="RadGrid1_ItemCommand" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="5" AllowSorting="true" AllowFilteringByColumn="true">
                            <MasterTableView DataKeyNames="id" NoMasterRecordsText="رکوردی برای نمایش وجود ندارد">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="شناسه" UniqueName="Id" />
                                    <telerik:GridImageColumn DataImageUrlFields="image" HeaderText="تصویر" UniqueName="Image" ImageHeight="50px" ImageWidth="50px" />
                                    <telerik:GridBoundColumn DataField="name" HeaderText="نام کالا" UniqueName="ProductName" />
                                    <telerik:GridBoundColumn DataField="brand" HeaderText="نام برند" UniqueName="Brand" />
                                    <telerik:GridBoundColumn DataField="category" HeaderText="نام دسته بندی" UniqueName="Category" />
                                    <telerik:GridBoundColumn DataField="supplier" HeaderText="تأمین کننده" UniqueName="Supplier" />
                                    <telerik:GridBoundColumn DataField="stock" HeaderText="موجودی" UniqueName="Stock" />
                                    <telerik:GridBoundColumn DataField="status" HeaderText="وضعیت" UniqueName="Status" />
                                    <telerik:GridEditCommandColumn ButtonType="PushButton" EditText="ویرایش" UpdateText="ذخیره" CancelText="انصراف"/>
                                    <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="DeleteColumn" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
            </asp:Panel>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:productsConnectionString %>" SelectCommand="SELECT * FROM proname" InsertCommand="INSERT INTO proname (name, category, brand, supplier, stock, status, image) 
                     VALUES (@name, @category, @brand, @supplier, @stock, @status, @image)">
                <InsertParameters>
                    <asp:Parameter Name="name" Type="String" />
                    
    <asp:Parameter Name="category" Type="String" />
    <asp:Parameter Name="brand" Type="String" />
    <asp:Parameter Name="supplier" Type="String" />
    <asp:Parameter Name="stock" Type="Int32" />
    <asp:Parameter Name="status" Type="String" />
    <asp:Parameter Name="image" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
        </div>

    </form>

</body>
</html>
