using MovilesApp.Logic;
using MovilesApp.Model;
using MovilesApp.UWP.Logic;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        public Event EventInfo { get; set; }
        private ObservableCollection<Channel> _channels;

        public ObservableCollection<Channel> ChannelList
        {
            get { return _channels; }
            set { _channels = value; }
        }


        public EventDetails()
        {
            this.InitializeComponent();
            EventInfo = new Event();
            sc = SynchronizationContext.Current;
            txbEventTitle.Text = "";
            txbEventSch.Text = "";
            txbDetails.Text = "";
            imgEventType.Source = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EventInfo.Id = e.Parameter.ToString();
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
            Task<ObservableCollection<Event>> taskEvents =
                Task.Run(() => service.GetData(EventInfo.Id, 1));
            Task<ObservableCollection<Channel>> taskChannels =
                Task.Run(() => service.GetData(EventInfo.Id));

            try
            {
                ObservableCollection<Event> _temp = new ObservableCollection<Event>();
                _temp = taskEvents.Result;
                EventInfo = setResources(_temp);
                _channels = taskChannels.Result;
                lvChannelList.ItemsSource = ChannelList;
                _temp = null;

                txbEventTitle.Text = EventInfo.Name;
                txbEventSch.Text = EventInfo.Schedule.ToString();
                BitmapImage bmi = new BitmapImage(EventInfo.Type.Uri);
                Validate val = new Validate();
                rpHeader.Background = val.HexToBrush(EventInfo.Type.HexColor);
                txbDetails.Foreground = val.HexToBrush(EventInfo.Type.HexColor);
                txbDetails.Text = EventInfo.Description;
                imgEventType.Source = bmi;
            }
            catch (Exception)
            {
                MessageDialog md = new MessageDialog(
                    "Verifique la conexión a internet.",
                    "Error al obtener los datos");
                await md.ShowAsync();
            }
            finally
            {
                prProgress.IsActive = false;
            }
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
