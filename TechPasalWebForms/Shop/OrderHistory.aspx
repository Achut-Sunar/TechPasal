<%@ Page Title="Order History" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="TechPasalWebForms.Shop.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Orders</h2>
    <asp:Label ID="lblPlaced" runat="server" CssClass="alert alert-success d-block" Visible="false"/>
    <asp:GridView ID="gvOrders" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
        OnSelectedIndexChanged="gvOrders_SelectedIndexChanged" DataKeyNames="OrderId">
        <Columns>
            <asp:BoundField DataField="OrderId" HeaderText="Order #"/>
            <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}"/>
            <asp:BoundField DataField="TotalAmount" HeaderText="Total" DataFormatString="Rs. {0:N0}"/>
            <asp:BoundField DataField="Status" HeaderText="Status"/>
            <asp:BoundField DataField="PaymentMethod" HeaderText="Payment"/>
            <asp:BoundField DataField="PaymentStatus" HeaderText="Paid"/>
            <asp:CommandField ShowSelectButton="true" SelectText="Details" ControlStyle-CssClass="btn btn-sm btn-outline-primary"/>
        </Columns>
        <EmptyDataTemplate><p class="text-muted">No orders yet.</p></EmptyDataTemplate>
    </asp:GridView>

    <asp:Panel ID="pnlDetails" runat="server" Visible="false" CssClass="card mt-4">
        <div class="card-header">Order Details</div>
        <div class="card-body">
            <asp:GridView ID="gvDetails" runat="server" CssClass="table" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product"/>
                    <asp:BoundField DataField="Quantity" HeaderText="Qty"/>
                    <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="Rs. {0:N0}"/>
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="Rs. {0:N0}"/>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
