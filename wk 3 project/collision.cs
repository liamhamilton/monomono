<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wk_3_project.Content
{
	class Collision
	{
		public Game1 game;

		public bool IsColliding(sprite hero, sprite otherSprite)
		{



			if (hero.rightEdge < otherSprite.leftEdge ||
				hero.leftEdge > otherSprite.rightEdge ||
				hero.bottomEdge < otherSprite.bottomEdge ||
				hero.topEdge > otherSprite.bottomEdge)
			{

				return false;
			}


			return true;
		}

	}
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wk_3_project.Content
{
	class Collision
	{
		public Game1 game;

		public bool IsColliding(sprite hero, sprite otherSprite)
		{



			if (hero.rightEdge < otherSprite.leftEdge ||
				hero.leftEdge > otherSprite.rightEdge ||
				hero.bottomEdge < otherSprite.bottomEdge ||
				hero.topEdge > otherSprite.bottomEdge)
			{

				return false;
			}


			return true;
		}

	}
}
>>>>>>> 8fc244606064e08908ab2f4aeadea30880342ef3
