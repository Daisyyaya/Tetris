using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlockT:Block
    {
 
        public BlockT()
        {
            PenColor = Color.Black;
            BrushColor = Color.Red;

            Shape[0] = new int[,] {
                { 1, 1, 1}, 
                { 0, 1, 0}
            };

            Shape[1] = new int[,]{
                {0,1},
                {1,1},
                {0,1}
            };

            Shape[2] = new int[,]{
                {0,1,0},
                {1,1,1}
            };

            Shape[3] = new int[,]{
                {1,0},
                {1,1},
                {1,0}
            };

        }



    }
}
