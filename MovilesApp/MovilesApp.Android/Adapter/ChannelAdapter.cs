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
using System.Collections.ObjectModel;
using MovilesApp.Model;

namespace MovilesApp.Droid.Adapter
{
    class ChannelRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView TxvChannelName { get; set; }

        public ChannelRecyclerViewHolder(View itemView) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.ivChannelImage);
            TxvChannelName = itemView.FindViewById<TextView>(Resource.Id.txvChannelName);
        }
    }

    public class ChannelAdapter : RecyclerView.Adapter
    {
        private ObservableCollection<Channel> _channels =
            new ObservableCollection<Channel>();

        public ChannelAdapter(ObservableCollection<Channel> items)
        {
            _channels = items;
        }

        public override int ItemCount { get { return _channels.Count; } }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.channels_card, parent, false);
            return new ChannelRecyclerViewHolder(itemView);
        }
    }
}