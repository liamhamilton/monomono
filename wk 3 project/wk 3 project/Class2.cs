using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace wk_3_project
{
	class Class2
	{
		Class1 playerSprite = new Class1();

		public Class2()
		{

		}

		public void Load( ContentManager content)
		{
			playerSprite.Load(content, "hero");
		}

		public void Update (float deltaTime)
		{
			playerSprite.Update(deltaTime);
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			playerSprite.Draw(spriteBatch);
		}
	}
}
