using System;
using System.Windows;

namespace School
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Window
    {
        #region Predefined code

        public StudentForm()
        {
            InitializeComponent();
        }

        #endregion

        // If the user clicks OK to save the Student details, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // Exercise 2: Task 2a: Check that the user has provided a first name
            if (firstName.Text == String.Empty)
            {
                MessageBox.Show("The student must have a last name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Exercise 2: Task 2b: Check that the user has provided a last name
            if (lastName.Text == String.Empty)
            {
                MessageBox.Show("The student must have a last name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Exercise 2: Task 3a: Check that the user has entered a valid date for the date of birth
            if (dateOfBirth.Text == String.Empty)
            {
                MessageBox.Show("The date of birth must be a valid date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Exercise 2: Task 3b: Verify that the student is at least 5 years old
            AgeConverter ageConverter = new AgeConverter();
            int age = 0;
            DateTime dob = Convert.ToDateTime(dateOfBirth.Text);
            try
            {
                age = Convert.ToInt32(ageConverter.Convert(dob, typeof(String), null, System.Globalization.CultureInfo.CurrentCulture));
            }
            catch (Exception ex)
            {
                if (ex is InvalidCastException || ex is InvalidOperationException)
                {
                    MessageBox.Show("The date of birth must be a valid date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (age < 5)
            {
                MessageBox.Show("The student must be at least 5 years old", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Indicate that the data is valid
            this.DialogResult = true;
        }
    }
}
