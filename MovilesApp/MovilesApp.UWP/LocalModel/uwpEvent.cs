using MovilesApp.Model;
using Windows.UI.Xaml.Media.Imaging;

namespace MovilesApp.UWP.LocalModel
{
    public class uwpEvent : Event
    {
        private BitmapImage _bmi;

        public BitmapImage BitmapImageResource
        {
            get { return _bmi; }
            set { _bmi = value; }
        }

        public uwpEvent()
        {
            _bmi = new BitmapImage();
        }
    }
}
