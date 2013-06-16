using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Collection
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
            
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask br = new WebBrowserTask();
            br.URL = "http://blog.sina.com.cn/s/blog_9e3dce07010193yr.html";
            br.Show();
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            MarketplaceSearchTask moreapp = new MarketplaceSearchTask();
            moreapp.SearchTerms = "SmartCampus";
            moreapp.Show();
        }
    }
}