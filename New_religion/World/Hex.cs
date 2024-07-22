using MG_Paketik_Extention;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using static MG_Paketik_Extention.Components.GameCore;
using MG_Paketik_Extention.GUI;

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

        private static Vector2 HexScale = new Vector2(71, 46);

        private static int horizontalOffset = 10;

        private static int verticalOffset = 23;

        /// <summary>
        /// Position in a hex world
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// position to be drawn on a scene
        /// </summary>
        private Vector2 realScenePosition;
         
        public int ID;

        public Button mainSprite { get; private set; }

        string texureName = "Hex/Blank";

        #endregion

        /// <summary>
        /// HexWorld cell
        /// </summary>
        public Hex(Vector2 pos, int id)
        {
            //Chech if the object can be created over here and not during the creation
            position = pos;
            ID = id;            

            //Setting up real-world position. Single-time action
            realScenePosition = pos * HexScale;
            if (Math.Abs(pos.X % 2) == 1)
            {
                realScenePosition.Y -= (verticalOffset * (position.Y / Math.Abs(position.Y)));
            }
            realScenePosition.X -= ((horizontalOffset + 1) * Math.Abs(position.X)) * (position.X != 0 ? (position.X / Math.Abs(position.X)) : 0);

            mainSprite = new Button(realScenePosition, texureName, Color.Red, new Tag[] { Tag.Render_Static });
            mainSprite.ValidateCover += ValidateAction;
            mainSprite.OnCover += OnClick;
        }

        private void OnClick(Button sender)
        {

        }

        private bool ValidateAction(Button sender)
        {
            return true;
        }

        /// <summary>
        /// Рекурсивно заполняет поле. Правила - см. файл CellGenRules и/или dev stream #1
        /// </summary>
        public void GenerateNeighbours(HexWorld field)
        {
            int currX = (int)position.X;
            int currY = (int)position.Y;

            Neighbours[(int)Neighbour.right] = TryGenerateNeighbour(currX, currY + 1, field);
            Neighbours[(int)Neighbour.left] = TryGenerateNeighbour(currX, currY -1, field);

            Neighbours[(int)Neighbour.upper_right] = TryGenerateNeighbour(currX + 1, currY + 1, field);
            Neighbours[(int)Neighbour.upper_left] = TryGenerateNeighbour(currX - 1, currY + 1, field);
            Neighbours[(int)Neighbour.lower_right] = TryGenerateNeighbour(currX + 1, currY - 1, field);
            Neighbours[(int)Neighbour.lower_left] = TryGenerateNeighbour(currX - 1, currY - 1, field);
        }

        /// <summary>
        /// Part of recursive world-generation function
        /// </summary>
        private Hex TryGenerateNeighbour(int tgX, int tgY, HexWorld field)
        {
            try
            {
                var realPos = field.GetPositionInArray(new Vector2(tgX, tgY));
                var trgX = (int)realPos.X;
                var trgY = (int)realPos.Y;
                if (field.mesh[trgX, trgY] is null)
                {
                    if (Math.Abs(tgX % 2) == 1 && tgY == 0 || //those do not exist (explained in the 1st stream)
                        Math.Abs(tgY) > field.Radius - Math.Abs(tgX / 2)) //forming a hex-like field out of smaller hexes
                    {
                        return null;
                    }

                    field.mesh[trgX, trgY] = new Hex(new Vector2(tgX, tgY), field.AllHexes.Count());
                    field.AllHexes.Add(field.mesh[trgX, trgY]);
                    field.mesh[trgX, trgY].GenerateNeighbours(field);
                }
                return field.mesh[trgX, trgY];
            }
            catch
            {
                return null;
            }
        }

        public void Update()
            => mainSprite.Update();

        public Tag[] GetRenderTags()
            => mainSprite.GetRenderTags();

        public Vector2 GetUpToScalePosition(float scale)
            => mainSprite.GetUpToScaleScale(scale);

        public Vector2 GetUpToScaleScale(float scale)
            => mainSprite.GetUpToScaleScale(scale);

        public void Draw(SpriteBatch batch)
            => mainSprite.Draw(batch); 

        public Rectangle? GetRenderBorders()
            => mainSprite.GetRenderBorders();
    }
}
