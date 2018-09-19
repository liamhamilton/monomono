using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wk_3_project
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Class2 player = new Class2();

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		
		protected override void Initialize()
		{
			

			base.Initialize();
		}

		
		protected override void LoadContent()
		{
			
			spriteBatch = new SpriteBatch(GraphicsDevice);

			player.Load(Content);
		}

		
		protected override void UnloadContent()
		{
			
		}


		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			player.Update(deltaTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			player.Draw(spriteBatch);
			spriteBatch.End(); 

			base.Draw(gameTime);
		}
	}
}
