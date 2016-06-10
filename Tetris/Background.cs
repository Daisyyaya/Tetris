using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
   
    class Background:GameObject
    {
        public Color[][,] grounds = new Color[UndoSteps][,];                 //cell[y,x]
        public Color[,] ground
        {
            set { }
            get { return grounds[CurrentStep]; }
        }
        public readonly int MaxY;               // 
        public readonly int MaxX;               // 


        public Background():this(20, 10)
        {

        }

        //使用构造函数初始化
        public Background(int i, int j)
        {
            MaxX = j;
            MaxY = i + BackgroundY;
            grounds[CurrentStep] = new Color[MaxY, MaxX];
        }

       
        //消除
        public void DeleteRow()
        {
            int i = MaxY - 1;
            Game.Score[NextStep] = Game.Score[CurrentStep];
            while (i >= 0)
            {
                if (IfFull(i))
                {
                    Game.Score[NextStep] += 10;
                    for (int r = i; r >= 0; r--)
                    {
                        if (r == 0)
                        {
                            for (int j = 0; j < MaxX; j++)
                            {
                                grounds[NextStep][r, j] = Color.Empty;
                            }
                        }
                        else
                        {
                            for (int c = 0; c < MaxX; c++)
                            {
                                grounds[NextStep][r, c] = grounds[NextStep][r - 1, c];
                            }
                        }
                    }
                }
                else
                {
                    i--;
                }
            }
        }

        public bool IfFull(int i)
        {
            for (int j = 0; j < MaxX; j++)
            {
                if (grounds[NextStep][i, j] == Color.Empty)
                {
                    return false;
                }
            }
            return true;
        }


        //override
        public override void Draw(System.Drawing.Graphics e)
        {
            Update();

            for (int i = BackgroundY; i < MaxY; i++)
            {
                for (int j = 0; j < MaxX; j++)
                {
                    if (ground[i, j] != Color.Empty) 
                    {
                        Painter.Color = ground[i, j];
                        e.FillRectangle(Painter, GetRect(j, i));
                    }
                    Pencil.Color = Color.Black;
                    e.DrawRectangle(Pencil, GetRect(j, i));

                }
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
