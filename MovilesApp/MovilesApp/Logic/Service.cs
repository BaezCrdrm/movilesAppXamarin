using MovilesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovilesApp.Logic
{
    public class Service
    {
        public ObservableCollection<Event> GetData(string id, int type)
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

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción producida: " + ex.Message);
            }

            return null;
        }
    }
}
