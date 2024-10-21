using MG_Paketik_Extention;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using static MG_Paketik_Extention.Components.GameCore;
using MG_Paketik_Extention.GUI;
using System.Collections.Generic;
using static New_religion.World.Biomes.Biomes;
using MG_Paketik_Extention.DebugTools;
using MG_Paketik_Extention.IO;
using New_religion.Interfaces;
using MG_Paketik_Extention.Visuals;
using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.Components.Assets;

namespace New_religion.World
{
    public class Hex : IGameObject, IGridElement<Hex>
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
         */
        public enum Neighbour
        {
            upper_right = 0,
            upper_left = 1,
            left = 2,
            lower_left = 3,
            lower_right = 4,
            right = 5
        }

        private Hex[] Neighbours = new Hex[6];

        #region Constants

        /// <summary>
        /// Constant
        /// </summary>
        private static Vector2 HexScale = new Vector2(71, 46);

        /// <summary>
        /// Size of the side tip of the hex
        /// </summary>
        private const int HORIZONTAL_OFFSET = 10;

        /// <summary>
        /// Distance between the top of the hex and the side tip
        /// </summary>
        private const int VERTICAL_OFFSET = 23;

        #endregion

        /// <summary>
        /// Position in a hex world
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// Position to be drawn on a scene
        /// </summary>
        private Vector2 realScenePosition;
        
        /// <summary>
        /// Inique identifier of the object
        /// </summary>
        public int ID;

        public Button mainSprite { get; private set; }

        /// <summary>
        /// The sprite with overlay decals on it
        /// </summary>
        public Sprite overlaySprite { get; private set; }

        /// <summary>
        /// The difference between hex scale and overaly scale
        /// </summary>
        Vector2 overlaySpriteOffset = new(0, 0);

        public Biome Biome { get; private set; }

