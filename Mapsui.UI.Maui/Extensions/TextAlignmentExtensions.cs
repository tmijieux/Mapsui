using Mapsui.Widgets;

namespace Mapsui.UI.Maui.Extensions
{
    public static class TextAlignmentExtensions
    {
        /// <summary>
        /// Convert Microsoft.Maui.TextAlignment to Mapsui/RichTextKit.Styles.Color
        /// </summary>
        /// <param name="textAlignment">TextAlignment in Xamarin.Forms format</param>
        /// <returns>TextAlignment in Mapsui/RichTextKit format</returns>
        public static Alignment ToMapsui(this Microsoft.Maui.TextAlignment textAlignment)
        {
            Alignment result;

            switch (textAlignment)
            {
                case Microsoft.Maui.TextAlignment.Start:
                    result = Alignment.Left;
                    break;
                case Microsoft.Maui.TextAlignment.Center:
                    result = Alignment.Center;
                    break;
                case Microsoft.Maui.TextAlignment.End:
                    result = Alignment.Right;
                    break;
                default:
                    result = Alignment.Auto;
                    break;
            }

            return result;
        }
    }
}