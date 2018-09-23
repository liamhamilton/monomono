using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended.ViewportAdapters;



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

		Camera2D camera = null;
		TiledMap map = null;
		TiledMapRenderer mapRenderer = null;


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

			BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

			camera = new Camera2D(viewportAdapter);

			camera.Position = new Vector2(0, graphics.GraphicsDevice.Viewport.Height);

			map = Content.Load<TiledMap>("level1");
			mapRenderer = new TiledMapRenderer(GraphicsDevice);

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

			var viewMatrix = camera.GetViewMatrix();
			var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0f, -1f);

			spriteBatch.Begin(transformMatrix: viewMatrix);

			mapRenderer.Draw(map, ref viewMatrix, ref projectionMatrix);
			player.Draw(spriteBatch);
			spriteBatch.End(); 

			base.Draw(gameTime);
		}
	}
}
