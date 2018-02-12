using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZRPG
{
    class Player
    {
        //fields
        public AnimatedSprite animWalkUp;
        public AnimatedSprite animWalkDown;
        public AnimatedSprite animWalkSide;
        public AnimatedSprite animIdleUp;
        public AnimatedSprite animIdleDown;
        public AnimatedSprite animIdleSide;
        private AnimatedSprite anim;

        public Vector2 position = new Vector2(100, 100);
        public Directions direction;
        private float speed = 1.5f;
        private bool isWalking;
        private int radius = 8;

        //properties
        public int Radius { get { return radius; } }
        public AnimatedSprite Anim { get { return anim; } }

        //constructor
        public Player()
        {

        }

        //methods
        private void Walk()
        {

            if (!isWalking)
            {
                if (direction == Directions.Up)
                {
                    anim = animIdleUp;
                }
                if (direction == Directions.Down)
                {
                    anim = animIdleDown;
                }
                if (direction == Directions.Right || direction == Directions.Left)
                {
                    anim = animIdleSide;
                }
            }
            else
            {
                if (direction == Directions.Up)
                {
                    anim = animWalkUp;
                }
                if (direction == Directions.Down)
                {
                    anim = animWalkDown;
                }
                if (direction == Directions.Left || direction == Directions.Right)
                {
                    anim = animWalkSide;
                }
            }

            isWalking = false;

            KeyboardState mouse = Keyboard.GetState();
            Vector2 tempPos = position;

            if (mouse.IsKeyDown(Keys.Right))
            {
                tempPos.X += speed;
                if (!EnemyMonster.didCollide(tempPos, radius))
                {
                    position.X += speed;
                    direction = Directions.Right;
                    isWalking = true;
                }
            }
            if (mouse.IsKeyDown(Keys.Left))
            {
                tempPos.X -= speed;
                if (!EnemyMonster.didCollide(tempPos, radius))
                {
                    position.X -= speed;
                    direction = Directions.Left;
                    isWalking = true;
                }
            }
            if (mouse.IsKeyDown(Keys.Up))
            {
                tempPos.Y -= speed;
                if (!EnemyMonster.didCollide(tempPos, radius))
                {
                    position.Y -= speed;
                    direction = Directions.Up;
                    isWalking = true;
                }
            }
            if (mouse.IsKeyDown(Keys.Down))
            {
                tempPos.Y += speed;
                if (!EnemyMonster.didCollide(tempPos, radius))
                {
                    position.Y += speed;
                    direction = Directions.Down;
                    isWalking = true;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            Walk();
            anim.Update(gameTime);
        }
    }
}
