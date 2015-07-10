<%@ Page Title="Contoso University | Add/Edit Departments" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="COMP2007_Lab3.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Department Details</h1>
    <h5>All fields are required</h5>

    <fieldset>
        <label for="txtName" class="col-sm-2">Name: </label>
        <asp:TextBox ID="txtName" runat="server" required="true" MaxLength="50" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Department Name is required." ControlToValidate="txtName" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </fieldset>
    <fieldset>
        <label for="txtBudget" class="col-sm-2">Budget: </label>
        <asp:TextBox ID="txtBudget" runat="server" required="true" MaxLength="50" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Department Budget is required." ControlToValidate="txtBudget" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidatorBudget" runat="server" ErrorMessage="Budget must be between 0 and 10,000,000" ControlToValidate="txtBudget" MinimumValue="0" MaximumValue="10000000" CssClass="label label-danger" Display="Dynamic"></asp:RangeValidator>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <asp:Panel ID="pnlCourses" runat="server" Visible="false">
        <h2>Courses</h2>

        <asp:GridView ID="grdCourses" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
            OnRowDeleting="grdCourses_RowDeleting" DataKeyNames="CourseID">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Course" />
                <asp:BoundField DataField="Credits" HeaderText="Credits" />
                <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
