﻿using BusinessLayer.Services;
using DataAccess.Services;
using Decorators;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ComposeObjects();
            Current.MainWindow.Title = "With Solid and Dependency Injections";
            Current.MainWindow.Show();
        }

        private static void ComposeObjects()
        {
            var employeeDataReader = new EmployeeDataReader();
            var caching = new CachingReader(employeeDataReader);
            var viewModel = new EmployeeReader(employeeDataReader);
            Current.MainWindow = new MainViewWindow(viewModel);
        }
    }
}