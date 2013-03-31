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
    public class Ball
    {
        Vector2 position;
        Vector2 motion;
        Rectangle boundary;
        Texture2D image;
        int velocity;
        Rectangle ub, db;
        Rectangle L, R;
        SpriteFont timerfont;
        public Vector2 Location { get { return position; } }
        public Vector2 Motion { get { return motion; } }
       
        
        public Ball(Texture2D ballimage,Rectangle upb,Rectangle downb,Rectangle Left, Rectangle Right,SpriteFont font){
            image = ballimage;
            ub = upb;
            db = downb;
            L = Left;
            R = Right;
            timerfont = font;
            
    }
        public void start_ball()
        {
            Random anglernd = new Random();
           
            float angle = anglernd.Next(25,30);
           
            velocity = (1000/ 60);
            motion = new Vector2((float)(velocity * Math.Cos(angle) ), (float)(velocity * Math.Sin(angle) ));
            boundary = new Rectangle((int)position.X , (int)position.Y, 25, 25);
        }
        public void keepinposition()
        {
            position = new Vector2(362.5f, 237.5f);
        }
        public void stop()
        {
            velocity = 0;
            motion = Vector2.Zero;
        }
        
        public void update()
        {
           
            collision();
            position += motion;
            boundary.X = (int)(position.X);
            boundary.Y = (int)(position.Y);
            
        }
        public void updatepaddleboundary(Rectangle lft, Rectangle rght)
        {
            L = lft;
            R = rght;
        }

         void collision()
        {
            if (boundary.Intersects(L)&& motion.X < 0)
            {
                if (position.Y + 25 > (L.Location.Y + L.Height) || position.Y < L.Location.Y || position.X < (L.Location.X + L.Width - 15))
                {
                }
                else
                {
                    motion.X *= -1;
                }
                
            }
            if(boundary.Intersects(R) && motion.X> 0){
                if (position.Y + 25 > (R.Location.Y + L.Height) || position.Y < R.Location.Y || position.X > R.Location.X)
                {
                }
                else
                {
                    motion.X *= -1;
                }
            }
            if (boundary.Intersects(ub) || boundary.Intersects(db))
            {
                motion.Y *= -1;
            }
            
        }
        public void draw(SpriteBatch spritebatch)
        {
           
            
                spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spritebatch.Draw(image, new Vector2((float)(position.X), (float)(position.Y)), Color.White);
                spritebatch.End();
           
           
        }
    }
}
