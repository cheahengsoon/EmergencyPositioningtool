using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Enums;
using Com.AMap.Maps.Api.Events;
using Com.AMap.Maps.Api.Layers;
using Com.AMap.Maps.Api.Overlays;
using Com.AMap.Search.API;
using Com.AMap.Search.API.Options;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.Device.Location;


namespace Collection
{
    public partial class MapPage : PhoneApplicationPage
    {
        MMarker mk;
        private IsolatedStorageSettings _appSettings;
        PhoneNumberChooserTask jd;
        public MapPage()
        {
            
            InitializeComponent();
            _appSettings = IsolatedStorageSettings.ApplicationSettings;
            jd = new PhoneNumberChooserTask();
            jd.Completed += jd_Completed;
            AMapConfig.Key = "4797d3e49bcc69a8bf4c77a949a56769";
            AGeoCoordinateWatcher amapGeoCoordinateWatcher = new AGeoCoordinateWatcher();

            amapGeoCoordinateWatcher.PositionChanged += amapGeoCoordinateWatcher_PositionChanged;
            amapGeoCoordinateWatcher.StatusChanged += amapGeoCoordinateWatcher_StatusChanged;
            amapGeoCoordinateWatcher.Start();
            
            
        }

        void jd_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                _appSettings.Add("phonenumber", e.PhoneNumber);
            }
        }
        void amapGeoCoordinateWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            
        }

        void amapGeoCoordinateWatcher_PositionChanged(object sender, AGeoPositionChangedEventArgs e)
        {
            if (mk == null)
            {
                mk = new MMarker() { LngLat = e.LngLat };

            }
            if (!mymap.Children.Contains(mk))
            {
                mymap.Children.Add(mk);
            }
            else
            {
                mk.LngLat = e.LngLat;
            }
            mymap.Center = e.LngLat;
            
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            SmsComposeTask help = new SmsComposeTask();
            if (_appSettings.Contains("phonenumber"))
            {
                help.To = _appSettings["phonenumber"].ToString();
                help.Body = "SOS!    Latitude：" + Convert.ToString(Dataclass.latX) + " ；Longitude：" + Convert.ToString(Dataclass.lonY);
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

                    help.Body = "SOS!    Latitude：" + Convert.ToString(Dataclass.latX) + " ；Longitude：" + Convert.ToString(Dataclass.lonY);
                    help.Show();
                }

            }
            
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            EmailComposeTask sendMyPosition = new EmailComposeTask();
            sendMyPosition.Body = "SOS!    Latitude：" + Convert.ToString(Dataclass.latX) + " ；Longitude：" + Convert.ToString(Dataclass.lonY);
            sendMyPosition.Subject = "SOS!";
            sendMyPosition.Show();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            ShareStatusTask sha = new ShareStatusTask();
            sha.Status = "SOS!    Latitude：" + Convert.ToString(Dataclass.latX) + " ；Longitude：" + Convert.ToString(Dataclass.lonY);
            sha.Show();
        }

        
        
    }
}