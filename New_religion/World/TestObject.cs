using MG_Paketik_Extention;
using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.DebugTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.World
{
    internal class TestObject : IGameObject
    {
        public void Draw(SpriteBatch batch)
        {
            ;
        }

        public Rectangle? GetRenderBorders()
        {
            return null;
        }

        public GameCore.Tag[] GetRenderTags()
        {
            return null;
        }

        public Vector2 GetUpToScalePosition(float scale)
        {
            return Vector2.One;
        }

        public Vector2 GetUpToScaleScale(float scale)
        {
            return Vector2.One;
        }

        private double FPS = 0;

        public void Update()
        {
            var fps = Math.Round(1000 / GameCore.DeltaTime);
            if (FPS != fps && GameCore.DeltaTime != 0) {
                FPS = fps;
                ConsoleLogger.SendInfo($"Update: { FPS } FPS;");
            }
        }
    }
}
