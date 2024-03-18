using MG_Paketik_Extention;
using MG_Paketik_Extention.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.World
{
    public class HexWorld
    {
        #region Variables
        public int Radius;

        /// <summary>
        /// field of hexes = game world
        /// </summary>
        public Hex[,] mesh;

        /// <summary>
        /// A list of all hexes numbered
        /// </summary>
        public List<Hex> AllHexes;
        #endregion


        public HexWorld(int radius)
        {
            Radius = radius;
           
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
        }

        /// <summary>
        /// Изначально координаты идут из левого верхнего. Эта функция вернёт их в центр.
        /// </summary>

        public Vector2 GetPositionInArray(Vector2 HexPosition)
        {
            HexPosition.X += Radius +1;
            HexPosition.Y += Radius +1;
            return HexPosition;
        }
        

        #endregion
    }
}
