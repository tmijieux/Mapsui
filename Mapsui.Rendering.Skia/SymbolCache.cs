using System.Collections.Generic;
using Mapsui.Styles;
using SkiaSharp;

namespace Mapsui.Rendering.Skia
{
    public class SymbolCache : ISymbolCache
    {
        private readonly Dictionary<int, BitmapInfo> _cache = new();

        private readonly Dictionary<string, SKImage> _renderedCache = new();

        public BitmapInfo GetOrCreate(int bitmapId)
        {
            if (!_cache.TryGetValue(bitmapId, out var bitmapInfo))
            {
                bitmapInfo = BitmapHelper.LoadBitmap(BitmapRegistry.Instance.Get(bitmapId));
                _cache[bitmapId] = bitmapInfo;
            }
            return bitmapInfo;
        }

        /// <summary>
        ///  scale must be global final scale including:
        ///    - SymbolScale for Point Styles (SymbolStyle/ImageStyle/Callout...)
        ///    - resolution for Rasters
        ///    - screen pixelRatio
        ///
        /// rotation must be global final rotation including
        ///    - SymbolRotation
        ///    - map rotation for Rasters or Point Style that `RotateWithMap`
        /// </summary>
        public BitmapInfo GetOrCreateRendered(int bitmapId, float rotation, float scale)
        {
            string key = $"{bitmapId}/{rotation}/{scale}";
            if (_renderedCache.TryGetValue(key, out var image))
            {
                return new BitmapInfo { Image = image };
            }
            else
            {
                var bitmapInfo = GetOrCreate(bitmapId);
                if (bitmapInfo.Type == BitmapType.Image && rotation == 0.0f && scale == 1.0f)
                {
                    image = bitmapInfo.Image;
                }
                else
                {
                    image = RenderBitmap(bitmapInfo, rotation, scale);
                }
                _renderedCache[key] = image;
                return new BitmapInfo { Image = image, Rotation=rotation, Scale=scale };
            }
        }

        private SKImage RenderBitmap(BitmapInfo bitmapInfo, float rotation, float scale)
        {
            int width = (int)(bitmapInfo.Width * scale);
            int height = (int)(bitmapInfo.Height * scale);

            var info = new SKImageInfo(width, height) { AlphaType = SKAlphaType.Premul };
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            using var paint = new SKPaint {
                IsAntialias = true,
                FilterQuality = SKFilterQuality.Low
            };
            canvas.Clear();
            if (scale != 1.0f)
                canvas.Scale(scale);
            if (rotation != 0.0f)
                canvas.RotateDegrees(rotation);
            switch(bitmapInfo.Type)
            {
            case BitmapType.Picture:
                canvas.DrawPicture(bitmapInfo.Picture, paint);
                break;
            case BitmapType.Image:
                canvas.DrawImage(bitmapInfo.Image, new SKPoint(0,0), paint);
                break;
            }
            return surface.Snapshot();
        }

        public Size GetSize(int bitmapId)
        {
            var bitmap = GetOrCreate(bitmapId);
            return new Size(bitmap.Width, bitmap.Height);
        }
    }
}
