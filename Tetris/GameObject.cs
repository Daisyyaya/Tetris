using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameObject
    {
        //**************************************************************************//

        public static Random Random = new Random();
        public const int Padding = 50;
        public const int BackgroundY = 4;
        public const int UndoSteps = 7;
        public static int CellSize = 20;
        public static SolidBrush Painter = new SolidBrush(Color.Empty);
        public static Pen Pencil = new Pen(Color.Black);

        public int X {  set; get; }
        public int Y {  set; get; }
        private static int currentStep = 0;
        public static int CurrentStep
        {
            set
            {
                currentStep = (value + UndoSteps) % UndoSteps;
            }
            get
            {
                return currentStep % UndoSteps;
            }
        }
        public static int NextStep
        {
            get
            {
                return (currentStep + 1) % UndoSteps;
            }
        }
        //**************************************************************************//

        public virtual void Draw(Graphics e)
        {
            Update();

            e.DrawRectangle(Pencil, GetRect(X, Y));
            e.FillRectangle(Painter, GetRect(X, Y));
        }


        //**************************************************************************//

        public virtual void Update()
        {

        }

        public Rectangle GetRect(int x, int y)
        {
            return new Rectangle(x*CellSize+Padding, y*CellSize+Padding, CellSize, CellSize);
        }

    }
}
