using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace ZRPG
{
    enum Directions
    {
        Right,
        Left,
        Up,
        Down
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        Player player = new Player();
        Monster monster = new Monster();
        EnemyMonster enemyMonster = new EnemyMonster();

        Texture2D heroUp_Sprite;
        Texture2D heroDown_Sprite;
        Texture2D heroSide_Sprite;
        Texture2D heroIdleUp_Sprite;
        Texture2D heroIdleDown_Sprite;
        Texture2D heroIdleSide_Sprite;

        Texture2D skeletonAttack_Sprite;
        Texture2D skeletonDead_Sprite;
        Texture2D skeletonHit_Sprite;
        Texture2D skeletonIdle_Sprite;
        Texture2D skeletonWalk_Sprite;
        Texture2D skeletonReact_Sprite;

        //map
        TiledMapRenderer mapRenderer;
        TiledMap firstMap;

        //camera
        Camera2D cam;

        protected override void Initialize()
        {
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            cam = new Camera2D(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            heroUp_Sprite = Content.Load<Texture2D>("Hero/hero-back-walk");
            heroDown_Sprite = Content.Load<Texture2D>("Hero/hero-walk-front");
            heroSide_Sprite = Content.Load<Texture2D>("Hero/hero-walk-side");
            heroIdleUp_Sprite = Content.Load<Texture2D>("Hero/hero-idle-back");
            heroIdleDown_Sprite = Content.Load<Texture2D>("Hero/hero-idle-front");
            heroIdleSide_Sprite = Content.Load<Texture2D>("Hero/hero-idle-side");

            player.animWalkUp = new AnimatedSprite(heroUp_Sprite, 1, 6);
            player.animWalkDown = new AnimatedSprite(heroDown_Sprite, 1, 6);
            player.animWalkSide = new AnimatedSprite(heroSide_Sprite, 1, 6);
            player.animIdleUp = new AnimatedSprite(heroIdleUp_Sprite, 1, 1);
            player.animIdleDown = new AnimatedSprite(heroIdleDown_Sprite, 1, 1);
            player.animIdleSide = new AnimatedSprite(heroIdleSide_Sprite, 1, 1);

            skeletonAttack_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Attack");
            skeletonDead_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Dead");
            skeletonHit_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Hit");
            skeletonIdle_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Idle");
            skeletonReact_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton React");
            skeletonWalk_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Walk");

            //my monster
            monster.animAttack = new AnimatedSprite(skeletonAttack_Sprite, 1, 18);
            monster.animDead = new AnimatedSprite(skeletonDead_Sprite, 1, 15);
            //monster.animReact = new AnimatedSprite(skeletonReact_Sprite, 1, 4);
            monster.animWalkSide = new AnimatedSprite(skeletonWalk_Sprite, 1, 13);
            monster.animHit = new AnimatedSprite(skeletonHit_Sprite, 1, 8);
            monster.animIdleSide = new AnimatedSprite(skeletonIdle_Sprite, 1, 11);

            //enemy monster
            EnemyMonster.EnemyMonsters.Add(enemyMonster);

            enemyMonster.animAttack = new AnimatedSprite(skeletonAttack_Sprite, 1, 18);
            enemyMonster.animDead = new AnimatedSprite(skeletonDead_Sprite, 1, 15);
            //monster.animReact = new AnimatedSprite(skeletonReact_Sprite, 1, 4);
            enemyMonster.animWalkSide = new AnimatedSprite(skeletonWalk_Sprite, 1, 13);
            enemyMonster.animHit = new AnimatedSprite(skeletonHit_Sprite, 1, 8);
            enemyMonster.animIdleSide = new AnimatedSprite(skeletonIdle_Sprite, 1, 11);

            //maps
            firstMap = Content.Load<TiledMap>("Maps/firstMap");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            monster.Update(gameTime, player);
            enemyMonster.Update(gameTime, player);

            cam.LookAt(player.position);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mapRenderer.Draw(firstMap, cam.GetViewMatrix());

            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix());

            if (player.direction == Directions.Up || player.direction == Directions.Down)
                player.Anim.Draw(spriteBatch, new Vector2(player.position.X - player.Radius, player.position.Y - player.Radius), SpriteEffects.None, Color.White);
            if (player.direction == Directions.Left)
                player.Anim.Draw(spriteBatch, new Vector2(player.position.X - player.Radius, player.position.Y - player.Radius), SpriteEffects.FlipHorizontally, Color.White);
            if (player.direction == Directions.Right)
                player.Anim.Draw(spriteBatch, new Vector2(player.position.X - player.Radius, player.position.Y - player.Radius), SpriteEffects.None, Color.White);

            if (monster.Direction == Directions.Up || monster.Direction == Directions.Down)
                monster.Anim.Draw(spriteBatch, new Vector2(monster.Position.X - monster.Radius, monster.Position.Y - monster.Radius), SpriteEffects.None, Color.White);
            if (monster.Direction == Directions.Left)
                monster.Anim.Draw(spriteBatch, new Vector2(monster.Position.X - monster.Radius, monster.Position.Y - monster.Radius), SpriteEffects.FlipHorizontally, Color.White);
            if (monster.Direction == Directions.Right)
                monster.Anim.Draw(spriteBatch, new Vector2(monster.Position.X - monster.Radius, monster.Position.Y - monster.Radius), SpriteEffects.None, Color.White);

            if (enemyMonster.Direction == Directions.Up || enemyMonster.Direction == Directions.Down)
                enemyMonster.Anim.Draw(spriteBatch, new Vector2(enemyMonster.Position.X - enemyMonster.Radius, enemyMonster.Position.Y - enemyMonster.Radius), SpriteEffects.None, Color.Red);
            if (enemyMonster.Direction == Directions.Left)
                enemyMonster.Anim.Draw(spriteBatch, new Vector2(enemyMonster.Position.X - enemyMonster.Radius, enemyMonster.Position.Y - enemyMonster.Radius), SpriteEffects.FlipHorizontally, Color.Red);
            if (enemyMonster.Direction == Directions.Right)
                enemyMonster.Anim.Draw(spriteBatch, new Vector2(enemyMonster.Position.X - enemyMonster.Radius, enemyMonster.Position.Y - enemyMonster.Radius), SpriteEffects.None, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
