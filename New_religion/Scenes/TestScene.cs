using Assimp.Configs;
using MG_Paketik_Extention;
using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.DebugTools;
using MG_Paketik_Extention.IO;
using MG_Paketik_Extention.Visuals;
using Microsoft.Xna.Framework.Input;
using New_religion.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace New_religion.Scenes
{
    public class TestScene : Scene
    {
        HexWorld world;

        private Sprite MouseSprite;

        public TestScene() : base(3, Camera.RenderMode.SoftCameraRestricted)
        {
            this.UpdatePartial = Update;

            //MouseSprite = new Sprite("Cursor", Vector2.One, Vector2.Zero, new GameCore.Tag[] { GameCore.Tag.Render_Static });

            ConsoleLogger.SendInfo("=============NEW INSTANCE============");

            ConsoleLogger.SendInfo($"Camera at {mainCamera.Position} - zoom {mainCamera.Distance} - mouse at {MouseController.InWorldMousePosition}");
            //mainCamera.Move(-543, -367);
            //mainCamera.Distance = 0.5f;

            //MouseController.SetMousePosition(mainCamera.Center.X, mainCamera.Center.Y);

            ConsoleLogger.SendInfo("World initiated");

            MouseController.SetMousePosition(0, 0);

            ConsoleLogger.SendInfo($"Camera at {mainCamera.Position} - zoom {mainCamera.Distance} - mouse at {MouseController.InWorldMousePosition}");
            world = new HexWorld(12);
            
            foreach (var hex in world.AllHexes)
                if(!(hex is null))
                    AddObject(0, hex);
            
            //AddObject(0, MouseSprite);
            KeybordController.AddAction(GetMoudeDebugInfo, Keys.U, KeybordController.InputEventType.OnPress);
        }

        public void GetMoudeDebugInfo()
        {
            ConsoleLogger.SendInfo("<------------>");
            ConsoleLogger.SendInfo($"Camera  | ");
        }

        private new void Update()
        {
            //MouseSprite.Position.X = MouseController.MouseRectangle.X;// - (GameCore.ScreenSize.X / mainCamera.Distance); // MouseController.OnScreenMousePosition.X + mainCamera.Position.X;
            //MouseSprite.Position.Y = MouseController.MouseRectangle.Y;// - (GameCore.ScreenSize.Y * mainCamera.Distance); //MouseController.OnScreenMousePosition.Y + mainCamera.Position.Y;
        }
    }
}
