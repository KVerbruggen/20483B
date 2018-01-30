using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    // Types of user
    public enum Role { Teacher, Student };

    // WPF Databinding requires properties

    // Exercise 1: Task 1a: Convert Grade into a class and define constructors
    public class Grade
    {
        public int StudentID { get; set; }
        public string AssessmentDate { get; set; }
        public string SubjectName { get; set; }
        public string Assessment { get; set; }
        public string Comments { get; set; }

        public Grade(int studentID, string assessmentDate, string subject, string assessment, string comments)
        {
            // It's not required to use the 'this' keyword here because of the capitalisation, but I prefer it, because of readability.
            this.StudentID = studentID;
            this.AssessmentDate = assessmentDate;
            this.SubjectName = subject;
            this.Assessment = assessment;
            this.Comments = comments;
        }

        public Grade()
        {
            this.StudentID = 0;
            this.AssessmentDate = DateTime.Now.ToString();
            this.SubjectName = "Math";
            this.Comments = String.Empty;
        }

        /*
        * Andere mogelijkheid - korter, maar minder leesbaar
        public Grade() : this(0, DateTime.Now.ToString(), "Math", "A", String.Empty)
        {
        }
        */
    }

    // Exercise 1: Task 2a: Convert Student into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Student
    {
        // The Guid.NewGuid() method creates a new 128-bit int id.
        private string password = Guid.NewGuid().ToString();

        public int StudentID { get; set; }
        public string UserName { get; set; }
        // Properties without get- or set-accessor can't be auto-initialised.
        public string Password
        {
            set { password = value; }
        }
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentID, string userName, string password, string firstName, string lastName, int teacherID)
        {
            this.StudentID = studentID;
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.TeacherID = teacherID;
        }

        public Student()
        {
            this.StudentID = 0;
            this.UserName = String.Empty;
            this.Password = String.Empty;
            this.FirstName = String.Empty;
            this.LastName = String.Empty;
            this.TeacherID = 0;
        }

        /*
        * Andere mogelijkheid - korter, maar minder leesbaar
        public Student() : this(0, String.Empty, String.Empty, String.Empty, String.Empty, 0)
        {
        }
        */

        public bool VerifyPassword(string password)
        {
            return (String.Compare(password, this.password) == 0);
        }
    }

    // TODO: Exercise 1: Task 2b: Convert Teacher into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Teacher
    {
        // The Guid.NewGuid() method creates a new 128-bit int id.
        private string password = Guid.NewGuid().ToString();

        public int TeacherID { get; set; }
        public string UserName { get; set; }
        // Properties without get- or set-accessor can't be auto-initialised.
        public string Password
        {
            set { password = value; }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        public Teacher(int teacherID, string userName, string password, string firstName, string lastName, string className)
        {
            this.TeacherID = teacherID;
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Class = className;
        }

        public Teacher()
        {
            this.TeacherID = 0;
            this.UserName = String.Empty;
            this.Password = String.Empty;
            this.FirstName = String.Empty;
            this.LastName = String.Empty;
            this.Class = String.Empty;
        }

        /*
        * Andere mogelijkheid - korter, maar minder leesbaar
        public Teacher() : this(0, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }
        */

        public bool VerifyPassword(string password)
        {
            return (String.Compare(password, this.password) == 0);
        }
    }
}
