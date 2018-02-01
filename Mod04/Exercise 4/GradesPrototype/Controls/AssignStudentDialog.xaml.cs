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
using System.Windows.Shapes;
using GradesPrototype.Data;
using GradesPrototype.Services;

namespace GradesPrototype.Controls
{
    /// <summary>
    /// Interaction logic for AssignStudentDialog.xaml
    /// </summary>
    public partial class AssignStudentDialog : Window
    {
        public AssignStudentDialog()
        {
            InitializeComponent();
        }

        // Exercise 4: Task 3b: Refresh the display of unassigned students
        private void Refresh()
        {
            List<Student> unassignedStudents = DataSource.Students.FindAll(student => student.TeacherID == 0);
            list.ItemsSource = unassignedStudents;
            if (unassignedStudents.Count == 0)
            {
                txtMessage.Visibility = Visibility.Visible;
                list.Visibility = Visibility.Collapsed;
            }
            else
            {
                list.Visibility = Visibility.Visible;
                txtMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void AssignStudentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        // Exercise 4: Task 3a: Enroll a student in the teacher's class
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedStudentId = (int)((Button)sender).Tag;
                Student selectedStudent = DataSource.Students.Find(student => student.StudentID == selectedStudentId);
                MessageBoxResult result = MessageBox.Show(String.Format("Are you sure you want to enroll {0} {1} in your class?", selectedStudent.FirstName, selectedStudent.LastName), "Enroll student", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    SessionContext.CurrentTeacher.EnrollInClass(selectedStudent);
                }
                this.Close(); // Added this after testing -- Only 1 student can be added at a time, as the MainWindow doesn't update until this window closes.
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog box
            this.Close();
        }
    }
}
