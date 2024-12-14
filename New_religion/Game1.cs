using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MG_Paketik_Extention.Components;
using New_religion.Scenes;
using MG_Paketik_Extention.IO;
using MG_Paketik_Extention.DebugTools;
using MG_Paketik_Extention.Essentials.Prebuilt.Scenes;

namespace New_religion
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = true; 
        }
        protected override void Initialize()
        {
            GameCore.Exit += Exit;
            GameCore.Initialize(_graphics, GraphicsDevice, Content);
            base.Initialize();

            IsMouseVisible = true;

            KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(0, -5), Keys.W, KeybordController.InputEventType.OnHold);
            KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(-5, 0), Keys.A, KeybordController.InputEventType.OnHold);
            KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(0, 5), Keys.S, KeybordController.InputEventType.OnHold);
            KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(5, 0), Keys.D, KeybordController.InputEventType.OnHold);

            // KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(0, -125), Keys.W, KeybordController.InputEventType.OnPress);
            // KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(-125, 0), Keys.A, KeybordController.InputEventType.OnPress);
            // KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(0, 125), Keys.S, KeybordController.InputEventType.OnPress);
            // KeybordController.AddAction(() => GameCore.SceneManager.CurrentScene.GetCamera().Move(125, 0), Keys.D, KeybordController.InputEventType.OnPress);

            // KeybordController.AddAction(() => GameCore.CurrentScene.GetCamera().Distance += 0.033f, Keys.Q, KeybordController.InputEventType.OnHold);
            // KeybordController.AddAction(() => GameCore.CurrentScene.GetCamera().Distance -= 0.033f, Keys.E, KeybordController.InputEventType.OnHold);

            KeybordController.AddAction(() => GameCore._graphics.ToggleFullScreen(), Keys.F, KeybordController.InputEventType.OnRelease);

            KeybordController.AddAction(() => GameCore.Quit(), Keys.Escape, KeybordController.InputEventType.OnRelease);

            ConsoleLogger.SendInfo("<==============~NEW INSTANCE~==============>");
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameCore.LoadContent(Content);
            GameCore.SceneManager.Scenes.Add(new GameScene());
        }
        protected override void Update(GameTime gameTime)
        {
            GameCore.DeltaTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            GameCore.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameCore.Draw(_spriteBatch); 
            base.Draw(gameTime);
        }
    }
}