using MG_Paketik_Extention;
using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.IO;
using MG_Paketik_Extention.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using New_religion.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.Scenes
{
    public class TestScene : Scene
    {
        HexWorld world;
        public TestScene() : base(3, Camera.RenderMode.SoftCameraRestricted)
        {
            mainCamera.Move(-543, -367);
            mainCamera.Distance = 0.6f;

            MouseController.SetMousePosition(0, 0);
            world = new HexWorld(12);
            foreach (var hex in world.AllHexes)
                AddObject(0, hex);
        }
   }
}
