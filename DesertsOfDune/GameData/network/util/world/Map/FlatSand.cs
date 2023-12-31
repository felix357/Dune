﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameData.network.util.world.mapField
{
    /// <summary>
    /// This class represents the MapField FlatSand
    /// </summary>
    public class FlatSand : MapField
    {
        /// <summary>
        /// Constructor of the class FlatSand
        /// </summary>
        /// <param name="hasSpice">tells weather there is spice on the MapField or not</param>
        /// <param name="isInSandstorm">tells weather the field is in a Sandstorm or not</param>
        public FlatSand(bool hasSpice, bool isInSandstorm) : base(enums.TileType.FLAT_SAND, enums.Elevation.low, hasSpice, isInSandstorm, true)
        {

        }
}
}
