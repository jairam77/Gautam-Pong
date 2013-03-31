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
using System.Threading;
using System.Diagnostics;

namespace Pong
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball;
        Texture2D paddle;
        Texture2D board;
        Texture2D difficultymenu;
        Ball gameball;
        Paddle Left;
        Paddle Right;
        KeyboardState keyboardstate;
        KeyboardState oldkeyboardstate;
        Stopwatch gametimer;
        public Boundary upb, downb;
        SpriteFont font;
        List<Text> ListMenu;
        GameScreens.GameState gamescreen;
        Player playerleft;
        Player playerright;
        bool gameend;
        Menu mainmenu;
        Menu difmenu;
        bool timerstarted;
        Stopwatch timestart;
        string endmessage;
        Texture2D mainmenuimg;
        AI computer;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
        }

      
        protected override void Initialize()
        {

            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 750;
            graphics.ApplyChanges();
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            gamescreen  = GameScreens.GameState.Menu;
            font = Content.Load<SpriteFont>("GameFont");
            ListMenu = new List<Text>();
            mainmenuimg = Content.Load<Texture2D>("MainMenu");
            ListMenu.Add(new Text(new Vector2(50, 50), "Human vs Human", font, gamestart));
            ListMenu.Add(new Text(new Vector2(50, 100), "Human vs Computer", font, difficulty));
            ListMenu.Add(new Text(new Vector2(50, 150), "Exit", font,End));
            mainmenu = new Menu(ListMenu,mainmenuimg);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("Ball");
            paddle = Content.Load<Texture2D>("Paddle");
            board = Content.Load<Texture2D>("Board");
            gametimer = new Stopwatch();
            keyboardstate = new KeyboardState();
            oldkeyboardstate = new KeyboardState();
            difficultymenu = Content.Load<Texture2D>("Difficulty Screen");
            timerstarted = false;
            timestart = new Stopwatch();
            gameend = false;
            
            
           
            }
       

       
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {
            oldkeyboardstate = keyboardstate;
            keyboardstate = Keyboard.GetState();

            if (gameend)
            {
                if (keyboardstate.IsKeyDown(Keys.Escape))
                {

                    gamescreen = GameScreens.GameState.Menu;
                    ListMenu = new List<Text>();

                    ListMenu.Add(new Text(new Vector2(50, 50), "Human vs Human", font, gamestart));
                    ListMenu.Add(new Text(new Vector2(50, 100), "Human vs Computer", font, difficulty));
                    ListMenu.Add(new Text(new Vector2(50, 150), "Exit", font, End));
                    mainmenu = new Menu(ListMenu,mainmenuimg);
                    gameend = false;
                    
                }
            }
        
            if (gamescreen == GameScreens.GameState.Play)
            {
                if (timerstarted)
                {
                    if (timestart.ElapsedMilliseconds > 3000)
                    {

                        timestart.Stop();
                        timestart.Reset();
                        timerstarted = false;
                        gameball.start_ball();
                    }
                }
                
                Left.TakeInput(keyboardstate);
                Right.TakeInput(keyboardstate);
                gameball.updatepaddleboundary(Left.boundary, Right.boundary);
                gameball.update();
                if (gameball.Location.X < -25) 
                {
                    playerright.scored();
                    gameball.stop();
                    gameball.keepinposition();
                    timestart.Start();
                    timerstarted = true;
                    
                }
                else if (gameball.Location.X > 750)
                {
                    playerleft.scored();
                    gameball.stop();
                    gameball.keepinposition();
                    timestart.Start();
                    timerstarted = true;
                    
                }
                if (playerleft.Score == 10)
                {
                    gameover(playerleft);
                    timestart.Stop();
                }
                if (playerright.Score == 10)
                {
                    gameover(playerright);
                    timestart.Stop();
                }
            }
            else if (gamescreen == GameScreens.GameState.Menu)
            { mainmenu.input(keyboardstate,oldkeyboardstate); }
            else if (gamescreen == GameScreens.GameState.PlayComp)
            {
                if (timerstarted)
                {
                    if (timestart.ElapsedMilliseconds > 3000)
                    {

                        timestart.Stop();
                        timestart.Reset();
                        timerstarted = false;
                        gameball.start_ball();
                    }
                }

               
                computer.updatepaddle(gameball,Left);
                gameball.updatepaddleboundary(Left.boundary, Right.boundary);
                Right.TakeInput(keyboardstate);
                gameball.update();
                if (gameball.Location.X < -25)
                {
                    playerright.scored();
                    gameball.stop();
                    gameball.keepinposition();
                    timestart.Start();
                    timerstarted = true;

                }
                else if (gameball.Location.X > 750)
                {
                    playerleft.scored();
                    gameball.stop();
                    gameball.keepinposition();
                    timestart.Start();
                    timerstarted = true;

                }
                if (playerleft.Score == 10)
                {
                    gameover(playerleft);
                    timestart.Stop();
                }
                if (playerright.Score == 10)
                {
                    gameover(playerright);
                    timestart.Stop();
                }
            }
            else if (gamescreen == GameScreens.GameState.DifficultySelect)
            {
                difmenu.input(keyboardstate, oldkeyboardstate);
            }
            
            

            base.Update(gameTime);
        }
        protected void gamestart()
        {
            playerleft = new Player(new Vector2(50, 480), font,"Player Left");
            playerright = new Player(new Vector2(600, 480), font,"Player Right");
            Left = new Paddle(0, 213, paddle, Keys.W, Keys.S);
            Right = new Paddle(740, 213, paddle, Keys.Up, Keys.Down);
            upb = new Boundary(0, 0, 750, 20);
            downb = new Boundary(0, 480, 750, 20);
            gameball = new Ball(ball, upb.boundary, downb.boundary, Left.boundary, Right.boundary, font);
            timestart.Start();
            timerstarted = true;
            gameball.keepinposition();
            gamescreen = GameScreens.GameState.Play;
            gametimer.Start();
           
        }
        protected void gamecompstart(int difficulty)
        {
            
            playerleft = new Player(new Vector2(50, 480), font, "Player Left");
            playerright = new Player(new Vector2(600, 480), font, "Player Right");
            Left = new Paddle(0, 213, paddle, Keys.W, Keys.S);
            Right = new Paddle(740, 213, paddle, Keys.Up, Keys.Down);
            upb = new Boundary(0, 0, 750, 20);
            downb = new Boundary(0, 480, 750, 20);
            gameball = new Ball(ball, upb.boundary, downb.boundary, Left.boundary, Right.boundary, font);
            timestart.Start();
            timerstarted = true;
            gameball.keepinposition();
            computer = new AI(Left,difficulty);
            gamescreen = GameScreens.GameState.PlayComp;
            gametimer.Start();
        }
        protected void difficulty()
        {
            gamescreen = GameScreens.GameState.DifficultySelect;
            List<Text> Menutextdiff = new List<Text>();
            Menutextdiff.Add(new Text(new Vector2(360,150),"Easy",font,easy));
            Menutextdiff.Add(new Text(new Vector2(360, 200), "Medium", font, easy));
            Menutextdiff.Add(new Text(new Vector2(360, 250), "Hard", font, easy));
            difmenu = new Menu(Menutextdiff, difficultymenu);
        }
        protected void easy()
        {
            gamecompstart(5);
        }
        protected void medium()
        {
            gamecompstart(10);
        }
        protected void hard()
        {
            gamecompstart(20);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (gamescreen == GameScreens.GameState.Menu)
            {
                mainmenu.draw(spriteBatch);
            }


            else if (gamescreen == GameScreens.GameState.Play || gamescreen == GameScreens.GameState.PlayComp)
            {
                if (!gameend)
                {
                    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    spriteBatch.Draw(board, new Vector2(0, 0), Color.White);
                    spriteBatch.End();
                    gameball.draw(spriteBatch);
                    Left.draw(spriteBatch);
                    Right.draw(spriteBatch);
                    playerleft.drawpoints(spriteBatch);
                    playerright.drawpoints(spriteBatch);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, "Time Elapsed:  " + gametimer.Elapsed.TotalSeconds.ToString(), new Vector2(375, 0), Color.Green);
                    spriteBatch.End();
                }
                else if (gameend)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.DrawString(font, endmessage, new Vector2(150, 150), Color.Black);
                    spriteBatch.End();
                }
            }
            else if (gamescreen == GameScreens.GameState.DifficultySelect)
            {
                difmenu.draw(spriteBatch);

            }
          
           
            base.Draw(gameTime);
        }
        public void End()
        {
            Exit();
        }
        public void gameover(Player winner)
        {
           
            gameball.stop();
            gametimer.Stop();
            gametimer.Reset();
            endmessage = (winner.Name + " Wins!!....... Press Esc to Go to The Menu");
            gameend = true;
            winner.isWinner = true;
        }
        
       
    }
}
