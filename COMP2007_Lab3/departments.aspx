<%@ Page Title="Contoso University | Departments" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="departments.aspx.cs" Inherits="COMP2007_Lab3.departments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Departments</h1>

    <a href="department.aspx">Add Department</a>
    <div>
        <strong>Records Per Page: </strong>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    <asp:GridView ID="grdDepartments" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
        OnRowDeleting="grdDepartments_RowDeleting" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdDepartments_PageIndexChanging" 
        AllowSorting="true" OnSorting="grdDepartments_Sorting" OnRowDataBound="grdDepartments_RowDataBound">
        <Columns>
            <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" SortExpression="DepartmentID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Budget" HeaderText="Budget" DataFormatString="{0:C2}" SortExpression="Budget" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="~/department.aspx" DataNavigateUrlFields="DepartmentID" DataNavigateUrlFormatString="department.aspx?DepartmentID={0}" />
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>