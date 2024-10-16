using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.DebugTools;
using MG_Paketik_Extention.IO;
using MG_Paketik_Extention.Visuals;
using New_religion.World;

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
        }


        private new void Update()
        {

        }
    }
}
