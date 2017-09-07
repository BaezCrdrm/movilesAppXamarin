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
using Android.Support.V7.Widget;
using MovilesApp.Model;
using System.Collections.ObjectModel;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.App;

namespace MovilesApp.Droid.Adapter
{
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView image { get; set; }
        public TextView txvTitle { get; set; }
        public TextView txvDescription { get; set; }
        public LinearLayout llEvent { get; set; }

        public RecyclerViewHolder(View itemView) : base(itemView)
        {
            image = itemView.FindViewById<ImageView>(Resource.Id.Recycler_images);
            txvTitle = itemView.FindViewById<TextView>(Resource.Id.txvTitle);
            txvDescription = itemView.FindViewById<TextView>(Resource.Id.txvDescription);
            llEvent = itemView.FindViewById<LinearLayout>(Resource.Id.llEvent);
        }
    }

    public class EventsAdapter : RecyclerView.Adapter
    {
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        public EventsAdapter(ObservableCollection<Event> events)
        {
            this._events = events;
        }

        public override int ItemCount { get { return _events.Count; } }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            int resource = 0;
            switch (_events[position].Type.Id)
            {
                case 1:
                    resource = Resource.Drawable.football;
                    break;
                case 2:
                    resource = Resource.Drawable.soccer;
                    break;
                case 3:
                    resource = Resource.Drawable.basketball;
                    break;
                case 4:
                    resource = Resource.Drawable.baseball;
                    break;
                case 5:
                    resource = Resource.Drawable.music;
                    break;
                case 6:
                    resource = Resource.Drawable.awards;
                    break;
                default:
                    resource = Resource.Drawable.gen;
                    break;
            }

            viewHolder.image.SetImageResource(resource);
            viewHolder.txvTitle.Text = _events[position].Name;
            viewHolder.txvDescription.Text = _events[position].Description;
            viewHolder.llEvent.Tag = _events[position].Id;
            viewHolder.llEvent.Click += LlEvent_Click;
        }

        private void LlEvent_Click(object sender, EventArgs e)
        {
            string id = ((LinearLayout)sender).Tag.ToString();
            var obj = (LinearLayout)sender;
            Navigate(obj.RootView, id, obj.Context);
        }

        private void Navigate(View view, String id, Context context)
        {
            SupportFragment fragment = new EventDetails();
            Bundle args = new Bundle();
            
            args.PutString("id", id);
            fragment.Arguments = args;
            SwitchContent(Resource.Id.rLayoutEventDetails, fragment, view, context);
        }

        private void SwitchContent(int replaced, SupportFragment fragment, View view, Context context)
        {
            if (view.Context == null)
                return;
            if(context.GetType() == typeof(MainActivity))
            {
                try
                {
                    MainActivity main = (MainActivity)context;
                    SupportFragment frag = fragment;
                    main.Navigate(replaced, frag);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.events_card, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}