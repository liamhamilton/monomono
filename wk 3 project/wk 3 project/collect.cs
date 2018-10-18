using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame
{
	public class collect
	{
		public sprite collectSprite = new sprite();
		Collision collision = new Collision();
		Game1 game = null;

		public void Load(ContentManager content, Game1 theGame)
		{
			game = theGame;

			AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0, 1, 1);
			animation.Load(content, "collect", 1, 1);

			collectSprite.AddAnimation(animation, 0, 3);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			collectSprite.Draw(spriteBatch);
		}
		public void Update(float deltaTime)
		{
			collision.game = game;
			collectSprite.UpdateHitBox();
		}
	}
}
