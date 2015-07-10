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
    public partial class editStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If save wasn't clicked AND we have a StudentID in the URL
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetStudent();
            }
        }

        protected void GetStudent()
        {
            // Populate form with existing student record
            Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            using (comp2007Entities db = new comp2007Entities())
            {
                Student s = (from objS in db.Students
                             where objS.StudentID == StudentID
                             select objS).FirstOrDefault();

                if (s != null)
                {
                    txtLastName.Text = s.LastName;
                    txtFirstName.Text = s.FirstMidName;
                    txtEnrollmentDate.Text = s.EnrollmentDate.ToString("yyyy-MM-dd");
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Use EF to connect to SQL Server
            using (comp2007Entities db = new comp2007Entities())
            {
                // Use the Student Model to save the new record
                Student s = new Student();
                Int32 StudentID = 0;

                // Check the QueryString for an ID so we can determine add / update
                if (Request.QueryString["StudentID"] != null)
                {
                    // Get the ID from the URL
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    // Get the current student from the Enity Framework
                    s = (from objS in db.Students
                         where objS.StudentID == StudentID
                         select objS).FirstOrDefault();

                }
                s.LastName = txtLastName.Text;
                s.FirstMidName = txtFirstName.Text;
                s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                // Call add only if we have no student ID
                if (StudentID == 0)
                {
                    db.Students.Add(s);
                }
                db.SaveChanges();

                // Redirect to the updated students page
                Response.Redirect("students.aspx");
            }
        }
    }
}