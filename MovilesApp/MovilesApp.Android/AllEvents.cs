using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using System.Collections.ObjectModel;
using MovilesApp.Model;
using MovilesApp.Droid.Adapter;

namespace MovilesApp.Droid
{
    public class AllEvents:Fragment
    {
        private ObservableCollection<Event> events;

        private RecyclerView recycler;
        private RecyclerView.Adapter adapter;
        private RecyclerView.LayoutManager IManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_all_events, container, false);
            events = new ObservableCollection<Event>();

            // Replace this fragment of code.
            // The app needs to get the data from the rest api.
            Event e = new Event();
            e.Id = "180204b4603"; e.Name = "SuperBowl"; e.Description = "Equipos por confirmar. Desde USA"; e.Type.Id = 1;
            events.Add(e);


            recycler = view.FindViewById<RecyclerView>(Resource.Id.rvEvents);
            IManager = new LinearLayoutManager(Context);
            recycler.SetLayoutManager(IManager);

            adapter = new EventsAdapter(events);
            recycler.SetAdapter(adapter);


            return view;
        }


    }
}