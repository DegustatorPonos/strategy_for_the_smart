using MG_Paketik_Extention.Components;
using MG_Paketik_Extention.IO;
using New_religion.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
        // 2 - Characters
        // 3 - VFX
        public GameScene() : base(4, Camera.RenderMode.SoftCameraRestricted)
        {
            MouseController.SetMousePosition(0, 0);

            _world = new HexWorld(6);

            foreach (var hex in _world.AllHexes)
                if (!(hex is null))
                    AddObject(1, hex);
        }


    }
}
