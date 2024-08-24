using Assimp;
using Microsoft.Xna.Framework;
using New_religion.Interfaces;
using New_religion.World.Biomes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace New_religion.World
{
    public class HexWorld
    {
        #region Variables

        /// <summary>
        /// D = 2*Radius + 1
        /// </summary>
        public int Radius;

        /// <summary>
        /// field of hexes = game world
        /// </summary>
        public Hex[,] mesh;

        /// <summary>
        /// A list of all hexes numbered
        /// </summary>
        public List<Hex> AllHexes;

        /// <summary>
        /// The ruleset whe world follows while generating
        /// </summary>
        public IBiomeGenerator BiomeGeneratonSchema;    
        #endregion

        public HexWorld(int radius, IBiomeGenerator biomeGenerator = null)
        {
            Radius = radius;

            if (biomeGenerator == null)
            {
                BiomeGeneratonSchema = new DumbGenerator();
            }
            else BiomeGeneratonSchema = biomeGenerator;
            
            AllHexes = new List<Hex>(radius * radius);
            RegenerateWorld();
        }
        #region Functions

        /// <summary>
        /// Generates a world from scratch
        /// </summary>
        public void RegenerateWorld()
        {
            var mesh_borders = (Radius * 2) + 1;
            mesh = new Hex[mesh_borders, mesh_borders];
            Vector2 center = GetPositionInArray(Vector2.Zero);
            mesh[(int)center.X, (int)center.Y] = new Hex(Vector2.Zero, 0);
            AllHexes.Add(mesh[(int)center.X, (int)center.Y]);
            mesh[(int)center.X, (int)center.Y].GenerateNeighbours(this);
            // We generate biomes after we ensure every hex is created and has the context of its neighbours
            foreach(var hex in mesh)
            {
                if(hex is null) continue;
                hex.GenerateBiome(BiomeGeneratonSchema);
            }
        }

        /// <summary>
        /// Изначально координаты идут из левого верхнего. Эта функция вернёт их в центр.
        /// </summary>
        public Vector2 GetPositionInArray(Vector2 HexPosition)
        {
            HexPosition.X += Radius;
            HexPosition.Y += Radius;
            return HexPosition;
        }
        
        #endregion
    }
}
