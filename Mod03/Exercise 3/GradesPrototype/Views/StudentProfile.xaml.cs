using System;
using System.Collections;
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
    /// Interaction logic for StudentProfile.xaml
    /// </summary>
    public partial class StudentProfile : UserControl
    {
        public StudentProfile()
        {
            InitializeComponent();
        }

        #region Event Members
        public event EventHandler Back;
        #endregion

        #region Events
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // If the user is not a teacher, do nothing
            if (SessionContext.UserRole != Role.Teacher)
            {
                return;
            }

            // If the user is a teacher, raise the Back event
            // The MainWindow page has a handler that catches this event and returns to the Students page
            if (Back != null)
            {
                Back(sender, e);
            }
        }
        #endregion

        
        public void Refresh()
        {
            // Exercise 3: Task 4a: Hide or show the back button based on the role of the logged in user.
            if (SessionContext.UserRole == Role.Teacher)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Collapsed;
            }

            // Exercise 3: Task 4a: Display the details for the current student (held in SessionContext.CurrentStudent)
            studentName.DataContext = SessionContext.CurrentStudent;

            // Exercise 3: Task 4d: Create a list of the grades for the student and display this list on the page
            studentGrades.ItemsSource =
                from Grade grade in DataSource.Grades
                where grade.StudentID == SessionContext.CurrentStudent.StudentID
                select grade;
        }
    }
}
