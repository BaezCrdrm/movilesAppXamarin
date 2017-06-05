using MovilesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovilesApp.Logic
{
    public class Service
    {
        private ObservableCollection<Event> getEventCollection(string json)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();

            try
            {
                var jsonResponseArray = JsonConvert.DeserializeObject<ObservableCollection<Response>>(json);

                foreach (Response item in jsonResponseArray)
                {
                    Event ev = new Event();
                    ev.Id = item.ev_id;
                    ev.Name = item.ev_name;
                    ev.Description = item.ev_des;

                    string strDate = item.ev_sch;
                    strDate = strDate.Replace("/", "-");
                    DateTime date = DateTime.ParseExact(strDate, "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    ev.Schedule = date;

                    try
                    {
                        ev.Type.Id = Int32.Parse(item.tp_id);
                        ev.Type.Name = item.tp_name;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Error al agregar datos del tipo de evento");
                    }

                    events.Add(ev);
                    ev = null;
                }

                jsonResponseArray = null;
                Debug.WriteLine("");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
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
                json = await da.GetAsync(ev);
                events = getEventCollection(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción producida: " + ex.Message);
                return null;
            }

            return events;
        }
    }

    public class Response
    {
        public string ev_id { get; set; }
        public string ev_name { get; set; }
        public string ev_sch { get; set; }
        public string ev_des { get; set; }
        public string tp_id { get; set; }
        public string tp_name { get; set; }
    }
}
