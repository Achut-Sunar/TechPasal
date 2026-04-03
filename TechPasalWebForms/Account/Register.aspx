<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TechPasalWebForms.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5">
            <div class="card shadow">
                <div class="card-header bg-dark text-white"><h4 class="mb-0">Create Account</h4></div>
                <div class="card-body">
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" Visible="false"/>
                    <asp:Label ID="lblSuccess" runat="server" CssClass="text-success d-block mb-2" Visible="false"/>
                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ControlToValidate="txtUsername" runat="server" ErrorMessage="Username required" CssClass="text-danger small" Display="Dynamic"/>
                    </div>
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
                    <div class="mb-3">
                        <label class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirm" runat="server" CssClass="form-control" TextMode="Password"/>
                        <asp:CompareValidator ControlToValidate="txtConfirm" ControlToCompare="txtPassword" runat="server" ErrorMessage="Passwords do not match" CssClass="text-danger small" Display="Dynamic"/>
                    </div>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary w-100" OnClick="btnRegister_Click"/>
                    <p class="mt-3 text-center">Already have an account? <a href="Login.aspx">Login</a></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
