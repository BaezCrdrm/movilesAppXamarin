using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovilesApp.Model
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Schedule { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public ObservableCollection<Channel> ChannelList { get; set; }
    }
}
