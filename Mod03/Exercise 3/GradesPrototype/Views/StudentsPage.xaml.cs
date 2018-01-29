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
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : UserControl
    {
        public StudentsPage()
        {
            InitializeComponent();
        }

        #region Display Logic

        // Exercise 3: Task 3a: Display students for the current teacher (held in SessionContext.CurrentTeacher )
        public void Refresh()
        {
            ArrayList students = new ArrayList();
            students.AddRange((
                from Student student in DataSource.Students
                where student.TeacherID == SessionContext.CurrentTeacher.TeacherID
                select student
            ).ToList());
            list.ItemsSource = students;
            txtClass.Text = SessionContext.CurrentTeacher.Class;
        }

        #endregion

        #region Event Members
        public delegate void StudentSelectionHandler(object sender, StudentEventArgs e);
        public event StudentSelectionHandler StudentSelected;        
        #endregion

        #region Event Handlers

        // Exercise 3: Task 3b: If the user clicks on a student, display the details for that student
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            StudentSelected(this, new StudentEventArgs((Student)((Button)sender).DataContext));
        }
        #endregion
    }

    // EventArgs class for passing Student information to an event
    public class StudentEventArgs : EventArgs
    {
        public Student Child { get; set; }

        public StudentEventArgs(Student s)
        {
            Child = s;
        }
    }
}
