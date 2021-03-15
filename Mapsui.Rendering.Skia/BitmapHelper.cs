using System.IO;
using System.Text;
using Mapsui.Extensions;
using Mapsui.Styles;
using SkiaSharp;
using Svg.Skia;


namespace Mapsui.Rendering.Skia
{
    public static class BitmapHelper
    {
        public static BitmapInfo LoadBitmap(object bitmapStream)
        {
            // todo: Our BitmapRegistry stores not only bitmaps. Perhaps we should store a class in it
            // which has all information. So we should have a SymbolImageRegistry in which we store a
            // SymbolImage. Which holds the type, data and other parameters.
            if (bitmapStream is Stream stream)
            {
                if (stream.IsSvg())
                {
                    var svg = new SKSvg();
                    svg.Load(stream);

                    return new BitmapInfo {Picture = svg.Picture};
                }
                else
                {
                    var image = SKImage.FromEncodedData(SKData.CreateCopy(stream.ToBytes()));
                    return new BitmapInfo { Image = image };
                    //return new BitmapInfo {Bitmap = SKBitmap.FromImage(image) };
                }
            }
            else if (bitmapStream is Sprite sprite)
            {
                return new BitmapInfo {Sprite = sprite};
            }
            else if (bitmapStream is SKPicture pic)
            {
                return new BitmapInfo { Picture = pic };
            }
            //else if (bitmapStream is SKBitmap bitmap)
            //{
            //    return new BitmapInfo { Bitmap = bitmap };
            //}
            else if (bitmapStream is SKImage image)
            {
                //return new BitmapInfo { Bitmap = SKBitmap.FromImage(image) };
                return new BitmapInfo { Image = image };
            }
            return null;
        }
    }
}