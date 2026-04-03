<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TechPasalWebForms.Shop.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Checkout</h2>
    <div class="row">
        <div class="col-md-7">
            <div class="card mb-3">
                <div class="card-header">Shipping Information</div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label">Full Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtAddress" runat="server" ErrorMessage="Address required" CssClass="text-danger small" Display="Dynamic"/>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Coupon Code (optional)</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtCoupon" runat="server" CssClass="form-control" placeholder="Enter coupon code"/>
                            <asp:Button ID="btnApplyCoupon" runat="server" Text="Apply" CssClass="btn btn-outline-secondary" OnClick="btnApplyCoupon_Click" CausesValidation="false"/>
                        </div>
                        <asp:Label ID="lblCouponMsg" runat="server" CssClass="small" Visible="false"/>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Payment Method</label>
                        <asp:RadioButtonList ID="rblPayment" runat="server" CssClass="list-unstyled">
                            <asp:ListItem Text="&#x1F4B0; Cash on Delivery" Value="CashOnDelivery" Selected="True"/>
                            <asp:ListItem Text="&#x1F3E6; Bank Transfer" Value="BankTransfer"/>
                            <asp:ListItem Text="&#x1F4F1; eSewa" Value="eSewa"/>
                            <asp:ListItem Text="&#x1F49C; Khalti" Value="Khalti"/>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" Visible="false"/>
            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-success w-100 btn-lg" OnClick="btnPlaceOrder_Click"/>
        </div>
        <div class="col-md-5">
            <div class="card">
                <div class="card-header">Order Summary</div>
                <div class="card-body">
                    <asp:Repeater ID="rptItems" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between mb-1">
                                <span><%# Server.HtmlEncode(Eval("ProductName").ToString()) %> x<%# Eval("Quantity") %></span>
                                <span>Rs. <%# string.Format("{0:N0}", Eval("Subtotal")) %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <hr/>
                    <div class="d-flex justify-content-between">
                        <span>Subtotal:</span><span>Rs. <asp:Label ID="lblSubtotal" runat="server"/></span>
                    </div>
                    <div class="d-flex justify-content-between">
                        <span>Discount:</span><span class="text-success">-Rs. <asp:Label ID="lblDiscount" runat="server" Text="0"/></span>
                    </div>
                    <div class="d-flex justify-content-between fw-bold">
                        <span>Total:</span><span class="text-danger">Rs. <asp:Label ID="lblTotal" runat="server"/></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
