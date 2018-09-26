using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace wk_3_project
{
	class player
	{
		public sprite playerSprite = new sprite();

		Game1 game = null;
		float runSpeed = 15000f;

		Collision collision = new Collision();


		public player()
		{

		}

		public void Load( ContentManager content, Game1 theGame)
		{
			playerSprite.Load(content, "hero");
			game = theGame;
			playerSprite.velocity = Vector2.Zero;
			playerSprite.position = new Vector2(theGame.GraphicsDevice.Viewport.Width / 2, 0);
		}

		public void Update (float deltaTime)
		{
			UpdateInput(deltaTime);
			playerSprite.Update(deltaTime);
			playerSprite.UpdateHitBox();
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			playerSprite.Draw(spriteBatch);
		}

		private void UpdateInput (float deltaTime)
		{
			Vector2 localAcceleration = new Vector2(0, 0);

			if (Keyboard.GetState().IsKeyDown(Keys.Left) == true)
			{
				localAcceleration.X = -runSpeed;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
			{
				localAcceleration.X = runSpeed;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Up) == true)
			{
				localAcceleration.Y = -runSpeed;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Down) == true)
			{
				localAcceleration.Y = runSpeed;
			}

			foreach (sprite tile in game.allCollisionTiles)
			{
				if (Collision.IsColliding(playerSprite,tile) == true)
				{
					int testVariable = 0;
				}
			}

			playerSprite.velocity = localAcceleration * deltaTime;
			playerSprite.position += playerSprite.velocity * deltaTime;
		}
	}

	internal class Collision
	{
		
	}



	//internal class Collision
	//{
	//	internal bool IsColliding(sprite playerSprite, object title)
	//	{
	//		throw new NotImplementedException();
	//	}
	//}
}
