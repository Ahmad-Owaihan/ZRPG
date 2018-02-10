using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZRPG
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        Texture2D skeletonAttack_Sprite;
        Texture2D skeletonDead_Sprite;
        Texture2D skeletonHit_Sprite;
        Texture2D skeletonIdle_Sprite;
        Texture2D skeletonWalk_Sprite;
        Texture2D skeletonReact_Sprite;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            skeletonAttack_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Attack");
            skeletonDead_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Dead");
            skeletonHit_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Hit");
            skeletonIdle_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Idle");
            skeletonReact_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton React");
            skeletonWalk_Sprite = Content.Load<Texture2D>("Skeleton/Skeleton Walk");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
