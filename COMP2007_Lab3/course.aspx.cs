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
    public partial class course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If save wasn't clicked AND we have a DepartmentID in the URL
            if ((!IsPostBack))
            {
                using (comp2007Entities db = new comp2007Entities()) {
                    foreach (Department d in db.Departments)
                    {
                        ddlDepartment.Items.Add(new ListItem(d.Name, d.DepartmentID.ToString()));
                    }

                }
                if (Request.QueryString.Count > 0)
                {
                    GetCourse();
                    using (comp2007Entities db = new comp2007Entities())
                    {
                        foreach (Student s in db.Students)
                        {
                            ddlAddStudent.Items.Add(new ListItem(s.LastName + ", " + s.FirstMidName, s.StudentID.ToString()));
                        }
                    }
                }
            }
        }
        protected void GetCourse()
        {
            // Populate form with existing department record
            Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

            using (comp2007Entities db = new comp2007Entities())
            {
                Course c = (from objS in db.Courses
                                where objS.CourseID == CourseID
                                select objS).FirstOrDefault();

                Department selectedItem = (from objD in db.Departments
                                       where c.DepartmentID == objD.DepartmentID
                                       select objD).FirstOrDefault();

                if (c != null)
                {
                    txtTitle.Text = c.Title;
                    txtCredits.Text = Convert.ToString(c.Credits);
                    ddlDepartment.SelectedValue = selectedItem.DepartmentID.ToString();
                }

                //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()               
                var objE = (from en in db.Enrollments
                            join st in db.Students on en.StudentID equals st.StudentID
                            where en.CourseID == CourseID
                            select new { en.EnrollmentID, st.StudentID, st.LastName, st.FirstMidName, st.EnrollmentDate });

                pnlCourses.Visible = true;

                grdStudents.DataSource = objE.ToList();
                grdStudents.DataBind();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Use EF to connect to SQL Server
            using (comp2007Entities db = new comp2007Entities())
            {
                // Use the Department Model to save the new record
                Course c = new Course();
                Int32 CourseID = 0;
                Int32 DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                // Check the QueryString for an ID so we can determine add / update
                if (Request.QueryString["CourseID"] != null)
                {
                    // Get the ID from the URL
                    CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                    // Get the current student from the Enity Framework
                    c = (from objS in db.Courses
                         where objS.CourseID == CourseID
                         select objS).FirstOrDefault();

                }
                c.Title = txtTitle.Text;
                c.Credits = Convert.ToInt32(txtCredits.Text);
                c.DepartmentID = DepartmentID;

                // Call add only if we have no department ID
                if (CourseID == 0)
                {
                    db.Courses.Add(c);
                }
                db.SaveChanges();

                // Redirect to the updated departments page
                Response.Redirect("courses.aspx");
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 EnrollmentID = Convert.ToInt32(grdStudents.DataKeys[e.RowIndex].Values["EnrollmentID"]);

            using (comp2007Entities db = new comp2007Entities())
            {
                Enrollment objE = (from en in db.Enrollments
                                   where en.EnrollmentID == EnrollmentID
                                   select en).FirstOrDefault();

                //Delete
                db.Enrollments.Remove(objE);
                db.SaveChanges();

                //Refresh the data on the page
                GetCourse();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Int32 StudentID = Convert.ToInt32(ddlAddStudent.SelectedValue);
            Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
            Boolean alreadyExists = false;
            Enrollment en = new Enrollment();

            using (comp2007Entities db = new comp2007Entities())
            {
                foreach (Enrollment enroll in db.Enrollments)
                {
                    if (enroll.StudentID == StudentID && enroll.CourseID == CourseID)
                    {
                        alreadyExists = true;
                    }
                }
                if (!alreadyExists)
                {
                    en.StudentID = StudentID;
                    en.CourseID = CourseID;

                    db.Enrollments.Add(en);

                    db.SaveChanges();
                    warningLabel.Visible = false;
                }
                else
                {
                    warningLabel.Visible = true;
                }
            }

            GetCourse();
        }
    }
}