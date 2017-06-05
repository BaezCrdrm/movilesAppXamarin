using MovilesApp.Logic;
using MovilesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovilesApp.UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventList : Page
    {
        private readonly SynchronizationContext sc;
        private ObservableCollection<Event> _events;

        public ObservableCollection<Event> EventCollection
        {
            get { return _events; }
            set { _events = value; }
        }

        public EventList()
        {
            this.InitializeComponent();
            sc = SynchronizationContext.Current;            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            GetData();
        }

        private async void GetData()
        {
            await Task.Run(() =>
            {
                UpdateUI(true);
            });
            Service service = new Service();
            Task<ObservableCollection<Event>> task = 
                Task.Run(() => service.GetData(null, 1));
            _events = task.Result;
            lvEvents.ItemsSource = this.EventCollection;
            prProgress.IsActive = false;
        }

        private void UpdateUI(Boolean value)
        {
            sc.Post(new SendOrPostCallback(o =>
            {
                try
                {
                    prProgress.IsActive = true;
                    if(lvEvents.Items.Count > 0)
                        prProgress.IsActive = false;
                } catch(Exception ex)
                {
                    prProgress.IsActive = false;
                }
            }), value);
        }
    }
}
