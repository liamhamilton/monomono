using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended.ViewportAdapters;
using System.Collections;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;



namespace MonoGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		player player = new player();

		public List<Enemy> enemies = new List<Enemy>();
		public List<collect> collectables = new List<collect>();
		public chest goal = null;
		public sprite currentCheckpoint = null;
	

		Camera2D camera = null;
		TiledMap map = null;
		TiledMapRenderer mapRenderer = null;
		TiledMapTileLayer collisionLayer;
		public ArrayList allCollisionTiles = new ArrayList();
		public sprite[,] levelGrid;

		public int tileHeight = 0;
		public int levelTileWidth = 0;
		public int levelTileHeight = 0;

		public Vector2 gravity = new Vector2(0, 1500);

		Song gameMusic;

		SpriteFont arialFont;
		public int score = 0;
		public int lives = 3;
		Texture2D heart  = null;

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

			SetUpTiles();
			LoadObjects();

			player.Load(Content, this);

			arialFont = Content.Load<SpriteFont>("Arial");
			heart = Content.Load<Texture2D>("heart 1");

			BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

			camera = new Camera2D(viewportAdapter);

			camera.Position = new Vector2(0, graphics.GraphicsDevice.Viewport.Height);

			map = Content.Load<TiledMap>("level1");
			mapRenderer = new TiledMapRenderer(GraphicsDevice);

			gameMusic = Content.Load<Song>("SuperHero_original_no_Intro");
			MediaPlayer.Play(gameMusic);

			
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

			foreach(Enemy enemy in enemies)
			{
				enemy.Update(deltaTime);
			}

			foreach (collect collect in collectables)
			{
				collect.Update(deltaTime);
			}
			goal.Update(deltaTime);

			camera.Position = player.playerSprite.position - new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			var viewMatrix = camera.GetViewMatrix();
			var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0f, -1f);

			spriteBatch.Begin(transformMatrix: viewMatrix);

			mapRenderer.Draw(map, ref viewMatrix, ref projectionMatrix);
			player.Draw(spriteBatch);
			goal.Draw(spriteBatch);

			foreach (Enemy enemy in enemies)
			{
				enemy.Draw(spriteBatch);
			}

			foreach (collect collect in collectables)
			{
				collect.Draw(spriteBatch);
			}


			spriteBatch.End();

			spriteBatch.Begin();
			spriteBatch.DrawString(arialFont, "Score:" + score.ToString(), new Vector2(20, 20), Color.Yellow);

			int loopCount = 0;
			while (loopCount < lives)
			{
				spriteBatch.Draw(heart , new Vector2(GraphicsDevice.Viewport.Width - 80 - loopCount * 20, 20), Color.White);
				loopCount++;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		public void SetUpTiles()
		{
			
			levelGrid = new sprite[levelTileWidth, levelTileHeight];

			foreach (TiledMapTileLayer layer in map.TileLayers)
			{
				if (layer.Name == "collide")
				{
					collisionLayer = layer;
				}
				
			}

			int columns = 0;
			int rows = 0;
			int loopCount = 0;

			while(loopCount < collisionLayer.Tiles.Count)
			{
				if (collisionLayer.Tiles[loopCount].GlobalIdentifier != 0)
				{
					sprite tileSprite = new sprite();
					tileSprite.position.X = columns * tileHeight;
					tileSprite.position.Y = rows * tileHeight;
					tileSprite.width = tileHeight;
					tileSprite.height = tileHeight;
					tileSprite.UpdateHitBox();
					allCollisionTiles.Add(tileSprite);
					levelGrid[columns, rows] = tileSprite;

				}

				columns++;

				if(columns == levelTileWidth)
				{
					columns = 0;
					rows++;
				}

				loopCount++;
					
			}

		}

		void LoadObjects()
		{
         foreach(TiledMapObjectLayer layer in map.ObjectLayers)
			{

				if(layer.Name == "respawn")
				{
					TiledMapObject thing = layer.Objects[0];
					if (thing != null)
					{
						sprite respawn = new sprite();
						respawn.position = new Vector2(thing.Position.X, thing.Position.Y);
						currentCheckpoint = respawn;

					}

					
						
				}
				if (layer.Name == "Enemies")
				{
					foreach(TiledMapObject thing in layer.Objects)
					{
						Enemy enemy = new Enemy();
						Vector2 tiles = new Vector2((int)(thing.Position.X / tileHeight), (int)(thing.Position.Y / tileHeight));
						enemy.enemySprite.position = tiles * tileHeight;
						enemy.Load(Content, this);
						enemies.Add(enemy);
					}
				}

				if (layer.Name == "goal")
				{
					TiledMapObject thing = layer.Objects[0];
					if (thing != null)
					{
						chest chest = new chest();
						chest.chestSprite.position = new Vector2(thing.Position.X, thing.Position.Y);
						chest.Load(Content, this);
						goal = chest;
					}
;
				}

				if (layer.Name == "Collectable")
				{

					foreach (TiledMapObject thing in layer.Objects)
					{
						collect collect = new collect();
						Vector2 tiles = new Vector2((int)(thing.Position.X / tileHeight), (int)(thing.Position.Y / tileHeight));
						collect.collectSprite.position = tiles * tileHeight;
						collect.Load(Content, this);
						collectables.Add(collect);
					}

				}
			}
		}
	}
}
