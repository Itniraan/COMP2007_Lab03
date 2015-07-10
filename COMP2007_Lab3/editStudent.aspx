<%@ Page Title="Add/Edit Students" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="editStudent.aspx.cs" Inherits="COMP2007_Lab3.editStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Student Details</h1>
    <h5>All fields are required</h5>

    <fieldset>
        <label for="txtLastName" class="col-sm-2">Last Name: </label>
        <asp:TextBox ID="txtLastName" runat="server" required="true" MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtFirstName" class="col-sm-2">First Name: </label>
        <asp:TextBox ID="txtFirstName" runat="server" required="true" MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtEnrollmentDate" class="col-sm-2">Enrollment Date: </label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" required="true" TextMode="Date" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a Date" ControlToValidate="txtEnrollmentDate" CssClass="alert alert-danger" Type="Date" MinimumValue="2000-01-01" MaximumValue="2999-12-31"></asp:RangeValidator>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
</asp:Content>
