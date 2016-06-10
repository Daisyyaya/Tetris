using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlockO:Block
    {


        public BlockO()
        {
            PenColor = Color.Black;
            BrushColor = Color.Orange;

            Shape[0] = Shape[1] = Shape[2] = Shape[3] = new int[,]{
                {1,1},
                {1,1}
            };
        }


    }
}
