using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlockI:Block
    {

        public BlockI()
        {
            PenColor = Color.Black;
            BrushColor = Color.Blue;

            Shape[0] = Shape[2] = new int[,]{
                {1,1,1,1}
            };

            Shape[1] = Shape[3] = new int[,]{
                {1},
                {1},
                {1},
                {1}
            };
        }
    }
}
