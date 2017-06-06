using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MovilesApp.UWP.Logic
{
    public class Validate
    {
        public Brush HexToBrush(string hexColor)
        {
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            return new SolidColorBrush(ColorHelper.FromArgb(255,
            red = byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier),
            green = byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier),
            blue = byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier)));
        }
    }
}
