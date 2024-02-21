﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MG_Paketik_Extention.GUI;
using MG_Paketik_Extention.Components;
using New_religion.Scenes;

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