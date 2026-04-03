<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TechPasalWebForms.Shop.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Shop Products</h2>
    <div class="row mb-3">
        <div class="col-md-5">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search products..."/>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select">
                <asp:ListItem Text="All Categories" Value=""/>
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click"/>
        </div>
    </div>
    <div class="row">
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src="<%# Eval("ImageUrl") %>" class="card-img-top" alt="<%# Eval("Name") %>" style="height:200px;object-fit:cover;" onerror="this.src='/Content/images/placeholder.jpg'"/>
                        <div class="card-body d-flex flex-column">
                            <h6 class="card-title"><%# Eval("Name") %></h6>
                            <span class="badge bg-secondary mb-1"><%# Eval("Category") %></span>
                            <div class="mt-auto">
                                <%# Convert.ToDecimal(Eval("DiscountedPrice") ?? 0m) > 0
                                    ? "<span class=\"text-danger fw-bold\">Rs. " + string.Format("{0:N0}", Eval("DiscountedPrice")) + "</span><span class=\"text-muted text-decoration-line-through ms-1\">Rs. " + string.Format("{0:N0}", Eval("Price")) + "</span>"
                                    : "<span class=\"fw-bold\">Rs. " + string.Format("{0:N0}", Eval("Price")) + "</span>" %>
                                <div class="mt-2">
                                    <a href="ProductDetails.aspx?id=<%# Eval("ProductId") %>" class="btn btn-sm btn-outline-primary w-100">View Details</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="lblNoProducts" runat="server" Text="No products found." CssClass="text-muted" Visible="false"/>
    </div>
</asp:Content>
