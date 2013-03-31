using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Pong
{
    public class Paddle
    {
        Vector2 position;
        Texture2D image;
        public Rectangle boundary;
        Keys kup, kdown;
        int maxY;
        int minY;
        public Vector2 Position { get { return position; } }
        public int Height { get { return 75; } }

        public Paddle(int X,int Y,Texture2D Pic,Keys KeyUp,Keys KeyDown)
        {
            position = new Vector2(X, Y);
            boundary = new Rectangle(X, Y, 10, 75);
            image = Pic;
            kup = KeyUp;
            kdown = KeyDown;
            maxY = 405;
            minY = 20;
        }

        public void TakeInput(KeyboardState K)
        {
            if (K.IsKeyDown(kup))
            {
                update(-10);
            }
            else if (K.IsKeyDown(kdown))
            {
                update(10);
            }
            else { update(0); }
        }
        public void computerinput(int upordown)
        {
            if (upordown == 10 || upordown == -10)
            {
                update(upordown);
            }
            else { update(0); }
        }

        void update(int motion)
        {
            if (motion == -10)                         
            {
                if ((position.Y - minY) < 10)
                {
                    motion = ((int)position.Y - minY) * -1;
                    position.Y += motion;
                }
                else { position.Y += motion; }
            }
            if (motion == 10)
            {
                if ((maxY - position.Y) < 10)
                {
                    motion = (maxY - (int)position.Y);
                    position.Y += motion;
                }
                else { position.Y += motion; }
            }
            boundary = new Rectangle((int)position.X, (int)position.Y, 10, 75);

            
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spritebatch.Draw(image, new Vector2((float)(position.X), (float)(position.Y)), Color.White);
            spritebatch.End();
        }

    }
}