        /// <summary>
        /// Default hex texture
        /// </summary>
        private string texureName = "Hex/Blank";

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
                realScenePosition.Y -= (VERTICAL_OFFSET * (position.Y / Math.Abs(position.Y)));
            }
            realScenePosition.X -= ((HORIZONTAL_OFFSET + 1) * Math.Abs(position.X)) * (position.X != 0 ? (position.X / Math.Abs(position.X)) : 0);
            
            mainSprite = new Button(realScenePosition, texureName, Color.Red, new Tag[] { Tag.Render_Static });
            mainSprite.ValidateCover += ValidateAction;
            mainSprite.OnAction += OnClick;

            overlaySprite = new Sprite("av", Vector2.One, realScenePosition, new Tag[] { Tag.Render_Static });

            KeybordController.AddAction(() => { this.SetBiome(null); }, Microsoft.Xna.Framework.Input.Keys.C);
        }

        private void OnClick(Button sender)
        {
            ConsoleLogger.SendInfo($"Pressed button at {position}");

            // <- For testing purpoces
            //SetBiome(Biome.Forest);
            //foreach (var n in GetAllNeighbours())
            //{
            //    if (n is null) continue;
            //    n.SetBiome(Biome.Lake);
            //}
        }

        private bool ValidateAction(Button sender)
        {
            // Position of the cursor on the button
            var positionOnButton = MouseController.InWorldMousePosition - sender.Position;

            // The difference between the closest tallest perpendicular line of the hex and the closest edge
            float coef = positionOnButton.Y / (HexScale.Y / 2);
            if(coef > 1)
                coef = 1 - (float)(coef - 1);

            // 0.5f prevents 1px overlap that occures due to hex structure
            float acceptableDx = (HORIZONTAL_OFFSET * (float)Math.Round(coef, 2)) - 0.5f;

            // If the cursor is off the hex borders
            if(positionOnButton.X < HORIZONTAL_OFFSET - acceptableDx 
                || positionOnButton.X > HexScale.X - HORIZONTAL_OFFSET + acceptableDx)
                return false;

            return true;
        }

        #region World generation

        /// <summary>
        /// Рекурсивно заполняет поле. Правила - см. файл CellGenRules и/или dev stream #1
        /// </summary>
        public void GenerateNeighbours(HexWorld field)
        {
            int currX = (int)position.X;
            int currY = (int)position.Y;

            // If the cell is on the border of the '-1' to '1' transition then there is a need to make an adjustment for this. 
            // That's just the side-effect of the given coordinations trnsformation
            Neighbours[(int)Neighbour.right] = TryGenerateNeighbour(currX, currY + ((Math.Abs(currX % 2) == 1 && currY == -1) ? 2 : 1), field);
            Neighbours[(int)Neighbour.left] =  TryGenerateNeighbour(currX, currY - ((Math.Abs(currX % 2) == 1 && currY == 1) ? 2 : 1), field);

            // TODO There is probably a better way of handling the corner cases
            // but as long as it does well for a single-time operation I'm cool with this
            if(currY == 0)
            {
                //This setup works fine in these cases so we remove them out of the way before all
                Neighbours[(int)Neighbour.upper_right] = TryGenerateNeighbour(currX + 1, currY + 1, field);
                Neighbours[(int)Neighbour.upper_left] = TryGenerateNeighbour(currX - 1, currY + 1, field);
                Neighbours[(int)Neighbour.lower_right] = TryGenerateNeighbour(currX + 1, currY - 1, field);
                Neighbours[(int)Neighbour.lower_left] = TryGenerateNeighbour(currX - 1, currY - 1, field);
                return;
            }
            if (currX % 2 == 0)
            {
                Neighbours[(int)Neighbour.upper_right] = TryGenerateNeighbour(currX + 1, currY + (currY  > 0? 1 : 0), field);
                Neighbours[(int)Neighbour.upper_left] = TryGenerateNeighbour(currX - 1, currY + (currY > 0 ? 1 : 0), field);
                Neighbours[(int)Neighbour.lower_right] = TryGenerateNeighbour(currX + 1, currY - (currY < 0 ? 1 : 0), field);
                Neighbours[(int)Neighbour.lower_left] = TryGenerateNeighbour(currX - 1, currY - (currY < 0 ? 1 : 0), field);
            }
            else
            {
                //If X is odd then everything is reversed
                Neighbours[(int)Neighbour.upper_right] = TryGenerateNeighbour(currX + 1, currY + (currY < 0 ? 1 : 0), field);
                Neighbours[(int)Neighbour.upper_left] = TryGenerateNeighbour(currX - 1, currY + (currY < 0 ? 1 : 0), field);
                Neighbours[(int)Neighbour.lower_right] = TryGenerateNeighbour(currX + 1, currY - (currY > 0 ? 1 : 0), field);
                Neighbours[(int)Neighbour.lower_left] = TryGenerateNeighbour(currX - 1, currY - (currY > 0 ? 1 : 0), field);
            }
        }
        
        /// <summary>
        /// Part of recursive world-generation function
        /// </summary>
        private Hex TryGenerateNeighbour(int targetX, int targetY, HexWorld field)
        {
            try
            {
                // Get a position of a hex in the array
                var ArrayPos = field.GetPositionInArray(new Vector2(targetX, targetY));
                var targetArrayX = (int)ArrayPos.X;
                var targetArrayY = (int)ArrayPos.Y;

                //If it does not exist - we make it
                if (field.mesh[targetArrayX, targetArrayY] is null)
                {
                    if (Math.Abs(targetX % 2) == 1 && targetY == 0 ||             // <- Those do not exist (explained in the 1st stream)
                        Math.Abs(targetY) > field.Radius - Math.Abs(targetX / 2)) // <- Forming a hex-like field out of smaller hexes
                    {
                        return null;
                    }

                    field.mesh[targetArrayX, targetArrayY] = new Hex(new Vector2(targetX, targetY), field.AllHexes.Count());
                    field.AllHexes.Add(field.mesh[targetArrayX, targetArrayY]);
                    field.mesh[targetArrayX, targetArrayY].GenerateNeighbours(field);
                }
                return field.mesh[targetArrayX, targetArrayY];
            }
            catch
            {
                return null;
            }
        }

        #endregion

        public void GenerateBiome(IBiomeGenerator schema)
        {
            this.SetBiome(schema.GetNextBiome(this.Biome));
        }

        public void Update()
        {
            mainSprite.Update();
            //overlaySprite.Update(); 
        }

        public Tag[] GetRenderTags()
            => mainSprite.GetRenderTags();

        public Vector2 GetUpToScalePosition(float scale)
            => mainSprite.GetUpToScaleScale(scale);

        public Vector2 GetUpToScaleScale(float scale)
            => mainSprite.GetUpToScaleScale(scale);

        public void Draw(SpriteBatch batch)
        {
            mainSprite.Draw(batch);
            //overlaySprite.Draw(batch); 
        }

        public Rectangle? GetRenderBorders()
            => overlaySprite.GetRenderBorders(); // It is usually bigger than mainSprite
        
        /// <summary>
        /// Changes this hex's biome. Use this instead of variable change
        /// </summary>
        public void SetBiome(Biome? biome)
        {
            Biome = (Biome)(biome ?? default);
            var biomeEntity = Biomes.Biomes.BiomeDict[Biome];
            mainSprite.TextureName = biomeEntity.TextureName;
            SetOveraly(biomeEntity.OveralyTextureName);
            mainSprite.ChangeColor(biomeEntity.Color);
        }

        /// <summary>
        /// Sets the overlay texture and recalculates the offset
        /// </summary>
        private void SetOveraly(string overlayTextureName)
        {
            try
            {
                var texture = CommonTextureBank.GetTexture(overlayTextureName);
                overlaySpriteOffset.X = (texture.Width - HexScale.X) / 2;
                overlaySpriteOffset.Y = (texture.Height - HexScale.Y) / 2;
                overlaySprite.Position = realScenePosition - overlaySpriteOffset;
                overlaySprite.TextureName = overlayTextureName;
            }
            catch
            {
                ConsoleLogger.SendError($"Unable to locate texture \"{overlayTextureName}\".");
            }    
        }

        #region IGridElement impl

        // Talk about it

        //AFAIK we can go nearly everywhere we want if it's not null
        public bool ValidationFunction(Hex neighbourToApprove)
            => true;

        //right now it's just GetAll but worse
        public IEnumerable<Hex> GetAccessibleNeighbours()
            => Neighbours.Where(x => ValidationFunction(x));

        public IEnumerable<Hex> GetAllNeighbours()
            => Neighbours;

        #endregion
    }
}
