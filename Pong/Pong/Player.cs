using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class Player
    {
        int points;
        Vector2 pointslocation;
        SpriteFont font;
        string txt;
        public string Name { get { return txt; } }
        public int Score { get { return points; } }
        public bool isWinner { get; set; }
        public Player(Vector2 locationtodraw,SpriteFont txtfont, string nametxt)
        {
            isWinner = false;
            points = 0;
            pointslocation = locationtodraw;
            font = txtfont;
            txt = nametxt;
        }
        public void scored()
        {
            points++;
        }
        public void drawpoints(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.DrawString(font, txt + ": " + points.ToString(), pointslocation, Color.Orange);
            spritebatch.End();
        }
    }
}
