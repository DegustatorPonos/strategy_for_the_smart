using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MG_Paketik_Extention.GUI;
using MG_Paketik_Extention.Components;
using New_religion.Scenes;
using New_religion.World;

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
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            GameCore.graphics = _graphics;
            GameCore.Initialize(); //!
            GameCore.content = Content;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameCore.LoadContent(Content); //!
            GameCore.Scenes.Add(new TestScene()); //Add your new object enherining "Scene" object here. TestScreen is the example here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                GameCore.CurrentScene.GetCamera().Move(0, -1);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                GameCore.CurrentScene.GetCamera().Move(-1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                GameCore.CurrentScene.GetCamera().Move(0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                GameCore.CurrentScene.GetCamera().Move(1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                GameCore.CurrentScene.GetCamera().Distance += 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                GameCore.CurrentScene.GetCamera().Distance -= 0.1f;
            GameCore.Update(); //!
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameCore.Draw(_spriteBatch); //!
            base.Draw(gameTime);
        }
    }
}