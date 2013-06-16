using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace Collection
{
    public partial class SettingPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings _appSettings;
        public SettingPage()
        {
            InitializeComponent();
            _appSettings = IsolatedStorageSettings.ApplicationSettings;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Dataclass.status == "ok")
            {
                setlocationButton.Content = "off";
                 

            }
            if (Dataclass.status == "cancel")
            {
                setlocationButton.Content = "on";

                
            }
        }

        private void setlocationButton_Click(object sender, RoutedEventArgs e)
        {
            bool b = false;
            if (setlocationButton.Content.ToString() == "off")
            {
                var result = MessageBox.Show("Turn location service off successfully!");
                if (result == MessageBoxResult.OK)
                {

                    setlocationButton.Content = "on";
                    Dataclass.status = "cancel";
                    b = true;
                }
                if (_appSettings.Contains("status"))
                {
                    _appSettings["status"] = null;
                    _appSettings.Save();
                }
            }
            if (setlocationButton.Content.ToString() == "on" && b == false) 
            {
                var result = MessageBox.Show("Turn location service on successfully!");
                if (result == MessageBoxResult.OK)
                {
                    setlocationButton.Content = "off";
                    Dataclass.status = "ok";
                }
                if (_appSettings.Contains("status"))
                {
                    _appSettings["status"] = "ok";
                    _appSettings.Save();
                }
            }
        }
    }
}