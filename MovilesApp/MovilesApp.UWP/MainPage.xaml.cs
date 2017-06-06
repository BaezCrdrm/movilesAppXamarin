using MovilesApp.Model;
using MovilesApp.UWP.Logic;
using MovilesApp.UWP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MovilesApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<EventType> _eventTypes = new ObservableCollection<EventType>();

        public ObservableCollection<EventType> EventTypeCollection
        {
            get { return _eventTypes; }
            set { _eventTypes = value; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            generateEventType();
            
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if(frameEventList.CanGoBack && frameEventDetails.IsEnabled == false)
            {
                frameEventList.GoBack();
                e.Handled = true;
            }
            else if (frameEventDetails.IsEnabled == true && 
                frameEventDetails.Visibility == Visibility.Visible)
            {
                frameEventDetails.Content = null;
                frameEventDetails.Visibility = Visibility.Collapsed;
                frameEventDetails.IsEnabled = false;
            }

            if (!frameEventList.CanGoBack && frameEventDetails.IsEnabled == false)
            {
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lvMenu.SelectedIndex = 0;
            frameEventDetails.Visibility = Visibility.Collapsed;
            frameEventDetails.IsEnabled = false;
            frameEventList.Visibility = Visibility.Visible;

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            spMenu.IsPaneOpen = !spMenu.IsPaneOpen;
            if (spMenu.IsPaneOpen)
            {
                spnlMenu.Width = 320;
                Validate val = new Validate();
                spnlMenu.Background = val.HexToBrush("D3D3D3");
            }
            else
            {
                spnlMenu.Background = 
                    new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
                spnlMenu.Width = 48;
            }
        }

        private void generateEventType()
        {
            EventTypeCollection.Add(new EventType(-1, "Todo"));
            EventTypeCollection.Add(new EventType(1, "Americano"));
            EventTypeCollection.Add(new EventType(2, "Soccer"));
            EventTypeCollection.Add(new EventType(3, "Basketball"));
            EventTypeCollection.Add(new EventType(4, "Baseball"));            
            EventTypeCollection.Add(new EventType(5, "Musicales"));
            EventTypeCollection.Add(new EventType(6, "Premios"));
            EventTypeCollection.Add(new EventType(7, "Otros"));
        }

        private void lvMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frameEventDetails.Visibility = Visibility.Collapsed;
            spMenu.IsPaneOpen = false;
            spnlMenu.Width = 48;
            frameEventList.Visibility = Visibility.Visible;

            try
            {
                EventType et = (EventType)((ListView)sender).SelectedItem;
                frameEventList.Navigate(typeof(EventList), et.Id);
            }
            catch (Exception)
            {
                Debug.WriteLine("Excepción producida al navegar");
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Visibility visible = Visibility.Visible;
            Visibility collapsed = Visibility.Collapsed;

            if (this.ActualWidth < 640)
            {
                if (frameEventDetails.Visibility == visible && frameEventDetails.IsEnabled == true)
                    frameEventList.Visibility = collapsed;
                else
                    frameEventList.Visibility = visible;
            }
            else
            {
                frameEventList.Visibility = visible;
                frameEventDetails.Visibility = visible;
            }

            if (spMenu.IsPaneOpen == false)
                spnlMenu.Width = 48;
        }

        private void spMenu_PaneClosed(SplitView sender, object args)
        {
            spnlMenu.Background =
                    new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
            spnlMenu.Width = 48;
        }
    }
}