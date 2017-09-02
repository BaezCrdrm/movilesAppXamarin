using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using System.Collections.ObjectModel;
using MovilesApp.Model;
using MovilesApp.Droid.Adapter;
using System.Threading.Tasks;
using Android.Widget;
using Android.Support.V4.Widget;
using System.Threading;
using Android.Util;

using SupportFragment = Android.Support.V4.App.Fragment;

namespace MovilesApp.Droid
{
    public class AllEvents: SupportFragment
    {
        private ObservableCollection<Event> events;
        private int typeEvent = 0;

        private RecyclerView recycler;
        private RecyclerView.Adapter adapter;
        private RecyclerView.LayoutManager IManager;

        SwipeRefreshLayout srlRefresh;
        Button btnReload;

        Thread tr;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_all_events, container, false);
            typeEvent = Int32.Parse(Arguments.GetSerializable("EventType").ToString());
            srlRefresh = view.FindViewById<SwipeRefreshLayout>(Resource.Id.srlRefresh);
            btnReload = view.FindViewById<Button>(Resource.Id.btnReload);
            events = new ObservableCollection<Event>();
            recycler = view.FindViewById<RecyclerView>(Resource.Id.rvEvents);
            IManager = new LinearLayoutManager(Context);
            recycler.SetLayoutManager(IManager);
            try
            {
                Activity.RunOnUiThread(() =>
                {
                    srlRefresh.Refreshing = true;
                });
                tr = SetLoadingThread();
                tr.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al intentar obtener los datos");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            srlRefresh.Refresh += SrlRefresh_Refresh;
            btnReload.Click += BtnReload_Click;
            return view;
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void SrlRefresh_Refresh(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            Activity.RunOnUiThread(() =>
            {
                srlRefresh.Refreshing = true;
                btnReload.Visibility = ViewStates.Gone;
            });

            try { events.Clear(); }
            catch (Exception) { }

            tr = SetLoadingThread();
            tr.Start();
        }

        private void LoadData()
        {
            if (events != null)
            {
                adapter = new EventsAdapter(events);
                recycler.SetAdapter(adapter);
            }
            else
                Toast.MakeText(Activity.ApplicationContext,
                            Resource.String.exceptionTryLater,
                            ToastLength.Long).Show();
        }

        private Thread SetLoadingThread()
        {
            Thread tr = new Thread(() =>
            {
                try
                {
                    MovilesApp.Logic.Service service = new MovilesApp.Logic.Service();
                    Task<ObservableCollection<Event>> task =
                        Task.Run(() => service.GetData(null, typeEvent));

                    try
                    {
                        events = task.Result;
                        //Toast.MakeText(Application.Context, events.Count.ToString(), ToastLength.Long).Show();
                        System.Diagnostics.Debug.WriteLine(events.Count.ToString());
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al obtener los datos");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    Log.Error("Error Thread", ex.Message);
                }

                Activity.RunOnUiThread(() =>
                {
                    srlRefresh.Refreshing = false;

                    try
                    {
                        LoadData();
                        if (events.Count == 0 || events == null)
                        {
                            if(events.Count == 0)
                            {
                                Toast.MakeText(Activity.ApplicationContext,
                                    Resource.String.msg_events_list_no_events,
                                    ToastLength.Short).Show();
                            }
                            else if(events == null)
                            {
                                Toast.MakeText(Activity.ApplicationContext,
                                    Resource.String.msg_no_internet_connection,
                                    ToastLength.Short).Show();
                            }
                            btnReload.Visibility = ViewStates.Visible;
                        }
                        else
                            btnReload.Visibility = ViewStates.Gone;
                    }
                    catch (Exception)
                    {
                        Toast.MakeText(Activity.ApplicationContext,
                            Resource.String.exceptionTryLater,
                            ToastLength.Long).Show();
                        btnReload.Visibility = ViewStates.Visible;
                    }
                });
            });

            return tr;
        }
    }
}