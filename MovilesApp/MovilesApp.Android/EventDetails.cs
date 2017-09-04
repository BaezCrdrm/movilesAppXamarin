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
using LogicService = MovilesApp.Logic.Service;

namespace MovilesApp.Droid
{
    public class EventDetails: SupportFragment
    {
        string eventId;

        LogicService service;
        TextView txvTitle, txvSch, txvDetails, txvDuration;
        ImageView imgType;
        ProgressBar pb;
        Thread tr;
        FrameLayout flBasicInfo;
        ListView lvChannelList;
        FloatingActionButton fbtnAddToCalendar;
        private Event _event;

        public Event Event
        {
            get { return _event; }
            set { _event = value; }
        }

        public EventDetails()
        {
            this._event = new Event();
            service = new LogicService();
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_event_details, container, false);
            txvTitle = view.FindViewById<TextView>(Resource.Id.txvTitle);
            txvSch = view.FindViewById<TextView>(Resource.Id.txvSchedule);
            txvDetails = view.FindViewById<TextView>(Resource.Id.txvDescription);
            txvDuration = view.FindViewById<TextView>(Resource.Id.txvDuration);

            return view;
        }
    }
}