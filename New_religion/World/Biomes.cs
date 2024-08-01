using MG_Paketik_Extention;
using MG_Paketik_Extention.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.World
{
    public static class Biomes
    {
        /// <summary>
        /// Biome the title can be. Converts to its heat where positive is warmer and negative is colder
        /// </summary>
        public enum Biome
        {
            Winter,
            Plane,
            Forest,
            Lake,
            Desert

        }

        
        // Dictionaries really don't like when you assign enums with 
        // values that do repeat themselves. Classic C# thing - it makes sence but WHY 

        /// <summary>
        /// Might or might not be temporary and for debug purpoces
        /// </summary>
        public static Dictionary<Biome, Color> BiomeColors = new()
        {
            {Biome.Winter, Color.White},
            {Biome.Plane, Color.Lime},
            {Biome.Forest, Color.Green},
            {Biome.Lake, Color.Blue},
            {Biome.Desert, Color.Yellow},
        };

        /// <summary>
        /// Might or might not be temporary and for debug purpoces. Returns the heat of the given biome
        /// </summary>
        public static Dictionary<Biome, int> BiomeHeatmap = new()
        {
            {Biome.Winter, -1},
            {Biome.Plane, 1},
            {Biome.Forest, 1},
            {Biome.Lake, 0},
            {Biome.Desert, 2},
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
