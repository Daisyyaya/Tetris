using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlockLL:Block
    {
   
        public BlockLL()
        {
            PenColor = Color.Black;
            BrushColor = Color.Yellow;

            Shape[0] = new int[,]{
                {0,1},
                {0,1},
                {1,1}
            };

            Shape[1] = new int[,]{
                {1,0,0},
                {1,1,1}
            };

            Shape[2] = new int[,]{
                {1,1},
                {1,0},
                {1,0}
            };

            Shape[3] = new int[,]{
                {1,1,1},
                {0,0,1}
            };
        }


    }
}
