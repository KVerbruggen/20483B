﻿using System;
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
using GradesPrototype.Services;
using Grades.DataModel;

namespace GradesPrototype.Controls
{
    /// <summary>
    /// Interaction logic for GradeDialog.xaml
    /// </summary>
    public partial class GradeDialog : Window
    {
        public GradeDialog()
        {
            InitializeComponent();
        }

        private void GradeDialog_Loaded(object sender, RoutedEventArgs e)
        {
            // Display the list of available subjects in the subject ListBox
            foreach (Subject subj in SessionContext.DBContext.Subjects)
            {
                subject.Items.Add(subj.Name);
            }
 
             // Set default values for the assessment date and subject
            assessmentDate.SelectedDate = DateTime.Now;
            subject.SelectedValue = subject.Items[0];
        }

        // If the user clicks OK to save the Grade details, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // Create a Grade object and use it to trap and report any data validation exceptions that are thrown
            try
            {
                // Exercise 3: Task 2a: Create a Grade object.
                // Why create a separate object just for validation? It'd make more sense to make these functions static, save the grade info from here, or call these functions from StudentProfile.xaml.cs (where the grade info is actually saved).
                Grade grade = new Grade();

                // Exercise 3: Task 2b: Call the ValidateAssessmentDate method.
                grade.ValidateAssessmentDate(assessmentDate.SelectedDate.Value);

                // Exercise 3: Task 2c: Call the ValidateAssessmentGrade method.
                grade.ValidateAssessmentGrade(assessmentGrade.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error creating assessment", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            

            // Indicate that the data is valid
            this.DialogResult = true;
        }

    }
}
