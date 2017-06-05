using MovilesApp.Model;
using MovilesApp.UWP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lvMenu.SelectedIndex = 0;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            spMenu.IsPaneOpen = !spMenu.IsPaneOpen;
            if (spMenu.IsPaneOpen)
                spnlMenu.Width = 320;
            else
                spnlMenu.Width = 48;
        }

        private void generateEventType()
        {
            EventTypeCollection.Add(new EventType(-1, "Todo"));
            EventTypeCollection.Add(new EventType(1, "Americano"));
            EventTypeCollection.Add(new EventType(2, "Soccer"));
            EventTypeCollection.Add(new EventType(3, "Baseball"));
            EventTypeCollection.Add(new EventType(4, "Basketball"));
            EventTypeCollection.Add(new EventType(5, "Musicales"));
            EventTypeCollection.Add(new EventType(6, "Premios"));
            EventTypeCollection.Add(new EventType(7, "Otros"));
        }

        private void lvMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventType et = (EventType)((ListView)sender).SelectedItem;

            frameEventList.Navigate(typeof(EventList));
        }
    }
}