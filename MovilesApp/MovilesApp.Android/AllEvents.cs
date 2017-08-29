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
using MovilesApp.Logic;
using MovilesApp.Droid.Adapter;
using System.Threading.Tasks;
using Android.Widget;

namespace MovilesApp.Droid
{
    public class AllEvents:Fragment
    {
        private ObservableCollection<Event> events;
        private int typeEvent = 0;

        private RecyclerView recycler;
        private RecyclerView.Adapter adapter;
        private RecyclerView.LayoutManager IManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_all_events, container, false);
            events = new ObservableCollection<Event>();
            try
            {
                getDataAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al intentar obtener los datos");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                recycler = view.FindViewById<RecyclerView>(Resource.Id.rvEvents);
                IManager = new LinearLayoutManager(Context);
                recycler.SetLayoutManager(IManager);

                loadData();
            }

            return view;
        }

        private void loadData()
        {
            adapter = new EventsAdapter(events);
            recycler.SetAdapter(adapter);
        }

        private async void getDataAsync()
        {
            MovilesApp.Logic.Service service = new MovilesApp.Logic.Service();
            Task<ObservableCollection<Event>> task =
                Task.Run(() => service.GetData(null, typeEvent));

            try
            {
                events = task.Result;
                Toast.MakeText(Application.Context, events.Count.ToString(), ToastLength.Long);
                System.Diagnostics.Debug.WriteLine(events.Count.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}