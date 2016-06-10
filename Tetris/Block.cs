using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    delegate bool MoveDelegate( int[,] shape, int x, int y);

    enum Action
    {
        Default,
        Turn,
        MoveLeft,
        MoveRight,
        MoveDown
    }

    abstract class Block:GameObject
    {
        public event MoveDelegate MoveEvent;
  
        protected int[][,] Shape = new int[4][,];
        protected int CurrentState = 0;
        protected Color PenColor;
        protected Color BrushColor;

        private int ShadowY { set; get; }
        private Color ShadowColor = Color.Silver;
        public Action CurrentAction { set; get; }

        public int[,] CurrentShape {
            set { }
            get { return Shape[CurrentState]; }
        }

        public Color CurrentColor
        {
            set { }
            get { return BrushColor; }
        }

        public int[,] NextShape
        {
            set { }
            get { 
                int temp = CurrentState;
                temp++;
                temp %= Shape.Length;
                return Shape[temp];
            }
        }
        //************************************************************
        public Block()
        {
            
        }


        public void Move()
        {
            ////碰撞检测
            //if (MoveEvent == null)
            //{
            //    return;
            //}

            Console.WriteLine("-------Move");
            switch (CurrentAction)
            {
                case Action.MoveRight:
                    if (MoveEvent(this.CurrentShape, X + 1, Y))
                    {
                        X++;
                    }
                    break;
                case Action.MoveLeft:
                    if (MoveEvent(this.CurrentShape, X - 1, Y))
                    {
                        X--;
                    }
                    break;
                case Action.MoveDown:
                    int count = 5;
                    while (MoveEvent(this.CurrentShape, X, Y + 1) && count > 0)
                    {
                        count--;
                        Y++;
                    }
                    break;
                case Action.Turn:
                    if (MoveEvent(this.NextShape, X, Y))
                    {
                        CurrentState++;
                        CurrentState %= Shape.Length;
                    }
                    break;
                default:
                    break;
            }

            CurrentAction = Action.Default;

            GenerateShadow();
        }

      

        //override
        public override void Draw(System.Drawing.Graphics e)
        {
            Update();

            Pencil.Color = PenColor;

            for (int i = 0; i < CurrentShape.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentShape.GetLength(1); j++)
                {
                    if (CurrentShape[i ,j] == 1)
                    {
                        if (ShadowY + i >= BackgroundY)
                        {
                            //shadow
                            Painter.Color = ShadowColor;
                            e.FillRectangle(Painter, GetRect(X + j, ShadowY + i));
                            e.DrawRectangle(Pencil, GetRect(X + j, ShadowY + i));
                        }
                       
                        if (Y + i >= BackgroundY)
                        {
                            //shape
                            Painter.Color = CurrentColor;
                            e.FillRectangle(Painter, GetRect(X + j, Y + i));
                            e.DrawRectangle(Pencil, GetRect(X + j, Y + i));
                        }
                       
                    }
                }
            } 
        }

        public override void Update()
        {
            base.Update();

            Move();

        }

        public void GenerateShadow()
        {
            ShadowY = Y;
            while (MoveEvent(this.CurrentShape, X, ShadowY + 1))
            {
                ShadowY++;
            }
        }

        public void CloneDraw(Graphics e, Background b)
        {
            for (int i = 0; i < CurrentShape.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentShape.GetLength(1); j++)
                {
                    if (CurrentShape[i, j] == 1)
                    {
                        //clone
                        Pencil.Color = PenColor;
                        Painter.Color = CurrentColor;
                        e.FillRectangle(Painter, GetRect(X + j + b.MaxX, Y + i + BackgroundY));
                        e.DrawRectangle(Pencil, GetRect(X + j + b.MaxX, Y + i + BackgroundY));
                    }
                }
            }
        }

        public void Reset()
        {
            CurrentState = 0;
            CurrentAction = Action.Default;
        }

    }      
}
