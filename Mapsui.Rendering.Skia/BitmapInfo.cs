using Mapsui.Styles;
using SkiaSharp;

namespace Mapsui.Rendering.Skia
{
    public enum BitmapType
    {
        //Bitmap,
        Picture,
        Image,
        Sprite,
    }

    public class BitmapInfo
    {
        private object _data;

        public BitmapType Type { get; private set; }

        public SKImage Image
        {
            get
            {
                if (Type == BitmapType.Image)
                    return (SKImage) _data;
                else
                    return null;
            }
            set
            {
                _data = value;
                Type = BitmapType.Image;
            }
        }

        //public SKBitmap Bitmap
        //{
        //    get
        //    {
        //        if (Type == BitmapType.Bitmap)
        //            return (SKBitmap) _data;
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        _data = value;
        //        Type = BitmapType.Bitmap;
        //    }
        //}


        public SKPicture Picture
        {
            get
            {
                if (Type == BitmapType.Picture)
                    return (SKPicture) _data;
                else
                    return null;
            }
            set
            {
                _data = value;
                Type = BitmapType.Picture;
            }
        }

        public Sprite Sprite
        {
            get
            {
                if (Type == BitmapType.Sprite)
                    return (Sprite)_data;
                else
                    return null;
            }
            set
            {
                _data = value;
                Type = BitmapType.Sprite;
            }
        }

        public long IterationUsed { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; } = 1.0f;

        public float Width
        {
            get
            {
                switch (Type)
                {
                    case BitmapType.Image:
                        return Image.Width;
                    //case BitmapType.Bitmap:
                    //    return Bitmap.Width;
                    case BitmapType.Picture:
                        return Picture.CullRect.Width;
                    case BitmapType.Sprite:
                        return ((Sprite) _data).Width;
                    default:
                        return 0;
                }
            }
        }

        public float Height
        {
            get
            {
                switch (Type)
                {
                    case BitmapType.Image:
                        return Image.Height;
                    //case BitmapType.Bitmap:
                    //    return Bitmap.Height;
                    case BitmapType.Picture:
                        return Picture.CullRect.Height;
                    case BitmapType.Sprite:
                        return ((Sprite) _data).Height;
                    default:
                        return 0;
                }
            }
        }
    }
}
