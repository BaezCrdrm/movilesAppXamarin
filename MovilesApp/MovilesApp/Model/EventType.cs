using System;
//using Windows.UI.Xaml.Media.Imaging;

namespace MovilesApp.Model
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //private BitmapImage _bmi;
        public string sUri { get; set; }
        public string HexColor { get; set; }
        public Uri Uri { get; set; }

        //public BitmapImage ImageResource
        //{
        //    get { return _bmi; }
        //    set { _bmi = value; }
        //}

        public EventType() { }

        public EventType(int id) { this.Id = id; }

        public EventType(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            //_bmi = new BitmapImage();
        }

        public EventType(int id, string name, string hexColor)
        {
            this.Id = id;
            this.Name = name;
            this.HexColor = hexColor;
            //_bmi = new BitmapImage();
        }
    }
}