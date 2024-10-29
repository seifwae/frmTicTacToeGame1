using frmTicTacToeGame1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmTicTacToeGame1
{
    
    public partial class frmGame : Form
    {
       
        List<byte> buttons = new List <byte>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private byte _Leave;
        public bool bot = false;
       

        public frmGame(bool bot = false,byte le =3)
        {

            InitializeComponent();
            _Leave = le;
            this.bot = bot;
        }
        enum enPlayer {
            Player1=0, Player2=1,Bot=2
        }
        enum enWinner { Player1=1, Player2=2,Bot=5 ,Draw=3 , InProgress=4 }
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;

        }
        stGameStatus GameStatus = new stGameStatus();
        enPlayer PlayerTurn = enPlayer.Player1;

        public bool ChackValues(Button btn1,Button btn2,Button btn3)
        {
           if (btn1.Tag.ToString()!="?"&& btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString()== btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;
                if (btn1.Tag.ToString()=="X")
                {
                    GameStatus.Winner = enWinner.Player1;
                   
                    GameStatus.GameOver = true;
                    EndGame();

                    return true;
                }
                else 
                {
                    if (!bot)
                    {
                        GameStatus.Winner = enWinner.Player2;
                    }
                    else
                    {
                        GameStatus.Winner = enWinner.Bot;

                    }
                    
                  
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
           }
           
            GameStatus.GameOver = false;
            return false;
         
        }
        void EndGame()
        {
            label2 .Text = "Game Over";
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lbWinner.Text = "Player1";
                    break;
                case enWinner.Player2:
                    lbWinner.Text = "Player2";
                    break;
                case enWinner.Bot:
                    lbWinner.Text = "Bot";
                    break;
                default:
                    lbWinner.Text = "Draw";
                    break;
            }
        }
        public void ChackWinner()
        {
            if (ChackValues(button1,button2,button3))
            {
                return;
            }
            if (ChackValues(button4,button5,button6))
            {
                return;
            }
            if (ChackValues(button7,button8,button9))
            {
                return;
            }

            if (ChackValues(button1,button4,button7))
            {
                return;
            }
            if (ChackValues(button2,button5,button8))
            {
                return;
            }
            if (ChackValues(button3,button6,button9))
            {
                return;
            }

            if (ChackValues(button1,button5,button9))
            {
                return;
            }
            if (ChackValues(button3,button5,button7))
            {
                return;
            }
            if (GameStatus.GameOver)
            {
                
            }
        }
        bool messagebox = true;
        private void EnabledButtons(bool b = false)
        {
            button1.Enabled = b;
            button2.Enabled =b;
            button3.Enabled = b;
            button4.Enabled = b;
            button5.Enabled = b;
            button6.Enabled = b;
            button7.Enabled = b;
            button8.Enabled =b;
            button9.Enabled = b;
        }
        void timerbot()
        {
            label5.ForeColor= Color.Red;
            for (byte i = 1; i <= 10; i++)
            {
                label5.Text = ("Bot :"+i.ToString("D2"));
                this.Refresh();
                Thread.Sleep(100  );
            }
            label5.ForeColor = Color.Black;
        }
        public async void ChangeImage (Button btn)
        {
            if (btn.Tag.ToString()=="?")
            {
                messagebox = true;
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        
                        btn.BackgroundImage= Resources.X_1_;
                        if (bot)
                        {
                            PlayerTurn = enPlayer.Bot;
                            label3.Text = "Bot";

                        }
                        else
                        {
                            PlayerTurn = enPlayer.Player2;
                            label3.Text = "Player2";
                            
                        }

                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        ChackWinner();

                        if (PlayerTurn ==enPlayer.Bot&&!GameStatus.GameOver&& GameStatus.PlayCount!=9)
                        {
                        EnabledButtons();
                        this.Refresh();

                        timerbot();


                        EnabledButtons(true);
                            
                        }

                        break;
                    case enPlayer.Player2:
                        btn.BackgroundImage = Resources.O_1_;
                        PlayerTurn = enPlayer.Player1;
                        GameStatus.PlayCount++;
                        label3.Text = "Player1";
                        btn.Tag = "O";
                        ChackWinner();
                        break;
                    case enPlayer.Bot:

                        btn.BackgroundImage = Resources.O_1_;
                        PlayerTurn = enPlayer.Player1;
                        GameStatus.PlayCount++;
                        label3.Text = "Player1";
                        btn.Tag = "O";

                        ChackWinner();
                        break;
                }
                
            }
            else
            {
                messagebox = false;
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            if (GameStatus.PlayCount==9&& lbWinner .Text.ToString()== "In Progress")
          {
                GameStatus.GameOver = true;
                
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
            if (GameStatus.GameOver)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }
        }
        private void RestButton(Button btn)
        {
            btn.BackgroundImage= Resources.question_mark_96_1_;
            btn.Tag = "?";
            btn.Enabled = true;
            btn.BackColor = Color.Transparent;
        }
        
        private void RestartGame()
        {
            DialogResult yesorno = DialogResult.Yes;  
            if (GameStatus.PlayCount != 9&& !GameStatus.GameOver)
            {

                 yesorno = MessageBox.Show("Are you sure (Restart Game)", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
              

            }
            if (yesorno==DialogResult.Yes)
            {
                label2 .Text= "Turn";
                RestButton(button1);
                RestButton(button2);
                RestButton(button3);
                RestButton(button4);
                RestButton(button5);
                RestButton(button6);
                RestButton(button7);
                RestButton(button8);
                RestButton(button9);
                PlayerTurn = enPlayer.Player1;
                label3.Text = "Player1";
                GameStatus.GameOver = false;
                GameStatus.PlayCount = 0;
                GameStatus.Winner = enWinner.InProgress;
                lbWinner.Text = "In Progress";
            }
            

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen whitePen = new Pen(white);
            whitePen.Width = 15;
            
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

           
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

           
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        void playBot()

        {
            if (!GameStatus.GameOver)
            {
                label3.Text = "Bot";

                Random r = new Random();
                Button btn = new Button();
                Button b = new Button();
                bool loop = true;
                List<Button> ltbuttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
                List<Button> ltbuttonsxx = new List<Button> { button1, button3, button7, button9 };
                if (_Leave == 3)
                {

                    if (button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }


                    else if ((button1.Tag.ToString() == "O" && button2.Tag.ToString() == "O") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }
                    else if ((button3.Tag.ToString() == "O" && button2.Tag.ToString() == "O") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }
                    else if ((button3.Tag.ToString() == "O" && button1.Tag.ToString() == "O") && button2.Tag.ToString() == "?")
                    {
                        btn = button2;
                    }

                    else if ((button4.Tag.ToString() == "O" && button5.Tag.ToString() == "O") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button4.Tag.ToString() == "O" && button6.Tag.ToString() == "O") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "O" && button6.Tag.ToString() == "O") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }

                    else if ((button7.Tag.ToString() == "O" && button8.Tag.ToString() == "O") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button7.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button8.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }


                    else if ((button1.Tag.ToString() == "O" && button4.Tag.ToString() == "O") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }
                    else if ((button1.Tag.ToString() == "O" && button7.Tag.ToString() == "O") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }
                    else if ((button4.Tag.ToString() == "O" && button7.Tag.ToString() == "O") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }

                    else if ((button2.Tag.ToString() == "O" && button5.Tag.ToString() == "O") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button2.Tag.ToString() == "O" && button8.Tag.ToString() == "O") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "O" && button8.Tag.ToString() == "O") && button2.Tag.ToString() == "?")
                    {
                        btn = button2;
                    }

                    else if ((button3.Tag.ToString() == "O" && button6.Tag.ToString() == "O") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button3.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button6.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }

                    else if ((button3.Tag.ToString() == "O" && button5.Tag.ToString() == "O") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }
                    else if ((button3.Tag.ToString() == "O" && button7.Tag.ToString() == "O") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "O" && button7.Tag.ToString() == "O") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }

                    else if ((button1.Tag.ToString() == "O" && button5.Tag.ToString() == "O") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button1.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }



                    else if ((button1.Tag.ToString() == "X" && button2.Tag.ToString() == "X") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }
                    else if ((button3.Tag.ToString() == "X" && button2.Tag.ToString() == "X") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }
                    else if ((button3.Tag.ToString() == "X" && button1.Tag.ToString() == "X") && button2.Tag.ToString() == "?")
                    {
                        btn = button2;
                    }

                    else if ((button4.Tag.ToString() == "X" && button5.Tag.ToString() == "X") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button4.Tag.ToString() == "X" && button6.Tag.ToString() == "X") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "X" && button6.Tag.ToString() == "X") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }

                    else if ((button7.Tag.ToString() == "X" && button8.Tag.ToString() == "X") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button7.Tag.ToString() == "X" && button9.Tag.ToString() == "X") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button8.Tag.ToString() == "X" && button9.Tag.ToString() == "X") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }


                    else if ((button1.Tag.ToString() == "X" && button4.Tag.ToString() == "X") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }
                    else if ((button1.Tag.ToString() == "X" && button7.Tag.ToString() == "X") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }
                    else if ((button4.Tag.ToString() == "X" && button7.Tag.ToString() == "X") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }

                    else if ((button2.Tag.ToString() == "X" && button5.Tag.ToString() == "X") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button2.Tag.ToString() == "X" && button8.Tag.ToString() == "X") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "X" && button8.Tag.ToString() == "X") && button2.Tag.ToString() == "?")
                    {
                        btn = button2;
                    }

                    else if ((button3.Tag.ToString() == "X" && button6.Tag.ToString() == "X") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button3.Tag.ToString() == "X" && button9.Tag.ToString() == "X") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button6.Tag.ToString() == "X" && button9.Tag.ToString() == "X") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }

                    else if ((button3.Tag.ToString() == "X" && button5.Tag.ToString() == "X") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }
                    else if ((button3.Tag.ToString() == "X" && button7.Tag.ToString() == "X") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "X" && button7.Tag.ToString() == "X") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;

                    }
                    else if (button5.Tag.ToString() == "X" && GameStatus.PlayCount < 4 && (button1.Tag.ToString() == "?" || button3.Tag.ToString() == "?" || button7.Tag.ToString() == "?" || button9.Tag.ToString() == "?"))
                    {
                        for (byte i = 0; i < 4; i++)
                        {
                            if (ltbuttonsxx[i].Tag.ToString() == "?")
                            {
                                btn = ltbuttonsxx[i];
                            }
                        }
                    }
                    else
                    {
                        while (loop)
                        {


                            btn = (Button)ltbuttons[r.Next(0, 9)];
                            if (btn.Tag.ToString() == "?")
                            {
                                loop = false;
                            }
                            if (GameStatus.PlayCount == 9)
                            {
                                loop = false;
                                break;
                            }
                        }
                    }
                }
                else if (_Leave == 2)
                {
                    if ((button4.Tag.ToString() == "O" && button5.Tag.ToString() == "O") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button4.Tag.ToString() == "O" && button6.Tag.ToString() == "O") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "O" && button6.Tag.ToString() == "O") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }

                    else if ((button7.Tag.ToString() == "O" && button8.Tag.ToString() == "O") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button7.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button8.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }


                    else if ((button1.Tag.ToString() == "O" && button4.Tag.ToString() == "O") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }
                    else if ((button1.Tag.ToString() == "O" && button7.Tag.ToString() == "O") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }
                    else if ((button4.Tag.ToString() == "O" && button7.Tag.ToString() == "O") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }

                    else if ((button2.Tag.ToString() == "O" && button5.Tag.ToString() == "O") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button2.Tag.ToString() == "O" && button8.Tag.ToString() == "O") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "O" && button8.Tag.ToString() == "O") && button2.Tag.ToString() == "?")
                    {
                        btn = button2;
                    }

                    else if ((button3.Tag.ToString() == "O" && button6.Tag.ToString() == "O") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button3.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button6.Tag.ToString() == "O" && button9.Tag.ToString() == "O") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }
                    else if ((button1.Tag.ToString() == "X" && button2.Tag.ToString() == "X") && button3.Tag.ToString() == "?")
                    {
                        btn = button3;
                    }
                    else if ((button3.Tag.ToString() == "X" && button2.Tag.ToString() == "X") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }
                    else if ((button3.Tag.ToString() == "X" && button1.Tag.ToString() == "X") && button2.Tag.ToString() == "?")
                    {
                        btn = button2;
                    }

                    else if ((button4.Tag.ToString() == "X" && button5.Tag.ToString() == "X") && button6.Tag.ToString() == "?")
                    {
                        btn = button6;
                    }
                    else if ((button4.Tag.ToString() == "X" && button6.Tag.ToString() == "X") && button5.Tag.ToString() == "?")
                    {
                        btn = button5;
                    }
                    else if ((button5.Tag.ToString() == "X" && button6.Tag.ToString() == "X") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }

                    else if ((button7.Tag.ToString() == "X" && button8.Tag.ToString() == "X") && button9.Tag.ToString() == "?")
                    {
                        btn = button9;
                    }
                    else if ((button7.Tag.ToString() == "X" && button9.Tag.ToString() == "X") && button8.Tag.ToString() == "?")
                    {
                        btn = button8;
                    }
                    else if ((button8.Tag.ToString() == "X" && button9.Tag.ToString() == "X") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }


                    else if ((button1.Tag.ToString() == "X" && button4.Tag.ToString() == "X") && button7.Tag.ToString() == "?")
                    {
                        btn = button7;
                    }
                    else if ((button1.Tag.ToString() == "X" && button7.Tag.ToString() == "X") && button4.Tag.ToString() == "?")
                    {
                        btn = button4;
                    }
                    else if ((button4.Tag.ToString() == "X" && button7.Tag.ToString() == "X") && button1.Tag.ToString() == "?")
                    {
                        btn = button1;
                    }
                    else
                    {
                        while (loop)
                        {


                            btn = (Button)ltbuttons[r.Next(0, 9)];
                            if (btn.Tag.ToString() == "?")
                            {
                                loop = false;
                            }
                            if (GameStatus.PlayCount == 9)
                            {
                                loop = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    while (loop)
                    {


                        btn = (Button)ltbuttons[r.Next(0, 9)];
                        if (btn.Tag.ToString() == "?")
                        {
                            loop = false;
                        }
                        if (GameStatus.PlayCount == 9)
                        {
                            loop = false;
                            break;
                        }
                    }
                }




                if (GameStatus.PlayCount != 9 && bot && btn .Tag.ToString() =="?")
                {


                    ChangeImage(btn);

                }
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
            if (bot && messagebox)
            {
                playBot();
            }

        }
        

        private void button10_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
