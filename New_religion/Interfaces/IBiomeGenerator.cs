using New_religion.World.Biomes;
namespace New_religion.Interfaces
{
    /// <summary>
    /// Sets to use class as a biome generator algorythm. 
    /// There might be multiple ways to generate world and in this case this interface will come in handy
    /// </summary>
    public interface IBiomeGenerator
    {
        /// <summary>
        /// Generate next biome without any context
        /// </summary>
        Biomes.Biome GetNextBiome(Biomes.Biome currentBiome);

        /// <summary>
        /// Generate next biome with context
        /// </summary>
        Biomes.Biome GetNextBiome(Biomes.Biome currentBiome, params Biomes.Biome[] contextBiomes);
    }
}
