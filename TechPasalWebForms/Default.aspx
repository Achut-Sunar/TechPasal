<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechPasalWebForms.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="hero-section bg-dark text-white p-5 rounded mb-4">
        <h1 class="display-4">Welcome to TechPasal</h1>
        <p class="lead">Your one-stop shop for the latest tech products in Nepal.</p>
        <a href="Shop/Products.aspx" class="btn btn-primary btn-lg">Shop Now</a>
    </div>

    <h2 class="mb-3">Featured Products</h2>
    <div class="row" id="featuredProducts">
        <asp:Repeater ID="rptFeatured" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src="<%# Eval("ImageUrl") %>" class="card-img-top" alt="<%# Eval("Name") %>" style="height:200px;object-fit:cover;" onerror="this.src='/Content/images/placeholder.jpg'"/>
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Name") %></h5>
                            <p class="card-text text-muted small"><%# Eval("Description") %></p>
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                    <%# Convert.ToDecimal(Eval("DiscountedPrice") ?? 0m) > 0
                                        ? "<span class=\"text-danger fw-bold\">Rs. " + string.Format("{0:N0}", Eval("DiscountedPrice")) + "</span><span class=\"text-muted text-decoration-line-through ms-1\">Rs. " + string.Format("{0:N0}", Eval("Price")) + "</span>"
                                        : "<span class=\"fw-bold\">Rs. " + string.Format("{0:N0}", Eval("Price")) + "</span>" %>
                                </div>
                                <a href="Shop/ProductDetails.aspx?id=<%# Eval("ProductId") %>" class="btn btn-sm btn-outline-primary">View</a>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
