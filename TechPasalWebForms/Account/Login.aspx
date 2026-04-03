<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TechPasalWebForms.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5">
            <div class="card shadow">
                <div class="card-header bg-dark text-white"><h4 class="mb-0">Login</h4></div>
                <div class="card-body">
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" Visible="false"/>
                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ErrorMessage="Email required" CssClass="text-danger small" Display="Dynamic"/>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" ErrorMessage="Password required" CssClass="text-danger small" Display="Dynamic"/>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click"/>
                    <p class="mt-3 text-center">Don't have an account? <a href="Register.aspx">Register</a></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
