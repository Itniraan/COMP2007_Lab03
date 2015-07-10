<%@ Page Title="Contoso University | Add/Edit Course" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="COMP2007_Lab3.course" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Course Details</h1>
    <h5>All fields are required</h5>

    <fieldset>
        <label for="txtTitle" class="col-sm-2">Title: </label>
        <asp:TextBox ID="txtTitle" runat="server" required="true" MaxLength="50" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Course Title is required." ControlToValidate="txtTitle" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
    </fieldset>
    <fieldset>
        <label for="txtCredits" class="col-sm-2">Credits: </label>
        <asp:TextBox ID="txtCredits" runat="server" required="true" MaxLength="50" Type="Number" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Course Credits is required." ControlToValidate="txtCredits" CssClass="label label-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidatorBudget" runat="server" ErrorMessage="Credits must be between 0 and 6" ControlToValidate="txtCredits" MinimumValue="0" MaximumValue="6" CssClass="label label-danger" Display="Dynamic" Type="Integer"></asp:RangeValidator>
    </fieldset>
    <fieldset>
        <label for="ddlDepartment" class="col-sm-2">Department: </label>
        <asp:DropDownList ID="ddlDepartment" runat="server" required="true">

        </asp:DropDownList>
    </fieldset>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <asp:Panel ID="pnlCourses" runat="server" Visible="false">
        <h2>Students</h2>

        <asp:GridView ID="grdStudents" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
            DataKeyNames="EnrollmentID" OnRowDeleting="grdStudents_RowDeleting">
            <Columns>
                <asp:BoundField DataField="StudentID" HeaderText="Student ID" SortExpression="StudentID" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="FirstMidName" HeaderText="First Name" SortExpression="FirstMidName" />
                <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:MM-dd-yyyy}" SortExpression="EnrollmentDate" />
                <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
        <div>
            <asp:DropDownList ID="ddlAddStudent" runat="server">

            </asp:DropDownList>
            <asp:Button ID="btnAdd" runat="server" Text="Add Student" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
            <asp:Label ID="warningLabel" runat="server" CssClass="label label-danger" Text="That student is already enrolled in this course!" Visible="false" />
        </div>
    </asp:Panel>
</asp:Content>
