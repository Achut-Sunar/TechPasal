<%@ Page Title="Manage Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TechPasalWebForms.Admin.AdminProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Products</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block" Visible="false"/>
    <asp:Label ID="lblErr" runat="server" CssClass="alert alert-danger d-block" Visible="false"/>

    <div class="card mb-4">
        <div class="card-header"><asp:Label ID="lblFormTitle" runat="server" Text="Add New Product"/></div>
        <div class="card-body">
            <asp:HiddenField ID="hdnProductId" runat="server" Value="0"/>
            <div class="row">
                <div class="col-md-6 mb-2">
                    <label class="form-label">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"/>
                    <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" ErrorMessage="Name required" CssClass="text-danger small" Display="Dynamic"/>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="form-label">Price</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"/>
                    <asp:RequiredFieldValidator ControlToValidate="txtPrice" runat="server" ErrorMessage="Price required" CssClass="text-danger small" Display="Dynamic"/>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="form-label">Discounted Price</label>
                    <asp:TextBox ID="txtDiscPrice" runat="server" CssClass="form-control" placeholder="Optional"/>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="form-label">Stock</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Text="0"/>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="form-label">Category</label>
                    <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control"/>
                </div>
                <div class="col-md-6 mb-2">
                    <label class="form-label">Image URL</label>
                    <asp:TextBox ID="txtImageUrl" runat="server" CssClass="form-control"/>
                </div>
                <div class="col-md-12 mb-2">
                    <label class="form-label">Description</label>
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"/>
                </div>
                <div class="col-md-12 mb-2">
                    <div class="form-check">
                        <asp:CheckBox ID="chkFeatured" runat="server" CssClass="form-check-input"/>
                        <label class="form-check-label">Featured</label>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save Product" CssClass="btn btn-success" OnClick="btnSave_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary ms-2" OnClick="btnCancel_Click" CausesValidation="false"/>
        </div>
    </div>

    <asp:GridView ID="gvProducts" runat="server" CssClass="table table-sm table-striped" AutoGenerateColumns="false"
        DataKeyNames="ProductId" OnRowCommand="gvProducts_RowCommand">
        <Columns>
            <asp:BoundField DataField="ProductId" HeaderText="#"/>
            <asp:BoundField DataField="Name" HeaderText="Name"/>
            <asp:BoundField DataField="Category" HeaderText="Category"/>
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="Rs. {0:N0}"/>
            <asp:BoundField DataField="Stock" HeaderText="Stock"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button Text="Edit" runat="server" CommandName="EditProduct" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-warning me-1"/>
                    <asp:Button Text="Delete" runat="server" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Delete this product?');"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
