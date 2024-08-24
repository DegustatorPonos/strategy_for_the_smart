using New_religion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.World.Biomes
{

    /// <summary>
    /// The simplest biome generation schema. Returns a random value. Default one for now.
    /// </summary>
    public class DumbGenerator : IBiomeGenerator
    {
        public Biomes.Biome GetNextBiome(Biomes.Biome currentBiome)
        {
            var rand = new Random();
            return Biomes.biomes[rand.Next(0, Biomes.biomes.Length)];
        }

        public Biomes.Biome GetNextBiome(Biomes.Biome currentBiome, params Biomes.Biome[] contextBiomes)
        {
            return GetNextBiome(currentBiome);
        }
    }
}
