using MovilesApp.Logic;
using MovilesApp.Model;
using MovilesApp.UWP.LocalModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        private ObservableCollection<uwpEvent> _events;
        private int typeEvent = 0;

        public ObservableCollection<uwpEvent> EventCollection
        {
            get { return _events; }
            set { _events = value; }
        }

        public EventList()
        {
            this.InitializeComponent();
            _events = new ObservableCollection<uwpEvent>();
            sc = SynchronizationContext.Current;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            typeEvent = (Int32)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetDataAsync();
        }

        private async void GetDataAsync()
        {
            await Task.Run(() =>
            {
                UpdateUI(true);
            });

            Service service = new Service();
            Task<ObservableCollection<Event>> task =
                Task.Run(() => service.GetData(null, typeEvent));

            ObservableCollection<Event> _temp = new ObservableCollection<Event>();
            try
            {
                _temp = task.Result;
                setImageResources(_temp);
                _temp = null;

                lvEvents.ItemsSource = this.EventCollection;
                prProgress.IsActive = false;
            }
            catch (Exception)
            {
                MessageDialog md = new MessageDialog(
                    "Verifique la conexión a internet.",
                    "Error al obtener los datos");
                await md.ShowAsync();
                prProgress.IsActive = false;
            }
        }

        private void setImageResources(ObservableCollection<Event> _temp)
        {
            foreach (Event ev in _temp)
            {
                string strUri = "ms-appx:///Assets/";

                switch (ev.Type.Id)
                {
                    case 1:
                        strUri += "americano.png";
                        break;

                    case 2:
                        strUri += "soccer.png";
                        break;

                    case 3:
                        strUri += "basketball.png";
                        break;

                    case 4:                        
                        strUri += "baseball.png";
                        break;

                    case 5:
                        strUri += "musica.png";
                        break;

                    case 6:
                        strUri += "premios.png";
                        break;

                    default:
                        strUri += "gen.png";
                        break;
                }
                ev.Type.Uri = new Uri(strUri);

                uwpEvent ue = new uwpEvent();
                ue.Id = ev.Id;
                ue.Name = ev.Name;
                ue.Schedule = ev.Schedule;
                ue.Description = ev.Description;
                ue.Type = ev.Type;
                ue.BitmapImageResource = 
                    new Windows.UI.Xaml.Media.Imaging.BitmapImage(ev.Type.Uri);

                _events.Add(ue);
            }
            _temp = null;
        }

        private void UpdateUI(Boolean value)
        {
            sc.Post(new SendOrPostCallback(o =>
            {
                try
                {
                    prProgress.IsActive = true;
                    if (lvEvents.Items.Count > 0)
                        prProgress.IsActive = false;
                } catch (Exception ex)
                {
                    prProgress.IsActive = false;
                }
            }), value);
        }

        private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            uwpEvent _ev = (uwpEvent)((ListView)sender).SelectedItem;
            var grid = this.Frame.Parent as Grid;
            var detFrame = grid.Children[1] as Frame;
            detFrame.IsEnabled = true;
            detFrame.Visibility = Visibility.Visible;

            //if (grid.ActualWidth <= 640)
            //{
            //    this.Frame.Visibility = Visibility.Collapsed;
            //}

            detFrame.Navigate(typeof(EventDetails), _ev.Id);
        }
    }
}