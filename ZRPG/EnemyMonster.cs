using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZRPG
{
    class EnemyMonster
    {
        //fields
        private string name;
        private Vector2 position = new Vector2(800, 100);
        private int radius;
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

        //static fields
        public static List<EnemyMonster> EnemyMonsters = new List<EnemyMonster>();

        //constructor
        public EnemyMonster()
        {
            speed = 1.1f;
            followRange = 60;
            radius = 8;
        }

        //methods
        public static bool didCollide(Vector2 otherPos, int otherRad)
        {
            foreach (var monster in EnemyMonsters)
            {
                int sum = monster.radius + otherRad;
                if (Vector2.Distance(monster.position, otherPos) < sum)
                {
                    return true;
                }
            }
            return false;
        }

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

        }

        public void Update(GameTime gameTime, Player player)
        {
            Walk(player);
            anim.Update(gameTime);
        }

    }
}
