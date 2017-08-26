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
            ObservableCollection<Event> events = 
                new ObservableCollection<Event>();
            try
            {
                var jsonResponseArray = 
                    JsonConvert.DeserializeObject<ObservableCollection<EventResponse>>(json);

                foreach (EventResponse item in jsonResponseArray)
                {
                    Event ev = new Event();
                    ev.Id = item.ev_id;
                    ev.Name = item.ev_name;
                    ev.Description = item.ev_des;

                    ev.Schedule = parseToDate(item.ev_sch);
                    ev.Ending = parseToDate(item.ev_sch_end);

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

        private DateTime parseToDate(string sch)
        {
            string strDate = sch;
            strDate = strDate.Replace("/", "-");
            DateTime date = DateTime.ParseExact(strDate, "yyyy-MM-dd HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }

        private ObservableCollection<Channel> getChannelCollection(string json)
        {
            ObservableCollection<Channel> channels = 
                new ObservableCollection<Channel>();

            try
            {
                var jsonResponseArray =
                    JsonConvert.DeserializeObject<ObservableCollection<ChannelResponse>>(json);

                foreach (ChannelResponse item in jsonResponseArray)
                {
                    Channel ch = new Channel();
                    ch.Id = Int32.Parse(item.ch_id);
                    ch.Name = item.ch_name;
                    ch.Abv = item.ch_abv;
                    ch.UrlImagePath = item.ch_img;

                    channels.Add(ch);
                    ch = null;
                }

                jsonResponseArray = null;
                Debug.WriteLine("");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return channels;
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
                json = await da.GetEventAsync(ev);
                events = getEventCollection(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción producida: " + ex.Message);
                return null;
            }

            return events;
        }

        public async Task<ObservableCollection<Channel>> GetData(string id)
        {
            ObservableCollection<Channel> channels = 
                new ObservableCollection<Channel>();
            string json = "";

            try
            {
                DataAsync da = new DataAsync();
                json = await da.GetChannelListAsync(id);
                channels = getChannelCollection(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción producida: " + ex.Message);
                return null;
            }

            return channels;
        }
    }

    public class EventResponse
    {
        public string ev_id { get; set; }
        public string ev_name { get; set; }
        public string ev_sch { get; set; }
        public string ev_sch_end { get; set; }
        public string ev_des { get; set; }
        public string tp_id { get; set; }
        public string tp_name { get; set; }
    }

    public class ChannelResponse
    {
        public string ch_id { get; set; }
        public string ch_name { get; set; }
        public string ch_abv { get; set; }
        public string ch_img { get; set; }
    }
}
