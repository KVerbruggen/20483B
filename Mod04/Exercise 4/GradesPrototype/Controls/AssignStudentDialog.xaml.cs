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
            var unassignedStudents = from student in DataSource.Students
                                     where student.TeacherID == 0
                                     select student;

            // Other option: Lambda-expression
            // var unassignedStudents = DataSource.Students.Where(student => student.TeacherID == 0);
            // Another option: .NET FindAll-method
            // var List<Student> unassignedStudents = DataSource.Students.FindAll(student => student.TeacherID == 0);

            list.ItemsSource = unassignedStudents;

            // if .NET's FindAll is used, the next line needs to be replaced by:
            // if (unassignedStudents.Count == 0)
            if (unassignedStudents.Count() == 0)
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
            Button clickedButton = sender as Button;
            try
            {
                int selectedStudentId = (int)clickedButton.Tag;
                Student selectedStudent = (from student in DataSource.Students
                                           where student.StudentID == selectedStudentId
                                           select student).FirstOrDefault();

                // Other option: Lamdba-expression
                // Student selectedStudent = DataSource.Students.FirstOrDefault(student => student.StudentID == selectedStudentId);
                // Another option: .NET Find-method
                // Student selectedStudent = DataSource.Students.Find(student => student.StudentID == selectedStudentId);

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
