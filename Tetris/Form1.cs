using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Game game = new Game();

        public Form1()
        {
            InitializeComponent();
            game.GameOverEvent += GameOver;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        private void timerFrame_Tick(object sender, EventArgs e)
        {
            Invalidate();
            label2.Text = Game.Score[GameObject.CurrentStep] + "";          
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:                                         
                case Keys.Up:
                    game.block.CurrentAction = Action.Turn;                    //转
                    break;
                case Keys.S:
                case Keys.Down:
                    game.block.CurrentAction = Action.MoveDown;                //向下
                    break;
                case Keys.A:
                case Keys.Left:
                    game.block.CurrentAction = Action.MoveLeft;                //左移动
                    break;
                case Keys.D:
                case Keys.Right:
                    game.block.CurrentAction = Action.MoveRight;              //右移
                    break;
                case Keys.U:
                    game.Undo();                                              //撤销一步 最多5
                    break;
                case Keys.R:
                    Restart();                                               //重新开始
                    break;
                default:
                    break;
            }
        }

        public void GameOver()
        {
            timerFrame.Enabled = false;
            label1.Visible = true;
        }

        public void Restart()
        {
            game = new Game();
            game.GameOverEvent += GameOver;
            timerFrame.Enabled = true;
            label1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
