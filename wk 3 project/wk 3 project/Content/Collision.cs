﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame
{
	class Collision
	{
		public Game1 game;

		public bool IsColliding(sprite hero, sprite otherSprite)
		{
			//compare the positionof each rectangle edge against the other
			//It compares opposite edgesof each rectangle, ie,the left edge of
			//the right of the other
			if (hero.rightEdge < otherSprite.leftEdge ||
				hero.leftEdge > otherSprite.rightEdge ||
				hero.bottomEdge < otherSprite.topEdge ||
				hero.topEdge > otherSprite.bottomEdge)
			{
				//these rectangles are NOT colliding
				return false;
			}
			//else, the two AABB rectangles are overlapping and there is a collision
			return true;
		}

		public sprite CollideWithPlatforms(sprite hero, float deltaTime)
		{


			sprite playerPrediction = new sprite();
			playerPrediction.position = hero.position;
			playerPrediction.width = hero.width;
			playerPrediction.height = hero.height;
			playerPrediction.offset = hero.offset;
			playerPrediction.UpdateHitBox();

			playerPrediction.position += hero.velocity * deltaTime;

			int playerColumn = (int)playerPrediction.position.X / game.tileHeight;
			int playerRow = (int)playerPrediction.position.Y / game.tileHeight;
			Vector2 playerTile = new Vector2(playerColumn, playerRow);

			Vector2 leftTile = new Vector2(playerTile.X - 1, playerTile.Y);
			Vector2 rightTile = new Vector2(playerTile.X + 1, playerTile.Y);
			Vector2 topTile = new Vector2(playerTile.X, playerTile.Y - 1);
			Vector2 bottomTile = new Vector2(playerTile.X, playerTile.Y + 1);

			Vector2 bottomLeftTile = new Vector2(playerTile.X - 1, playerTile.Y + 1);
			Vector2 bottomRightTile = new Vector2(playerTile.X + 1, playerTile.Y + 1);
			Vector2 topLeftTile = new Vector2(playerTile.X - 1, playerTile.Y - 1);
			Vector2 topRightTile = new Vector2(playerTile.X + 1, playerTile.Y - 1);

			bool leftCheck = CheckForTile(game, leftTile);
			bool rightCheck = CheckForTile(game, rightTile);
			bool bottomCheck = CheckForTile(game, bottomTile);
			bool topCheck = CheckForTile(game, topTile);

			bool bottomLeftCheck = CheckForTile(game, bottomLeftTile);
			bool bottomRightCheck = CheckForTile(game, bottomRightTile);
			bool topLeftCheck = CheckForTile(game, topLeftTile);
			bool topRightCheck = CheckForTile(game, topRightTile);



			if (leftCheck == true)
			{
				hero = CollideLeft(game, hero, leftTile, playerPrediction);
			}

			if (rightCheck == true)
			{
				hero = CollideRight(game, hero, rightTile, playerPrediction);
			}
			if (bottomCheck == true)
			{
				hero = CollideBelow(game, hero, bottomTile, playerPrediction);
			}
			if (topCheck == true)
			{
				hero = CollideAbove(game, hero, topTile, playerPrediction);
			}

			if (leftCheck == false && bottomCheck == false && bottomLeftCheck == true)
			{
				hero = CollideBottomDiagonals(hero, bottomLeftTile, playerPrediction);
			}

			if (rightCheck == false && bottomCheck == false && bottomRightCheck == true)
			{
				hero = CollideBottomDiagonals(hero, bottomRightTile, playerPrediction);
			}
			if (leftCheck == false && topCheck == false && topLeftCheck == true)
			{
				hero = CollideAboveDiagonals(hero, topLeftTile, playerPrediction);
			}

			if (rightCheck == false && topCheck == false && topRightCheck == true)
			{
				hero = CollideAboveDiagonals(hero, topRightTile, playerPrediction);
			}
			return hero;

		}

		bool CheckForTile(Game1 game, Vector2 coordinates)
		{
			int column = (int)coordinates.X;
			int row = (int)coordinates.Y;

			if (column < 0 || column > game.levelTileWidth - 1)
			{
				return false;
			}
			if (row < 0 || row > game.levelTileHeight - 1)
			{
				return false;
			}

			sprite tileFound = game.levelGrid[column, row];

			if (tileFound != null)
			{
				return true;
			}

			return false;
		}

		sprite CollideLeft(Game1 game, sprite hero, Vector2 tileIndex, sprite playerPrediction)
		{
			sprite tile = game.levelGrid[(int)tileIndex.X, (int)tileIndex.Y];

			if (IsColliding(playerPrediction, tile) == true && hero.velocity.X < 0)
			{
				hero.position.X = tile.rightEdge + hero.offset.X;
				hero.velocity.X = 0;
			}

			return hero;
		}
		sprite CollideRight(Game1 game, sprite hero, Vector2 tileIndex, sprite playerPrediction)
		{
			sprite tile = game.levelGrid[(int)tileIndex.X, (int)tileIndex.Y];

			if (IsColliding(playerPrediction, tile) == true && hero.velocity.X > 0)
			{
				hero.position.X = tile.leftEdge - hero.width + hero.offset.X;
				hero.velocity.X = 0;
			}

			return hero;
		}
		sprite CollideAbove(Game1 game, sprite hero, Vector2 tileIndex, sprite playerPrediction)
		{
			sprite tile = game.levelGrid[(int)tileIndex.X, (int)tileIndex.Y];

			if (IsColliding(playerPrediction, tile) == true && hero.velocity.Y < 0)
			{
				hero.position.Y = tile.bottomEdge + hero.offset.Y;
				hero.velocity.Y = 0;
			}

			return hero;
		}
		sprite CollideBelow(Game1 game, sprite hero, Vector2 tileIndex, sprite playerPrediction)
		{
			sprite tile = game.levelGrid[(int)tileIndex.X, (int)tileIndex.Y];

			if (IsColliding(playerPrediction, tile) == true && hero.velocity.X > 0)
			{
				hero.position.Y = tile.topEdge - hero.height + hero.offset.Y;
				hero.velocity.Y = 0;
			}

			return hero;
		}

		sprite CollideBottomDiagonals(sprite hero, Vector2 tileIndex, sprite playerPrediction)
		{
			sprite tile = game.levelGrid[(int)tileIndex.X, (int)tileIndex.Y];

			int leftEdgeDistance = Math.Abs(tile.leftEdge - playerPrediction.rightEdge);
			int rightEdgeDistance = Math.Abs(tile.rightEdge - playerPrediction.leftEdge);
			int topEdgeDistance = Math.Abs(tile.rightEdge - playerPrediction.bottomEdge);

			if (IsColliding(playerPrediction, tile) == true)
			{
				if (topEdgeDistance < rightEdgeDistance && topEdgeDistance < leftEdgeDistance)
				{
					hero.position.Y = tile.topEdge - hero.height + hero.offset.Y;
					hero.velocity.Y = 0;
				}
				else if (rightEdgeDistance < leftEdgeDistance)
				{
					hero.position.X = tile.rightEdge + hero.offset.X;
					hero.velocity.X = 0;
				}
				else
				{
					hero.position.X = tile.leftEdge  + hero.offset.X;
					hero.velocity.X = 0;
				}
			}
			return hero;
		}

		sprite CollideAboveDiagonals(sprite hero, Vector2 tileIndex, sprite playerPrediction)
		{
			sprite tile = game.levelGrid[(int)tileIndex.X, (int)tileIndex.Y];

			int leftEdgeDistance = Math.Abs(tile.rightEdge - playerPrediction.leftEdge);
			int rightEdgeDistance = Math.Abs(tile.rightEdge - playerPrediction.rightEdge);
			int bottomEdgeDistance = Math.Abs(tile.rightEdge - playerPrediction.topEdge);

			if (IsColliding(playerPrediction, tile) == true)
			{
				if (bottomEdgeDistance < leftEdgeDistance && bottomEdgeDistance < rightEdgeDistance)
				{
					hero.position.Y = tile.bottomEdge + hero.offset.Y;
					hero.velocity.Y = 0;
				}

				else if (leftEdgeDistance < rightEdgeDistance)
				{
					hero.position.X = tile.rightEdge + hero.offset.X;
					hero.velocity.X = 0;
				}
				else
				{
					hero.position.X = tile.leftEdge + hero.offset.X;
					hero.velocity.X = 0;
				}

			}
			return hero;

		}
	}
}
