using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    delegate void GameDelegate();

    class Game:GameObject
    {
        public Background background = new Background();
        public Block[] blocks = new Block[UndoSteps];
        public int count;
        public static int[] Score = new int[UndoSteps];
        public int UndoCount
        {
            get { return (CurrentStep + UndoSteps - 1) % UndoSteps; }
        }
        public Block block
        {
            get {return blocks[CurrentStep]; }
        }
        public GameDelegate GameOverEvent;

        //
        public Game()
        {
            Score = new int[UndoSteps];
            blocks[CurrentStep] = GenerateRandomeBlock();
            blocks[NextStep] = GenerateRandomeBlock();
        }

        //override
        public override void Draw(System.Drawing.Graphics e)
        {
            Update();

            background.Draw(e);
            block.Draw(e);
            blocks[NextStep].CloneDraw(e, background);
  
        }

        public override void Update()
        {
            base.Update();

            if (count == 8)
            {
                if (!IfMove(block.CurrentShape, block.X, block.Y + 1))
                { 
                    // 存颜色
                    PushBlock();
                    //delete
                    background.DeleteRow();
                    //判断是否gameover
                    if (IfDead())
                    {
                        GameOverEvent();
                    }

                    CurrentStep++;
                    //生成新的block
                    blocks[NextStep] = GenerateRandomeBlock();
                }
                else
                {
                    block.Y++;
                }
            }
            
            count++;
            count %= 10;
        }



        //随机生成 block
        public Block GenerateRandomeBlock()
        {
            Block B;
            switch (Random.Next(0,7))
	        {
		        case 0:
                    B = new BlockT();
                    break;
                case 1:
                    B = new BlockO();
                    break;
                case 2:
                    B = new BlockLL();
                    break;
                case 3:
                    B = new BlockL();
                    break;
                case 4:
                    B = new BlockI();
                    break;
                case 5:
                    B = new BlockZ();
                    break;
                case 6:
                    B = new BlockLZ();
                    break;
                default:
                    B = new BlockLZ();
                    break;
	        }

            B.X = background.MaxX/2;
            B.Y = BackgroundY - B.CurrentShape.GetLength(0);
            B.MoveEvent += IfMove;

            return B;
        }
       

        // 碰撞检测
        public bool IfMove(int[,] shape, int x, int y)
        {
            for (int i = shape.GetLength(0)-1; i >= 0 ; i--)                                //i j 代表行列
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == 0)
                    {
                        continue;
                    }

                    if (j + x < 0)
                    {
                        return false;
                    }

                    if (j + x >= background.MaxX)
                    {
                        return false;
                    }

                    if (i + y >= background.MaxY)
                    {
                        return false;
                    }

                    if ( background.ground[i + y, j + x] != Color.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //ground 存储 数据
        public void PushBlock()
        {
            //
            background.grounds[NextStep] = new Color[background.MaxY, background.MaxX];

            //
            for (int i = 0; i < background.MaxY; i++)
            {
                for (int j = 0; j < background.MaxX; j++)
                {
                    background.grounds[NextStep][i, j] = background.grounds[CurrentStep][i, j];
                }
            }

            for (int i = 0; i < block.CurrentShape.GetLength(0); i++)
            {
                for (int j = 0; j < block.CurrentShape.GetLength(1); j++)
                {
                    if (block.CurrentShape[i, j] == 1)
                    {
                        background.grounds[NextStep][i + block.Y, j + block.X] = block.CurrentColor;
                    }
                }
            }
        }


        public bool IfDead()
        {
            for (int i = 0; i < block.CurrentShape.GetLength(0); i++)
            {
                for (int j = 0; j < block.CurrentShape.GetLength(1); j++)
                {
                    if (i + block.Y < BackgroundY)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        //撤销
        public void Undo()
        {
            if ( blocks[UndoCount] != null)
            {
                blocks[NextStep] = null;
                CurrentStep--;

                block.X = background.MaxX / 2;
                block.Y = BackgroundY - block.CurrentShape.GetLength(0);
                block.Reset();

                blocks[NextStep].X = background.MaxX / 2;
                blocks[NextStep].Y = BackgroundY - block.CurrentShape.GetLength(0);
                blocks[NextStep].Reset();
            }
        }
       

       
    }
}
