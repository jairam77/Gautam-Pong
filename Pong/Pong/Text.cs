using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Text
    {
        string text;
        bool selected;
        
        
        Color currentcolor;
        Vector2 RenderLocation;
        SpriteFont font;
        public delegate void run();
        run nextaction;
        public bool isselected { get { return selected; } set { selected = value; } }
        public Text(Vector2 location,string txt, SpriteFont txtfont,run action)
        {
            selected = false;
            currentcolor = Color.Black;
            RenderLocation = location;
            font = txtfont;
            nextaction = action;
            text = txt;
        }
        public void update()
        {
            if (selected)
            {
                currentcolor = Color.Red;
            }
            else { currentcolor = Color.Black; }
        }

        public void select()
        {
            nextaction.DynamicInvoke();
        }
        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend);
            spritebatch.DrawString(font, text, RenderLocation, currentcolor);
            spritebatch.End();
        }
    }
}
