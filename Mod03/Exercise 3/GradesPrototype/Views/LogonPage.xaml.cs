using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Views
{
    /// <summary>
    /// Interaction logic for LogonPage.xaml
    /// </summary>
    public partial class LogonPage : UserControl
    {
        public LogonPage()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler LogonSuccess;

        // Exercise 3: Task 1a: Define LogonFailed event
        public event EventHandler LogonFailed;

        #endregion

        #region Logon Validation

        // Exercise 3: Task 1b: Validate the username and password against the Users collection in the MainWindow window
        private void Logon_Click(object sender, RoutedEventArgs e)
        {
            // Try to login as a teacher
            var teachers = TryLogin<Teacher>(username.Text, password.Password, DataSource.Teachers);
            if (teachers.Count() == 1)
            {
                Teacher teacher = teachers.Single();
                SessionContext.UserID = teacher.TeacherID;
                SessionContext.UserRole = Role.Teacher;
                SessionContext.UserName = teacher.UserName;
                SessionContext.CurrentTeacher = teacher;
                LogonSuccess(sender, e);

                // If logged in, return, to not call the LogonFailed-event.
                return;
            }
            else
            {
                // If logging in as a teacher failed, try to login as a student.
                var students = TryLogin<Student>(username.Text, password.Password, DataSource.Students);
                if (students.Count() == 1)
                {
                    Student student = students.Single();
                    SessionContext.UserID = student.StudentID;
                    SessionContext.UserRole = Role.Student;
                    SessionContext.UserName = student.UserName;
                    SessionContext.CurrentStudent = student;
                    LogonSuccess(sender, e);

                    // If logged in, return, to not call the LogonFailed-event.
                    return;
                }
            }
            // If logging in failed, call the LogonFailed-event.
            LogonFailed(this, e);
        }

        // Try to login as a certain type of user, designated by type T.
        private IEnumerable<T> TryLogin<T>(string username, string password, System.Collections.ArrayList source) where T : IPerson
        {
            return (
                from T user in source
                where user.UserName == username && user.Password == password
                select user
                );
        }
        #endregion
    }
}
