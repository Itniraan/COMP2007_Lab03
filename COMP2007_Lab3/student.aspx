<%@ Page Title="Contoso University | Add/Edit Students" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="COMP2007_Lab3.student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Student Details</h1>
    <h5>All fields are required</h5>

    <fieldset>
        <label for="txtLastName" class="col-sm-2">Last Name: </label>
        <asp:TextBox ID="txtLastName" runat="server" required="true" MaxLength="50" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Student Last Name is required." ControlToValidate="txtLastName" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </fieldset>
    <fieldset>
        <label for="txtFirstName" class="col-sm-2">First Name: </label>
        <asp:TextBox ID="txtFirstName" runat="server" required="true" MaxLength="50" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Student First Name is required." ControlToValidate="txtFirstName" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </fieldset>
    <fieldset>
        <label for="txtEnrollmentDate" class="col-sm-2">Enrollment Date: </label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" required="true" TextMode="Date" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Student Enrollment Date is required." ControlToValidate="txtEnrollmentDate" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a Date" ControlToValidate="txtEnrollmentDate" CssClass="alert alert-danger" Type="Date" MinimumValue="2000-01-01" MaximumValue="2999-12-31"></asp:RangeValidator>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <asp:Panel ID="pnlCourses" runat="server" Visible="false">
        <h2>Courses</h2>

        <asp:GridView ID="grdCourses" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
            DataKeyNames="EnrollmentID" OnRowDeleting="grdCourses_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Department" />
                <asp:BoundField DataField="Title" HeaderText="Course" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

</asp:Content>
