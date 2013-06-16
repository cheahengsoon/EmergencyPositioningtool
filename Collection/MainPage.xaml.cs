using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Resources;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using Microsoft.Phone.Tasks;
using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.Overlays;
using Com.AMap.Maps.Api.Events;
using Com.AMap.Maps.Api.Layers;
using System.Collections.ObjectModel;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Enums;
using System.Diagnostics;
using System.Text;
using Microsoft.Phone.Shell;
using Com.AMap.Search.API;
using Com.AMap.Search.API.Options;
using System.IO.IsolatedStorage;



namespace Collection
{
    public partial class MainPage : PhoneApplicationPage
    {
        MMarker mylocation;
        private IsolatedStorageSettings _appSettings;
        PhoneNumberChooserTask jd;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            jd = new PhoneNumberChooserTask();
            jd.Completed += jd_Completed;
            _appSettings = IsolatedStorageSettings.ApplicationSettings;
            if (_appSettings.Contains("status"))
            {
                if (_appSettings["status"] != null)
                {
                    Dataclass.status = "ok";
                    
                }
                else
                {
                    Dataclass.status = "cancel";
                }
            }
            else
            {
                var result = MessageBox.Show("Some Emergency Positioning Tool features need your location in order to work. You can turn this off at any time under the settings menu.", "Allow app to access and use your location?", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    _appSettings.Add("status", "ok");
                    _appSettings.Save();
                    Dataclass.status = "ok";
                    
                }
                if (result == MessageBoxResult.Cancel)
                {
                    _appSettings.Add("status", null);
                    _appSettings.Save();
                    Dataclass.status = "cancel";
                }
            }
            
            
        }

        void jd_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                _appSettings.Add("phonenumber", e.PhoneNumber);
            }
        }
        void locationwatcher_PositionChanged(object sender, AGeoPositionChangedEventArgs e)
        {
            if (mylocation == null)
            {
               mylocation = new MMarker() { LngLat = e.LngLat };

            }
            this.LatitudeTextBlock.Text = e.LngLat.LatY.ToString();
            this.LongitudeTextBlock.Text = e.LngLat.LngX.ToString();
            Dataclass.latX = e.LngLat.LatY;
            Dataclass.lonY = e.LngLat.LngX;

 
        }
        void locationwatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            this.StatusTextBlock.Text = e.Status.ToString();
            
 
        }


       
        private void startLocationButton_Click(object sender, EventArgs e)
        {
            if (Dataclass.status == "ok")
            {
                this.NavigationService.Navigate(new Uri("/MapPage.xaml", UriKind.Relative));
            }
            else 
            {
                MessageBox.Show("Turn location service on please!", "Tips", MessageBoxButton.OK);
            }
        }
       
        
       

        private void SharePositionButton_Click(object sender, EventArgs e)
        {

            SmsComposeTask help = new SmsComposeTask();
            if (_appSettings.Contains("phonenumber"))
            {
                help.To = _appSettings["phonenumber"].ToString();
                help.Body = "HELP! My position is：" + Convert.ToString(Dataclass.latX) + " degrees latitude," + Convert.ToString(Dataclass.lonY) + " degrees longitude.";
                help.Show();
            }
            else
            {
                var result1 = MessageBox.Show("Do you want to set default contacts?", "Tips", MessageBoxButton.OKCancel);
                if (result1 == MessageBoxResult.OK)
                {

                    jd.Show();
                }
                else
                {

                    help.Body = "HELP! My position is：" + Convert.ToString(Dataclass.latX) + " degrees latitude," + Convert.ToString(Dataclass.lonY) + " degrees longitude.";
                    help.Show();
                }

            }
            
            


        }

        private void ContactMe_Click(object sender, EventArgs e)
        {
            EmailComposeTask mymail = new EmailComposeTask();
            mymail.To = "z18205051832@hotmail.com";
            mymail.Show();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void ratingButton_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask rating = new MarketplaceReviewTask();
            rating.Show();
        }
       

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            if (Dataclass.status == "ok")
            {
                AMapConfig.Key = "4797d3e49bcc69a8bf4c77a949a56769";
                AGeoCoordinateWatcher locationwatcher = new AGeoCoordinateWatcher();
                locationwatcher.PositionChanged += locationwatcher_PositionChanged;
                locationwatcher.StatusChanged += locationwatcher_StatusChanged;
                locationwatcher.Start();
            }
            else
            {
                this.LatitudeTextBlock.Text = "0";
                this.LongitudeTextBlock.Text = "0";
                StatusTextBlock.Text = "location service is off";
            }
        }


    }
}