using MovilesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace MovilesApp.Logic
{
    public class Service
    {
        private ObservableCollection<Event> getEventCollection(string json)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();

            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }

            return events;
        }

        public async Task<ObservableCollection<Event>> GetData(string id, int type)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            if (id == null)
                id = "null";

            Event ev = new Event();
            ev.Id = id;
            ev.Type.Id = type;
            string json = "";

            try
            {
                DataAsync da = new DataAsync();
                json = await da.Get(ev);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción producida: " + ex.Message);
            }

            return null;
        }
    }
}
