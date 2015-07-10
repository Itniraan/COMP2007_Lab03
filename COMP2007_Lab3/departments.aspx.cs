using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Reference the EF Models
using COMP2007_Lab3.Models;
using System.Web.ModelBinding;

using System.Linq.Dynamic;

namespace COMP2007_Lab3
{
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID";
                Session["SortDirection"] = "ASC";
                // If loading the page for the first time, populate the department grid
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            // Connect to EF
            using (comp2007Entities db = new comp2007Entities())
            {
                String sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                // Query the Departments table, using the Enity Framework
                var Departments = from d in db.Departments
                                  select d;

                // Bind the result to the gridview
                grdDepartments.DataSource = Departments.AsQueryable().OrderBy(sortString).ToList();
                grdDepartments.DataBind();
            }
        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Store which row was clicked
            Int32 selectedRow = e.RowIndex;

            // Get the selected DepartmentID using the grid's Data Key collection
            Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[selectedRow].Values["DepartmentID"]);

            // Use Enity Framework to remove the selected student from the DB
            using (comp2007Entities db = new comp2007Entities())
            {
                // Need to clear out any courses inside of Department, and need to clear out any enrollments inside of each course. Otherwise, 
                // i'll get a foreign key restraint error.
                foreach (Course c in db.Courses)
                {
                    if (c.DepartmentID == DepartmentID)
                    {
                        foreach (Enrollment en in db.Enrollments)
                        {
                            if (en.CourseID == c.CourseID)
                            {
                                db.Enrollments.Remove(en);
                            }
                        }
                        db.Courses.Remove(c);
                    }
                }

                Department d = (from objS in db.Departments
                                where objS.DepartmentID == DepartmentID
                                select objS).FirstOrDefault(); // Using First would get an error if no data comes back, FirstOrDefault won't throw an error

                // Do the delete
                db.Departments.Remove(d);
                db.SaveChanges();
            }

            // Refresh the grid
            GetDepartments();
        }

        protected void grdDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            grdDepartments.PageIndex = e.NewPageIndex;
            GetDepartments();
        }

        protected void grdDepartments_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // Reload the Grid
            GetDepartments();

            // Toggle Direction
            if (Session["SortDirection"].ToString() == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdDepartments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdDepartments.Columns.Count - 1; i++)
                    {
                        if (grdDepartments.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "images/desc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);

                        }
                    }
                }
            }

        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set new page size
            grdDepartments.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetDepartments();
        }

    }
}