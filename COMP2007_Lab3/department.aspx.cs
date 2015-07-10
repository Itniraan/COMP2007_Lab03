using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Reference the EF Models
using COMP2007_Lab3.Models;
using System.Web.ModelBinding;

namespace COMP2007_Lab3
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If save wasn't clicked AND we have a DepartmentID in the URL
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetDepartment();
            }
        }

        protected void GetDepartment()
        {
            // Populate form with existing department record
            Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            using (comp2007Entities db = new comp2007Entities())
            {
                Department d = (from objS in db.Departments
                                where objS.DepartmentID == DepartmentID
                                select objS).FirstOrDefault();

                if (d != null)
                {
                    txtName.Text = d.Name;
                    txtBudget.Text = Convert.ToString(d.Budget);
                }

                //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()               
                var objE = (from en in db.Enrollments
                            join c in db.Courses on en.CourseID equals c.CourseID
                            join dept in db.Departments on c.DepartmentID equals dept.DepartmentID
                            where c.DepartmentID == DepartmentID
                            select new { en.EnrollmentID, c.CourseID, c.Title, c.Credits });

                pnlCourses.Visible = true;

                grdCourses.DataSource = objE.ToList();
                grdCourses.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Use EF to connect to SQL Server
            using (comp2007Entities db = new comp2007Entities())
            {
                // Use the Department Model to save the new record
                Department d = new Department();
                Int32 DepartmentID = 0;

                // Check the QueryString for an ID so we can determine add / update
                if (Request.QueryString["DepartmentID"] != null)
                {
                    // Get the ID from the URL
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    // Get the current student from the Enity Framework
                    d = (from objS in db.Departments
                         where objS.DepartmentID == DepartmentID
                         select objS).FirstOrDefault();

                }
                d.Name = txtName.Text;
                d.Budget = Convert.ToDecimal(txtBudget.Text);

                // Call add only if we have no department ID
                if (DepartmentID == 0)
                {
                    db.Departments.Add(d);
                }
                db.SaveChanges();

                // Redirect to the updated departments page
                Response.Redirect("departments.aspx");
            }

        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 CourseID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["CourseID"]);

            using (comp2007Entities db = new comp2007Entities())
            {
                Enrollment objE = (from en in db.Enrollments
                                   where en.CourseID == CourseID
                                   select en).FirstOrDefault();

                //Delete
                db.Enrollments.Remove(objE);


                //Course objC = (from c in db.Courses
                //                   where c.CourseID == CourseID
                //                   select c).FirstOrDefault();

                //Delete
                //db.Courses.Remove(objC);

                db.SaveChanges();

                //Refresh the data on the page
                GetDepartment();
            }

        }
    }
}