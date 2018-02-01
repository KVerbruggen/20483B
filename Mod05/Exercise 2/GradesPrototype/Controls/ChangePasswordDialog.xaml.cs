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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        // If the user clicks OK to change the password, validate the information that the user has provided
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // Exercise 2: Task 4a: Get the details of the current user
            User currentUser = null;
            switch (SessionContext.UserRole)
            {
                case Role.Student:
                    currentUser = SessionContext.CurrentStudent;
                    break;
                case Role.Teacher:
                    currentUser = SessionContext.CurrentTeacher;
                    break;
            }
            if (currentUser == null)
            {
                throw new ArgumentException("User not found");
            }

            // Exercise 2: Task 4b: Check that the old password is correct for the current user
            if (!currentUser.VerifyPassword(oldPassword.Password))
            {
                MessageBox.Show("Password incorrect", String.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            // Exercise 2: Task 4c: Check that the new password and confirm password fields are the same
            if (String.Compare(newPassword.Password, confirm.Password) != 0)
            {
                MessageBox.Show("The new password and the confimration value are not the same. Please check both values.", String.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            // Exercise 2: Task 4d: Attempt to change the password
            // If the password is not sufficiently complex, display an error message
            if (!currentUser.SetPassword(newPassword.Password))
            {
                MessageBox.Show("Something went wrong while trying to change the password! Pleas make sure the password complies to all requirements.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            // Indicate that the data is valid
            this.DialogResult = true;
        }
    }
}
