using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlockLZ:Block
    {
 
        public BlockLZ()
        {
            PenColor = Color.Black;
            BrushColor = Color.BurlyWood;

            Shape[0] = Shape[2] = new int[,]{
                {0,1,1},
                {1,1,0}
            };

            Shape[1] = Shape[3] = new int[,]{
                {1,0},
                {1,1},
                {0,1}
            };

        }


    }
}
