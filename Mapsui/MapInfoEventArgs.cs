﻿using System;

namespace Mapsui;

public class MapInfoEventArgs : EventArgs
{
    public MapInfoBase? MapInfo { get; set; }

    /// <summary>
    /// Number of times the user tapped the location
    /// </summary>
    public int NumTaps { get; set; }
    /// <summary>
    /// If the interaction was handled by the event subscriber
    /// </summary>
    public bool Handled { get; set; }
}
