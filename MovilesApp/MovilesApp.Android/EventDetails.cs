using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovilesApp.Model;
using SupportFragment = Android.Support.V4.App.Fragment;
using System.Threading;
using Android.Support.Design.Widget;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Globalization;
using Android.Graphics;

namespace MovilesApp.Droid
{
    public class EventDetails: SupportFragment
    {
        private string eventId;
        
        TextView txvTitle, txvSch, txvDetails, txvDuration;
        ImageView imgType;
        ProgressBar pb;
        Thread tr;
        FrameLayout flBasicInfo;
        ListView lvChannelList;
        FloatingActionButton fbtnAddToCalendar;
        private Event _event;

        public Event Event { get { return _event; } }

        public EventDetails()
        {
            this._event = new Event();
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_event_details, container, false);

            eventId = Arguments.GetString("id").ToString();
            InitView(view);

            try
            {
                tr = SetLoadingThread();
                tr.Start();
            }
            catch (Exception ex)
            {                
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return view;
        }

        private void InitView(View view)
        {
            txvTitle = view.FindViewById<TextView>(Resource.Id.txvEventTitle);
            txvSch = view.FindViewById<TextView>(Resource.Id.txvSchedule);
            txvDetails = view.FindViewById<TextView>(Resource.Id.txvDetails);
            txvDuration = view.FindViewById<TextView>(Resource.Id.txvDuration);
            pb = view.FindViewById<ProgressBar>(Resource.Id.pbProgress);

            imgType = view.FindViewById<ImageView>(Resource.Id.imgType);
            flBasicInfo = view.FindViewById<FrameLayout>(Resource.Id.flBasicInfo);

            txvTitle.Text = "";
            txvSch.Text = "";
            txvDetails.Text = "";
            txvDuration.Text = "";
            flBasicInfo.SetBackgroundColor(Color.ParseColor("#fefeef"));
        }

        private Thread SetLoadingThread()
        {
            Thread thread = new Thread(() =>
            {
                MovilesApp.Logic.Service service = new MovilesApp.Logic.Service();
                Task<ObservableCollection<Event>> eventTask = 
                    Task.Run(() => service.GetData(eventId, 0));

                Task<ObservableCollection<Channel>> channelsTask =
                    Task.Run(() => service.GetData(eventId));

                try
                {
                    _event = eventTask.Result[0];
                    _event.ChannelList = channelsTask.Result;
                    System.Diagnostics.Debug.WriteLine(_event);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                Activity.RunOnUiThread(() =>
                {
                    pb.Visibility = ViewStates.Gone;
                    try
                    {
                        LoadData();
                    }
                    catch (Exception)
                    {

                    }
                });
            });
            return thread;
        }        

        private void LoadData()
        {
            txvTitle.Text = _event.Name;

            //string format = "";

            //if (CultureInfo.CurrentCulture == new CultureInfo("es-MX"))
            //    format = "dddd dd MMM yyyy HH:mm";
            //else
            //    format = "dddd MMM dd yyyy HH:mm";

            txvSch.Text = _event.Schedule.ToString(CultureInfo.CurrentUICulture);
            txvDetails.Text = _event.Description;

            //var color = val.HexToBrush(_event.Type.HexColor);
            //rpHeader.Background = color;
            //txvDetails.Foreground = color;
            //btnAddToCalendar.Background = color;
            SetAssetResources();

            if (_event.Duration > new TimeSpan(0, 0, 0))
            {
                txvDuration.Visibility = ViewStates.Visible;
                //txvDuration.Foreground = color;
                txvDuration.Text = String.Format("Duración aprox: {0:%h} horas {0:%m} minutos",
                    _event.Duration);
            }
            else
                txvDuration.Visibility = ViewStates.Gone;
        }

        private void SetAssetResources()
        {
            switch (_event.Type.Id)
            {
                case 1:                    
                    _event.Type.ImageResource = Resource.Drawable.football_rect;
                    _event.Type.HexColor = "555EA7";
                    break;

                case 2:
                    _event.Type.ImageResource = Resource.Drawable.soccer_rect;
                    _event.Type.HexColor = "469234";
                    break;

                case 3:
                    _event.Type.ImageResource = Resource.Drawable.basketball_rect;
                    _event.Type.HexColor = "E56C0C";
                    break;

                case 4:
                    _event.Type.ImageResource = Resource.Drawable.basketball_rect;
                    _event.Type.HexColor = "E52420";
                    break;
                case 5:
                    _event.Type.ImageResource = Resource.Drawable.music_rect;
                    _event.Type.HexColor = "41BAC1";
                    break;
                case 6:
                    _event.Type.ImageResource = Resource.Drawable.awards_rect;
                    _event.Type.HexColor = "D1C103";
                    break;
                default:
                    _event.Type.HexColor = "D3D3D3";
                    break;
            }

            Color uiMainColor = Color.ParseColor("#" + _event.Type.HexColor);
            flBasicInfo.SetBackgroundColor(uiMainColor);
            imgType.SetImageResource(_event.Type.ImageResource);
        }
    }
}