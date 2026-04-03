<%@ Page Title="Shopping Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="TechPasalWebForms.Shop.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Shopping Cart</h2>
    <asp:Panel ID="pnlCart" runat="server">
        <div class="table-responsive">
            <table class="table align-middle">
                <thead class="table-dark">
                    <tr>
                        <th>Product</th><th>Price</th><th>Qty</th><th>Subtotal</th><th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img src="<%# Eval("ImageUrl") %>" style="width:50px;height:50px;object-fit:cover;" class="me-2" onerror="this.src='/Content/images/placeholder.jpg'"/>
                                    <%# Server.HtmlEncode(Eval("ProductName").ToString()) %>
                                </td>
                                <td>Rs. <%# string.Format("{0:N0}", Eval("Price")) %></td>
                                <td style="width:120px;">
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Quantity") %>' CssClass="form-control form-control-sm" style="width:70px;" TextMode="Number"/>
                                </td>
                                <td>Rs. <%# string.Format("{0:N0}", Eval("Subtotal")) %></td>
                                <td>
                                    <asp:Button Text="Update" runat="server" CommandName="Update" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-secondary me-1"/>
                                    <asp:Button Text="Remove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-danger"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="3" class="text-end fw-bold">Total:</td>
                        <td colspan="2" class="fw-bold text-danger">Rs. <asp:Label ID="lblTotal" runat="server"/></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <div class="text-end">
            <a href="Products.aspx" class="btn btn-outline-secondary me-2">Continue Shopping</a>
            <a href="Checkout.aspx" class="btn btn-primary">Proceed to Checkout</a>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
        <div class="text-center py-5">
            <h4 class="text-muted">Your cart is empty</h4>
            <a href="Products.aspx" class="btn btn-primary mt-2">Start Shopping</a>
        </div>
    </asp:Panel>
</asp:Content>
