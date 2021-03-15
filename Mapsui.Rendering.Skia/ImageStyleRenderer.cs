using Mapsui.Geometries;
using Mapsui.Styles;
using SkiaSharp;
using System.Diagnostics;

namespace Mapsui.Rendering.Skia
{
    class ImageStyleRenderer
    {
        public static void Draw(SKCanvas canvas, ImageStyle symbolStyle, Point destination,
                SymbolCache symbolCache, float opacity, double mapRotation, float pixelDensity)
        {
            if (symbolStyle.BitmapId < 0)
                return;
            float rotation = (float)symbolStyle.SymbolRotation;
            if (symbolStyle.RotateWithMap)
                rotation += (float)mapRotation;

            float scale = (float)symbolStyle.SymbolScale * pixelDensity;
            var bitmapInfo = symbolCache.GetOrCreateRendered(symbolStyle.BitmapId, rotation, scale);
            //var bitmap = symbolCache.GetOrCreate(symbolStyle.BitmapId);

            // Calc offset (relative or absolute)
            var offsetX = symbolStyle.SymbolOffset.X;
            var offsetY = symbolStyle.SymbolOffset.Y;
            if (symbolStyle.SymbolOffset.IsRelative)
            {
                offsetX *= bitmapInfo.Width;
                offsetY *= bitmapInfo.Height;
            }
            offsetX *= symbolStyle.SymbolScale;
            offsetY *= symbolStyle.SymbolScale;

            switch (bitmapInfo.Type)
            {
                case BitmapType.Image:
                    BitmapRenderer.Draw(canvas, bitmapInfo.Image,
                        (float)destination.X, (float)destination.Y,
                        rotation: 0.0f,
                        (float)offsetX, (float)offsetY,
                        opacity: opacity);
                    break;
                //case BitmapType.Picture:
                //    SvgRenderer.Draw(canvas, bitmapInfo.Picture,
                //        (float)destination.X, (float)destination.Y,
                //        rotation,
                //        (float)offsetX, (float)offsetY,
                //        opacity: opacity, scale: (float)symbolStyle.SymbolScale);
                //    break;
                //case BitmapType.Sprite:
                //    var sprite = bitmapInfo.Sprite;
                //    if (sprite.Data == null)
                //    {
                //        var bitmapAtlas = symbolCache.GetOrCreate(sprite.Atlas);
                //        var rect = new SKRectI(sprite.X, sprite.Y,
                //                               sprite.X + sprite.Width,
                //                               sprite.Y + sprite.Height);
                //        sprite.Data = bitmapAtlas.Image.Subset(rect);
                //    }
                //    BitmapRenderer.Draw(canvas, (SKImage)sprite.Data,
                //        (float)destination.X, (float)destination.Y,
                //        rotation,
                //        (float)offsetX, (float)offsetY,
                //        opacity: opacity, scale: (float)symbolStyle.SymbolScale);
                //    break;
            }
        }
    }
}
