using System;
using Mapsui.Utilities;

// ReSharper disable NonReadonlyMemberInGetHashCode // todo: Fix this real issue
namespace Mapsui.Styles
{
    public enum SymbolType
    {
        Ellipse,
        Rectangle,
        Triangle
    }

    // todo: derive SymbolStyle from VectorStyle after v2.
    public class SymbolStyle : ImageStyle
    {
        public static double DefaultWidth { get; set; } = 32;
        public static double DefaultHeight { get; set; } = 32;
        public SymbolType SymbolType { get; set; }
    }
}
