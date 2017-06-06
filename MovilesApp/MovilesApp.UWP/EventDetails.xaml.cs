using MovilesApp.Logic;
using MovilesApp.Model;
using MovilesApp.UWP.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovilesApp.UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventDetails : Page
    {
        private readonly SynchronizationContext sc;
        public Event eventInfo { get; set; }
        public EventDetails()
        {
            this.InitializeComponent();
            eventInfo = new Event();
            sc = SynchronizationContext.Current;
            txbEventTitle.Text = "";
            txbEventSch.Text = "";
            imgEventType.Source = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            eventInfo.Id = e.Parameter.ToString();
            base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            await Task.Run(() =>
            {
                UpdateUI(true);
            });

            Service service = new Service();
            Task<ObservableCollection<Event>> task =
                Task.Run(() => service.GetData(eventInfo.Id, 1));

            ObservableCollection<Event> _temp = new ObservableCollection<Event>();
            _temp = task.Result;
            eventInfo = setResources(_temp);
            _temp = null;

            txbEventTitle.Text = eventInfo.Name;
            txbEventSch.Text = eventInfo.Schedule.ToString();
            BitmapImage bmi = new BitmapImage(eventInfo.Type.Uri);
            Validate val = new Validate();
            rpHeader.Background = val.HexToBrush(eventInfo.Type.HexColor);
            imgEventType.Source = bmi;
            prProgress.IsActive = false;
            
        }

        private void UpdateUI(bool v)
        {
            sc.Post(new SendOrPostCallback(o =>
            {
                try
                {
                    prProgress.IsActive = true;
                }
                catch (Exception ex)
                {
                    prProgress.IsActive = false;
                }
            }), v);
        }

        private Event setResources(ObservableCollection<Event> temp)
        {
            Event ev = temp[0];
            string strUri = "ms-appx:///Assets/";

            switch (ev.Type.Id)
            {
                case 1:
                    strUri += "americanogrande_rect.png";
                    ev.Type.HexColor = "555EA7";
                    break;

                case 2:
                    strUri += "soccergrande_rect.png";
                    ev.Type.HexColor = "469234";
                    break;

                case 3:
                    strUri += "basquetgrande_rect.png";                    
                    ev.Type.HexColor = "E56C0C";
                    break;

                case 4:
                    strUri += "baseballgrande_rect.png";
                    ev.Type.HexColor = "E52420";
                    break;

                case 5:
                    strUri += "musicagrande_rect.png";
                    ev.Type.HexColor = "41BAC1";
                    break;

                case 6:
                    strUri += "premiosgrande_rect.png";
                    ev.Type.HexColor = "D1C103";
                    break;

                default:
                    strUri += "gen.png";
                    ev.Type.HexColor = "DDDDDD";
                    break;
            }
            ev.Type.Uri = new Uri(strUri);

            return ev;
        }
    }
}
