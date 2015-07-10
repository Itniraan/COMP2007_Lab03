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
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortColumn"] = "StudentID";
                Session["SortDirection"] = "ASC";
                // If loading the page for the first time, populate the student grid
                GetStudents();
            }
        }

        protected void GetStudents()
        {
            // Connect to EF
            using (comp2007Entities db = new comp2007Entities())
            {
                String sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                // Query the Students table, using the Enity Framework
                var Students = from s in db.Students
                               select s;

                // Bind the result to the gridview
                grdStudents.DataSource = Students.AsQueryable().OrderBy(sortString).ToList();
                grdStudents.DataBind();
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Store which row was clicked
            Int32 selectedRow = e.RowIndex;

            // Get the selected StudentID using the grid's Data Key collection
            Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[selectedRow].Values["StudentID"]);

            // Use Enity Framework to remove the selected student from the DB
            using (comp2007Entities db = new comp2007Entities())
            {
                Student s = (from objS in db.Students
                             where objS.StudentID == StudentID
                             select objS).FirstOrDefault(); // Using First would get an error if no data comes back, FirstOrDefault won't throw an error

                // Do the delete
                db.Students.Remove(s);
                db.SaveChanges();
            }

            // Refresh the grid
            GetStudents();
        }

        protected void grdStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            grdStudents.PageIndex = e.NewPageIndex;
            GetStudents();
        }

        protected void grdStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // Reload the Grid
            GetStudents();

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

        protected void grdStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdStudents.Columns.Count - 1; i++)
                    {
                        if (grdStudents.Columns[i].SortExpression == Session["SortColumn"].ToString())
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
            grdStudents.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetStudents();
        }
    }
}