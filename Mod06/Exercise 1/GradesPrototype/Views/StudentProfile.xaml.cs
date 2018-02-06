using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;
using GradesPrototype.Controls;
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
            // If the user is not a teacher, do nothing (the button should not appear anyway)
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

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            // If the user is not a teacher, do nothing (the button should not appear anyway)
            if (SessionContext.UserRole != Role.Teacher)
            {
                return;
            }

            try
            {
                // If the user is a teacher, ask the user to confirm that this student should be removed from their class
                string message = String.Format("Remove {0} {1}", SessionContext.CurrentStudent.FirstName, SessionContext.CurrentStudent.LastName);
                MessageBoxResult reply = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // If the user confirms, then call the RemoveFromClass method of the current teacher to remove this student from their class 
                if (reply == MessageBoxResult.Yes)
                {
                    SessionContext.CurrentTeacher.RemoveFromClass(SessionContext.CurrentStudent);

                    // Go back to the previous page - the student is no longer a member of the class for the current teacher
                    if (Back != null)
                    {
                        Back(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error removing student from class", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddGrade_Click(object sender, RoutedEventArgs e)
        {
            // If the user is not a teacher, do nothing (the button should not appear anyway)
            if (SessionContext.UserRole != Role.Teacher)
            {
                return;
            }

            try
            {
                // Use the GradeDialog to get the details of the assessment grade
                GradeDialog gd = new GradeDialog();

                // Display the form and get the details of the new grade
                if (gd.ShowDialog().Value)
                {
                    // When the user closes the form, retrieve the details of the assessment grade from the form
                    // and use them to create a new Grade object
                    Grade newGrade = new Grade();
                    newGrade.AssessmentDate = gd.assessmentDate.SelectedDate.Value.ToString("d");
                    newGrade.SubjectName = gd.subject.SelectedValue.ToString();
                    newGrade.Assessment = gd.assessmentGrade.Text;
                    newGrade.Comments = gd.comments.Text;

                    // Save the grade to the list of grades
                    DataSource.Grades.Add(newGrade);

                    // Add the grade to the current student
                    SessionContext.CurrentStudent.AddGrade(newGrade);

                    // Refresh the display so that the new grade appears
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error adding assessment grade", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //  Generate the grades report for the currently selected student
        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "XML documents|*.xml";
                dialog.FileName = "Grades";
                dialog.DefaultExt = ".xml";
                // Exercise 1: Task 1a: Store the return value from the SaveFileDialog in a nullable Boolean variable.
                bool? dialogResult = dialog.ShowDialog();
                
                // Exercise 1: Task 1b: Get the grades for the currently selected student.
                if (dialogResult == true)
                {
                    // Linq query syntax:
                    var grades = (from grade in DataSource.Grades
                                  where grade.StudentID == SessionContext.CurrentStudent.StudentID
                                  select grade).ToList();
                    /*
                      or Linq lambda syntax:
                      var grades = DataSource.Grades.Where(grade => grade.StudentID == SessionContext.CurrentStudent.StudentID).ToList();
                    */

                    // Exercise 1: Task 1c: Serialize the grades to a MemoryStream.
                    MemoryStream stream = FormatAsXMLStream(grades);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Generating Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Utility and Helper Methods

        // Format the list of grades as an XML document and write it to a MemoryStream
        private MemoryStream FormatAsXMLStream(List<Grade> grades)
        {
            // Exercise 1: Task 2a: Save the XML document to a MemoryStream by using an XmlWriter
            MemoryStream stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                // Another solution:
                /*
                    XElement root = new XElement("Grades",
                        new XAttribute("Student", String.Format("{0} {1}", SessionContext.CurrentStudent.FirstName, SessionContext.CurrentStudent.LastName))
                        );
                    foreach (Grade grade in grades)
                    {
                        root.Add(new XElement("Grade",
                            new XAttribute("Date", grade.AssessmentDate),
                            new XAttribute("Subject", grade.SubjectName),
                            new XAttribute("Assessment", grade.Assessment),
                            new XAttribute("Comments", grade.Comments)
                            ));
                    }
                    root.WriteTo(writer);
                */


                // Exercise 1: Task 2b: Create the root node of the XML document.
                // The document root has the format <Grades Student="Eric Gruber">
                writer.WriteStartDocument();
                writer.WriteStartElement("Grades");
                writer.WriteAttributeString("Student", String.Format("{0} {1}", SessionContext.CurrentStudent.FirstName, SessionContext.CurrentStudent.LastName));



                // Exercise 1: Task 2c: Format the grades for the student and add them as child elements of the root node
                // Grade elements have the format <Grade Date="01/01/2012" Subject="Math" Assessment="A-" Comments="Good" />
                foreach (Grade grade in grades)
                {
                    writer.WriteStartElement("Grade");
                    writer.WriteAttributeString("Date", grade.AssessmentDate);
                    writer.WriteAttributeString("Subject", grade.SubjectName);
                    writer.WriteAttributeString("Assessment", grade.Assessment);
                    writer.WriteAttributeString("Comments", grade.Comments);
                    writer.WriteEndElement();
                }

                // Exercise 1: Task 2d: Finish the XML document with the appropriate end elements
                writer.WriteFullEndElement();

                // Exercise 1: Task 2e: Flush the XmlWriter and close it to ensure that all the data is written to the MemoryStream
            }

            // The MemoryStream now contains the formatted data

            // Exercise 1: Task 2f: Reset the MemoryStream so it can be read from the start and then return it
            stream.Seek(0, SeekOrigin.Begin);

            // string s = new StreamReader(stream).ReadToEnd();

            return stream;  
        }
        #endregion

        // Display the details for the current student (held in SessionContext.CurrentStudent), including the grades for the student
        public void Refresh()
        {
            // Bind the studentName StackPanel to display the details of the student in the TextBlocks in this panel
            studentName.DataContext = SessionContext.CurrentStudent;

            // If the current user is a student, hide the Back, Remove, and Add Grade buttons
            // (these features are only applicable to teachers)
            if (SessionContext.UserRole == Role.Student)
            {
                btnBack.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Hidden;
                btnAddGrade.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBack.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Visible;
                btnAddGrade.Visibility = Visibility.Visible;
            }

            // Find all the grades for the student
            List<Grade> grades = new List<Grade>();
            foreach (Grade grade in DataSource.Grades)
            {
                if (grade.StudentID == SessionContext.CurrentStudent.StudentID)
                {
                    grades.Add(grade);
                }
            }
            
            // Display the grades in the studentGrades ItemsControl by using databinding
            studentGrades.ItemsSource = grades;
        }
    }
}
