using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace New_religion.World.Biomes
{
    public static class Biomes
    {
        /// <summary>
        /// Biome the title can be. Converts to its heat where positive is warmer and negative is colder
        /// </summary>
        public enum Biome
        {
            Field = 0,
            Mountains = 1,
            Swamp = 2,
            Forest = 3,
            //None = 1000,
        }

        public static Dictionary<Biome, World.Biomes.Biome> BiomeDict = new()
        {
            { Biome.Field, new() 
                { Identifier = Biome.Field, Color = Color.White, TextureName = "Hex/Field", OveralyTextureName = "Hex/Overlays/Field", OverlayWeight = 0 } },
            { Biome.Mountains, new() 
                { Identifier = Biome.Mountains, Color = Color.White, TextureName = "Hex/Mountains", OveralyTextureName = "Hex/Overlays/Mountains", OverlayWeight = 2 } },
            { Biome.Swamp, new() 
                { Identifier = Biome.Swamp, Color = Color.White, TextureName = "Hex/Swamp", OveralyTextureName = "Hex/Overlays/Swamp", OverlayWeight = 0 } },
            { Biome.Forest, new()
                { Identifier = Biome.Forest, Color = Color.White, TextureName = "Hex/Forest", OveralyTextureName = "Hex/Overlays/Forest", OverlayWeight = 1 } },
        };

        /// <summary>
        /// Might or might not be temporary and for debug purpoces. Returns the heat of the given biome
        /// </summary>
        public static Dictionary<Biome, int> BiomeHeatmap = new()
        {
            {Biome.Field, 0},
        };

        /// <summary>
        /// All possible biomes
        /// </summary>
        public static Biome[] biomes =>
            Enum.GetValues(typeof(Biome)) as Biome[];

        /// <summary>
        /// Returns the array of biomes sotresd by probability of spawning nearby the given one
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Biome> GetRelativeBiomes(Biome startBiome)
        {
            return biomes.OrderBy(x => GetRelativeHeat(x, startBiome));
        }

        /// <summary>
        /// Returns the array of biomes sotresd by probability of spawning nearby the given one
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Tuple<Biome, int>> GetRelativeBiomesWithValues(Biome startBiome)
        {
            return biomes.Select(x => new Tuple<Biome, int>(x, GetRelativeHeat(x, startBiome))).OrderBy(x => x.Item2);
        }

        /// <summary>
        /// Function to determine the relative heat of two biomes
        /// </summary>
        private static Func<Biome, Biome, int> GetRelativeHeat = (a, b) => BiomeHeatmap[a] - BiomeHeatmap[b];

        /// <summary>
        /// Determines the next biome to generate 
        /// </summary>
        /// <param name="startBiome"></param>
        /// <returns></returns>
        public static Biome GetNextBiome(Biome startBiome)
        {
            var possibilities = GetRelativeBiomesWithValues(startBiome).ToArray();
            if (possibilities is null || possibilities.Length == 0) 
                throw new NullReferenceException("Somehow there are no biomes initailized HOW THE FUCK");

            //Biome selection algorythm
            var coeffitients = possibilities.Select(x => Math.Pow(2, x.Item2)).OrderBy(x => x).ToArray();

            var selectedCcoeffitient = new Random().Next(0, (int)coeffitients.Last());

            // TODO

            //Select the closest items to the selected coefficient and choose the random one

            //wrong
            return possibilities.Select(x => x.Item1).FirstOrDefault();

        }
    }
}
