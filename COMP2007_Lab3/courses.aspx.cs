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
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortColumn"] = "CourseID";
                Session["SortDirection"] = "ASC";
                // If loading the page for the first time, populate the department grid
                GetCourses();
            }
        }
        protected void GetCourses()
        {
            // Connect to EF
            using (comp2007Entities db = new comp2007Entities())
            {
                String sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                // Query the Departments table, using the Enity Framework
                var Courses = from c in db.Courses
                              select new { c.CourseID, c.Title, c.Credits, c.Department.Name };

                
                grdCourses.DataSource = Courses.AsQueryable().OrderBy(sortString).ToList();
                grdCourses.DataBind();
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Store which row was clicked
            Int32 selectedRow = e.RowIndex;

            // Get the selected DepartmentID using the grid's Data Key collection
            Int32 CourseID = Convert.ToInt32(grdCourses.DataKeys[selectedRow].Values["CourseID"]);

            // Use Enity Framework to remove the selected student from the DB
            using (comp2007Entities db = new comp2007Entities())
            {
                Course c = (from objS in db.Courses
                                where objS.CourseID == CourseID
                                select objS).FirstOrDefault(); // Using First would get an error if no data comes back, FirstOrDefault won't throw an error

                // Do the delete
                db.Courses.Remove(c);
                db.SaveChanges();
            }

            // Refresh the grid
            GetCourses();

        }

        protected void grdCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            grdCourses.PageIndex = e.NewPageIndex;
            GetCourses();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set new page size
            grdCourses.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetCourses();
        }

        protected void grdCourses_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // Reload the Grid
            GetCourses();

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

        protected void grdCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if (IsPostBack) { 
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();
                
                    for (int i = 0; i <= grdCourses.Columns.Count -1; i++) {
                        if (grdCourses.Columns[i].SortExpression == Session["SortColumn"].ToString())
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
    }
}