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
        private EventType _eventType;
        private ObservableCollection<Channel> _channelList;

        public EventType Type
        {
            get { return _eventType; }
            set { _eventType = value; }
        }

        public ObservableCollection<Channel> ChannelList
        {
            get { return _channelList; }
            set { _channelList = value; }
        }        

        public Event()
        {
            _eventType = new EventType();
            _channelList = new ObservableCollection<Channel>();
        }
    }
}
