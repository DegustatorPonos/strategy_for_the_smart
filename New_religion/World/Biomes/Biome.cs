using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.World.Biomes
{
    /// <summary>
    /// The type of the hex
    /// </summary>
    public class Biome
    {
        /// <summary>
        /// The name of the base texture in texture bank
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        /// The name of the overlay texture in texture bank
        /// </summary>
        public string OveralyTextureName { get; set; }

        /// <summary>
        /// The layer that the overlay must be drawn. The bigger the higher the priority
        /// </summary>
        public byte OverlayWeight = 0;

        /// <summary>
        /// Enum value
        /// </summary>
        public Biomes.Biome Identifier { get; set; }

        /// <summary>
        /// Overlay color (for debug purposes). Default - white
        /// </summary>
        public Color Color { get; set; } = Color.White;
    }
}
