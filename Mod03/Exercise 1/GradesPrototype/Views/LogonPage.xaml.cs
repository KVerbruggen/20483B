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
        // Exercise 1: Task 2a: Define the LogonSuccess event handler
        public event EventHandler LogonSuccess;

        #endregion

        #region Logon Validation

        // Exercise 1: Task 2b: Implement the Logon_Click event handler for the Logon button
        // Simulate logging on (no validation or authentication performed yet)
        private void Logon_Click(object sender, RoutedEventArgs e)
        {
            SessionContext.UserName = username.Text;
            SessionContext.UserRole = userrole.IsChecked.Value ? Role.Teacher : Role.Student;
            if (SessionContext.UserRole == Role.Student)
            {
                SessionContext.CurrentStudent = "Eric Gruber";
            }
            LogonSuccess(this, e);
        }

        #endregion
    }
}
