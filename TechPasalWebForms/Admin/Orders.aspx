<%@ Page Title="Manage Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="TechPasalWebForms.Admin.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Orders</h2>
    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success d-block" Visible="false"/>
    <asp:GridView ID="gvOrders" runat="server" CssClass="table table-striped table-sm" AutoGenerateColumns="false"
        DataKeyNames="OrderId" OnRowCommand="gvOrders_RowCommand">
        <Columns>
            <asp:BoundField DataField="OrderId" HeaderText="#"/>
            <asp:BoundField DataField="UserId" HeaderText="User"/>
            <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}"/>
            <asp:BoundField DataField="TotalAmount" HeaderText="Total" DataFormatString="Rs. {0:N0}"/>
            <asp:BoundField DataField="PaymentMethod" HeaderText="Payment"/>
            <asp:BoundField DataField="PaymentStatus" HeaderText="Pay Status"/>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select form-select-sm">
                        <asp:ListItem Value="Pending"/>
                        <asp:ListItem Value="Processing"/>
                        <asp:ListItem Value="Shipped"/>
                        <asp:ListItem Value="Delivered"/>
                        <asp:ListItem Value="Cancelled"/>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button Text="Update" runat="server" CommandName="UpdateStatus" CommandArgument='<%# Eval("OrderId") %>' CssClass="btn btn-sm btn-primary"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
