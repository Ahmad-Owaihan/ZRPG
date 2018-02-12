using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZRPG
{
    class Monster
    {
        //fields
        private string name;
        private Vector2 position = new Vector2(100, 100);
        private int radius;
        private bool isFollowing;
        private int followRange;
        private float speed;
        private bool isWalking;
        private Directions direction;

        public AnimatedSprite animWalkUp;
        public AnimatedSprite animWalkDown;
        public AnimatedSprite animWalkSide;
        public AnimatedSprite animIdleUp;
        public AnimatedSprite animIdleDown;
        public AnimatedSprite animIdleSide;
        public AnimatedSprite animAttack;
        public AnimatedSprite animHit;
        public AnimatedSprite animDead;
        private AnimatedSprite anim;

        //properties
        public Vector2 Position { get { return position; } }
        public AnimatedSprite Anim { get { return anim; } }
        public Directions Direction { get { return direction; } }
        public int Radius { get { return radius; } }

        //constructor
        public Monster()
        {
            isFollowing = true;
            speed = 1.3f;
            followRange = 60;
            radius = 8;
        }

        //methods
        private void Walk(Player player)
        {

            if (!isWalking)
            {
                if (direction == Directions.Up && animIdleUp != null)
                {
                    anim = animIdleUp;
                }
                if (direction == Directions.Down && animIdleDown != null)
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
                if (direction == Directions.Up && animWalkUp != null)
                {
                    anim = animWalkUp;
                }
                if (direction == Directions.Down && animWalkDown != null)
                {
                    anim = animWalkDown;
                }
                if (direction == Directions.Left || direction == Directions.Right)
                {
                    anim = animWalkSide;
                }
            }

            isWalking = false;

            if (isFollowing)
            {
                float distance = Vector2.Distance(position, player.position);
                if (followRange < distance) // away from player
                {
                    Vector2 movedirection = player.position - position;
                    movedirection.Normalize();
                    Vector2 tempPos = position;

                    tempPos += movedirection * speed;
                    if (!EnemyMonster.didCollide(tempPos, radius))
                    {
                        position += movedirection * speed;
                    }
                    else
                    {
                        position.Y += speed;
                    }

                    if (movedirection.X > movedirection.Y)
                    {
                        direction = Directions.Right;
                    }
                    if (movedirection.X < movedirection.Y)
                    {
                        direction = Directions.Left;
                    }
                    isWalking = true;
                }
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            Walk(player);
            anim.Update(gameTime);
        }

    }
}
