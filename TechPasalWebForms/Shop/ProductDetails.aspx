<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="TechPasalWebForms.Shop.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlProduct" runat="server">
        <div class="row">
            <div class="col-md-5">
                <img id="imgProduct" runat="server" alt="Product Image" class="img-fluid rounded shadow" style="max-height:400px;object-fit:cover;"/>
            </div>
            <div class="col-md-7">
                <h2><asp:Label ID="lblName" runat="server"/></h2>
                <span class="badge bg-secondary mb-2"><asp:Label ID="lblCategory" runat="server"/></span>
                <p class="text-muted"><asp:Label ID="lblDescription" runat="server"/></p>
                <div class="mb-3">
                    <asp:Label ID="lblPrice" runat="server" CssClass="h4 text-danger"/>
                    <asp:Label ID="lblOriginalPrice" runat="server" CssClass="text-muted text-decoration-line-through ms-2"/>
                </div>
                <p>Stock: <asp:Label ID="lblStock" runat="server"/></p>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-success d-block mb-2" Visible="false"/>
                <div class="d-flex align-items-center gap-2">
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" style="width:80px;" Text="1" TextMode="Number"/>
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary" OnClick="btnAddToCart_Click"/>
                    <a href="Cart.aspx" class="btn btn-outline-secondary">View Cart</a>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNotFound" runat="server" Visible="false">
        <p class="text-danger">Product not found.</p>
    </asp:Panel>
</asp:Content>
