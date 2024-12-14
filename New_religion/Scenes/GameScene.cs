using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.Essentials.Prebuilt.Smoothening;
using MG_Paketik_Extention.GUI;
using MG_Paketik_Extention.IO;
using MG_Paketik_Extention.Visuals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using New_religion.World;

namespace New_religion.Scenes
{
    /// <summary>
    /// Main game scene
    /// </summary>
    public class GameScene : Scene
    {

        private HexWorld _world;

        //  LAYERS:
        // 0 - Backgraund
        // 1 - Hexes
        // 2 - Hex overlays
        // 3 - Characters
        // 4 - VFX
        public GameScene() : base(4, Camera.RenderMode.SoftCameraRestricted)
        {
            MouseController.SetMousePosition(0, 0);
            this.mainCamera.SmootheningFunction = new TimedSmootheningFinction(5, 0.1f);
            _world = new HexWorld(6);

            foreach (var hex in _world.AllHexes)
            {
                if (!(hex is null))
                {
                    AddObject(1, hex);
                }
            }

            foreach (var overaly in _world.HexOverlays)
            {
                if (overaly is null)
                    continue;
                AddObject(2, overaly);
            }

            //============================ TEMP ============================
            var font = new Font("av", "cursedfont", new Vector2(30, 30), "ab");

            var label = new Label("ab ba\nba ab", font);

            AddObject(3, label);
            AddObject(0, new TestObject());
            KeybordController.AddAction(() => label.Position.Y += 0.5f, Keys.J, KeybordController.InputEventType.OnHold);
            KeybordController.AddAction(() => label.Position.X += 0.5f, Keys.K, KeybordController.InputEventType.OnHold);
            KeybordController.AddAction(() => label.changeText($"{label.Text}b"), Keys.B, KeybordController.InputEventType.OnRelease);

            var sl = GameCore.CommonSoundManager.ProduceSoundLine();

            KeybordController.AddAction(() => sl.PlayEffect("womp_womp"), Keys.I, KeybordController.InputEventType.OnRelease);
        }
    }
}
