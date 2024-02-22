using MG_Paketik_Extention;
using MG_Paketik_Extention.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MG_Paketik_Extention.Components.GameCore;

namespace New_religion.World
{
    public class Hex : IGameObject
    {
        #region Variables (+ ASCII vagina)

        /*
         *  1  /\  0
         *    /()\
         *   |    |  
         * 2 |    |  5
         *    \  /
         *  3  \/  4
         *     
         * */
        public enum Neighbour
        {
            upper_right = 0,
            upper_left = 1,
            left = 2,
            lower_left = 3,
            lower_right = 4,
            right = 5
        }

        public Hex[] Neighbours = new Hex[6];

        public Vector2 position;
        #endregion

        /// <summary>
        /// HexWorld cell
        /// </summary>
        public Hex(Vector2 pos)
        {
            position = pos;
        }


        /// <summary>
        /// Рекурсивно заполняет поле. Правила - см. файл CellGenRules и/или dev stream #1
        /// </summary>
        /// <param name="field"></param>
        public void GenerateNeighbours(HexWorld field)
        {
            //var realPos = field.GetPositionInArray(position);
            int currX = (int)position.X;
            int currY = (int)position.Y;
            if ((int)position.Y % 2 == 0) //Potentially make the game faster
            {
                Neighbours[(int)Neighbour.right] = TryGenerateNeighbour(currX + 1, currY, field);
                Neighbours[(int)Neighbour.left] = TryGenerateNeighbour(currX - 1, currY, field);
            }
            else
            {
                Neighbours[(int)Neighbour.right] = TryGenerateNeighbour(currX + currX + 1 == 0? 2 : 1, currY, field);
                Neighbours[(int)Neighbour.right] = TryGenerateNeighbour(currX - currX - 1 == 0 ? 2 : 1, currY, field);
            }

            if (currX == 0)
            {
                Neighbours[(int)Neighbour.upper_right] = TryGenerateNeighbour(currX + 1, currY + 1, field);
                Neighbours[(int)Neighbour.upper_left] = TryGenerateNeighbour(currX - 1, currY + 1, field);
                Neighbours[(int)Neighbour.lower_right] = TryGenerateNeighbour(currX + 1, currY - 1, field);
                Neighbours[(int)Neighbour.lower_left] = TryGenerateNeighbour(currX - 1, currY - 1, field);
            }
            else
            {
                Neighbours[(int)Neighbour.upper_right] = TryGenerateNeighbour(currX + 1, currY + 1, field);
                Neighbours[(int)Neighbour.upper_left] = TryGenerateNeighbour(currX, currY + 1, field);
                Neighbours[(int)Neighbour.lower_right] = TryGenerateNeighbour(currX + 1, currY - 1, field);
                Neighbours[(int)Neighbour.lower_left] = TryGenerateNeighbour(currX, currY - 1, field);
            }
            
                
        }

        private Hex TryGenerateNeighbour(int tgX, int tgY, HexWorld field)
        {
            try
            {
                var realPos = field.GetPositionInArray(new Vector2(tgX, tgY));
                var trgX = (int)realPos.X;
                var trgY = (int)realPos.Y;
                if (field.mesh[trgX, trgY] is null)
                {
                    field.mesh[trgX, trgY] = new Hex(new Vector2(tgX, tgY));
                    field.mesh[trgX, trgY].GenerateNeighbours(field);
                }
                return field.mesh[trgX, trgY];
            }
            catch
            {
                return default;
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public GameCore.Tag[] GetRenderTags()
        {
            throw new NotImplementedException();
        }

        public Microsoft.Xna.Framework.Vector2 GetUpToScalePosition(float scale)
        {
            throw new NotImplementedException();
        }

        public Microsoft.Xna.Framework.Vector2 GetUpToScaleScale(float scale)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public Microsoft.Xna.Framework.Rectangle? GetRenderBorders()
        {
            throw new NotImplementedException();
        }
    }
}
