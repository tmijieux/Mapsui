// ReSharper disable NonReadonlyMemberInGetHashCode // todo: Fix this real issue
namespace Mapsui.Styles
{
    public class VectorStyle : Style
    {
        public VectorStyle()
        {
        }

        /// <summary>
        /// Linestyle for line geometries
        /// </summary>
        public Pen Line { get; set; } = new Pen { Color = Color.Gray, Width = 1 };

        /// <summary>
        /// Outline style for line and polygon geometries
        /// </summary>
        public Pen Outline { get; set; } = new Pen { Color = Color.Black, Width = 1 };

        /// <summary>
        /// Fillstyle for Polygon geometries
        /// </summary>
        public Brush Fill { get; set; } = new Brush { Color = Color.White };


        protected void SetAndInvalidateIfChanged<T>(ref T backingField, T value)
        {
            if (backingField == null)
            {
                if (value != null)
                {
                    backingField = value;
                    Invalidated = true;
                }
                 return;
            }

            if (!backingField.Equals(value))
            {
                backingField = value;
                Invalidated = true;
            }
        }

        // renderer implementation
        public virtual bool Invalidated { get; set; }
        public object _rendererImpl;
    }
}
