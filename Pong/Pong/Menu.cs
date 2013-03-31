using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class Menu
    {
        List<Text> ListofOptions;
        int maxitems;
        int currentitem;
        Texture2D img;
        GameScreens.GameState G;

        public Menu(List<Text> listoptions,Texture2D Menuimg)
        {
            ListofOptions = listoptions;
            maxitems = listoptions.Count - 1;
            currentitem = 0;
            G = GameScreens.GameState.Menu;
            ListofOptions[currentitem].isselected = true;
            img = Menuimg;
        }
        public void input(KeyboardState K,KeyboardState old)
        {
            if (K.IsKeyDown(Keys.Up) && currentitem != 0 && old.IsKeyUp(Keys.Up))
            {
                ListofOptions[currentitem].isselected = false;
                currentitem--;
                ListofOptions[currentitem].isselected = true;
            }
            if (K.IsKeyDown(Keys.Down) && currentitem < maxitems && old.IsKeyUp(Keys.Down))
            {
                ListofOptions[currentitem].isselected = false;
                currentitem++;
                ListofOptions[currentitem].isselected = true;
            }
            for (int counter = 0; counter <= maxitems; counter++)
            {
                ListofOptions[counter].update();
            }
            if (K.IsKeyDown(Keys.Enter) && old.IsKeyUp(Keys.Enter))
            {
                ListofOptions[currentitem].select();
            }
              
        }
        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(img, new Vector2(0, 0), Color.White);
            spritebatch.End();
            for (int counter = 0; counter <= maxitems; counter++)
            {
                ListofOptions[counter].draw(spritebatch);
            }
            
        }
       
    }
}
